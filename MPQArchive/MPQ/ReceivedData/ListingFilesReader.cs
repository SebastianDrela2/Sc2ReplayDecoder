using MPQArchive.MPQ.DecryptedData;

namespace MPQArchive.MPQ.ReceivedData
{
    public class ListingFilesReader(MPQFileReader mpqFileReader)
    {
        public Dictionary<string, string[]> Read()
        {
            var listingFiles = mpqFileReader.ReadFile("(listfile)");

            var listingFilesDict = new Dictionary<string, string[]>();

            foreach (var listingFile in listingFiles)
            {
                if (!string.IsNullOrEmpty(listingFile))
                {
                    listingFilesDict.Add(listingFile, mpqFileReader.ReadFile(listingFile));
                }
            }

            return listingFilesDict;
        }
    }
}
