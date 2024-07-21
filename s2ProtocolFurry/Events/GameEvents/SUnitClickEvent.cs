namespace s2ProtocolFurry.Events.GameEvents
{
    internal class SUnitClickEvent
    {
        public SUnitClickEvent(GameEvent gameEvent, int unitTag)
        {
            GameEvent = gameEvent;
            UnitTag = unitTag;
        }

        public GameEvent GameEvent { get; }
        public int UnitTag { get; }
    }
}