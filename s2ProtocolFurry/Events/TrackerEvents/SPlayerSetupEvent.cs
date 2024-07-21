namespace s2ProtocolFurry.Events.TrackerEvents
{
    public class SPlayerSetupEvent : TrackerEvent
    {
        public SPlayerSetupEvent(TrackerEvent trackerEvent, int type, int? userId, int slotId) : base(trackerEvent)
        {
            TrackerEvent = trackerEvent;
            Type = type;
            UserId = userId;
            SlotId = slotId;
        }

        public TrackerEvent TrackerEvent { get; }
        public int Type { get; }
        public int? UserId { get; }
        public int SlotId { get; }
    }
}