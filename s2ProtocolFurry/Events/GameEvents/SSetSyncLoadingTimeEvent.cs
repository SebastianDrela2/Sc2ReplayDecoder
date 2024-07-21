namespace s2ProtocolFurry.Events.GameEvents
{
    internal class SSetSyncLoadingTimeEvent
    {
        public SSetSyncLoadingTimeEvent(GameEvent gameEvent, int syncTime)
        {
            GameEvent = gameEvent;
            SyncTime = syncTime;
        }

        public GameEvent GameEvent { get; }
        public int SyncTime { get; }
    }
}