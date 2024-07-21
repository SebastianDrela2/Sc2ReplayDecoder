namespace s2ProtocolFurry.Events.GameEvents
{
    internal class SControlGroupUpdateEvent
    {
        public SControlGroupUpdateEvent(GameEvent gameEvent, int controlGroupUpdate)
        {
            GameEvent = gameEvent;
            ControlGroupUpdate = controlGroupUpdate;
        }

        public GameEvent GameEvent { get; }
        public int ControlGroupUpdate { get; }
    }
}