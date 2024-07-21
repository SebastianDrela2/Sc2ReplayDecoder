namespace s2ProtocolFurry.Events.TrackerEvents
{
    public class SUnitDoneEvent : TrackerEvent
    {
        // Public properties to replace private fields
        public TrackerEvent TrackerEvent { get; }
        public int UnitTagIndex { get; }
        public int UnitTagRecycle { get; }
        public int UnitIndex { get; internal set; }

        // Constructor to initialize the properties
        public SUnitDoneEvent(TrackerEvent trackerEvent, int unitTagIndex, int unitTagRecycle) : base(trackerEvent)
        {
            TrackerEvent = trackerEvent;
            UnitTagIndex = unitTagIndex;
            UnitTagRecycle = unitTagRecycle;
        }
    }
}