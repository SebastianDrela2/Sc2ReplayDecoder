
namespace s2ProtocolFurry.Models.Details
{
    public class Details
    {
        public Details(int campaignIndex, int defaultDiff, string desc, string diff, bool disableRec, int speed, string image, bool isBlizzard, string mapName, bool mini, bool restart, long offset, long time, string title, List<DetailsPlayer> players)
        {
            CampaignIndex = campaignIndex;
            DefaultDiff = defaultDiff;
            Desc = desc;
            Diff = diff;
            DisableRec = disableRec;
            Speed = speed;
            Image = image;
            IsBlizzard = isBlizzard;
            MapName = mapName;
            Mini = mini;
            Restart = restart;
            Offset = offset;
            Time = time;
            Title = title;
            Players = players;
        }

        public int CampaignIndex { get; }
        public int DefaultDiff { get; }
        public string Desc { get; }
        public string Diff { get; }
        public bool DisableRec { get; }
        public int Speed { get; }
        public string Image { get; }
        public bool IsBlizzard { get; }
        public string MapName { get; }
        public bool Mini { get; }
        public bool Restart { get; }
        public long Offset { get; }
        public long Time { get; }
        public string Title { get; }
        public List<DetailsPlayer> Players { get; }
    }
}