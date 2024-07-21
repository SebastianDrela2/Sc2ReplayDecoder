namespace s2ProtocolFurry.Events.GameEvents
{
    internal class SCommandManagerStateEvent
    {
        public SCommandManagerStateEvent(GameEvent gameEvent, int state, int? sequence)
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