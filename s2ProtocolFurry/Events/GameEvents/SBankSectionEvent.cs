namespace s2ProtocolFurry.Events.GameEvents
{
    internal class SBankSectionEvent
    {
        public SBankSectionEvent(GameEvent gameEvent, string name)
        {
            GameEvent = gameEvent;
            Name = name;
        }

        public GameEvent GameEvent { get; }
        public string Name { get; }
    }
}