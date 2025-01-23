namespace s2ProtocolFurry.Events.TrackerEvents;

public class SUnitDoneEvent
{
    public int UnitTagIndex { get; init; }
    public int UnitTagRecycle { get; init; }
    public int UnitIndex { get; internal set; }
}