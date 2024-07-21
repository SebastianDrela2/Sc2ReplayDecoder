namespace s2ProtocolFurry.Events.GameEvents
{
    internal class SCameraUpdateEvent : GameEvent
    {
        public SCameraUpdateEvent(GameEvent gameEvent, string? reason, int? distance, long? targetX, long? targetY, int? yaw, int? pitch, bool follow) : base(gameEvent)
        {
            GameEvent = gameEvent;
            Reason = reason;
            Distance = distance;
            TargetX = targetX;
            TargetY = targetY;
            Yaw = yaw;
            Pitch = pitch;
            Follow = follow;
        }

        public GameEvent GameEvent { get; }
        public string? Reason { get; }
        public int? Distance { get; }
        public long? TargetX { get; }
        public long? TargetY { get; }
        public int? Yaw { get; }
        public int? Pitch { get; }
        public bool Follow { get; }
    }
}