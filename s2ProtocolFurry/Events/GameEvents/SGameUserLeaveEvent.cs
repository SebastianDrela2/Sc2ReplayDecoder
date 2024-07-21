namespace s2ProtocolFurry.Events.GameEvents
{
    internal class SGameUserLeaveEvent : GameEvent
    {
        public SGameUserLeaveEvent(GameEvent gameEvent, int leaveReason) : base(gameEvent)
        {
            GameEvent = gameEvent;
            LeaveReason = leaveReason;
        }

        public GameEvent GameEvent { get; }
        public int LeaveReason { get; }
    }
}