namespace s2ProtocolFurry.Events.TrackerEvents
{
    public class TrackerEvent
    {
        // Public properties to replace private fields
        public int PlayerId { get; init; }        
        public string Type { get; init; }
        public int Bits { get; init; }
        public uint Gameloop { get; init; }
        public TrackerEventType EventType { get; init; }

        // Constructor to initialize the properties
        public TrackerEvent(int playerId, string type, int bits, uint gameloop)
        {
            PlayerId = playerId;
            Type = type;
            Bits = bits;
            Gameloop = gameloop;
            EventType = type switch
            {
                "NNet.Replay.Tracker.SPlayerSetupEvent" => TrackerEventType.SPlayerSetupEvent,
                "NNet.Replay.Tracker.SPlayerStatsEvent" => TrackerEventType.SPlayerStatsEvent,
                "NNet.Replay.Tracker.SUnitBornEvent" => TrackerEventType.SUnitBornEvent,
                "NNet.Replay.Tracker.SUnitDiedEvent" => TrackerEventType.SUnitDiedEvent,
                "NNet.Replay.Tracker.SUnitOwnerChangeEvent" => TrackerEventType.SUnitOwnerChangeEvent,
                "NNet.Replay.Tracker.SUnitPositionsEvent" => TrackerEventType.SUnitPositionsEvent,
                "NNet.Replay.Tracker.SUnitTypeChangeEvent" => TrackerEventType.SUnitTypeChangeEvent,
                "NNet.Replay.Tracker.SUpgradeEvent" => TrackerEventType.SUpgradeEvent,
                "NNet.Replay.Tracker.SUnitInitEvent" => TrackerEventType.SUnitInitEvent,
                "NNet.Replay.Tracker.SUnitDoneEvent" => TrackerEventType.SUnitDoneEvent,
                _ => TrackerEventType.None
            };
        }

        public TrackerEvent(TrackerEvent trackerEvent)
        {
            ArgumentNullException.ThrowIfNull(trackerEvent);
            PlayerId = trackerEvent.PlayerId;            
            Bits = trackerEvent.Bits;
            Gameloop = trackerEvent.Gameloop;
            EventType = trackerEvent.EventType;
        }
    }

    // Assuming the TrackerEventType enum is defined as follows:
    public enum TrackerEventType
    {
        None,
        SPlayerSetupEvent,
        SPlayerStatsEvent,
        SUnitBornEvent,
        SUnitDiedEvent,
        SUnitOwnerChangeEvent,
        SUnitPositionsEvent,
        SUnitTypeChangeEvent,
        SUpgradeEvent,
        SUnitInitEvent,
        SUnitDoneEvent
    }
}