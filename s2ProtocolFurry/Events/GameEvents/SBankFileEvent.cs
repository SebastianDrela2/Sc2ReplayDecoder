namespace s2ProtocolFurry.Events.GameEvents
{
    internal class SBankFileEvent : GameEvent
    {
        public SBankFileEvent(GameEvent gameEvent, string name) : base(gameEvent)
        {
            GameEvent = gameEvent;
            Name = name;
        }

        public GameEvent GameEvent { get; }
        public string Name { get; }
    }
}