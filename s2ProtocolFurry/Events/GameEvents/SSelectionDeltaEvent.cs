namespace s2ProtocolFurry.Events.GameEvents
{
    internal class SSelectionDeltaEvent
    {
        public SSelectionDeltaEvent(GameEvent gameEvent, SelectionDeltaEventDelta delta, int controlGroupId)
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