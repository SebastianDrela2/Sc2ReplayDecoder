using MPQArchive.MPQ;
using s2ProtocolFurry.Decoders;
using s2ProtocolFurry.Events.MetaData;
using s2ProtocolFurry.NNetGame;
using s2ProtocolFurry.Protocol;
using System.Text.Json;
using System.Text;

namespace s2ProtocolFurry.Decoder
{
    public class Sc2ReplayDecoder
    {               
        private readonly ProtocolImporter _protocolImporter;
        private readonly EventDecoder _eventDecoder;
        private MPQArchive.MPQ.ReceivedData.MPQArchive _mpqArchive;              

        private List<ProtocolTypeInfo> _typeInfos;

        public Sc2ReplayDecoder(string protocolVersionsDir)
        {          
            _eventDecoder = new EventDecoder();
            _protocolImporter = new ProtocolImporter(protocolVersionsDir);

            _typeInfos = _protocolImporter.GetTypeInfos();
        }

        public Sc2Replay DecodeSc2Replay(string path)
        {           
            using var stream = File.Open(path, FileMode.Open, FileAccess.Read, FileShare.Read);

            var mpqReader = new MPQReader(stream);
            _mpqArchive = mpqReader.Read();                    

            var replay = new Sc2Replay(path);
            var replayHeader = DecodeReplayHeader();

            var version = replayHeader["m_version"] as Dictionary<string, object>;
            var baseBuild = version["m_baseBuild"];

            _typeInfos = _protocolImporter.GetTypeInfos(90870);

            var initData = DecodeReplayInitData();
            replay.InitData = Parse.Parse.InitData(initData);

            var trackerEvents = DecodeReplayTrackerEvents();
            replay.TrackerEvents = Parse.Parse.Tracker(trackerEvents);

            var replayDetails = DecodeReplayDetails();
            replay.Details = Parse.Parse.Details(replayDetails);

            var gameEvents = DecodeReplayGameEvents();
            replay.GameEvents = Parse.Parse.GameEvents(gameEvents);

            var metaData = DecodeReplayMetaData();
            replay.MetaData = metaData;

            var messages = DecodeReplayMessageEvents();
            Parse.Parse.SetMessages(messages, replay);

            if (replay.TrackerEvents is not null)
            {
                replay.TrackerEvents.SUnitBornEvents.Data.Select(f => f.UnitIndex = GetUnitIndex(f.UnitTagIndex, f.UnitTagRecycle));
                replay.TrackerEvents.SUnitInitEvents.Data.Select(f => f.UnitIndex = GetUnitIndex(f.UnitTagIndex, f.UnitTagRecycle));
                replay.TrackerEvents.SUnitDiedEvents.Data.Select(f => f.UnitIndex = GetUnitIndex(f.UnitTagIndex, f.UnitTagRecycle));
                replay.TrackerEvents.SUnitDoneEvents.Data.Select(f => f.UnitIndex = GetUnitIndex(f.UnitTagIndex, f.UnitTagRecycle));
                replay.TrackerEvents.SUnitOwnerChangeEvents.Data.Select(f => f.UnitIndex = GetUnitIndex(f.UnitTagIndex, f.UnitTagRecycle));

                Parse.Parse.SetTrackerEventsUnitConnections(replay.TrackerEvents);
            }

            return replay;
        }

        private Dictionary<string, object> DecodeReplayHeader()
        {
            var decoder = new VersionedDecoder(_mpqArchive.MPQUserData.Content, _typeInfos);
            return (Dictionary<string, object>)decoder.Instance(EventMappedTypes.ReplayHeaderTypeId);
        }

        private Dictionary<string, object> DecodeReplayDetails()
        {
            var decoder = new VersionedDecoder(GetListItemContent("replay.details"), _typeInfos);
            return (Dictionary<string, object>)decoder.Instance(EventMappedTypes.GameDetailsTypeId);
        }

        private Dictionary<string, object> DecodeReplayInitData()
        {
            var decoder = new BitPackedDecoder(GetListItemContent("replay.initData"), _typeInfos);
            return (Dictionary<string, object>)decoder.Instance(EventMappedTypes.ReplayInitDataTypeId);
        }

        private IEnumerable<Dictionary<string, object>> DecodeReplayGameEvents()
        {
            var decoder = new BitPackedDecoder(GetListItemContent("replay.game.events"), _typeInfos);
            foreach (var eventItem in _eventDecoder.DecodeEventStream(
                decoder, EventMappedTypes.GameEventIdTypeId, EventMappedTypes.GameEventMappedTypes, true))
            {
                yield return eventItem;
            }
        }

        private IEnumerable<Dictionary<string, object>> DecodeReplayMessageEvents()
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

        private ReplayMetadata DecodeReplayMetaData()
        {
            var metaDataContent = GetListItemContent("replay.gamemetadata.json");

            var meta_string = Encoding.UTF8.GetString(metaDataContent.ToArray());

            if (meta_string != null)
            {
                return JsonSerializer.Deserialize<ReplayMetadata>(meta_string);               
            }

            return null;
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
