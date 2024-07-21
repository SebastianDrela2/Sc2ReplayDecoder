namespace MPQArchive.MPQ.ReceivedData
{
    public class MPQArchive
    {
        public MPQHeader1 MPQHeader { get; set; }
        public MPQUserData MPQUserData { get; set; }
        public Dictionary<string, string[]> ListingFiles { get; set; }
      
        public MPQArchive(MPQHeader1 mpqHeader, MPQUserData mpqUserData, Dictionary<string, string[]> listingFiles)
        {
            MPQHeader = mpqHeader ?? throw new ArgumentNullException(nameof(mpqHeader));
            MPQUserData = mpqUserData ?? throw new ArgumentNullException(nameof(mpqUserData));
            ListingFiles = listingFiles ?? throw new ArgumentNullException(nameof(listingFiles));
        }
    }
}
