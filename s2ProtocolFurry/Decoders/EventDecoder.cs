using s2ProtocolFurry.Decoders;

namespace s2ProtocolFurry.Decoder
{
    public class CorruptedException : Exception
    {
        public CorruptedException(string message) : base(message) { }
    }

    public class EventDecoder
    {
        public IEnumerable<Dictionary<string, object>>DecodeEventStream<TEnum>(
            IDecoder decoder,
            int eventIdTypeId,
            Dictionary<TEnum, (int typeId, string typeName)> eventTypes,
            bool decodeUserId)
        {
            uint gameloop = 0;

            while (!decoder.Done())
            {
                int startBits = decoder.UsedBits();
                
                var delta = GetVarUint32Value(decoder.Instance(svaruint32TypeId));
                gameloop += delta;
                
                object userId = null;
                if (decodeUserId)
                {
                    userId = decoder.Instance(replayUserIdTypeId);
                }
                
                var instance = decoder.Instance(eventIdTypeId);
                var eventId = (TEnum)instance;
                if (!eventTypes.TryGetValue(eventId, out var eventType))
                {
                    throw new CorruptedException($"eventid({eventId}) at {decoder}");
                }

                int typeId = eventType.typeId;
                string typeName = eventType.typeName;
                
                var eventInstance = decoder.Instance(typeId) as Dictionary<string, object>;
                if (eventInstance == null)
                {
                    throw new CorruptedException($"Failed to decode event instance for typeId({typeId})");
                }
                
                eventInstance["_event"] = typeName;
                eventInstance["_eventid"] = eventId;
                eventInstance["_gameloop"] = gameloop;
                if (decodeUserId)
                {
                    eventInstance["_userid"] = userId;
                }
              
                decoder.ByteAlign();
                
                eventInstance["_bits"] = decoder.UsedBits() - startBits;

                yield return eventInstance;
            }
        }

        private static uint GetVarUint32Value(object value)
        {
            if (value is Dictionary<string, object> dict && dict.Values.Count > 0)
            {
                foreach (var v in dict.Values)
                {
                    return Convert.ToUInt32(v);
                }
            }
            return 0;
        }

        // Define constants used in the method
        private const int svaruint32TypeId = 7;
        private const int replayUserIdTypeId = 8;
    }
}
