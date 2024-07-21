namespace s2ProtocolFurry.Events.GameEvents
{
    internal class SSelectionDeltaEvent : GameEvent
    {
        public SSelectionDeltaEvent(GameEvent gameEvent, SelectionDeltaEventDelta delta, int controlGroupId) : base(gameEvent)
        {
            GameEvent = gameEvent;
            Delta = delta;
            ControlGroupId = controlGroupId;
        }

        public GameEvent GameEvent { get; }
        public SelectionDeltaEventDelta Delta { get; }
        public int ControlGroupId { get; }
    }
}