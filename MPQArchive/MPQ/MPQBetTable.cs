namespace MPQArchive.MPQ
{
    internal class MPQBetTable
    {
        public UInt32 Signature;                      // 'BET\x1A'
        public UInt32 Version;                        // Version. Seems to be always 1
        public UInt32 DataSize;                       // Size of the contained table
        public UInt32 TableSize;                      // Size of the entire hash table, including the header (in bytes)
        public UInt32 FileCount;                      // Number of files in the BET table
        public UInt32 Unknown08;                      // Unknown, set to 0x10
        public UInt32 TableEntrySize;                 // Size of one table entry (in bits)
        public UInt32 BitIndex_FilePos;               // Bit index of the file position (within the entry record)
        public UInt32 BitIndex_FileSize;              // Bit index of the file size (within the entry record)
        public UInt32 BitIndex_CmpSize;               // Bit index of the compressed size (within the entry record)
        public UInt32 BitIndex_FlagIndex;             // Bit index of the flag index (within the entry record)
        public UInt32 BitIndex_Unknown;               // Bit index of the ??? (within the entry record)
        public UInt32 BitCount_FilePos;               // Bit size of file position (in the entry record)
        public UInt32 BitCount_FileSize;              // Bit size of file size (in the entry record)
        public UInt32 BitCount_CmpSize;               // Bit size of compressed file size (in the entry record)
        public UInt32 BitCount_FlagIndex;             // Bit size of flags index (in the entry record)
        public UInt32 BitCount_Unknown;               // Bit size of ??? (in the entry record)
        public UInt32 TotalBetHashSize;               // Total size of the BET hash
        public UInt32 BetHashSizeExtra;               // Extra bits in the BET hash
        public UInt32 BetHashSize;                    // Effective size of BET hash (in bits)
        public UInt32 BetHashArraySize;               // Size of BET hashes array, in bytes
        public UInt32 FlagCount;                      // Number of flags in the following array

        // Followed by array of file flags. Each entry is 32-bit size and its meaning is the same like
        public UInt32[] FlagsArray;

        // File table. Size of each entry is taken from dwTableEntrySize.
        // Size of the table is (dwTableEntrySize * dwMaxFileCount), round up to 8.

        // Array of BET hashes. Table size is taken from dwMaxFileCount from HET table
    }
}
