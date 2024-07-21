using s2ProtocolFurry.Decoder;
using System.Collections;
using System.Reflection;

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
        var methodName = typeInfo.Key;
        var parameters = (object[])typeInfo.Value;

        var method = GetType().GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Instance);
        if (method == null)
        {
            throw new InvalidOperationException($"Method {methodName} not found");
        }

        return method.Invoke(this, parameters);
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

    private Dictionary<string, object> Struct(List<Tuple<string, int, int>> fields)
    {
        var result = new Dictionary<string, object>();
        int length = Int((0, 0)); // This should be the correct way to get the length
        for (int i = 0; i < length; i++)
        {
            int tag = Int((0, 0)); // This should be the correct way to get the tag
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
