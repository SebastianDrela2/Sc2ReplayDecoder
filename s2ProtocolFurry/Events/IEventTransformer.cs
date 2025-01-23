using s2ProtocolFurry.Decoder;
using s2ProtocolFurry.Events.TrackerEvents;

namespace s2ProtocolFurry.Events;

public interface IEventTransformer<T>
{
  T Transform(EventHeader header, SPlayerSetupEvent value);
  T Transform(EventHeader header, SPlayerStatsEvent value);
  T Transform(EventHeader header, SUnitBornEvent value);
  T Transform(EventHeader header, SUnitDiedEvent value);
  T Transform(EventHeader header, SUnitOwnerChangeEvent value);
  T Transform(EventHeader header, SUnitPositionsEvent value);
  T Transform(EventHeader header, SUnitTypeChangeEvent value);
  T Transform(EventHeader header, SUpgradeEvent value);
  T Transform(EventHeader header, SUnitInitEvent value);
  T Transform(EventHeader header, SUnitDoneEvent value);
  T Tranform_Default(EventHeader header, string evType);
}
public static class EventTransformer
{
  public static TrackerEvents.TrackerEvents ParseTracker<TSelf>(this TSelf transformer, IEnumerable<Dictionary<string, object>> eventDicList)
    where TSelf : IEventTransformer<EventDecoderBuffer.UntypedKey>
  {
    return new TrackerEvents.TrackerEvents() {
      Data = [..Parse.Parse.Tracker<TSelf, EventDecoderBuffer.UntypedKey>(transformer, eventDicList)]
    };
  }
}
