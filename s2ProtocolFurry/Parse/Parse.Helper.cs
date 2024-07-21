using System.Numerics;
using System.Text;

namespace s2ProtocolFurry.Parse;

public static partial class Parse
{
    internal static string GetString(Dictionary<string, object> dic, string property)
    {
        if (dic.TryGetValue(property, out object? value))
        {
            if (value is byte[] bytes)
                return Encoding.UTF8.GetString(bytes);
            else
                return value?.ToString() ?? "";
        }
        else
        {
            return "";
        }
    }

    internal static string? GetNullableString(Dictionary<string, object> dic, string property)
    {
        if (dic.TryGetValue(property, out object? value))
        {
            if (value is byte[] bytes)
                return Encoding.UTF8.GetString(bytes);
            else
                return value?.ToString();
        }
        else
        {
            return null;
        }
    }

    internal static uint GetUInt(Dictionary<string, object> dic, string property)
    {
        if (dic.TryGetValue(property, out object? value))
        {
            if (value is uint i)
                return i;
            else
                return 0;
        }
        else
        {
            return 0;
        }
    }

    internal static int GetInt(Dictionary<string, object> dic, string property)
    {
        if (dic.TryGetValue(property, out object? value))
        {
            if (value is int i)
                return i;
            else
                return 0;
        }
        else
        {
            return 0;
        }
    }

    internal static int? GetNullableInt(Dictionary<string, object> dic, string property)
    {
        if (dic.TryGetValue(property, out object? value))
        {
            if (value is int i)
                return i;
            else
                return null;
        }
        else
        {
            return null;
        }
    }

    internal static long GetBigInt(Dictionary<string, object> dic, string property)
    {
        if (dic.TryGetValue(property, out object? value))
        {
            if (value is BigInteger bigInt)
                return (long)bigInt;
            else if (value is int i)
                return i;
            else
                return 0;
        }
        else
        {
            return 0;
        }
    }

    internal static long? GetNullableBigInt(Dictionary<string, object> dic, string property)
    {
        if (dic.TryGetValue(property, out object? value))
        {
            if (value is BigInteger bigInt)
                return (long)bigInt;
            else if (value is int i)
                return i;
            else
                return null;
        }
        else
        {
            return null;
        }
    }

    internal static bool GetBool(Dictionary<string, object> dic, string property)
    {
        if (dic.TryGetValue(property, out object? value))
        {
            if (value is bool b)
                return b;
            else
                return false;
        }
        else
        {
            return false;
        }
    }

    internal static string GetAsciiString(Dictionary<string, object> dic, string property)
    {
        if (dic.TryGetValue(property, out object? value))
        {
            if (value is string str)
                return str; // Assuming the value is already a string
            else
                return value?.ToString() ?? "";
        }
        else
        {
            return "";
        }
    }

    internal static double GetDouble(Dictionary<string, object> dic, string property)
    {
        if (dic.TryGetValue(property, out object? value))
        {
            if (value is double d)
                return d;
            else
                return 0;
        }
        else
        {
            return 0;
        }
    }

    internal static List<int> GetIntList(Dictionary<string, object> dic, string property)
    {
        var intList = new List<int>();
        if (dic.TryGetValue(property, out object? value))
        {
            if (value is IEnumerable<object> list)
            {
                foreach (var item in list)
                {
                    if (item is int i)
                        intList.Add(i);
                }
            }
        }
        return intList;
    }

    internal static List<long> GetLongList(Dictionary<string, object> dic, string property)
    {
        var longList = new List<long>();
        if (dic.TryGetValue(property, out object? value))
        {
            if (value is IEnumerable<object> list)
            {
                foreach (var item in list)
                {
                    if (item is long l)
                        longList.Add(l);
                }
            }
        }
        return longList;
    }

    internal static KeyValuePair<int, BigInteger> GetIntBigTuple(Dictionary<string, object> dic, string property)
    {
        int intEnt = 0;
        BigInteger bigEnt = 0;
        if (dic.TryGetValue(property, out object? value))
        {
            if (value is Tuple<object, object> tuple)
            {
                if (tuple.Item1 is int i)
                    intEnt = i;

                if (tuple.Item2 is BigInteger bigI)
                    bigEnt = bigI;
                else if (tuple.Item2 is int intI)
                    bigEnt = intI;
            }
        }
        return new KeyValuePair<int, BigInteger>(intEnt, bigEnt);
    }

    internal static List<string> GetStringList(Dictionary<string, object> dic, string property)
    {
        var stringList = new List<string>();
        if (dic.TryGetValue(property, out object? value))
        {
            if (value is IEnumerable<object> list)
            {
                foreach (var item in list)
                {
                    if (item is string str)
                        stringList.Add(str);
                }
            }
        }
        return stringList;
    }
}
