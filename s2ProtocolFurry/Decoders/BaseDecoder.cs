namespace s2ProtocolFurry.Decoders;

public class BaseDecoder
{
    protected List<Tuple<string, int>> ProcessListOrDictionary(object listObject)
    {
        var result = new List<Tuple<string, int>>();

        if (listObject is List<object> list)
        {
            foreach (var item in list)
            {
                AddToResult(item, result);
            }
        }
        else if (listObject is Dictionary<int, object> dictionary)
        {
            foreach (var item in dictionary.Values)
            {
                AddToResult(item, result);
            }
        }
        else
        {
            throw new InvalidCastException("The provided object is neither a List<object> nor a Dictionary<int, object>.");
        }

        return result;
    }

    private static void AddToResult(object item, List<Tuple<string, int>> result)
    {
        var itemType = item.GetType();

        var typeProp = itemType.GetProperty("Type");
        var intValue1Prop = itemType.GetProperty("Value1") ?? itemType.GetProperty("Value");

        if (typeProp != null && intValue1Prop != null)
        {
            var typeStr = (string)typeProp.GetValue(item);
            var intValue1 = (int)intValue1Prop.GetValue(item);

            result.Add(new Tuple<string, int>(typeStr, intValue1));
        }
        else
        {
            throw new InvalidCastException("The list or dictionary item does not have the expected properties.");
        }
    }

    protected Dictionary<int, Tuple<string, int>> CastToDictionaryIntTupleStringInt(object dictObject)
    {
        if (dictObject is Dictionary<int, object> dict)
        {
            var result = new Dictionary<int, Tuple<string, int>>();

            foreach (var kvp in dict)
            {                    
                var value = kvp.Value;
                var valueType = value.GetType();

                var typeProp = valueType.GetProperty("Type");
                var valueProp = valueType.GetProperty("Value");

                if (typeProp != null && valueProp != null)
                {
                    var typeStr = (string)typeProp.GetValue(value);
                    var intValue = (int)valueProp.GetValue(value);
                    result[kvp.Key] = new Tuple<string, int>(typeStr, intValue);
                }
                else
                {
                    throw new InvalidCastException("The dictionary value does not have the expected properties.");
                }
            }

            return result;
        }
        else
        {
            throw new InvalidCastException("The provided object is not a Dictionary<int, object>.");
        }
    }
}
