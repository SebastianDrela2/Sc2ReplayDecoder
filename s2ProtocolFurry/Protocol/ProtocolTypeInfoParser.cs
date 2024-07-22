using System.Reflection;
using System.Text.RegularExpressions;

namespace s2ProtocolFurry.Protocol;

public class ProtocolTypeInfoParser2
{
    private const string StartString = "# Decoding instructions for each protocol type.";

    public static Int128 ParseIntegerNumber(ref ReadOnlySpan<char> input)
    {
        bool isNegative = TryExactChar(ref input, '-');
        Int128 num = ParseDigits(ref input);
        return isNegative ? -num : num;
    }
    public static ReadOnlySpan<char> ParsePythonString(ref ReadOnlySpan<char> input)
    {
        ExactChar(ref input, '\'');
        SplitOnce(ref input, '\'', out var value);
        return value;
    }
    public static Int128 ParseDigits(ref ReadOnlySpan<char> input)
    {
        int foundAt = input.IndexOfAnyExceptInRange('0', '9');
        Int128 value;
        if (foundAt < 0)
        {
            value = Int128.Parse(input);
            input = [];
        }
        else
        {
            value = Int128.Parse(input[..foundAt]);
            input = input[foundAt..];
        }

        return value;
    }
    public static void SplitOnce(ref ReadOnlySpan<char> input, char value, out ReadOnlySpan<char> output)
    {
        int foundAt = input.IndexOf(value);
        if (foundAt < 0)
        {
            output = input;
            input = [];
        }
        else
        {
            output = input[..foundAt];
            input = input[(foundAt + 1)..];
        }
    }
    public static void SkipToNextLine(ref ReadOnlySpan<char> input)
    {
        int foundAt = input.IndexOf('\n');
        if (foundAt < 0) input = ReadOnlySpan<char>.Empty;
        else input = input[(foundAt + 1)..];
    }
    public static void SkipWhiteSpaces(ref ReadOnlySpan<char> input)
    {
        input = input.TrimStart();
    }
    public static void ExpectSequence(ref ReadOnlySpan<char> input, ReadOnlySpan<char> value)
    {
        if (!input.StartsWith(value)) throw new InvalidOperationException();
        input = input[value.Length..];
    }
    public static void ExpectNewLine(ref ReadOnlySpan<char> input)
    {
        input = input switch
        {
            ['\r', '\n', .. var rest] => rest,
            ['\r', .. var rest] => rest,
            ['\n', .. var rest] => rest,
            _ => throw new InvalidOperationException(),
        };
    }
    public static bool TryExactChar(ref ReadOnlySpan<char> input, char expected)
    {
        if (input is not [ var c, .. var rest]) return false;
        if (c != expected) return false;

        input = rest;
        return true;
    }
    public static void ExactChar(ref ReadOnlySpan<char> input, char expected)
    {
        if (!TryExactChar(ref input, expected)) throw new InvalidOperationException();
    }
    public static List<ProtocolTypeInfo> Parse(ReadOnlySpan<char> input)
    {
        List<ProtocolTypeInfo> output = new();

        // Skup until comment "# Decoding instructions..."
        int foundAt = input.IndexOf(StartString);
        if (foundAt < 0) throw new InvalidOperationException();
        input = input[foundAt..];

        SkipToNextLine(ref input);
        SkipWhiteSpaces(ref input);
        ExpectSequence(ref input, @"typeinfos = [");
        ExpectNewLine(ref input);

        for (int i = 0; ; ++i)
        {
            SkipWhiteSpaces(ref input);
            if (TryExactChar(ref input, ']')) break;

            ExactChar(ref input, '(');

            var key = ParsePythonString(ref input);

            ExactChar(ref input, ',');
            var item = key switch
            {
                "_int" => Parse_int(ref input),
                "_choice" => Parse_choice(ref input),
                "_struct" => Parse_struct(ref input),
                "_blob" => Parse_blob(ref input),
                "_array" => Parse_array(ref input),
                "_optional" => Parse_optional(ref input),
                "_bool" => Parse_bool(ref input),
                "_fourcc" => Parse_fourcc(ref input),
                "_bitarray" => Parse_bitarray(ref input),
                "_null" => Parse_null(ref input),
                _ => throw new InvalidOperationException(),
            };
            output.Add(item);

            ExactChar(ref input, ')');
            ExactChar(ref input, ',');
            SkipWhiteSpaces(ref input);
            ExactChar(ref input, '#');

            var idx = ParseDigits(ref input);
            if (idx != i) throw new InvalidOperationException();

            SkipToNextLine(ref input);
        }

        return output;
    }
    public static ProtocolTypeInfo Parse_bool(ref ReadOnlySpan<char> input)
    {
        ExactChar(ref input, '[');
        ExactChar(ref input, ']');
        return ProtocolTypeInfo.MakeBool();
    }
    public static ProtocolTypeInfo Parse_null(ref ReadOnlySpan<char> input)
    {
        ExactChar(ref input, '[');
        ExactChar(ref input, ']');
        return ProtocolTypeInfo.MakeNull();
    }
    public static ProtocolTypeInfo Parse_fourcc(ref ReadOnlySpan<char> input)
    {
        ExactChar(ref input, '[');
        ExactChar(ref input, ']');
        return ProtocolTypeInfo.MakeFourcc();
    }
    public static ProtocolTypeInfo Parse_int(ref ReadOnlySpan<char> input)
    {
        ExactChar(ref input, '[');
        
        ExactChar(ref input, '(');
        var arg1 = ParseIntegerNumber(ref input);
        ExactChar(ref input, ',');
        var arg2 = ParseIntegerNumber(ref input);
        ExactChar(ref input, ')');

        ExactChar(ref input, ']');

        return ProtocolTypeInfo.MakeInt(arg1, arg2);
    }
    public static ProtocolTypeInfo Parse_blob(ref ReadOnlySpan<char> input)
    {
        ExactChar(ref input, '[');

        ExactChar(ref input, '(');
        var arg1 = ParseIntegerNumber(ref input);
        ExactChar(ref input, ',');
        var arg2 = ParseIntegerNumber(ref input);
        ExactChar(ref input, ')');

        ExactChar(ref input, ']');

        return ProtocolTypeInfo.MakeBlob(arg1, arg2);
    }
    public static ProtocolTypeInfo Parse_bitarray(ref ReadOnlySpan<char> input)
    {
        ExactChar(ref input, '[');

        ExactChar(ref input, '(');
        var arg1 = ParseIntegerNumber(ref input);
        ExactChar(ref input, ',');
        var arg2 = ParseIntegerNumber(ref input);
        ExactChar(ref input, ')');

        ExactChar(ref input, ']');

        return ProtocolTypeInfo.MakeBitArray(arg1, arg2);
    }
    public static ProtocolTypeInfo Parse_array(ref ReadOnlySpan<char> input)
    {
        ExactChar(ref input, '[');

        ExactChar(ref input, '(');
        var arg1 = ParseIntegerNumber(ref input);
        ExactChar(ref input, ',');
        var arg2 = ParseIntegerNumber(ref input);
        ExactChar(ref input, ')');

        ExactChar(ref input, ',');
        var arg3 = ParseIntegerNumber(ref input);

        ExactChar(ref input, ']');

        return ProtocolTypeInfo.MakeArray(arg1, arg2, arg2);
    }
    public static ProtocolTypeInfo Parse_optional(ref ReadOnlySpan<char> input)
    {
        ExactChar(ref input, '[');
        
        var arg1 = ParseIntegerNumber(ref input);

        ExactChar(ref input, ']');

        return ProtocolTypeInfo.MakeOptional(arg1);
    }
    public static ProtocolTypeInfo Parse_choice(ref ReadOnlySpan<char> input)
    {
        ExactChar(ref input, '[');

        ExactChar(ref input, '(');
        var arg1 = ParseIntegerNumber(ref input);
        ExactChar(ref input, ',');
        var arg2 = ParseIntegerNumber(ref input);
        ExactChar(ref input, ')');

        ExactChar(ref input, ',');

        List<(string Arg1, Int128 Arg2)> arg3 = new();

        ExactChar(ref input, '{');
        if (!TryExactChar(ref input, '}'))
        {
            for (int i = 0; ; i++)
            {
                var keyIndex = ParseDigits(ref input);
                if (keyIndex != i) throw new InvalidOperationException();
                ExactChar(ref input, ':');

                ExactChar(ref input, '(');
                var choiceArg1 = ParsePythonString(ref input);
                ExactChar(ref input, ',');
                var choiceArg2 = ParseIntegerNumber(ref input);
                ExactChar(ref input, ')');

                // TODO: ToString :/
                arg3.Add((choiceArg1.ToString(), choiceArg2));

                if (!TryExactChar(ref input, ',')) break;
            }
            ExactChar(ref input, '}');
        }

        ExactChar(ref input, ']');

        return ProtocolTypeInfo.MakeChoice(arg1, arg2, arg3);
    }
    public static ProtocolTypeInfo Parse_struct(ref ReadOnlySpan<char> input)
    {
        ExactChar(ref input, '[');

        List<(string Arg1, Int128 Arg2, Int128 Arg3)> arg1 = new();

        ExactChar(ref input, '[');
        if (!TryExactChar(ref input, ']'))
        {
            for (int i = 0; ; i++)
            {
                ExactChar(ref input, '(');
                var fieldName = ParsePythonString(ref input);
                ExactChar(ref input, ',');
                var fieldArg2 = ParseIntegerNumber(ref input);
                ExactChar(ref input, ',');
                var fieldArg3 = ParseIntegerNumber(ref input);
                ExactChar(ref input, ')');

                // TODO: ToString :/
                arg1.Add((fieldName.ToString(), fieldArg2, fieldArg3));

                if (!TryExactChar(ref input, ',')) break;
            }
            ExactChar(ref input, ']');
        }

        ExactChar(ref input, ']');

        return ProtocolTypeInfo.MakeStruct(arg1);
    }
}

