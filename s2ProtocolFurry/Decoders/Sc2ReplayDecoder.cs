using MPQArchive.MPQ;
using s2ProtocolFurry.Decoders;
using s2ProtocolFurry.NNetGame;
using s2ProtocolFurry.Protocol;

namespace s2ProtocolFurry.Decoder
{
    public class Sc2ReplayDecoder
    {        
        private readonly EventDecoder _eventDecoder;
        private readonly MPQArchive.MPQ.ReceivedData.MPQArchive _mpqArchive;
        private readonly string _path;
        private List<ProtocolTypeInfo> _typeInfos;

        public Sc2ReplayDecoder(string path)
        {
            _path = path;
            using var stream = File.Open(path, FileMode.Open, FileAccess.Read, FileShare.Read);

            var mpqReader = new MPQReader(stream);

            _mpqArchive = mpqReader.Read();
            _eventDecoder = new EventDecoder();

            var protocolTypeInfoParser = new ProtocolTypeInfoParser();
            _typeInfos = protocolTypeInfoParser.ParseProtocolTypes("protocol92440");
        }

        public Sc2Replay DecodeSc2Replay()
        {
            var replay = new Sc2Replay(_path);

            var initData = DecodeReplayInitData();
            replay.InitData = Parse.Parse.InitData(initData);

            var replayDetails = DecodeReplayDetails();
            replay.Details = Parse.Parse.Details(replayDetails);

            var trackerEvents = DecodeReplayTrackerEvents();
            replay.TrackerEvents = Parse.Parse.Tracker(trackerEvents);

            var gameEvents = DecodeReplayGameEvents();
            replay.GameEvents = Parse.Parse.GameEvents(gameEvents);

            var messages = DecodeReplayMessageEvents();
            Parse.Parse.SetMessages(messages, replay);
            
            if (replay.TrackerEvents is not null)
            {
                replay.TrackerEvents.SUnitBornEvents.ToList().ForEach(f => f.UnitIndex = GetUnitIndex(f.UnitTagIndex, f.UnitTagRecycle));
                replay.TrackerEvents.SUnitInitEvents.ToList().ForEach(f => f.UnitIndex = GetUnitIndex(f.UnitTagIndex, f.UnitTagRecycle));
                replay.TrackerEvents.SUnitDiedEvents.ToList().ForEach(f => f.UnitIndex = GetUnitIndex(f.UnitTagIndex, f.UnitTagRecycle));
                replay.TrackerEvents.SUnitDoneEvents.ToList().ForEach(f => f.UnitIndex = GetUnitIndex(f.UnitTagIndex, f.UnitTagRecycle));
                replay.TrackerEvents.SUnitOwnerChangeEvents.ToList().ForEach(f => f.UnitIndex = GetUnitIndex(f.UnitTagIndex, f.UnitTagRecycle));

                Parse.Parse.SetTrackerEventsUnitConnections(replay.TrackerEvents);
            }

            return replay;
        }

        public Dictionary<string, object> DecodeReplayHeader()
        {
            var decoder = new VersionedDecoder(_mpqArchive.MPQUserData.Content, _typeInfos);
            return (Dictionary<string, object>)decoder.Instance(EventMappedTypes.ReplayHeaderTypeId);
        }

        public Dictionary<string, object> DecodeReplayDetails()
        {
            var decoder = new VersionedDecoder(GetListItemContent("replay.details"), _typeInfos);
            return (Dictionary<string, object>)decoder.Instance(EventMappedTypes.GameDetailsTypeId);
        }

        public Dictionary<string, object> DecodeReplayInitData()
        {
            var decoder = new BitPackedDecoder(GetListItemContent("replay.initData"), _typeInfos);
            return (Dictionary<string, object>)decoder.Instance(EventMappedTypes.ReplayInitDataTypeId);
        }

        public IEnumerable<Dictionary<string, object>> DecodeReplayGameEvents()
        {
            var decoder = new BitPackedDecoder(GetListItemContent("replay.game.events"), _typeInfos);
            foreach (var eventItem in _eventDecoder.DecodeEventStream(
                decoder, EventMappedTypes.GameEventIdTypeId, EventMappedTypes.GameEventMappedTypes, true))
            {
                yield return eventItem;
            }
        }

        public IEnumerable<Dictionary<string, object>> DecodeReplayMessageEvents()
        {
            var decoder = new BitPackedDecoder(GetListItemContent("replay.message.events"), _typeInfos);
            foreach (var eventItem in _eventDecoder.DecodeEventStream(
                decoder, EventMappedTypes.MessageEventIdTypeId, EventMappedTypes.MessageEventTypes, true))
            {
                yield return eventItem;
            }
        }

        private IEnumerable<Dictionary<string, object>> DecodeReplayTrackerEvents()
        {
            var decoder = new VersionedDecoder(GetListItemContent("replay.tracker.events"), _typeInfos);
            foreach (var eventItem in _eventDecoder.DecodeEventStream(
                decoder, EventMappedTypes.TrackerEventIdTypeId, EventMappedTypes.TrackedEventMappedTypes, false))
            {
                yield return eventItem;
            }
        }

        private static int GetUnitIndex(int unitTagIndex, int unitTagRecycle)
        {
            return (unitTagIndex << 18) + unitTagRecycle;
        }

        private byte[] GetListItemContent(string listItemFile)
        {
            return _mpqArchive.ListingFiles.First(x => x.Key == listItemFile).Value;
        }
    }
}
