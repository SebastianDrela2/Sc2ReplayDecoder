namespace s2ProtocolFurry.Events.GameEvents
{
    internal class UnknownGameEvent : GameEvent
    {
        public UnknownGameEvent(GameEvent gameEvent, string eventObj) : base(gameEvent)
        {
            GameEvent = gameEvent;
            Event = eventObj;
        }

        public GameEvent GameEvent { get; }
        public string Event { get; }
    }
}