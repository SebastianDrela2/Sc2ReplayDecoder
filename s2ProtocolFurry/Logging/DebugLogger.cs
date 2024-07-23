using System.Text;

namespace s2ProtocolFurry.Logging;

public class DebugLogger
{
    public static int Counter = 0;

    private static int BumpCounter() => Counter++;

    private readonly StringBuilder _debugOutput = new();
    public int Indent = 0;
    public bool RequireLineProlog = true;
    public string DebugOutput => _debugOutput.ToString();

    private void WriteLineProlog()
    {
        _debugOutput.Append($"[{BumpCounter(),4}] ");
        WriteIndent();
    }
    private void WriteIndent() => _debugOutput.Append(' ', Indent * 2);
    private void EnsureLineProlog()
    {
        if (!RequireLineProlog) return;
        WriteLineProlog();
        RequireLineProlog = false;
    }

    public void Append(ReadOnlySpan<char> value)
    {
        var en = value.EnumerateLines().GetEnumerator();
        if (!en.MoveNext()) return;
        ReadOnlySpan<char> current = en.Current;
        bool hasNext = en.MoveNext();

        if (!hasNext)
        {
            if (current.IsEmpty) return;
            EnsureLineProlog();
            _debugOutput.Append(current);
            return;
        }

        EnsureLineProlog();
        goto L_FirstLine;

    L_NextLine:
        current = en.Current;
        if (!en.MoveNext()) goto L_LastLine;

        WriteLineProlog();

    L_FirstLine:
        _debugOutput.Append(current);
        _debugOutput.Append('\n');
        goto L_NextLine;

    L_LastLine:
        if (current.IsEmpty)
        {
            RequireLineProlog = true;
            return;
        }
        WriteLineProlog();
        _debugOutput.Append(current);
    }

    public void AppendLine() => Append("\n");
    public void AppendLine(ReadOnlySpan<char> value) => Append($"{value}\n");

    public IndentGuard PushIndent()
    {
        ++Indent;
        return new(this);
    }

    public readonly ref struct IndentGuard(DebugLogger self)
    {
        public readonly void Dispose() => --self.Indent;
    }
    public void Indented(Action action)
    {
        ++Indent;
        try
        {
            action();
        }
        finally
        {
            --Indent;
        }
    }
    public T Indented<T>(Func<T> action)
    {
        ++Indent;
        try
        {
            return action();
        }
        finally
        {
            --Indent;
        }
    }

    public override string ToString() => _debugOutput.ToString();
}
