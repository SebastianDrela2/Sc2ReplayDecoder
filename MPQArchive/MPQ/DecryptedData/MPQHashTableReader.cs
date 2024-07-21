using MPQArchive.MPQ.Hashing;

namespace MPQArchive.MPQ.DecryptedData
{
    public class MPQHashTableReader(Encryption encryption, MPQHashTableEntry[] mPQHashTableEntries)
    {
        public MPQHashTableEntry? GetHashTableEntry(string fileName)
        {
            var hashA = encryption.Hash(fileName, "HASH_A");
            var hashB = encryption.Hash(fileName, "HASH_B");

            foreach (var entry in mPQHashTableEntries)
            {
                if (entry.HashA == hashA && entry.HashB == hashB)
                {
                    return entry;
                }
            }

            return null;
        }
    }
}
