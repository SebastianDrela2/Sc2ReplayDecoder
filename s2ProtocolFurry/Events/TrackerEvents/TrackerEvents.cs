using System.Collections.Immutable;

namespace s2ProtocolFurry.Events.TrackerEvents;

public class TrackerEvents
{
    public TrackerEvents(List<TrackerEvent> trackerevents)
    {
        SPlayerSetupEvents = trackerevents.OfType<SPlayerSetupEvent>().ToArray();
        SPlayerStatsEvents = trackerevents.OfType<SPlayerStatsEvent>().ToArray();
        SUnitBornEvents = trackerevents.OfType<SUnitBornEvent>().ToArray();
        
        SUnitDiedEvents = trackerevents.OfType<SUnitDiedEvent>().ToImmutableSortedDictionary(x => x.UnitIndex, x => x);
        
        SUnitOwnerChangeEvents = trackerevents.OfType<SUnitOwnerChangeEvent>().ToArray();
        SUnitPositionsEvents = trackerevents.OfType<SUnitPositionsEvent>().ToArray();
        SUnitTypeChangeEvents = trackerevents.OfType<SUnitTypeChangeEvent>().ToArray();
        SUpgradeEvents = trackerevents.OfType<SUpgradeEvent>().ToArray();
        SUnitInitEvents = trackerevents.OfType<SUnitInitEvent>().ToArray();

        SUnitDoneEvents = trackerevents.OfType<SUnitDoneEvent>().ToImmutableSortedDictionary(x => x.UnitIndex, x => x);
    }

    public SPlayerSetupEvent[] SPlayerSetupEvents { get; }
    public SPlayerStatsEvent[] SPlayerStatsEvents { get; }
    public SUnitBornEvent[] SUnitBornEvents { get; }
    public ImmutableSortedDictionary<int, SUnitDiedEvent> SUnitDiedEvents { get; }
    public SUnitOwnerChangeEvent[] SUnitOwnerChangeEvents { get; }
    public SUnitPositionsEvent[] SUnitPositionsEvents { get; }
    public SUnitTypeChangeEvent[] SUnitTypeChangeEvents { get; }
    public SUpgradeEvent[] SUpgradeEvents { get; }
    public SUnitInitEvent[] SUnitInitEvents { get; }
    public ImmutableSortedDictionary<int, SUnitDoneEvent> SUnitDoneEvents { get; }
}