public class ProtocolTypeInfoParser
{
    public static readonly Regex s_Regex = new (@"\('(?<type>_int|_choice|_struct|_blob|_array|_optional|_bool|_fourcc|_bitarray|_null)',\[(?<args>.*?)\]\)(,?\s*#\d+)?", RegexOptions.Compiled);

    public List<ProtocolTypeInfo> ParseProtocolTypes(List<string> lines)
    {
        var protocolTypes = new List<ProtocolTypeInfo>();

        foreach (var line in lines)
        {
            if (s_Regex.Matches(line) is not [{ Success: true } match]) throw new InvalidOperationException();

            var type = match.Groups["type"].Value;
            var args = match.Groups["args"].Value;
            var arguments = ParseArguments(args);
            protocolTypes.Add(new ProtocolTypeInfo(type, arguments));
        }

        return protocolTypes;
    }

    private object[] ParseArguments(string args)
    {
        var arguments = new List<object>();
        
        var matches = Regex.Matches(args, @"(?<arg>\{[^{}]*\}|\[[^\[\]]*\]|-?\d+|'.*?'|true|false|null)");

        foreach (Match match in matches)
        {
            var arg = match.Groups["arg"].Value;
            if (arg.StartsWith("{"))
            {
                arguments.Add(ParseComplexArgument(arg));
            }
            else if (arg.StartsWith("["))
            {
                arguments.Add(ParseArguments(arg.Trim('[', ']')));
            }
            else if (arg.StartsWith("'") && arg.EndsWith("'"))
            {
                arguments.Add(arg.Trim('\''));
            }
            else if (bool.TryParse(arg, out bool boolArg))
            {
                arguments.Add(boolArg);
            }
            else if (int.TryParse(arg, out int intArg))
            {
                arguments.Add(intArg);
            }
            else if (arg == "null")
            {
                arguments.Add(null);
            }
            else
            {
                //throw new InvalidOperationException();
            }
        }

