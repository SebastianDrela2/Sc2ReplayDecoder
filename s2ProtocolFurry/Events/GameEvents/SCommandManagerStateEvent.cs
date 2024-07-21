namespace s2ProtocolFurry.Events.GameEvents
{
    internal class SCommandManagerStateEvent : GameEvent
    {
        public SCommandManagerStateEvent(GameEvent gameEvent, int state, int? sequence) : base(gameEvent)
        {
            GameEvent = gameEvent;
            State = state;
            Sequence = sequence;
        }

        public GameEvent GameEvent { get; }
        public int State { get; }
        public int? Sequence { get; }
    }
}