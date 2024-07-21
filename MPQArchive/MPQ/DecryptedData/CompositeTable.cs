namespace MPQArchive.MPQ.DecryptedData
{
    public class CompositeTable
    {
        public MPQHashTableEntry[] MPQHashTableEntries;
        public MPQBlockTableEntry[] MPQBlockTableEntries;

        public CompositeTable(DecryptedTableReader decryptedTableReader, long headerOffset)
        {
            MPQHashTableEntries = decryptedTableReader.ReadHashTable(headerOffset);
            MPQBlockTableEntries = decryptedTableReader.ReadBlockTable(headerOffset);
        }
    }
}
