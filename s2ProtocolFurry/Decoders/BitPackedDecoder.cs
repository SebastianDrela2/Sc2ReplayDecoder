using s2ProtocolFurry.Decoder;
using s2ProtocolFurry.Protocol;
using System.Collections;

namespace s2ProtocolFurry.Decoders;

public class BitPackedDecoder : BaseDecoder, IDecoder
{
    private readonly BitPackedBuffer _buffer;
    private readonly List<ProtocolTypeInfo> _typeInfos;

    public BitPackedDecoder(byte[] contents, List<ProtocolTypeInfo> typeInfos)
    {
        _buffer = new BitPackedBuffer(contents) { DebugOutput = new() };
        _typeInfos = typeInfos;
    }

    public override string ToString()
    {
        return _buffer.ToString();
    }

    public object Instance(int typeId)
    {
        if (typeId >= _typeInfos.Count)
        {
            throw new InvalidOperationException("Corrupted data");
        }
        
        var typeInfo = _typeInfos[typeId];
        var result = typeInfo switch
        {
            ProtocolTypeArray(Int128 arg1, Int128 arg2, Int128 typeid) => Array(((int)arg1, (int)arg2), (int)typeid),
            ProtocolTypeBitArray(Int128 arg1, Int128 arg2) => BitArray(((int)arg1, (int)arg2)),
            ProtocolTypeBlob(Int128 arg1, Int128 arg2) => Blob(((int)arg1, (int)arg2)),
            ProtocolTypeBool => Bool(),
            ProtocolTypeChoice(Int128 arg1, Int128 arg2, List<(string Arg1, Int128 Arg2)> arg3) => Choice(((int)arg1, (int)arg2), ProcessListOrDictionary(arg3)),
            ProtocolTypeFourcc => FourCC(),
            ProtocolTypeInt(Int128 arg1, Int128 arg2) => Int(((int)arg1, (int)arg2)),
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

    public bool Done()
    {
        return _buffer.Done();
    }

    public int UsedBits()
    {
        return _buffer.UsedBits();
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

    private object[] Array((int min, int max) bounds, int typeId)
    {
        int length = Int(bounds);
        var instances = new object[length];
        for (int i = 0; i < length; i++)
        {
            instances[i] = Instance(typeId);
        }
        return instances;
    }

    private Tuple<int, BitArray> BitArray((int min, int max) bounds)
    {
        int length = Int(bounds);       
        var bitArrayBytes = _buffer.ReadAlignedBytes((length + 7) / 8);
        var bitArray = new BitArray(bitArrayBytes);
        return Tuple.Create(length, bitArray);
    }

    private byte[] Blob((int min, int max) bounds)
    {
        int length = Int(bounds);
        return _buffer.ReadAlignedBytes(length);
    }

    private bool Bool()
    {
        return Int((0, 1)) != 0;
    }

    private Dictionary<string, object> Choice((int min, int max) bounds, List<Tuple<string, int>> fields)
    {       
        int tag = Int(bounds);
        
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
        return _buffer.ReadAlignedBytes(4);
    }

    private int Int((int, int) bounds)
    {
        return bounds.Item1 + _buffer.ReadBits(bounds.Item2);
    }

    private object Null()
    {
        return null;
    }

    private object Optional(int typeId)
    {
        bool exists = Bool();
        return exists ? Instance(typeId) : null;
    }

    private float Real32()
    {
        var bytes = _buffer.ReadAlignedBytes(4);
        return BitConverter.ToSingle(bytes, 0);
    }

    private double Real64()
    {
        var bytes = _buffer.ReadAlignedBytes(8);
        return BitConverter.ToDouble(bytes, 0);
    }

    private Dictionary<string, object> Struct(List<(string Item1, Int128 Item2, Int128 Item3)> fields)
    {
        var result = new Dictionary<string, object>();

        foreach (var field in fields)
        {
            string fieldName = field.Item1;
            int fieldId = checked((int)field.Item2);

            if (fieldName == "__parent")
            {
                var parent = Instance(fieldId);
                if (parent is Dictionary<string, object> parentDict)
                {
                    // Merge parent dictionary into result
                    foreach (var kvp in parentDict)
                    {
                        result[kvp.Key] = kvp.Value;
                    }
                }
                else if (fields.Count == 1)
                {
                    // Replace result with parent if there's only one field
                    result = (Dictionary<string, object>)parent;
                }
                else
                {
                    // Add parent to result with fieldName as the key
                    result[fieldName] = parent;
                }
            }
            else
            {
                // Add instance to result using fieldName as the key
                result[fieldName] = Instance(fieldId);
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
                int length = Int((0, 0));
                for (int i = 0; i < length; i++)
                {
                    SkipInstance();
                }
                break;
            case 1: // bitblob
                int bitLength = Int((0, 0));
                _buffer.ReadAlignedBytes((bitLength + 7) / 8);
                break;
            case 2: // blob
                int blobLength = Int((0, 0));
                _buffer.ReadAlignedBytes(blobLength);
                break;
            case 3: // choice
                Int((0, 0));
                SkipInstance();
                break;
            case 4: // optional
                bool exists = Bool();
                if (exists)
                {
                    SkipInstance();
                }
                break;
            case 5: // struct
                int structLength = Int((0, 0));
                for (int i = 0; i < structLength; i++)
                {
                    Int((0, 0));
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
                Int((0, 0));
                break;
            default:
                throw new InvalidOperationException("Unknown skip type.");
        }
    }
}
