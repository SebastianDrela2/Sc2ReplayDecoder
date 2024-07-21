namespace s2ProtocolFurry.Events.TrackerEvents;

public class TrackerEvents
{
    public TrackerEvents(SPlayerSetupEvent[] sPlayerSetupEvents, SPlayerStatsEvent[] sPlayerStatsEvents, SUnitBornEvent[] sUnitBornEvents, SUnitDiedEvent[] sUnitDiedEvents, SUnitOwnerChangeEvent[] sUnitOwnerChangeEvents, SUnitPositionsEvent[] sUnitPositionsEvents, SUnitTypeChangeEvent[] sUnitTypeChangeEvents, SUpgradeEvent[] sUpgradeEvents, SUnitInitEvent[] sUnitInitEvents, SUnitDoneEvent[] sUnitDoneEvents)
    {
        SPlayerSetupEvents = sPlayerSetupEvents;
        SPlayerStatsEvents = sPlayerStatsEvents;
        SUnitBornEvents = sUnitBornEvents;
        SUnitDiedEvents = sUnitDiedEvents;
        SUnitOwnerChangeEvents = sUnitOwnerChangeEvents;
        SUnitPositionsEvents = sUnitPositionsEvents;
        SUnitTypeChangeEvents = sUnitTypeChangeEvents;
        SUpgradeEvents = sUpgradeEvents;
        SUnitInitEvents = sUnitInitEvents;
        SUnitDoneEvents = sUnitDoneEvents;
    }

    public SPlayerSetupEvent[] SPlayerSetupEvents { get; }
    public SPlayerStatsEvent[] SPlayerStatsEvents { get; }
    public SUnitBornEvent[] SUnitBornEvents { get; }
    public SUnitDiedEvent[] SUnitDiedEvents { get; }
    public SUnitOwnerChangeEvent[] SUnitOwnerChangeEvents { get; }
    public SUnitPositionsEvent[] SUnitPositionsEvents { get; }
    public SUnitTypeChangeEvent[] SUnitTypeChangeEvents { get; }
    public SUpgradeEvent[] SUpgradeEvents { get; }
    public SUnitInitEvent[] SUnitInitEvents { get; }
    public SUnitDoneEvent[] SUnitDoneEvents { get; }
}
