namespace s2ProtocolFurry.Events.TrackerEvents
{
    public class SUnitInitEvent : TrackerEvent
    {
        // Public properties to replace private fields
        public TrackerEvent TrackerEvent { get; }
        public int UnitTagIndex { get; }
        public int UnitTagRecycle { get; }
        public int ControlPlayerId { get; }
        public int X { get; }
        public int Y { get; }
        public int UpkeepPlayerId { get; }
        public string UnitTypeName { get; }
        public int UnitIndex { get; internal set; }
        public SUnitDiedEvent? SUnitDiedEvent { get; internal set; }
        public SUnitDoneEvent? SUnitDoneEvent { get; internal set; }

        // Constructor to initialize the properties
        public SUnitInitEvent(TrackerEvent trackerEvent, int unitTagIndex, int unitTagRecycle, int controlPlayerId, int x, int y, int upkeepPlayerId, string unitTypeName) : base(trackerEvent)
        {
            TrackerEvent = trackerEvent;
            UnitTagIndex = unitTagIndex;
            UnitTagRecycle = unitTagRecycle;
            ControlPlayerId = controlPlayerId;
            X = x;
            Y = y;
            UpkeepPlayerId = upkeepPlayerId;
            UnitTypeName = unitTypeName;
        }
    }
}