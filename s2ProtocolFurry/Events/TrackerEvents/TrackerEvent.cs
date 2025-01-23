namespace s2ProtocolFurry.Events.TrackerEvents
{
    public class TrackerEvent
    {
        // Public properties to replace private fields
        public int PlayerId { get; init; }
        public int Bits { get; init; }
        public uint Gameloop { get; init; }
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