namespace s2ProtocolFurry.Events.TrackerEvents;

public class SPlayerSetupEvent
{
    public int Type { get; init; }
    public int? UserId { get; init; }
    public int SlotId { get; init; }
}