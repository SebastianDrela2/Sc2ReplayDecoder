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
        private List<ProtocolTypeInfo> _typeInfos;

        public Sc2ReplayDecoder(string path)
        {                        
            using var stream = File.Open(path, FileMode.Open, FileAccess.Read, FileShare.Read);

            var mpqReader = new MPQReader(stream);

            _mpqArchive = mpqReader.Read();
            _eventDecoder = new EventDecoder();

            var protocolTypeInfoParser = new ProtocolTypeInfoParser();
            _typeInfos = protocolTypeInfoParser.ParseProtocolTypes("protocol92440");
        }
        public Dictionary<string, object> DecodeReplayHeader()
        {
            var decoder = new VersionedDecoder(_mpqArchive.MPQUserData.Content, _typeInfos);
            return (Dictionary<string, object>)decoder.Instance(EventMappedTypes.ReplayHeaderTypeId);
        }

        public Dictionary<string, object> DecodeReplayDetails()
        {
            var decoder = new VersionedDecoder(GetListItemContent("replay.details.backup"), _typeInfos);
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

        public IEnumerable<Dictionary<string, object>> DecodeReplayTrackerEvents()
        {
            var decoder = new VersionedDecoder(GetListItemContent("replay.tracker.events"), _typeInfos);
            foreach (var eventItem in _eventDecoder.DecodeEventStream(
                decoder, EventMappedTypes.TrackerEventIdTypeId, EventMappedTypes.TrackedEventMappedTypes, false))
            {
                yield return eventItem;
            }
        }
        
        private byte[] GetListItemContent(string listItemFile)
        {
            return _mpqArchive.ListingFiles.First(x => x.Key == listItemFile).Value;
        }
    }
}
