using System;

namespace MPQArchive.MPQ
{
    internal class MPQHeader2
    {
        public UInt64 ExtBlockTablePos;   // Offset to the beginning of the extended block table, relative to the beginning of the archive.
	    public UInt16 HashTablePosHigh;   // High 16 bits of the hash table offset for large archives.
	    public UInt16 BlockTablePosHigh;  // High 16 bits of the block table offset for large archives. 
    }
}
