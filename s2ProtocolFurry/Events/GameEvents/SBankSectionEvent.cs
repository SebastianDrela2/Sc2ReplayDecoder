namespace s2ProtocolFurry.Events.GameEvents
{
    internal class SBankSectionEvent : GameEvent
    {
        public SBankSectionEvent(GameEvent gameEvent, string name) : base(gameEvent)
        {
            GameEvent = gameEvent;
            Name = name;
        }

        public GameEvent GameEvent { get; }
        public string Name { get; }
    }
}