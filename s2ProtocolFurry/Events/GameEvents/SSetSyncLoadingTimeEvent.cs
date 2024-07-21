namespace s2ProtocolFurry.Events.GameEvents
{
    internal class SSetSyncLoadingTimeEvent : GameEvent
    {
        public SSetSyncLoadingTimeEvent(GameEvent gameEvent, int syncTime) : base(gameEvent)
        {
            GameEvent = gameEvent;
            SyncTime = syncTime;
        }

        public GameEvent GameEvent { get; }
        public int SyncTime { get; }
    }
}