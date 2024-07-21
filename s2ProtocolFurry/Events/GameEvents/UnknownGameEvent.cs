namespace s2ProtocolFurry.Models.GameEvents
{
    internal class UnknownGameEvent
    {
        public UnknownGameEvent(GameEvent gameEvent, string eventObj)
        {
            GameEvent = gameEvent;
            Event = eventObj;
        }

        public GameEvent GameEvent { get; }
        public string Event { get; }
    }
}