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
        public static ProtocolTypeStruct MakeStruct(List<(string Arg1, Int128 Arg2, Int128 Arg3)> arg1)
            => new(arg1);
    }

    public record ProtocolTypeBool() : ProtocolTypeInfo("_bool");
    public record ProtocolTypeNull() : ProtocolTypeInfo("_null");
    public record ProtocolTypeFourcc() : ProtocolTypeInfo("_fourcc");
    public record ProtocolTypeInt(Int128 Arg1, Int128 Arg2) : ProtocolTypeInfo("_int");
    public record ProtocolTypeBlob(Int128 Arg1, Int128 Arg2) : ProtocolTypeInfo("_blob");
    public record ProtocolTypeBitArray(Int128 Arg1, Int128 Arg2) : ProtocolTypeInfo("_bitarray");
    public record ProtocolTypeArray(Int128 Arg1, Int128 Arg2, Int128 TypeId) : ProtocolTypeInfo("_array");
    public record ProtocolTypeOptional(Int128 Arg1) : ProtocolTypeInfo("_optional");
    public record ProtocolTypeChoice(Int128 Arg1, Int128 Arg2, List<(string Arg1, Int128 Arg2)> Arg3) : ProtocolTypeInfo("_choice");
    public record ProtocolTypeStruct(List<(string Arg1, Int128 Arg2, Int128 Arg3)> Arg1) : ProtocolTypeInfo("_struct");

}
