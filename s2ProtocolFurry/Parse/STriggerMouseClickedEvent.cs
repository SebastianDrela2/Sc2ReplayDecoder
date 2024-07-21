using s2ProtocolFurry.Events.GameEvents;

namespace s2ProtocolFurry.Parse
{
    internal class STriggerMouseClickedEvent
    {
        public STriggerMouseClickedEvent(GameEvent gameEvent, bool down, int button, int flags, long posX, long posY)
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