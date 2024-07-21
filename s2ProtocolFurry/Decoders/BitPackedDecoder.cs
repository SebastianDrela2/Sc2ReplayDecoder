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
        var methodName = typeInfo.Type;
        var parameters = typeInfo.Arguments;

        var result = methodName switch
        {
            "_array" => Array(
                (Convert.ToInt32(parameters[0]), Convert.ToInt32(parameters[1])), // Tuple for bounds
                Convert.ToInt32(parameters[2]) // typeId
            ),
            "_bitarray" => BitArray(
                (Convert.ToInt32(parameters[0]), Convert.ToInt32(parameters[1])) // Tuple for bounds
            ),
            "_blob" => Blob(
                (Convert.ToInt32(parameters[0]), Convert.ToInt32(parameters[1])) // Tuple for bounds
            ),
            "_bool" => Bool(),
            "_choice" => Choice((
                Convert.ToInt32(parameters[0]), // min bound
                Convert.ToInt32(parameters[1])), // max bound
                CastToListOfTuples(parameters[2]) // fields
            ),

            "_fourcc" => FourCC(),
            "_int" => Int(
                (Convert.ToInt32(parameters[0]), Convert.ToInt32(parameters[1])) // Tuple for bounds
            ),
            "_null" => Null(),
            "_optional" => Optional(Convert.ToInt32(parameters[0])),
            "_real32" => Real32(),
            "_real64" => Real64(),
            "_struct" => Struct(
                ConvertToListOfTuples(parameters) // fields
            ),
            _ => throw new InvalidOperationException($"Unknown method '{methodName}'")
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

    private Dictionary<string, object> Struct(List<Tuple<string, int, int>> fields)
    {
        var result = new Dictionary<string, object>();

        foreach (var field in fields)
        {
            string fieldName = field.Item1;
            int fieldId = field.Item2;

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
