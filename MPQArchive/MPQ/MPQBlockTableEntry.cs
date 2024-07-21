namespace MPQArchive.MPQ
{
    public class MPQBlockTableEntry
    {
        public UInt32 FilePosition;
        public UInt32 CompressedSize;
        public UInt32 UncompressedSize;
        public UInt32 Flags;

        public MPQBlockTableEntry()
        {

        }       
    }
}
