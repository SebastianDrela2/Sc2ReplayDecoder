namespace MPQArchive.MPQ.Constants
{
    public static class MPQFileConstant
    {
        public const long MPQ_FILE_IMPLODE = 0x00000100;
        public const long MPQ_FILE_COMPRESS = 0x00000200;
        public const long MPQ_FILE_ENCRYPTED = 0x00010000;
        public const long MPQ_FILE_FIX_KEY = 0x00020000;
        public const long MPQ_FILE_SINGLE_UNIT = 0x01000000;
        public const long MPQ_FILE_DELETE_MARKER = 0x02000000;
        public const long MPQ_FILE_SECTOR_CRC = 0x04000000;
        public const long MPQ_FILE_EXISTS = 0x80000000;
    }
}
