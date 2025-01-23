using System.Buffers;
using System.Buffers.Binary;
using System.Collections.Immutable;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using s2ProtocolFurry.Decoder;

namespace s2ProtocolFurry.Events.TrackerEvents;

public class TrackerEvents()
{
    public EventDecoderBuffer.UntypedKey[] Data;
    public RefList<SPlayerSetupEvent> SPlayerSetupEvents = new ();
    public RefList<SPlayerStatsEvent> SPlayerStatsEvents = new ();
    public RefList<SUnitBornEvent> SUnitBornEvents = new ();
    public RefList<SUnitDiedEvent> SUnitDiedEvents = new ();
    public RefList<SUnitOwnerChangeEvent> SUnitOwnerChangeEvents = new ();
    public RefList<SUnitPositionsEvent> SUnitPositionsEvents = new ();
    public RefList<SUnitTypeChangeEvent> SUnitTypeChangeEvents = new ();
    public RefList<SUpgradeEvent> SUpgradeEvents = new ();
    public RefList<SUnitInitEvent> SUnitInitEvents = new ();
    public RefList<SUnitDoneEvent> SUnitDoneEvents = new ();
    public SortKeys Keys;

    public void BuildIndexes()
    {
        Keys.SUnitBornEvents_UnitIndex = [.. SUnitBornEvents.Data.Select(x => x.UnitIndex)];
        Keys.SUnitDiedEvents_UnitIndex = [.. SUnitDiedEvents.Data.Select(x => x.UnitIndex)];
        Keys.SUnitInitEvents_UnitIndex = [.. SUnitInitEvents.Data.Select(x => x.UnitIndex)];
        Keys.SUnitDoneEvents_UnitIndex = [.. SUnitDoneEvents.Data.Select(x => x.UnitIndex)];

        Keys.SUnitBornEvents_UnitIndex.AsSpan().Sort(SUnitDoneEvents.Span);
        Keys.SUnitDiedEvents_UnitIndex.AsSpan().Sort(SUnitDiedEvents.Span);
        Keys.SUnitInitEvents_UnitIndex.AsSpan().Sort(SUnitInitEvents.Span);
        Keys.SUnitDoneEvents_UnitIndex.AsSpan().Sort(SUnitDoneEvents.Span);
    }
    public struct SortKeys
    {
        public int[] SUnitBornEvents_UnitIndex;
        public int[] SUnitDiedEvents_UnitIndex;
        public int[] SUnitInitEvents_UnitIndex;
        public int[] SUnitDoneEvents_UnitIndex;
    }
}
