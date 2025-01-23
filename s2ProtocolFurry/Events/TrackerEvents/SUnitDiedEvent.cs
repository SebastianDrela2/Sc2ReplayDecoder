namespace s2ProtocolFurry.Events.TrackerEvents;

public class SUnitDiedEvent : TrackerEvent
{
    public int UnitTagIndex { get; init; }
    public int UnitTagRecycle { get; init; }
    public int? KillerPlayerId { get; init; }
    public int X { get; init; }
    public int Y { get; init; }
    public int? KillerUnitTagRecycle { get; init; }
    public int? KillerUnitTagIndex { get; init; }

    public int UnitIndex { get; internal set; }
    public SUnitBornEvent? KillerUnitBornEvent { get; internal set; }
    public SUnitInitEvent? KillerUnitInitEvent { get; internal set; }
}