namespace MPQArchive.MPQ
{
    public class MPQHeader1
    {
       public const UInt32 Magic = 0x1A_51_50_4D;
	   public UInt32 HeaderSize;
	   public UInt32 ArchiveSize;
	   public UInt16 FormatVersion;      // 0 = Original format, 1 = Extended format(The Burning Crusade and newer)
	   public UInt16 SectorSizeShift;
	   public UInt32 HastTableOffset;  // Relative to the beginning of the archive
	   public UInt32 BlockTableOffset; // Relative to the beginning of the archive
	   public UInt32 HashTableCount;
       public UInt32 BlockTableCount;
    }
}
