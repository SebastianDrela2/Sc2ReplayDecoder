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
            ProtocolTypeChoice(Int128 arg1, Int128 arg2, List<(string Arg1, Int128 Arg2)> arg3) => Choice(((int)arg1, (int)arg2), arg3),
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

    private (int, int) BitArray((int min, int max) bounds)
    {
        int length = Int(bounds);
        var bits = _buffer.ReadBits(length);

        return (length, bits);
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

    private Dictionary<string, object> Choice((int min, int max) bounds, List<(string Arg1, Int128 Arg2)> fields)
    {       
        int tag = Int(bounds);
        
        if (tag < 0 || tag >= fields.Count)
        {
            throw new IndexOutOfRangeException();           
        }

        var field = fields[tag];

        var result = new Dictionary<string, object>
        {
            [field.Arg1] = Instance((int)field.Arg2)
        };
        return result;
    }

    private byte[] FourCC()
    {
        return _buffer.ReadUnalignedBytes(4);
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
        var bytes = _buffer.ReadUnalignedBytes(4);
        return BitConverter.ToSingle(bytes, 0);
    }

    private double Real64()
    {
        var bytes = _buffer.ReadUnalignedBytes(8);
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
}
