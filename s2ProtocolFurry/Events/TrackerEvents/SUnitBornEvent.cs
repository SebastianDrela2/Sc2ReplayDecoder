namespace s2ProtocolFurry.Events.TrackerEvents;

public class SUnitBornEvent : TrackerEvent
{
    public int UnitTagIndex { get; init; }
    public int UnitTagRecycle { get; init; }
    public string? CreatorAbilityName { get; init; }
    public int? CreatorUnitTagRecycle { get; init; }
    public int ControlPlayerId { get; init; }
    public int X { get; init; }
    public int Y { get; init; }
    public int UpkeepPlayerId { get; init; }
    public string UnitTypeName { get; init; }
    public int? CreatorUnitTagIndex { get; init; }

    public int UnitIndex { get; internal set; }
    public SUnitDiedEvent? SUnitDiedEvent { get; internal set; }
}