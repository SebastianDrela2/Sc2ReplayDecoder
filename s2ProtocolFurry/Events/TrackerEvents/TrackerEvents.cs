using System.Collections.Immutable;
using System.Numerics;

namespace s2ProtocolFurry.Events.TrackerEvents;

public struct RefList<T>()
{
    private T[] data = new T[64];
    public int size = 0;
	public readonly ArraySegment<T> Data => new(data, 0, size);
	public readonly Span<T> Span => data.AsSpan(0, size);
    public readonly int Length => size;
    public readonly int Capacity => data.Length;
    public readonly ref T this[int index] => ref data[index];
    public readonly Span<T> this[int begin, int end] => data.AsSpan(begin, end - begin);

    public ref T NextRef()
    {
        int i = size++;
        if (size > Capacity) {
            Array.Resize(ref data, Capacity << 1);
        }
        return ref data[i];
    }
    public void Add(T value) => NextRef() = value;
}

public class TrackerEvents()
{
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
}
