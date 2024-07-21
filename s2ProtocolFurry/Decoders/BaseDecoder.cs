namespace s2ProtocolFurry.Decoders;

public class BaseDecoder
{
    protected List<Tuple<string, int>> CastToListOfTuples(object listObject)
    {
        if (listObject is List<object> list)
        {
            var result = new List<Tuple<string, int>>();

            foreach (var item in list)
            {
                var itemType = item.GetType();

                var typeProp = itemType.GetProperty("Type");
                var intValue1Prop = itemType.GetProperty("Value1");               

                if (typeProp != null && intValue1Prop != null)
                {
                    var typeStr = (string)typeProp.GetValue(item);
                    var intValue1 = (int)intValue1Prop.GetValue(item);

                    result.Add(new Tuple<string, int>(typeStr, intValue1));
                }
                else
                {
                    throw new InvalidCastException("The list item does not have the expected properties.");
                }
            }

            return result;
        }
        else
        {
            throw new InvalidCastException("The provided object is not a List<object>.");
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

    protected List<Tuple<string, int, int>> ConvertToListOfTuples(object[] fields)
    {
        var list = new List<Tuple<string, int, int>>();
        fields = (object[])fields[0];

        if (fields.Length % 3 != 0)
        {
            throw new ArgumentException("The fields array length must be a multiple of 3.");
        }

        for (int i = 0; i < fields.Length; i += 3)
        {            
            if (i + 2 >= fields.Length)
            {
                throw new ArgumentException("The fields array does not have enough elements to form complete tuples.");
            }        
            
            var fieldName = fields[i] as string;
            var firstValue = Convert.ToInt32(fields[i + 1]);
            var secondValue = Convert.ToInt32(fields[i + 2]);


            if (fieldName == null)
            {
                throw new ArgumentException($"Expected string at index {i}, but got {fields[i]?.GetType().Name}.");
            }

            list.Add(new Tuple<string, int, int>(fieldName, firstValue, secondValue));
        }

        return list;
    }
}
