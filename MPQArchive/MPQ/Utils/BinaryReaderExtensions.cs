namespace MPQArchive.MPQ.Utils
{
    internal static class BinaryReaderExtensions
    {
        public static void GoTo(this BinaryReader reader, long newPosition)
        {
            reader.BaseStream.Position = newPosition;
        }
    }
}
