using s2ProtocolFurry.Events.GameEvents;
using s2ProtocolFurry.Events.InitEvents;
using s2ProtocolFurry.Events.MessageEvents;
using s2ProtocolFurry.Events.MetaData;
using s2ProtocolFurry.Events.TrackerEvents;
using s2ProtocolFurry.Models.Details;

namespace s2ProtocolFurry
{
    public class Sc2Replay
    {
        public string FileName;
        
        public Sc2Replay(string replayPath)
        {
            FileName = replayPath;
        }

        public InitData? InitData { get; internal set; }
        public Details Details { get; internal set; }
        public GameEvents GameEvents { get; internal set; }
        public TrackerEvents TrackerEvents;
        public ReplayMetadata MetaData { get; internal set; }
        public List<ChatMessageEvent> ChatMessages { get; set; }
        public List<PingMessageEvent> PingMessages { get; set; }
    }
}
