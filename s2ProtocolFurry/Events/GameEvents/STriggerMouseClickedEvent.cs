namespace s2ProtocolFurry.Events.GameEvents
{
    public class STriggerMouseClickedEvent : GameEvent
    {
        public STriggerMouseClickedEvent(GameEvent gameEvent, bool down, int button, int flags, long posX, long posY) : base(gameEvent)
        {
            GameEvent = gameEvent;
            Down = down;
            Button = button;
            Flags = flags;
            PosX = posX;
            PosY = posY;
        }

        public GameEvent GameEvent { get; }
        public bool Down { get; }
        public int Button { get; }
        public int Flags { get; }
        public long PosX { get; }
        public long PosY { get; }
    }
}