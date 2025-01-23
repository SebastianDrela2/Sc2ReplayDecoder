using MPQArchive.MPQ;
using s2ProtocolFurry.Decoders;
using s2ProtocolFurry.Events.MetaData;
using s2ProtocolFurry.NNetGame;
using s2ProtocolFurry.Protocol;
using System.Text.Json;
using System.Text;
using s2ProtocolFurry.Events;

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
            replay.TrackerEvents = _eventDecoder.ParseTracker(trackerEvents);

            var replayDetails = DecodeReplayDetails();
            replay.Details = Parse.Parse.Details(replayDetails);

            var gameEvents = DecodeReplayGameEvents();
            replay.GameEvents = Parse.Parse.GameEvents(gameEvents);
            if (replay.TrackerEvents is not { } ev) throw new NullReferenceException();

            var metaData = DecodeReplayMetaData();
            replay.MetaData = metaData;

            var messages = DecodeReplayMessageEvents();
            Parse.Parse.SetMessages(messages, replay);

            foreach (ref var f in ev.SUnitBornEvents) f.UnitIndex = GetUnitIndex(f.UnitTagIndex, f.UnitTagRecycle);
            foreach (ref var f in ev.SUnitInitEvents) f.UnitIndex = GetUnitIndex(f.UnitTagIndex, f.UnitTagRecycle);
            foreach (ref var f in ev.SUnitDiedEvents) f.UnitIndex = GetUnitIndex(f.UnitTagIndex, f.UnitTagRecycle);
            foreach (ref var f in ev.SUnitDoneEvents) f.UnitIndex = GetUnitIndex(f.UnitTagIndex, f.UnitTagRecycle);
            foreach (ref var f in ev.SUnitOwnerChangeEvents) f.UnitIndex = GetUnitIndex(f.UnitTagIndex, f.UnitTagRecycle);

            Connect(
                ev.SUnitBornEvents.Data,
                ev.SUnitDiedEvents.Data,
                x => x.UnitIndex,
                x => x.UnitIndex,
                (a, b) => a.SUnitDiedEvent = b
            );
            Connect(
                ev.SUnitInitEvents.Data,
                ev.SUnitDiedEvents.Data,
                x => x.UnitIndex,
                x => x.UnitIndex,
                (a, b) => a.SUnitDiedEvent = b
            );
            Connect(
                ev.SUnitInitEvents.Data,
                ev.SUnitDoneEvents.Data,
                x => x.UnitIndex,
                x => x.UnitIndex,
                (a, b) => a.SUnitDoneEvent = b
            );
            Connect(
                ev.SUnitDiedEvents.Data,
                ev.SUnitBornEvents.Data,
                x => (x.KillerUnitTagIndex, x.KillerUnitTagRecycle),
                x => (x.UnitTagIndex, x.UnitTagRecycle),
                (a, b) => a.KillerUnitBornEvent = b
            );
            Connect(
                ev.SUnitDiedEvents.Data,
                ev.SUnitInitEvents.Data,
                x => (x.KillerUnitTagIndex, x.KillerUnitTagRecycle),
                x => (x.UnitTagIndex, x.UnitTagRecycle),
                (a, b) => a.KillerUnitInitEvent = b
            );

            return replay;
            static void Connect<T1, T2, TKey>(
                IEnumerable<T1> xs,
                IEnumerable<T2> ys,
                Func<T1, TKey> selector1,
                Func<T2, TKey> selector2,
                Connector<T1, T2> action)
            {
                foreach (var (a, b) in xs.Join(ys, selector1, selector2, ValueTuple.Create))
                {
                    action(a, b);
                }
            }
        }
        public delegate void Connector<T1, T2>(T1 a, T2 b);

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
            return _eventDecoder.DecodeEventStream(
                new VersionedDecoder(GetListItemContent("replay.tracker.events"), _typeInfos),
                EventMappedTypes.TrackerEventIdTypeId,
                EventMappedTypes.TrackedEventMappedTypes,
                false
            );
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
