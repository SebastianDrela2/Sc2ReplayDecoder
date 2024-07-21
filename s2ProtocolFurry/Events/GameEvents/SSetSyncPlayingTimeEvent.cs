namespace s2ProtocolFurry.Events.GameEvents
{
    internal class SSetSyncPlayingTimeEvent : GameEvent
    {
        public SSetSyncPlayingTimeEvent(GameEvent gameEvent, int syncTime) : base(gameEvent)
        {
            GameEvent = gameEvent;
            SyncTime = syncTime;
        }

        public GameEvent GameEvent { get; }
        public int SyncTime { get; }
    }
}