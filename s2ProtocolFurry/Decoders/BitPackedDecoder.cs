using s2ProtocolFurry.Decoder;
using s2ProtocolFurry.Protocol;
using System.Collections;

namespace s2ProtocolFurry.Decoders;

public class BitPackedDecoder : BaseDecoder, IDecoder
{
    private readonly BitPackedBuffer _buffer;
    private readonly TypeInfoIdMap _typeInfos;

    public BitPackedDecoder(byte[] contents, TypeInfoIdMap typeInfos)
    {
        _buffer = new BitPackedBuffer(contents) { DebugOutput = new() };
        _typeInfos = typeInfos;
    }

    public override string ToString()
    {
        return _buffer.ToString();
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

    private bool Bool()
    {
        return Int((0, 1)) != 0;
    }

    private int Int((int, int) bounds)
    {
        return bounds.Item1 + _buffer.ReadBits(bounds.Item2);
    }

    public object Instance(int typeId)
    {
        if (typeId >= _typeInfos.Length)
        {
            throw new InvalidOperationException("Corrupted data");
        }

        var typeInfo = _typeInfos[typeId];
        return ReadInstance(typeInfo);
    }
    public object ReadInstance(IProtocolTypeInfo typeInfo) => typeInfo.Decode(this);
    public object ReadInstance(ProtocolTypeInfo_2.ProtocolTypeBool typeInfo)
    {
        return Int((0, 1)) != 0;
    }
    public object ReadInstance(ProtocolTypeInfo_2.ProtocolTypeNull typeInfo)
    {
        return null;
    }
    public object ReadInstance(ProtocolTypeInfo_2.ProtocolTypeFourcc typeInfo)
    {
        return _buffer.ReadUnalignedBytes(4);
    }
    public object ReadInstance(ProtocolTypeInfo_2.ProtocolTypeInt typeInfo)
    {
        var (min, max) = typeInfo;
        return min + _buffer.ReadBits((int)max);
    }
    public object ReadInstance(ProtocolTypeInfo_2.ProtocolTypeBlob typeInfo)
    {
        (Int128 min, Int128 max) = typeInfo;
        (int, int) bounds = ((int)min, (int)max);
        int length = Int(bounds);
        return _buffer.ReadAlignedBytes(length);
    }
    public object ReadInstance(ProtocolTypeInfo_2.ProtocolTypeBitArray typeInfo)
    {
        (Int128 min, Int128 max) = typeInfo;
        (int, int) bounds = ((int)min, (int)max);
        int length = Int(bounds);
        var bits = _buffer.ReadBits(length);

        return (length, bits);
    }
    public object ReadInstance<T>(ProtocolTypeInfo_2.ProtocolTypeArray<T> typeInfo) where T : IProtocolTypeInfo
    {
        (Int128 min, Int128 max, var elemInfo) = typeInfo;
        (int, int) bounds = ((int)min, (int)max);

        int length = Int(bounds);
        var instances = new object[length];
        for (int i = 0; i < length; i++)
        {
            instances[i] = ReadInstance(elemInfo);
        }
        return instances;
    }
    public object ReadInstance<T>(ProtocolTypeInfo_2.ProtocolTypeOptional<T> typeInfo) where T : IProtocolTypeInfo
    {
        bool exists = Bool();
        return exists ? ReadInstance(typeInfo.Element) : null;
    }
    public object ReadInstance(ProtocolTypeInfo_2.ProtocolTypeChoice typeInfo)
    {
        (Int128 min, Int128 max, var fields) = typeInfo;
        (int, int) bounds = ((int)min, (int)max);
        int tag = Int(bounds);

        if (tag < 0 || tag >= fields.Length)
        {
            throw new IndexOutOfRangeException();
        }

        var field = fields[tag];

        var result = new Dictionary<string, object>
        {
            [field.Name] = ReadInstance(field.FieldType)
        };
        return result;
    }
    public object ReadInstance(ProtocolTypeInfo_2.ProtocolTypeStruct typeInfo)
    {
        var fields = typeInfo.Fields;
        var result = new Dictionary<string, object>();

        foreach (var field in fields)
        {
            string fieldName = field.Name;

            var fieldType = ReadInstance(field.FieldType);

            if (fieldName == "__parent")
            {
                if (fieldType is Dictionary<string, object> parentDict)
                {
                    // Merge parent dictionary into result
                    foreach (var kvp in parentDict)
                    {
                        result[kvp.Key] = kvp.Value;
                    }
                }
                else if (fields.Length == 1)
                {
                    // Replace result with parent if there's only one field
                    result = (Dictionary<string, object>)fieldType;
                }
                else
                {
                    // Add parent to result with fieldName as the key
                    result[fieldName] = fieldType;
                }
            }
            else
            {
                // Add instance to result using fieldName as the key
                result[fieldName] = fieldType;
            }
        }

        return result;
    }
}
