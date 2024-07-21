namespace s2ProtocolFurry.Events.GameEvents
{
    internal class SGameUserLeaveEvent
    {
        public SGameUserLeaveEvent(GameEvent gameEvent, int leaveReason)
        {
            GameEvent = gameEvent;
            LeaveReason = leaveReason;
        }

        public GameEvent GameEvent { get; }
        public int LeaveReason { get; }
    }
}