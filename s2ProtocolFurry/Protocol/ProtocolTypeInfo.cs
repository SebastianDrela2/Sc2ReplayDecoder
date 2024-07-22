namespace s2ProtocolFurry.Protocol
{
    public class ProtocolTypeInfo
    {
        public string Type { get; set; }
        public object[] Arguments { get; set; }

        public ProtocolTypeInfo(string type, object[] arguments)
        {
            Type = type;
            Arguments = arguments;
        }

        public static ProtocolTypeInfo MakeBool() => new("_bool", []);
        public static ProtocolTypeInfo MakeNull() => new("_null", []);
        public static ProtocolTypeInfo MakeFourcc() => new("_fourcc", []);
        public static ProtocolTypeInfo MakeInt(Int128 arg1, Int128 arg2)
            => new("_int", [ arg1, arg2 ]);
        public static ProtocolTypeInfo MakeBlob(Int128 arg1, Int128 arg2)
            => new("_blob", [arg1, arg2]);
        public static ProtocolTypeInfo MakeBitArray(Int128 arg1, Int128 arg2)
            => new("_bitarray", [arg1, arg2]);
        public static ProtocolTypeInfo MakeArray(Int128 arg1, Int128 arg2, Int128 arg3)
            => new("_array", [arg1, arg2, arg3]);
        public static ProtocolTypeInfo MakeOptional(Int128 arg1)
            => new("_optional", [arg1]);
        public static ProtocolTypeInfo MakeChoice(Int128 arg1, Int128 arg2, List<(string Arg1, Int128 Arg2)> arg3)
            => new("_choice", [arg1, arg2, arg3]);
        public static ProtocolTypeInfo MakeStruct(List<(string Arg1, Int128 Arg2, Int128 Arg3)> arg1)
            => new("_struct", [arg1]);

        public override string ToString()
        {
            return $"{Type}: {string.Join(", ", Arguments)}";
        }
    }
}
