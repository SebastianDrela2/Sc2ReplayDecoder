namespace s2ProtocolFurry.Events.TrackerEvents
{
    public class SUnitDiedEvent : TrackerEvent
    {
        // Public properties to replace private fields
        public TrackerEvent TrackerEvent { get; }
        public int UnitTagIndex { get; }
        public int UnitTagRecycle { get; }
        public int? KillerPlayerId { get; }
        public int X { get; }
        public int Y { get; }
        public int? KillerUnitTagRecycle { get; }
        public int? KillerUnitTagIndex { get; }
        public int UnitIndex { get; internal set; }
        public SUnitBornEvent? KillerUnitBornEvent { get; internal set; }
        public SUnitInitEvent? KillerUnitInitEvent { get; internal set; }

        // Constructor to initialize the properties
        public SUnitDiedEvent(
            TrackerEvent trackerEvent,
            int unitTagIndex,
            int unitTagRecycle,
            int? killerPlayerId,
            int x,
            int y,
            int? killerUnitTagRecycle,
            int? killerUnitTagIndex) : base ( trackerEvent )
        {
            TrackerEvent = trackerEvent;
            UnitTagIndex = unitTagIndex;
            UnitTagRecycle = unitTagRecycle;
            KillerPlayerId = killerPlayerId;
            X = x;
            Y = y;
            KillerUnitTagRecycle = killerUnitTagRecycle;
            KillerUnitTagIndex = killerUnitTagIndex;
        }
    }
}