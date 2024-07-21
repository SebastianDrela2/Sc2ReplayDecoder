using MPQArchive.MPQ.DecryptedData;
using System.Text;

namespace MPQArchive.MPQ.ReceivedData
{
    public class ListingFilesReader(MPQFileReader mpqFileReader)
    {
        public Dictionary<string, byte[]> Read()
        {
            var listingFiles = mpqFileReader.ReadFile("(listfile)");
            var fileContent = Encoding.UTF8.GetString(listingFiles);
            var lines = fileContent.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
          
            var listingFilesDict = new Dictionary<string, byte[]>();

            foreach (var listingFile in lines)
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
