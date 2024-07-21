namespace MPQArchive.MPQ;

internal class MPQHeader4
{
    // Compressed size of the hash table
    public UInt64 HashTableSize64;

    // Compressed size of the block table
    public UInt64 BlockTableSize64;

    // Compressed size of the hi-block table
    public UInt64 HiBlockTableSize64;

    // Compressed size of the HET block
    public UInt64 HetTableSize64;

    // Compressed size of the BET block
    public UInt64 BetTableSize64;

    // Size of raw data chunk to calculate MD5.
    // MD5 of each data chunk follows the raw file data.
    public UInt32 RawChunkSize;

    public MD5 MD5_BlockTable;      // MD5 of the block table before decryption
    public MD5 MD5_HashTable;       // MD5 of the hash table before decryption
    public MD5 MD5_HiBlockTable;    // MD5 of the hi-block table
    public MD5 MD5_BetTable;        // MD5 of the BET table before decryption
    public MD5 MD5_HetTable;        // MD5 of the HET table before decryption
    public MD5 MD5_MpqHeader;       // MD5 of the MPQ header from signature to (including) MD5_HetTable
}

struct MD5
{
    public UInt64 Low;
    public UInt64 High;
}
