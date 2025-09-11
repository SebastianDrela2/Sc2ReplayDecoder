using s2ProtocolFurry.Decoders;

namespace s2ProtocolFurry.Protocol
{
    public abstract record ProtocolTypeInfo(string Type)
    {       
        public static ProtocolTypeBool MakeBool() => new ();
        public static ProtocolTypeNull MakeNull() => new();
        public static ProtocolTypeFourcc MakeFourcc() => new();
        public static ProtocolTypeInt MakeInt(Int128 arg1, Int128 arg2)
            => new(arg1, arg2);
        public static ProtocolTypeBlob MakeBlob(Int128 arg1, Int128 arg2)
            => new(arg1, arg2);
        public static ProtocolTypeBitArray MakeBitArray(Int128 arg1, Int128 arg2)
            => new(arg1, arg2);
        public static ProtocolTypeArray MakeArray(Int128 arg1, Int128 arg2, Int128 arg3)
            => new(arg1, arg2, arg3);
        public static ProtocolTypeOptional MakeOptional(Int128 arg1)
            => new(arg1);
        public static ProtocolTypeChoice MakeChoice(Int128 arg1, Int128 arg2, List<(string Arg1, Int128 Arg2)> arg3)
            => new(arg1, arg2, arg3);
        public static ProtocolTypeStruct MakeStruct(List<(string Name, Int128 TypeId, Int128 Tag)> fields)
            => new(fields);

        public abstract IProtocolTypeInfo Transform(TypeInfoIdMap map);
    }

    public record ProtocolTypeBool() : ProtocolTypeInfo("_bool")
    {
        public override IProtocolTypeInfo Transform(TypeInfoIdMap map) => new ProtocolTypeInfo_2.ProtocolTypeBool();
    }
    public record ProtocolTypeNull() : ProtocolTypeInfo("_null")
    {
        public override IProtocolTypeInfo Transform(TypeInfoIdMap map) => new ProtocolTypeInfo_2.ProtocolTypeNull();
    }
    public record ProtocolTypeFourcc() : ProtocolTypeInfo("_fourcc")
    {
        public override IProtocolTypeInfo Transform(TypeInfoIdMap map) => new ProtocolTypeInfo_2.ProtocolTypeFourcc();
    }
    public record ProtocolTypeInt(Int128 Min, Int128 Max) : ProtocolTypeInfo("_int")
    {
        public override IProtocolTypeInfo Transform(TypeInfoIdMap map) => new ProtocolTypeInfo_2.ProtocolTypeInt(Min, Max);
    }
    public record ProtocolTypeBlob(Int128 Min, Int128 Max) : ProtocolTypeInfo("_blob")
    {
        public override IProtocolTypeInfo Transform(TypeInfoIdMap map) => new ProtocolTypeInfo_2.ProtocolTypeBlob(Min, Max);
    }
    public record ProtocolTypeBitArray(Int128 Min, Int128 Max) : ProtocolTypeInfo("_bitarray")
    {
        public override IProtocolTypeInfo Transform(TypeInfoIdMap map) => new ProtocolTypeInfo_2.ProtocolTypeBitArray(Min, Max);
    }
    public record ProtocolTypeArray(Int128 Arg1, Int128 Arg2, Int128 TypeId) : ProtocolTypeInfo("_array")
    {
        public override IProtocolTypeInfo Transform(TypeInfoIdMap map) => map.Resolve(TypeId).AsArrayType(Arg1, Arg2);
    }
    public record ProtocolTypeOptional(Int128 TypeId) : ProtocolTypeInfo("_optional")
    {
        public override IProtocolTypeInfo Transform(TypeInfoIdMap map) => map.Resolve(TypeId).AsOptionalType();
    }
    public record ProtocolTypeChoice(Int128 Arg1, Int128 Arg2, List<(string Name, Int128 TypeId)> Fields) : ProtocolTypeInfo("_choice")
    {
        public override IProtocolTypeInfo Transform(TypeInfoIdMap map)
        {
            var fields = new (string Name, IProtocolTypeInfo FieldType)[Fields.Count];

            for (int i = 0; i < fields.Length; i++)
            {
                var (name, typeId) = Fields[i];
                
                fields[i] = (name, map.Resolve(typeId));
            }
            return new ProtocolTypeInfo_2.ProtocolTypeChoice(Arg1, Arg2, fields);
        }
    }
    public record ProtocolTypeStruct(List<(string Name, Int128 TypeId, Int128 Tag)> Fields) : ProtocolTypeInfo("_struct")
    {
        public override IProtocolTypeInfo Transform(TypeInfoIdMap map)
        {
            var fields = new (string Name, IProtocolTypeInfo FieldType, Int128 Tag)[Fields.Count];

            for (int i = 0; i < fields.Length; i++)
            {
                var (name, typeId, tag) = Fields[i];
                fields[i] = (name, map.Resolve(typeId), tag);
            }

            return new ProtocolTypeInfo_2.ProtocolTypeStruct(fields);
        }
    }

