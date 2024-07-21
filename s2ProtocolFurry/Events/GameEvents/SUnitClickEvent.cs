namespace s2ProtocolFurry.Events.GameEvents
{
    internal class SUnitClickEvent : GameEvent
    {
        public SUnitClickEvent(GameEvent gameEvent, int unitTag) : base(gameEvent)
        {
            GameEvent = gameEvent;
            UnitTag = unitTag;
        }

        public GameEvent GameEvent { get; }
        public int UnitTag { get; }
    }
}