using s2ProtocolFurry.Protocol;

namespace s2ProtocolFurry.Decoders;

public interface IDecoder
{
    void ByteAlign();
    bool Done();
    object Instance(int svaruint32TypeId);
    int UsedBits();

    object ReadInstance(IProtocolTypeInfo typeInfo);
    object ReadInstance(ProtocolTypeInfo_2.ProtocolTypeBool typeInfo);
    object ReadInstance(ProtocolTypeInfo_2.ProtocolTypeNull typeInfo);
    object ReadInstance(ProtocolTypeInfo_2.ProtocolTypeFourcc typeInfo);
    object ReadInstance(ProtocolTypeInfo_2.ProtocolTypeInt typeInfo);
    object ReadInstance(ProtocolTypeInfo_2.ProtocolTypeBlob typeInfo);
    object ReadInstance(ProtocolTypeInfo_2.ProtocolTypeBitArray typeInfo);
    object ReadInstance<T>(ProtocolTypeInfo_2.ProtocolTypeArray<T> typeInfo)
        where T : IProtocolTypeInfo;
    object ReadInstance<T>(ProtocolTypeInfo_2.ProtocolTypeOptional<T> typeInfo)
        where T : IProtocolTypeInfo;
    object ReadInstance(ProtocolTypeInfo_2.ProtocolTypeChoice typeInfo);
    object ReadInstance(ProtocolTypeInfo_2.ProtocolTypeStruct typeInfo);
}
