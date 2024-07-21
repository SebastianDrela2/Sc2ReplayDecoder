namespace s2ProtocolFurry.Decoders
{
    public interface IDecoder
    {
        void ByteAlign();
        bool Done();
        object Instance(int svaruint32TypeId);
        int UsedBits();
    }
}
