namespace s2ProtocolFurry.Events.TrackerEvents;

public class SUnitOwnerChangeEvent
{
    public int UnitTagIndex { get; init; }
    public int UnitTagRecycle { get; init; }
    public int ControlPlayerId { get; init; }
    public int UpkeepPlayerId { get; init; }
    
    public int UnitIndex { get; internal set; }
}