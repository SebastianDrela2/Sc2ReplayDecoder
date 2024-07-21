namespace s2ProtocolFurry.Events.GameEvents
{
    internal class STriggerPingEvent
    {
        public STriggerPingEvent(GameEvent gameEvent, bool pingedMinimap, int unitLink, bool unitIsUnderConstruction, long option, int unit, long unitX, long unitY, long unitZ, int? unitControlPlayerId, long pointX, long pointY, int? unitUpkeepPlayerId)
        {
            GameEvent = gameEvent;
            PingedMinimap = pingedMinimap;
            UnitLink = unitLink;
            UnitIsUnderConstruction = unitIsUnderConstruction;
            Option = option;
            Unit = unit;
            UnitX = unitX;
            UnitY = unitY;
            UnitZ = unitZ;
            UnitControlPlayerId = unitControlPlayerId;
            PointX = pointX;
            PointY = pointY;
            UnitUpkeepPlayerId = unitUpkeepPlayerId;
        }

        public GameEvent GameEvent { get; }
        public bool PingedMinimap { get; }
        public int UnitLink { get; }
        public bool UnitIsUnderConstruction { get; }
        public long Option { get; }
        public int Unit { get; }
        public long UnitX { get; }
        public long UnitY { get; }
        public long UnitZ { get; }
        public int? UnitControlPlayerId { get; }
        public long PointX { get; }
        public long PointY { get; }
        public int? UnitUpkeepPlayerId { get; }
    }
}