namespace s2ProtocolFurry.Events.TrackerEvents;

public class SUnitInitEvent
{
    public int UnitTagIndex { get; init; }
    public int UnitTagRecycle { get; init; }
    public int ControlPlayerId { get; init; }
    public int X { get; init; }
    public int Y { get; init; }
    public int UpkeepPlayerId { get; init; }
    public string UnitTypeName { get; init; }

    public int UnitIndex { get; internal set; }
    public SUnitDiedEvent? SUnitDiedEvent { get; internal set; }
    public SUnitDoneEvent? SUnitDoneEvent { get; internal set; }
}