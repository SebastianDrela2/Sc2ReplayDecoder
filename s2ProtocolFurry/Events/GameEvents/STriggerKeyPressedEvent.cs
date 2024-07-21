namespace s2ProtocolFurry.Events.GameEvents
{
    internal class STriggerKeyPressedEvent : GameEvent
    {
        public STriggerKeyPressedEvent(GameEvent gameEvent, int flags, int key) : base(gameEvent)
        {
            GameEvent = gameEvent;
            Flags = flags;
            Key = key;
        }

        public GameEvent GameEvent { get; }
        public int Flags { get; }
        public int Key { get; }
    }
}