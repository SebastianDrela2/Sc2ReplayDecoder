using s2ProtocolFurry.Decoder;

namespace s2ProtocolFurry.Decoders;

public class BitPackedDecoder : IDecoder
{
    private readonly BitPackedBuffer _buffer;
    private readonly List<KeyValuePair<string, object>> _typeInfos;

    public BitPackedDecoder(byte[] contents, List<KeyValuePair<string, object>> typeInfos)
    {
        _buffer = new BitPackedBuffer(contents);
        _typeInfos = typeInfos;
    }

    public override string ToString() => _buffer.ToString();

    public object Instance(int typeId)
    {
        if (typeId >= _typeInfos.Count)
        {
            throw new Exception("Corrupted error: Type ID not found");
        }

        var typeInfo = _typeInfos[typeId];
        var type = Type.GetType(typeInfo.Key);
        if (type == null)
        {
            throw new Exception($"Type '{typeInfo.Key}' not found");
        }

        // Assuming the constructor takes parameters of type `object[]`
        var constructor = type.GetConstructor(new[] { typeof(object[]) });
        if (constructor == null)
        {
            throw new Exception($"No valid constructor found for type '{typeInfo.Key}'");
        }

        return constructor.Invoke(new[] { typeInfo.Value });
    }

    public void ByteAlign() => _buffer.ByteAlign();

    public bool Done() => _buffer.Done();

    public int UsedBits() => _buffer.UsedBits();

    public T[] Array<T>(int bounds, int typeId)
    {
        int length = Int(bounds);
        var result = new T[length];
        for (int i = 0; i < length; i++)
        {
            result[i] = (T)Instance(typeId);
        }
        return result;
    }

    public (int Length, byte[] Bits) BitArray(int bounds)
    {
        int length = Int(bounds);
        return (length, _buffer.ReadBits(length));
    }

    public byte[] Blob(int bounds)
    {
        int length = Int(bounds);
        return _buffer.ReadAlignedBytes(length);
    }

    public bool Bool() => Int(0, 1) != 0;

    public Dictionary<string, object> Choice(int bounds, Dictionary<int, (string FieldName, int TypeId)> fields)
    {
        int tag = Int(bounds);
        if (!fields.TryGetValue(tag, out var field))
        {
            throw new Exception("Corrupted error: Choice tag not found");
        }
        return new Dictionary<string, object> { [field.FieldName] = Instance(field.TypeId) };
    }

    public byte[] FourCC() => _buffer.ReadUnalignedBytes(4);

    public int Int(int minValue, int bits) => minValue + _buffer.ReadBits(bits);

    public object Null() => null;

    public object Optional(int typeId)
    {
        return Bool() ? Instance(typeId) : null;
    }

    public float Real32() => BitConverter.ToSingle(_buffer.ReadUnalignedBytes(4), 0);

    public double Real64() => BitConverter.ToDouble(_buffer.ReadUnalignedBytes(8), 0);

    public Dictionary<string, object> Struct(Dictionary<string, int> fields)
    {
        var result = new Dictionary<string, object>();
        foreach (var field in fields)
        {
            if (field.Key == "__parent")
            {
                var parent = Instance(field.Value);
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
                    result[field.Key] = parent;
                }
            }
            else
            {
                result[field.Key] = Instance(field.Value);
            }
        }
        return result;
    }
}
