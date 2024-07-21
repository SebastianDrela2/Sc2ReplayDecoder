namespace s2ProtocolFurry.Events.TrackerEvents
{
    public class SUnitTypeChangeEvent : TrackerEvent
    {
        // Public properties to replace private fields
        public TrackerEvent TrackerEvent { get; }
        public int UnitTagIndex { get; }
        public int UnitTagRecycle { get; }
        public string UnitTypeName { get; }

        // Constructor to initialize the properties
        public SUnitTypeChangeEvent(TrackerEvent trackerEvent, int unitTagIndex, int unitTagRecycle, string unitTypeName) : base(trackerEvent)
        {
            TrackerEvent = trackerEvent;
            UnitTagIndex = unitTagIndex;
            UnitTagRecycle = unitTagRecycle;
            UnitTypeName = unitTypeName;
        }
    }
}