        return arguments.ToArray();
    }

    private object ParseComplexArgument(string arg)
    {
        if (arg.StartsWith("{"))
        {
            var dict = new Dictionary<int, object>();            
            var matches = Regex.Matches(arg, @"(?<key>\d+):\('(?<type>.*?)',(?<value>.*?)\)");

            foreach (Match match in matches)
            {
                var key = int.Parse(match.Groups["key"].Value);
                var type = match.Groups["type"].Value;
                var value = ParseArgumentValue(match.Groups["value"].Value);
                dict[key] = new { Type = type, Value = value };
            }

            return dict;
        }
        else if (arg.StartsWith("["))
        {
            return ParseArguments(arg.Trim('[', ']'));
        }

        return null;
    }

    private object ParseArgumentValue(string value)
    {
        if (int.TryParse(value, out int intValue))
        {
            return intValue;
        }
        else if (bool.TryParse(value, out bool boolValue))
        {
            return boolValue;
        }
        else if (value == "null")
        {
            return null;
        }
        else if (value.StartsWith("'") && value.EndsWith("'"))
        {
            return value.Trim('\'');
        }
        else if (value.StartsWith("{") || value.StartsWith("["))
        {
            return ParseComplexArgument(value);
        }

        throw new InvalidDataException("Not implemeneted type.");
    }

    private string ReadProtocol(string protocolName)
    {
        var protocolResource = $"s2ProtocolFurry.Protocol.{protocolName}.txt";
        var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(protocolResource)!;        
        using var reader = new StreamReader(stream);

        var content = reader.ReadToEnd();

        return content;
    }
}
