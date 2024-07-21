namespace MPQArchive.MPQ
{
    public class MPQHashTableEntry
    {
        // The hash of the full file name (part A)
        public UInt32 HashA;

        // The hash of the full file name (part B)
        public UInt32 HashB;

        // The language of the file. This is a Windows LANGID data type, and uses the same values.
        // 0 indicates the default language (American English), or that the file is language-neutral.
        public UInt16 lcLocale;

        // The platform the file is used for. 0 indicates the default platform.
        // No other values have been observed.
        public UInt16 Platform;

        // If the hash table entry is valid, this is the index into the block table of the file.
        // Otherwise, one of the following two values:
        //  - 0xFFFFFFFF: Hash table entry is empty, and has always been empty.
        //                Terminates searches for a given file.
        //  - 0xFFFFFFFE: Hash table entry is empty, but was valid at some point (a deleted file).
        //                Does not terminate searches for a given file.
        public UInt32 BlockIndex { get; set; }

        public MPQHashTableEntry()
        {

        }
    }
}