namespace s2ProtocolFurry.Events.GameEvents
{
    internal class SCmdEvent
    {
        public SCmdEvent(GameEvent gameEvent, int? unitGroup, int abilLink, int abilCmdIndex, string? abilCmdData, long? targetX, long? targetY, long? targetZ, int cmdFalgs, int sequence, int? otherUnit)
        {
            GameEvent = gameEvent;
            UnitGroup = unitGroup;
            AbilLink = abilLink;
            AbilCmdIndex = abilCmdIndex;
            AbilCmdData = abilCmdData;
            TargetX = targetX;
            TargetY = targetY;
            TargetZ = targetZ;
            CmdFalgs = cmdFalgs;
            Sequence = sequence;
            OtherUnit = otherUnit;
        }

        public GameEvent GameEvent { get; }
        public int? UnitGroup { get; }
        public int AbilLink { get; }
        public int AbilCmdIndex { get; }
        public string? AbilCmdData { get; }
        public long? TargetX { get; }
        public long? TargetY { get; }
        public long? TargetZ { get; }
        public int CmdFalgs { get; }
        public int Sequence { get; }
        public int? OtherUnit { get; }
    }
}