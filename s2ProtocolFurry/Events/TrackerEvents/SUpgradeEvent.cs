namespace s2ProtocolFurry.Events.TrackerEvents
{
    public class SUpgradeEvent : TrackerEvent
    {
        // Public properties to replace private fields
        public TrackerEvent TrackerEvent { get; }
        public int Count { get; }
        public string UpgradeTypeName { get; }

        // Constructor to initialize the properties
        public SUpgradeEvent(TrackerEvent trackerEvent, int count, string upgradeTypeName) : base(trackerEvent)
        {
            TrackerEvent = trackerEvent;
            Count = count;
            UpgradeTypeName = upgradeTypeName;
        }
    }
}