    public interface IProtocolTypeInfo
    {
        IProtocolTypeInfo AsArrayType(Int128 Arg1, Int128 Arg2);
        IProtocolTypeInfo AsOptionalType();
        object Decode(IDecoder decoder);
    }

    public interface IProtocolTypeInfo<TSelf> : IProtocolTypeInfo
        where TSelf : IProtocolTypeInfo<TSelf>
    {
        TSelf Self { get; }

        IProtocolTypeInfo IProtocolTypeInfo.AsArrayType(Int128 min, Int128 max) => new ProtocolTypeInfo_2.ProtocolTypeArray<TSelf>(min, max, Self);
        IProtocolTypeInfo IProtocolTypeInfo.AsOptionalType() => new ProtocolTypeInfo_2.ProtocolTypeOptional<TSelf>(Self);
    }

    public class TypeInfoIdMap(List<ProtocolTypeInfo> map)
    {
        private readonly List<ProtocolTypeInfo> _source = map;
        private readonly IProtocolTypeInfo?[] _resolved = new IProtocolTypeInfo?[map.Count];
        public int Length => _resolved.Length;
        
        public IProtocolTypeInfo this[int index] => Resolve(index);
        public IProtocolTypeInfo this[Int128 index] => Resolve((int)index);

        public IProtocolTypeInfo Resolve(Int128 index) => Resolve((int)index);
        public IProtocolTypeInfo Resolve(int index)
        {
            ref var x = ref _resolved[index];
            return x ??= _source[index].Transform(this);
        }
        public List<ProtocolTypeInfo>.Enumerator GetEnumerator() => _source.GetEnumerator();
    }

    public abstract record ProtocolTypeInfo_2(string Type)
    {
        public record ProtocolTypeBool() : ProtocolTypeInfo_2("_bool"), IProtocolTypeInfo<ProtocolTypeBool>
        {
            public ProtocolTypeBool Self => this;
            public object Decode(IDecoder decoder) => decoder.ReadInstance(this);
        }
        public record ProtocolTypeNull() : ProtocolTypeInfo_2("_null"), IProtocolTypeInfo<ProtocolTypeNull>
        {
            public ProtocolTypeNull Self => this;
            public object Decode(IDecoder decoder) => decoder.ReadInstance(this);
        }
        public record ProtocolTypeFourcc() : ProtocolTypeInfo_2("_fourcc"), IProtocolTypeInfo<ProtocolTypeFourcc>
        {
            public ProtocolTypeFourcc Self => this;
            public object Decode(IDecoder decoder) => decoder.ReadInstance(this);
        }
        public record ProtocolTypeInt(Int128 Min, Int128 Max) : ProtocolTypeInfo_2("_int"), IProtocolTypeInfo<ProtocolTypeInt>
        {
            public ProtocolTypeInt Self => this;
            public object Decode(IDecoder decoder) => decoder.ReadInstance(this);
        }
        public record ProtocolTypeBlob(Int128 Min, Int128 Max) : ProtocolTypeInfo_2("_blob"), IProtocolTypeInfo<ProtocolTypeBlob>
        {
            public ProtocolTypeBlob Self => this;
            public object Decode(IDecoder decoder) => decoder.ReadInstance(this);
        }
        public record ProtocolTypeBitArray(Int128 Min, Int128 Max) : ProtocolTypeInfo_2("_bitarray"), IProtocolTypeInfo<ProtocolTypeBitArray>
        {
            public ProtocolTypeBitArray Self => this;
            public object Decode(IDecoder decoder) => decoder.ReadInstance(this);
        }
        public record ProtocolTypeArray<T>(Int128 Arg1, Int128 Arg2, T Element) : ProtocolTypeInfo_2("_array"), IProtocolTypeInfo<ProtocolTypeArray<T>>
            where T : IProtocolTypeInfo
        {
            public ProtocolTypeArray<T> Self => this;
            public object Decode(IDecoder decoder) => decoder.ReadInstance(this);
        }
        public record ProtocolTypeOptional<T>(T Element) : ProtocolTypeInfo_2("_optional"), IProtocolTypeInfo<ProtocolTypeOptional<T>>
            where T : IProtocolTypeInfo
        {
            public ProtocolTypeOptional<T> Self => this;
            public object Decode(IDecoder decoder) => decoder.ReadInstance(this);
        }
        public record ProtocolTypeChoice(Int128 Min, Int128 Max, (string Name, IProtocolTypeInfo FieldType)[] Fields) : ProtocolTypeInfo_2("_choice"), IProtocolTypeInfo<ProtocolTypeChoice>
        {
            public ProtocolTypeChoice Self => this;
            public object Decode(IDecoder decoder) => decoder.ReadInstance(this);
        }
        public record ProtocolTypeStruct((string Name, IProtocolTypeInfo FieldType, Int128 Tag)[] Fields) : ProtocolTypeInfo_2("_struct"), IProtocolTypeInfo<ProtocolTypeStruct>
        {
            public ProtocolTypeStruct Self => this;
            public object Decode(IDecoder decoder) => decoder.ReadInstance(this);
        }
    }
}
