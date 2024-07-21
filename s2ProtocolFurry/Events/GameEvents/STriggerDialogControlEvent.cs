namespace s2ProtocolFurry.Events.GameEvents
{
    internal class STriggerDialogControlEvent
    {
        public STriggerDialogControlEvent(GameEvent gameEvent, long controlId, int? mouseButton, string? textChanged, long eventType)
        {
            GameEvent = gameEvent;
            ControlId = controlId;
            MouseButton = mouseButton;
            TextChanged = textChanged;
            EventType = eventType;
        }

        public GameEvent GameEvent { get; }
        public long ControlId { get; }
        public int? MouseButton { get; }
        public string? TextChanged { get; }
        public long EventType { get; }
    }
}