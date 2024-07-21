namespace s2ProtocolFurry.Events.TrackerEvents
{
    public class SUnitBornEvent : TrackerEvent
    {
        public TrackerEvent TrackerEvent { get; }
        public int UnitTagIndex { get; }
        public int UnitTagRecycle { get; }
        public string? CreatorAbilityName { get; }
        public int? CreatorUnitTagRecycle { get; }
        public int ControlPlayerId { get; }
        public int X { get; }
        public int Y { get; }
        public int UpkeepPlayerId { get; }
        public string UnitTypeName { get; }
        public int? CreatorUnitTagIndex { get; }
        public int UnitIndex { get; internal set; }
        public SUnitDiedEvent? SUnitDiedEvent { get; internal set; }

        public SUnitBornEvent(
            TrackerEvent trackerEvent,
            int unitTagIndex,
            int unitTagRecycle,
            string? creatorAbilityName,
            int? creatorUnitTagRecycle,
            int controlPlayerId,
            int x,
            int y,
            int upkeepPlayerId,
            string unitTypeName,
            int? creatorUnitTagIndex) : base (trackerEvent)
        {
            TrackerEvent = trackerEvent;
            UnitTagIndex = unitTagIndex;
            UnitTagRecycle = unitTagRecycle;
            CreatorAbilityName = creatorAbilityName;
            CreatorUnitTagRecycle = creatorUnitTagRecycle;
            ControlPlayerId = controlPlayerId;
            X = x;
            Y = y;
            UpkeepPlayerId = upkeepPlayerId;
            UnitTypeName = unitTypeName;
            CreatorUnitTagIndex = creatorUnitTagIndex;
        }
    }
}