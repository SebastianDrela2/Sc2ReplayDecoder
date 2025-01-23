namespace s2ProtocolFurry.Events.TrackerEvents;

public class SUnitTypeChangeEvent : TrackerEvent
{
    public int UnitTagIndex { get; init; }
    public int UnitTagRecycle { get; init; }
    public string UnitTypeName { get; init; }
}