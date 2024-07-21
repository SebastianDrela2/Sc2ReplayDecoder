﻿using s2ProtocolFurry.Decoders;
using System.Reflection;

namespace s2ProtocolFurry.Decoder
{
    public class VersionedDecoder : IDecoder
    {
        private readonly BitPackedBuffer _buffer;
        private readonly List<KeyValuePair<string, object>> _typeInfos;

        public VersionedDecoder(byte[] contents, List<KeyValuePair<string, object>> typeInfos)
        {
            _buffer = new BitPackedBuffer(contents);
            _typeInfos = typeInfos;
        }

        public override string ToString() => _buffer.ToString();

        public object Instance(int typeId)
        {
            if (typeId >= _typeInfos.Count)
            {
                throw new InvalidOperationException("Corrupted data");
            }

            var typeInfo = _typeInfos[typeId];
            var methodName = typeInfo.Key;
            var parameters = (object[])typeInfo.Value;

            var method = GetType().GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Instance);
            if (method == null)
            {
                throw new InvalidOperationException($"Method {methodName} not found");
            }

            return method.Invoke(this, parameters)!;
        }

        public void ByteAlign() => _buffer.ByteAlign();

        public bool Done() => _buffer.Done();

        public int UsedBits() => _buffer.UsedBits();

        private void ExpectSkip(int expected)
        {
            if (_buffer.ReadBits(8) != expected)
            {
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

        private Dictionary<string, object> Choice(int bounds, List<Tuple<string, int>> fields)
        {
            ExpectSkip(3);
            int tag = VInt();
            var field = fields.FirstOrDefault(f => f.Item2 == tag);
            if (field == null)
            {
                SkipInstance();
                return new Dictionary<string, object>();
            }

            var result = new Dictionary<string, object>
            {
                [field.Item1] = Instance(field.Item2)
            };
            return result;
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

        private object Optional(int typeid)
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

        private Dictionary<string, object> Struct(List<Tuple<string, int, int>> fields)
        {
            ExpectSkip(5);
            var result = new Dictionary<string, object>();
            int length = VInt();
            for (int i = 0; i < length; i++)
            {
                int tag = VInt();
                var field = fields.FirstOrDefault(f => f.Item3 == tag);
                if (field != null)
                {
                    if (field.Item1 == "__parent")
                    {
                        var parent = Instance(field.Item2);
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
                        result[field.Item1] = Instance(field.Item2);
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
