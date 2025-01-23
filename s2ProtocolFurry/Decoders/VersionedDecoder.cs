using System.Text;
using s2ProtocolFurry.Decoders;
using s2ProtocolFurry.Logging;
using s2ProtocolFurry.Protocol;

namespace s2ProtocolFurry.Decoder
{   
    public class VersionedDecoder : BaseDecoder, IDecoder
    {
        private readonly BitPackedBuffer _buffer;
        private readonly List<ProtocolTypeInfo> _typeInfos;

        public VersionedDecoder(byte[] contents, List<ProtocolTypeInfo> typeInfos)
        {
            _buffer = new BitPackedBuffer(contents) { };
            _typeInfos = typeInfos;
        }

        public override string ToString() => _buffer.ToString();

        public string GetTypeName(int typeId)
        {
            var typeInfo = _typeInfos[typeId];
            var methodName = typeInfo.Type;
            return methodName;
        }

        public object? Instance(int typeId)
        {
            if (typeId >= _typeInfos.Count)
            {
                throw new InvalidOperationException("Corrupted data");
            }

            var typeInfo = _typeInfos[typeId];
            var result = typeInfo switch
            {
                ProtocolTypeArray(Int128 arg1, Int128 arg2, Int128 typeid) => Array((int)arg1, (int)typeid),
                ProtocolTypeBitArray(Int128 arg1, Int128 arg2) => BitArray((int)arg1),
                ProtocolTypeBlob(Int128 arg1, Int128 arg2) => Blob((int)arg1),
                ProtocolTypeBool => Bool(),
                ProtocolTypeChoice(Int128 arg1, Int128 arg2, List<(string Arg1, Int128 Arg2)> arg3) => Choice((int)arg1, (int)arg2, arg3),
                ProtocolTypeFourcc => FourCC(),
                ProtocolTypeInt(Int128 arg1, Int128 arg2) => Int((int)arg1),
                ProtocolTypeNull => Null(),
                ProtocolTypeOptional(Int128 arg1) => Optional((int)arg1),
                ProtocolTypeStruct(List<(string Arg1, Int128 Arg2, Int128 Arg3)> arg1) => Struct(arg1),
                var x => throw new InvalidOperationException($"Unknown method '{x.Type}'")
            };

            return result;
        }

        public void ByteAlign()
        {
            _buffer.ByteAlign();
        }

        public bool Done() => _buffer.Done();

        public int UsedBits() => _buffer.UsedBits();

        private void ExpectSkip(int expected)
        {
            var bits = _buffer.ReadBits(8);

            if (bits != expected)
            {
                Console.WriteLine($"Expecting: {expected}, Found: {bits}, Position: {_buffer.UsedBits()}");

                throw new InvalidOperationException("Corrupted data");
            }
        }

        private int VInt()
        {
            int b = _buffer.ReadBits(8);
            bool negative = (b & 1) != 0;
            int result = (b >> 1) & 0x3f;
            int bits = 6;
            while ((b & 0x80) != 0)
            {
                b = _buffer.ReadBits(8);
                result |= (b & 0x7f) << bits;
                bits += 7;
            }
            return negative ? -result : result;
        }

        private object[] Array(int bounds, int typeid)
        {
            ExpectSkip(0);
            int length = VInt();
            var instances = new object[length];
            for (int i = 0; i < length; i++)
            {
                instances[i] = Instance(typeid);
            }
            return instances;
        }

        private Tuple<int, byte[]> BitArray(int bounds)
        {
            ExpectSkip(1);
            int length = VInt();
            return Tuple.Create(length, _buffer.ReadAlignedBytes((length + 7) / 8));
        }

        private byte[] Blob(int bounds)
        {
            ExpectSkip(2);
            int length = VInt();
            return _buffer.ReadAlignedBytes(length);
        }

        private bool Bool()
        {
            ExpectSkip(6);
            return _buffer.ReadBits(8) != 0;
        }
        
        private Dictionary<string, object> Choice(int mix, int max, List<(string Arg1, Int128 Arg2)> fields)
        {
            ExpectSkip(3);
            int tag = VInt();
            if (tag < 0 || tag >= fields.Count)
            {
                SkipInstance();
                return new Dictionary<string, object>();
            }
            var field = fields[tag];
            return new Dictionary<string, object> { { field.Item1, Instance((int)field.Item2) } };
        }

        private byte[] FourCC()
        {
            ExpectSkip(7);
            return _buffer.ReadAlignedBytes(4);
        }

        private int Int(int bounds)
        {
            ExpectSkip(9);
            return VInt();
        }

        private object Null() => null;

        private object? Optional(int typeid)
        {
            ExpectSkip(4);
            bool exists = _buffer.ReadBits(8) != 0;
            return exists ? Instance(typeid) : null;
        }

        private float Real32()
        {
            ExpectSkip(7);
            var bytes = _buffer.ReadAlignedBytes(4);
            return BitConverter.ToSingle(bytes, 0);
        }

        private double Real64()
        {
            ExpectSkip(8);
            var bytes = _buffer.ReadAlignedBytes(8);
            return BitConverter.ToDouble(bytes, 0);
        }

        private Dictionary<string, object> Struct(List<(string Item1, Int128 Item2, Int128 Item3)> fields)
        {
            ExpectSkip(5);
            var result = new Dictionary<string, object>();
            int length = VInt();
            for (int i = 0; i < length; i++)
            {
                int tag = VInt();
                var fieldIdx = fields.FindIndex(f => f.Item3 == tag);

                if (fieldIdx >= 0)
                {
                    var field = fields[fieldIdx];
                    int fieldTypeId = checked((int)field.Item2);

                    if (field.Item1 == "__parent")
                    {
                        var parent = Instance(fieldTypeId);
                        if (parent is Dictionary<string, object> parentDict)
                        {
                            foreach (var kvp in parentDict)
                            {
                                result[kvp.Key] = kvp.Value;
                            }
                        }
                        else if (fields.Count == 1)
                        {
                            result = (Dictionary<string, object>)parent;
                        }
                        else
                        {
                            result[field.Item1] = parent;
                        }
                    }
                    else
                    {
                        result[field.Item1] = Instance(fieldTypeId);
                    }
                }
                else
                {
                    SkipInstance();
                }
            }
            return result;
        }

        private void SkipInstance()
        {
            int skip = _buffer.ReadBits(8);
            switch (skip)
            {
                case 0: // array
                    int length = VInt();
                    for (int i = 0; i < length; i++)
                    {
                        SkipInstance();
                    }
                    break;
                case 1: // bitblob
                    int bitLength = VInt();
                    _buffer.ReadAlignedBytes((bitLength + 7) / 8);
                    break;
                case 2: // blob
                    int blobLength = VInt();
                    _buffer.ReadAlignedBytes(blobLength);
                    break;
                case 3: // choice
                    VInt();
                    SkipInstance();
                    break;
                case 4: // optional
                    bool exists = _buffer.ReadBits(8) != 0;
                    if (exists)
                    {
                        SkipInstance();
                    }
                    break;
                case 5: // struct
                    int structLength = VInt();
                    for (int i = 0; i < structLength; i++)
                    {
                        VInt();
                        SkipInstance();
                    }
                    break;
                case 6: // u8
                    _buffer.ReadAlignedBytes(1);
                    break;
                case 7: // u32
                    _buffer.ReadAlignedBytes(4);
                    break;
                case 8: // u64
                    _buffer.ReadAlignedBytes(8);
                    break;
                case 9: // vint
                    VInt();
                    break;
                default:
                    throw new InvalidOperationException("Unknown skip type.");
            }
        }
    }
}
