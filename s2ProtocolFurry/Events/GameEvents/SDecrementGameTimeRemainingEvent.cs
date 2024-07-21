namespace s2ProtocolFurry.Events.GameEvents
{
    internal class SDecrementGameTimeRemainingEvent
    {
        public SDecrementGameTimeRemainingEvent(GameEvent gameEvent, int decerementSeconds)
        {
            GameEvent = gameEvent;
            DecerementSeconds = decerementSeconds;
        }

        public GameEvent GameEvent { get; }
        public int DecerementSeconds { get; }
    }
}