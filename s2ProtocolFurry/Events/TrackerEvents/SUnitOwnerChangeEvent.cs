namespace s2ProtocolFurry.Events.TrackerEvents
{
    public class SUnitOwnerChangeEvent : TrackerEvent
    {
        // Public properties to replace private fields
        public TrackerEvent TrackerEvent { get; }
        public int UnitTagIndex { get; }
        public int UnitTagRecycle { get; }
        public int ControlPlayerId { get; }
        public int UpkeepPlayerId { get; }
        public int UnitIndex { get; internal set; }

        // Constructor to initialize the properties
        public SUnitOwnerChangeEvent(TrackerEvent trackerEvent, int unitTagIndex, int unitTagRecycle, int controlPlayerId, int upkeepPlayerId) : base(trackerEvent)
        {
            TrackerEvent = trackerEvent;
            UnitTagIndex = unitTagIndex;
            UnitTagRecycle = unitTagRecycle;
            ControlPlayerId = controlPlayerId;
            UpkeepPlayerId = upkeepPlayerId;
        }
    }
}