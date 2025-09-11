using System.Text;
using s2ProtocolFurry.Decoders;
using s2ProtocolFurry.Logging;
using s2ProtocolFurry.Protocol;

namespace s2ProtocolFurry.Decoder
{   
    public class VersionedDecoder : BaseDecoder, IDecoder
    {
        private readonly BitPackedBuffer _buffer;
        private readonly TypeInfoIdMap _typeInfos;

        private readonly DebugLogger _debugOutput = new();

        public VersionedDecoder(byte[] contents, TypeInfoIdMap typeInfos)
        {
            _buffer = new BitPackedBuffer(contents) { DebugOutput = _debugOutput };
            _typeInfos = typeInfos;
        }

        public override string ToString() => _buffer.ToString();

        public object? Instance(int typeId)
        {
            if (typeId >= _typeInfos.Length)
            {
                throw new InvalidOperationException("Corrupted data");
            }

            var typeInfo = _typeInfos[typeId];
            return ReadInstance(typeInfo);
        }

        public void ByteAlign()
        {
            _debugOutput.AppendLine($"{nameof(ByteAlign)}()");
            _debugOutput.Indented(() =>
            {
                _buffer.ByteAlign();
            });
        }

        public bool Done() => _buffer.Done();

        public int UsedBits() => _buffer.UsedBits();

        private void ExpectSkip(int expected)
        {
            _debugOutput.AppendLine($"{nameof(ExpectSkip)}({nameof(expected)}: {expected})");
            using var guard = _debugOutput.PushIndent();

            var bits = _buffer.ReadBits(8);

            if (bits != expected)
            {
                Console.WriteLine($"Expecting: {expected}, Found: {bits}, Position: {_buffer.UsedBits()}");

                throw new InvalidOperationException("Corrupted data");
            }
        }

        private int VInt()
        {
            _debugOutput.AppendLine($"{nameof(VInt)}()");
            using var guard = _debugOutput.PushIndent();

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

        public object ReadInstance<T>(ProtocolTypeInfo_2.ProtocolTypeArray<T> typeInfo) where T : IProtocolTypeInfo
        {
            ExpectSkip(0);
            int length = VInt();
            var instances = new object[length];
            for (int i = 0; i < length; i++)
            {
                instances[i] = ReadInstance(typeInfo.Element);
            }
            return instances;
        }

        public object ReadInstance(ProtocolTypeInfo_2.ProtocolTypeBitArray typeInfo)
        {
            ExpectSkip(1);
            int length = VInt();
            return (length, _buffer.ReadAlignedBytes((length + 7) / 8));
        }

        public object ReadInstance(ProtocolTypeInfo_2.ProtocolTypeBlob typeInfo)
        {
            ExpectSkip(2);
            int length = VInt();
            return _buffer.ReadAlignedBytes(length);
        }

        public object ReadInstance(ProtocolTypeInfo_2.ProtocolTypeBool typeInfo)
        {
            ExpectSkip(6);
            return _buffer.ReadBits(8) != 0;
        }

        public object ReadInstance(ProtocolTypeInfo_2.ProtocolTypeChoice typeInfo)
        {
            var fields = typeInfo.Fields;
            ExpectSkip(3);
            int tag = VInt();
            if (tag < 0 || tag >= fields.Length)
            {
                SkipInstance();
                return new Dictionary<string, object>();
            }
            var field = fields[tag];
            return new Dictionary<string, object> { { field.Name, ReadInstance(field.FieldType) } };
        }

        public object ReadInstance(ProtocolTypeInfo_2.ProtocolTypeFourcc typeInfo)
        {
            ExpectSkip(7);
            return _buffer.ReadAlignedBytes(4);
        }

        public object ReadInstance(ProtocolTypeInfo_2.ProtocolTypeInt typeInfo)
        {
            ExpectSkip(9);
            return VInt();
        }

        public object ReadInstance(ProtocolTypeInfo_2.ProtocolTypeNull typeInfo) => null;

        public object ReadInstance<T>(ProtocolTypeInfo_2.ProtocolTypeOptional<T> typeInfo) where T : IProtocolTypeInfo
        {
            ExpectSkip(4);
            bool exists = _buffer.ReadBits(8) != 0;
            return exists ? ReadInstance(typeInfo.Element) : null;
        }

        public object ReadInstance(ProtocolTypeInfo_2.ProtocolTypeStruct typeInfo)
        {
            var fields = typeInfo.Fields;
            ExpectSkip(5);
            var result = new Dictionary<string, object>();
            int length = VInt();
            for (int i = 0; i < length; i++)
            {
                int tag = VInt();
                var fieldIdx = Array.FindIndex(fields, f => f.Tag == tag);

                if (fieldIdx >= 0)
                {
                    var field = fields[fieldIdx];
                    var fieldType = ReadInstance(field.FieldType);
                    
                    if (field.Name == "__parent")
                    {
                        var parent = fieldType;
                        if (parent is Dictionary<string, object> parentDict)
                        {
                            foreach (var kvp in parentDict)
                            {
                                result[kvp.Key] = kvp.Value;
                            }
                        }
                        else if (fields.Length == 1)
                        {
                            result = (Dictionary<string, object>)parent;
                        }
                        else
                        {
                            result[field.Name] = parent;
                        }
                    }
                    else
                    {
                        result[field.Name] = fieldType;
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
            _debugOutput.AppendLine($"{nameof(SkipInstance)}()");
            _debugOutput.Indented(() =>
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
            });
        }

        public object ReadInstance(IProtocolTypeInfo typeInfo) => typeInfo.Decode(this);
    }
}
