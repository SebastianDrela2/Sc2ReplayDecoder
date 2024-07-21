namespace MPQArchive.MPQ
{
    internal class MPQHeader3
    {
        // 64-bit version of the archive size
        public UInt64 ArchiveSize64;

        // 64-bit position of the BET table
        public UInt64 BetTablePos64;

        // 64-bit position of the HET table
        public UInt64 HetTablePos64;
    }
}
