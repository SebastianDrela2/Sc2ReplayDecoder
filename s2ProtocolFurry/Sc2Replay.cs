using s2ProtocolFurry.Events.InitEvents;
using s2ProtocolFurry.Events.TrackerEvents;
using s2ProtocolFurry.Models.Details;

namespace s2ProtocolFurry
{
    public class Sc2Replay
    {
        public string ReplayPath;

        public TrackerEvents TrackerEvents;

        public Sc2Replay(string replayPath)
        {
            ReplayPath = replayPath;
        }

        public InitData? InitData { get; internal set; }
        public Details Details { get; internal set; }
    }
}
