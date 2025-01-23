using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace s2ProtocolFurry.Collections;

public static class RefList
{
    // public static void ForEach<T>(this IEnumerable<T> items, Action<T> action)
    // {

    // }
    public static void ForEach<T>(this RefList<T> items, Action<T> action)
    {
        foreach (var item in items.Span) action(item);
    }
}
public struct RefList<T>()
{
    public static readonly int InitialCapacity = 1 << (12 - int.Log2(Unsafe.SizeOf<T>()));
    
    private T[] data = new T[InitialCapacity];
    private nuint used_size = 0;

    public readonly nuint Length_u => used_size;
    public readonly uint Length_u32 => unchecked((uint)used_size);
    public readonly int Length => unchecked((int)(uint)used_size);
    public readonly int Capacity => data.Length;
    public readonly int Available => Capacity - Length;

	public readonly ArraySegment<T> Data => new(data, 0, Length);
	public readonly Span<T> Span => data.AsSpan(0, Length);
    public readonly Memory<T> Memory => data.AsMemory(0, Length);

    public readonly ref T this[int index] => ref data[index];
    public readonly Span<T> this[int begin, int end] => data.AsSpan(begin, end - begin);

    public ref T NextRef(out nuint index)
    {
        index = Length_u;
        ++used_size;
        if (Length > Capacity) {
            Array.Resize(ref data, Capacity << 1);
        }
        return ref data[index];
    }
    public void Add(T value) => NextRef(out _) = value;
    public void Add(T value, out nuint index) => NextRef(out index) = value;
    public readonly ref T GetReference() => ref MemoryMarshal.GetArrayDataReference(data);

    public interface IStaticSelector<TValue>
    {
        public abstract static ref TValue Select(ref T src);
    }

    public readonly Span<T>.Enumerator GetEnumerator() => Span.GetEnumerator();
}
