namespace Sc2ReplayTests.Data;

public class TestExpectedData
{
    public s2ProtocolFurry.Events.TrackerEvents.SPlayerSetupEvent[] PlayerSetup;
    public s2ProtocolFurry.Events.TrackerEvents.SPlayerStatsEvent[] PlayerStats;
    public s2ProtocolFurry.Events.TrackerEvents.SUnitBornEvent[] UnitBorn;
    public s2ProtocolFurry.Events.TrackerEvents.SUnitDiedEvent[] UnitDied;
    public s2ProtocolFurry.Events.TrackerEvents.SUnitOwnerChangeEvent[] UnitOwnerChange;
    public s2ProtocolFurry.Events.TrackerEvents.SUnitPositionsEvent[] UnitPositions;
    public s2ProtocolFurry.Events.TrackerEvents.SUnitTypeChangeEvent[] UnitTypeChange;
    public s2ProtocolFurry.Events.TrackerEvents.SUpgradeEvent[] Upgrade;
    public s2ProtocolFurry.Events.TrackerEvents.SUnitInitEvent[] UnitInit;
    public s2ProtocolFurry.Events.TrackerEvents.SUnitDoneEvent[] UnitDone;

    public TestExpectedData(s2ProtocolFurry.Events.TrackerEvents.TrackerEvents data)
    {
        PlayerSetup = data.PlayerSetup.Span.ToArray();
        PlayerStats = data.PlayerStats.Span.ToArray();
        UnitBorn = data.UnitBorn.Span.ToArray();
        UnitDied = data.UnitDied.Span.ToArray();
        UnitOwnerChange = data.UnitOwnerChange.Span.ToArray();
        UnitPositions = data.UnitPositions.Span.ToArray();
        UnitTypeChange = data.UnitTypeChange.Span.ToArray();
        Upgrade = data.Upgrade.Span.ToArray();
        UnitInit = data.UnitInit.Span.ToArray();
        UnitDone = data.UnitDone.Span.ToArray();
    }
}
