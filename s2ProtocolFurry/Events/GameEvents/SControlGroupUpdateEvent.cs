namespace s2ProtocolFurry.Events.GameEvents
{
    internal class SControlGroupUpdateEvent : GameEvent
    {
        public SControlGroupUpdateEvent(GameEvent gameEvent, int controlGroupUpdate) : base(gameEvent)
        {
            GameEvent = gameEvent;
            ControlGroupUpdate = controlGroupUpdate;
        }

        public GameEvent GameEvent { get; }
        public int ControlGroupUpdate { get; }
    }
}