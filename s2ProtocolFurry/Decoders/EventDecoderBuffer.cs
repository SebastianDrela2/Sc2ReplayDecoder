using System.Buffers;
using System.Runtime.CompilerServices;
using s2ProtocolFurry.Events.TrackerEvents;

namespace s2ProtocolFurry.Decoder;

public sealed class EventDecoderBuffer
{
    public record struct UntypedKey(nuint TargetType, nuint Index);
    public static uint s_NextTyId;
    public struct BufferItem<T>()
    {
        public static readonly uint ElemTypeId = Interlocked.Increment(ref s_NextTyId);
        public RefList<T> Data = new();
        
        public UntypedKey Add(T value)
        {
            Data.Add(value, out var index);
            return new UntypedKey(ElemTypeId, index);
        }
    }
    public BufferItem<SPlayerSetupEvent> PlayerSetup = new();
    public BufferItem<SPlayerStatsEvent> PlayerStats = new();
    public BufferItem<SUnitBornEvent> UnitBorn = new();
    public BufferItem<SUnitDiedEvent> UnitDied = new();
    public BufferItem<SUnitOwnerChangeEvent> UnitOwnerChange = new();
    public BufferItem<SUnitPositionsEvent> UnitPositions = new();
    public BufferItem<SUnitTypeChangeEvent> UnitTypeChange = new();
    public BufferItem<SUpgradeEvent> Upgrade = new();
    public BufferItem<SUnitInitEvent> UnitInit = new();
    public BufferItem<SUnitDoneEvent> UnitDone = new();

    public EventDecoderBuffer()
    {
    }

    public UntypedKey Add(SPlayerSetupEvent value) => PlayerSetup.Add(value);
    public UntypedKey Add(SPlayerStatsEvent value) => PlayerStats.Add(value);
    public UntypedKey Add(SUnitBornEvent value) => UnitBorn.Add(value);
    public UntypedKey Add(SUnitDiedEvent value) => UnitDied.Add(value);
    public UntypedKey Add(SUnitOwnerChangeEvent value) => UnitOwnerChange.Add(value);
    public UntypedKey Add(SUnitPositionsEvent value) => UnitPositions.Add(value);
    public UntypedKey Add(SUnitTypeChangeEvent value) => UnitTypeChange.Add(value);
    public UntypedKey Add(SUpgradeEvent value) => Upgrade.Add(value);
    public UntypedKey Add(SUnitInitEvent value) => UnitInit.Add(value);
    public UntypedKey Add(SUnitDoneEvent value) => UnitDone.Add(value);
}
