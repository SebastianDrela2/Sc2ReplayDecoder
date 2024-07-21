using System.Reflection;
using System.Text.RegularExpressions;

namespace s2ProtocolFurry.Protocol;

public class ProtocolTypeInfoParser
{
    public List<ProtocolTypeInfo> ParseProtocolTypes(string protocolName)
    {
        var input = ReadProtocol(protocolName);
        var protocolTypes = new List<ProtocolTypeInfo>();
        var matches = Regex.Matches(input, @"\('(?<type>_int|_choice|_struct|_blob|_array|_optional|_bool|_fourcc|_bitarray|_null)',\[(?<args>.*?)\]\)(,?\s*#\d+)?");

        foreach (Match match in matches)
        {
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
