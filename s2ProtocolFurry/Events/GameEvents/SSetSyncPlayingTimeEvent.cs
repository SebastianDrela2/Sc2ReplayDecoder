namespace s2ProtocolFurry.Events.GameEvents
{
    internal class SSetSyncPlayingTimeEvent
    {
        public SSetSyncPlayingTimeEvent(GameEvent gameEvent, int syncTime)
        {
            GameEvent = gameEvent;
            SyncTime = syncTime;
        }

        public GameEvent GameEvent { get; }
        public int SyncTime { get; }
    }
}