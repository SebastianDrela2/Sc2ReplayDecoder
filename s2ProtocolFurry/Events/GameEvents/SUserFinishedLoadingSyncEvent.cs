using s2ProtocolFurry.Models.GameEvents;

namespace s2ProtocolFurry.Parse
{
    public class SUserFinishedLoadingSyncEvent : GameEvent
    {
        public SUserFinishedLoadingSyncEvent(GameEvent gameEvent) : base(gameEvent)
        {
        }
    }
}