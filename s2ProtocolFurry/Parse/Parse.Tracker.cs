using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;
using s2ProtocolFurry.Events.TrackerEvents;

namespace s2ProtocolFurry.Parse;

public static partial class Parse
{
    public static TrackerEvents Tracker(IEnumerable<Dictionary<string, object>> eventDicList)
    {
        TrackerEvents output = new();
        foreach (var eventDic in eventDicList)
        {
            var trackerEvent = GetTrackerEvent(eventDic);
            switch (trackerEvent.EventType)
            {
                case TrackerEventType.SPlayerSetupEvent: output.SPlayerSetupEvents.Add(GetSPlayerSetupEvent(eventDic, trackerEvent)); break;
                case TrackerEventType.SPlayerStatsEvent: output.SPlayerStatsEvents.Add(GetSPlayerStatsEvent(eventDic, trackerEvent)); break;
                case TrackerEventType.SUnitBornEvent: output.SUnitBornEvents.Add(GetSUnitBornEvent(eventDic, trackerEvent)); break;
                case TrackerEventType.SUnitDiedEvent: output.SUnitDiedEvents.Add(GetSUnitDiedEvent(eventDic, trackerEvent)); break;
                case TrackerEventType.SUnitOwnerChangeEvent: output.SUnitOwnerChangeEvents.Add(GetSUnitOwnerChangeEvent(eventDic, trackerEvent)); break;
                case TrackerEventType.SUnitPositionsEvent: output.SUnitPositionsEvents.Add(GetSUnitPositionsEvent(eventDic, trackerEvent)); break;
                case TrackerEventType.SUnitTypeChangeEvent: output.SUnitTypeChangeEvents.Add(GetSUnitTypeChangeEvent(eventDic, trackerEvent)); break;
                case TrackerEventType.SUpgradeEvent: output.SUpgradeEvents.Add(GetSUpgradeEvent(eventDic, trackerEvent)); break;
                case TrackerEventType.SUnitInitEvent: output.SUnitInitEvents.Add(GetSUnitInitEvent(eventDic, trackerEvent)); break;
                case TrackerEventType.SUnitDoneEvent: output.SUnitDoneEvents.Add(GetSUnitDoneEvent(eventDic, trackerEvent)); break;
                default: GetUnknownEvent(eventDic, trackerEvent); break;
            }
        }

        output.SUnitDiedEvents.Span.Sort((x, y) => x.UnitIndex.CompareTo(y.UnitIndex));
        output.SUnitDoneEvents.Span.Sort((x, y) => x.UnitIndex.CompareTo(y.UnitIndex));
        output.SUnitBornEvents.Span.Sort((x, y) => (x.UnitTagIndex, x.UnitTagRecycle).CompareTo((y.UnitTagIndex, y.UnitTagRecycle)));
        output.SUnitInitEvents.Span.Sort((x, y) => (x.UnitTagIndex, x.UnitTagRecycle).CompareTo((y.UnitTagIndex, y.UnitTagRecycle)));

        return output;
    }

    [InlineArray(4)]
    struct Uple<T>
    {
        private T _elem;
    }
    ref struct Taple<T>(T died, T done, T born, T init)
        where T : allows ref struct
    {
        public T Died = died;
        public T Done = done;
        public T Born = born;
        public T Init = init;
    }
    ref struct Taple<T1, T2, T3, T4>(T1 died, T2 done, T3 born, T4 init)
        where T1 : allows ref struct
        where T2 : allows ref struct
        where T3 : allows ref struct
        where T4 : allows ref struct
    {
        public T1 Died = died;
        public T2 Done = done;
        public T3 Born = born;
        public T4 Init = init;
    }
    static Taple<T1, T2, T3, T4> make_taple<T1, T2, T3, T4>(T1 died, T2 done, T3 born, T4 init)
        where T1 : allows ref struct
        where T2 : allows ref struct
        where T3 : allows ref struct
        where T4 : allows ref struct
    {
        return new(died, done, born, init);
    }
    static Taple<T> make_taple<T>(T died, T done, T born, T init)
        where T : allows ref struct
    {
        return new(died, done, born, init);
    }
    internal static void SetTrackerEventsUnitConnections(TrackerEvents trackerEvents)
    {
        var data = make_taple(
            trackerEvents.SUnitDiedEvents.Span,
            trackerEvents.SUnitDoneEvents.Span,
            trackerEvents.SUnitBornEvents.Span,
            trackerEvents.SUnitInitEvents.Span
        );

        if (data.Died.Length is 0) throw new NotImplementedException();
        if (data.Done.Length is 0) throw new NotImplementedException();
        if (data.Born.Length is 0) throw new NotImplementedException();
        if (data.Init.Length is 0) throw new NotImplementedException();

        Vector256<int> i = Vector256<int>.Zero;
        Vector256<int> len = Vector256.Create([data.Died.Length, data.Done.Length, data.Born.Length, data.Init.Length]);
        Vector256<int> has_next = Vector256.LessThan(i, len);

        static void Unborn(Vector256<int> i, Taple<Span<SUnitDiedEvent>, Span<SUnitDoneEvent>, Span<SUnitBornEvent>, Span<SUnitInitEvent>> data) {
            data.Born[i[2]].SUnitDiedEvent = data.Died[i[0]];
        }
        static void Uninit(Vector256<int> i, Taple<Span<SUnitDiedEvent>, Span<SUnitDoneEvent>, Span<SUnitBornEvent>, Span<SUnitInitEvent>> data) {
            data.Init[i[3]].SUnitDiedEvent = data.Died[i[0]];
        }
        static void Completed(Vector256<int> i, Taple<Span<SUnitDiedEvent>, Span<SUnitDoneEvent>, Span<SUnitBornEvent>, Span<SUnitInitEvent>> data) {
            data.Init[i[3]].SUnitDoneEvent = data.Done[i[1]];
        }
        
        Vector256<int> keys = Vector256.Create([
            data.Died[0].UnitIndex,
            data.Done[0].UnitIndex,
            data.Born[0].UnitIndex,
            data.Init[0].UnitIndex
        ]);
        
        var min = Vector256.Create(int.Min(keys[0], int.Min(keys[1], int.Min(keys[2], keys[3]))));
        while (has_next == Vector256<int>.Zero)
        {
            var order = Vector256.Equals(keys, min);
            var x = Avx2.And(has_next, order).ExtractMostSignificantBits() & 0b_00001000_00000100_00000010_00000001;
            x |= x >> 16;
            x |= x >> 8;
            x &= 0b1111;
            switch (x)
            {
                case 0b_0_0_0_0: break;
                case 0b_0_0_0_1:
                    i = Vector256.Add(i, Vector256.Create([0, 0, 0, 1]));
                    break;
                case 0b_0_0_1_0:
                    i = Vector256.Add(i, Vector256.Create([0, 0, 1, 0]));
                    break;
                case 0b_0_0_1_1:
                    i = Vector256.Add(i, Vector256.Create([0, 0, 1, 1]));
                    break;
                case 0b_0_1_0_0:
                    i = Vector256.Add(i, Vector256.Create([0, 1, 0, 0]));
                    break;
                case 0b_0_1_0_1:
                    Unborn(i, data);
                    i = Vector256.Add(i, Vector256.Create([0, 1, 0, 1]));
                    break;
                case 0b_0_1_1_0:
                    i = Vector256.Add(i, Vector256.Create([0, 1, 1, 0]));
                    break;
                case 0b_0_1_1_1:
                    Unborn(i, data);
                    i = Vector256.Add(i, Vector256.Create([0, 1, 1, 1]));
                    break;
                case 0b_1_0_0_0:
                    i = Vector256.Add(i, Vector256.Create([1, 0, 0, 0]));
                    break;
                case 0b_1_0_0_1:
                    Uninit(i, data);
                    i = Vector256.Add(i, Vector256.Create([1, 0, 0, 1]));
                    break;
                case 0b_1_0_1_0:
                    Completed(i, data);
                    i = Vector256.Add(i, Vector256.Create([1, 0, 1, 0]));
                    break;
                case 0b_1_0_1_1:
                    Uninit(i, data);
                    Completed(i, data);
                    i = Vector256.Add(i, Vector256.Create([1, 0, 1, 1]));
                    break;
                case 0b_1_1_0_0:
                    i = Vector256.Add(i, Vector256.Create([1, 1, 0, 0]));
                    break;
                case 0b_1_1_0_1:
                    Uninit(i, data);
                    Unborn(i, data);
                    i = Vector256.Add(i, Vector256.Create([1, 1, 0, 1]));
                    break;
                case 0b_1_1_1_0:
                    Completed(i, data);
                    i = Vector256.Add(i, Vector256.Create([1, 1, 1, 0]));
                    break;
                case 0b_1_1_1_1:
                    Unborn(i, data);
                    Uninit(i, data);
                    Completed(i, data);
                    i = Vector256.Add(i, Vector256.Create([1, 1, 1, 1]));
                    break;
            }

            has_next = Vector256.LessThan(i, len);
            keys = Vector256.Create([
                has_next[0] is 0 ? int.MinValue : data.Died[i[0]].UnitIndex,
                has_next[1] is 0 ? int.MinValue : data.Done[i[1]].UnitIndex,
                has_next[2] is 0 ? int.MinValue : data.Born[i[2]].UnitIndex,
                has_next[3] is 0 ? int.MinValue : data.Init[i[3]].UnitIndex
            ]);
            min = Vector256.Create(int.Min(keys[0], int.Min(keys[1], int.Min(keys[2], keys[3]))));
        }

        // case (0, _, 0, _):
        //     items.Born.SUnitDiedEvent = items.Died;
        //     break;
        // case (0, _, _, 0):
        //     items.Init.SUnitDiedEvent = items.Died;
        //     break;
        // case (_, 0, _, 0):
        //     items.Init.SUnitDoneEvent = items.Done;
        //     break;

        /*
            0b_1_0_1_0
            0b_1_0_0_1
            0b_0_1_0_1

            (Died_0 == Born_2) => Born_2.Died_0 = Died_0
            (Died_0 == Init_3) => Init_3.Died_0 = Died_0
            (Done_1 == Init_3) => Init_3.Done_1 = Done_1
        
            Died
            Done
            Born
            Init
        */
        // trackerEvents.SUnitBornEvents.data.Select(x => x.SUnitDiedEvent = trackerEvents.SUnitDiedEvents.data.FirstOrDefault(f => f.UnitIndex == x.UnitIndex));
        // trackerEvents.SUnitInitEvents.data.Select(x => x.SUnitDiedEvent = trackerEvents.SUnitDiedEvents.data.FirstOrDefault(f => f.UnitIndex == x.UnitIndex));
        // trackerEvents.SUnitInitEvents.data.Select(x => x.SUnitDoneEvent = trackerEvents.SUnitDoneEvents.data.FirstOrDefault(f => f.UnitIndex == x.UnitIndex));
        trackerEvents.SUnitDiedEvents.Data.Select(x => x.KillerUnitBornEvent = trackerEvents.SUnitBornEvents.Data.FirstOrDefault(f => f.UnitTagIndex == x.KillerUnitTagIndex && f.UnitTagRecycle == x.KillerUnitTagRecycle));
        trackerEvents.SUnitDiedEvents.Data.Select(x => x.KillerUnitInitEvent = trackerEvents.SUnitInitEvents.Data.FirstOrDefault(f => f.UnitTagIndex == x.KillerUnitTagIndex && f.UnitTagRecycle == x.KillerUnitTagRecycle));
    }

    private static TrackerEvent GetTrackerEvent(Dictionary<string, object> dic)
    {
        int playerId = GetInt(dic, "m_playerId");
        string type = GetString(dic, "_event");
        int bits = GetInt(dic, "_bits");
        uint gameloop = GetUInt(dic, "_gameloop");
        return new TrackerEvent(playerId, type, bits, gameloop);
    }
    private static TrackerEvent GetUnknownEvent(Dictionary<string, object> dic, TrackerEvent trackerEvent)
    {
        return trackerEvent;
    }

    private static SUnitDoneEvent GetSUnitDoneEvent(Dictionary<string, object> dic, TrackerEvent trackerEvent)
    {
        int unitTagIndex = GetInt(dic, "m_unitTagIndex");
        int unitTagRecycle = GetInt(dic, "m_unitTagRecycle");
        return new SUnitDoneEvent(trackerEvent, unitTagIndex, unitTagRecycle);
    }

    private static SUnitInitEvent GetSUnitInitEvent(Dictionary<string, object> dic, TrackerEvent trackerEvent)
    {
        int unitTagIndex = GetInt(dic, "m_unitTagIndex");
        int unitTagRecycle = GetInt(dic, "m_unitTagRecycle");
        string unitTypeName = GetString(dic, "m_unitTypeName");
        int controlPlayerId = GetInt(dic, "m_controlPlayerId");
        int x = GetInt(dic, "m_x");
        int y = GetInt(dic, "m_y");
        int upkeepPlayerId = GetInt(dic, "m_upkeepPlayerId");
        return new SUnitInitEvent(trackerEvent, unitTagIndex, unitTagRecycle, controlPlayerId, x, y, upkeepPlayerId, unitTypeName);
    }

    private static SUpgradeEvent GetSUpgradeEvent(Dictionary<string, object> dic, TrackerEvent trackerEvent)
    {
        int count = GetInt(dic, "m_count");
        string upgradeTypeName = GetString(dic, "m_upgradeTypeName");
        return new SUpgradeEvent(trackerEvent, count, upgradeTypeName);
    }

    private static SUnitTypeChangeEvent GetSUnitTypeChangeEvent(Dictionary<string, object> dic, TrackerEvent trackerEvent)
    {
        int unitTagIndex = GetInt(dic, "m_unitTagIndex");
        int unitTagRecycle = GetInt(dic, "m_unitTagRecycle");
        string unitTypeName = GetString(dic, "m_unitTypeName");
        return new SUnitTypeChangeEvent(trackerEvent, unitTagIndex, unitTagRecycle, unitTypeName);
    }

    private static SUnitPositionsEvent GetSUnitPositionsEvent(Dictionary<string, object> dic, TrackerEvent trackerEvent)
    {
        int firstUnitIndex = GetInt(dic, "m_firstUnitIndex");
        List<int> items = new List<int>();
        if (dic.TryGetValue("m_items", out var itemsObj))
        {
            if (itemsObj is ICollection<object> nums)
            {
                foreach (var num in nums)
                {
                    if (num is int n)
                    {
                        items.Add(n);
                    }
                }
            }
        }
        return new SUnitPositionsEvent(trackerEvent, firstUnitIndex, items.ToArray());
    }

    private static SUnitOwnerChangeEvent GetSUnitOwnerChangeEvent(Dictionary<string, object> dic, TrackerEvent trackerEvent)
    {
        int unitTagIndex = GetInt(dic, "m_unitTagIndex");
        int unitTagRecycle = GetInt(dic, "m_unitTagRecycle");
        int controlPlayerId = GetInt(dic, "m_controlPlayerId");
        int upkeepPlayerId = GetInt(dic, "m_upkeepPlayerId");
        return new SUnitOwnerChangeEvent(trackerEvent, unitTagIndex, unitTagRecycle, controlPlayerId, upkeepPlayerId);
    }

    private static SUnitDiedEvent GetSUnitDiedEvent(Dictionary<string, object> dic, TrackerEvent trackerEvent)
    {
        int unitTagIndex = GetInt(dic, "m_unitTagIndex");
        int unitTagRecycle = GetInt(dic, "m_unitTagRecycle");
        int? killerPlayerId = GetNullableInt(dic, "m_killerPlayerId");
        int x = GetInt(dic, "m_x");
        int y = GetInt(dic, "m_y");
        int? killerUnitTagRecycle = GetNullableInt(dic, "m_killerUnitTagRecycle");
        int? killerUnitTagIndex = GetNullableInt(dic, "m_killerUnitTagIndex");
        return new SUnitDiedEvent(trackerEvent, unitTagIndex, unitTagRecycle, killerPlayerId, x, y, killerUnitTagRecycle, killerUnitTagIndex);
    }

    private static SUnitBornEvent GetSUnitBornEvent(Dictionary<string, object> dic, TrackerEvent trackerEvent)
    {
        int unitTagIndex = GetInt(dic, "m_unitTagIndex");
        int unitTagRecycle = GetInt(dic, "m_unitTagRecycle");
        string? creatorAbilityName = GetNullableString(dic, "m_creatorAbilityName");
        int? creatorUnitTagRecycle = GetNullableInt(dic, "m_creatorUnitTagRecycle");
        int controlPlayerId = GetInt(dic, "m_controlPlayerId");
        int x = GetInt(dic, "m_x");
        int y = GetInt(dic, "m_y");
        int upkeepPlayerId = GetInt(dic, "m_upkeepPlayerId");
        string unitTypeName = GetString(dic, "m_unitTypeName");
        int? creatorUnitTagIndex = GetNullableInt(dic, "m_creatorUnitTagIndex");
        return new SUnitBornEvent(trackerEvent, unitTagIndex, unitTagRecycle, creatorAbilityName, creatorUnitTagRecycle, controlPlayerId, x, y, upkeepPlayerId, unitTypeName, creatorUnitTagIndex);
    }

    private static SPlayerSetupEvent GetSPlayerSetupEvent(Dictionary<string, object> dic, TrackerEvent trackerEvent)
    {
        int type = GetInt(dic, "m_type");
        int? userId = GetNullableInt(dic, "m_userId");
        int slotId = GetInt(dic, "m_slotId");
        return new SPlayerSetupEvent(trackerEvent, type, userId, slotId);
    }

    private static SPlayerStatsEvent GetSPlayerStatsEvent(Dictionary<string, object> dic, TrackerEvent trackerEvent)
    {
        if (dic.ContainsKey("m_stats"))
        {
            var statsDic = dic["m_stats"] as Dictionary<string, object>;

            if (statsDic != null)
            {
                int scoreValueVespeneUsedCurrentTechnology = GetInt(statsDic, "m_scoreValueVespeneUsedCurrentTechnology");
                int scoreValueVespeneFriendlyFireArmy = GetInt(statsDic, "m_scoreValueVespeneFriendlyFireArmy");
                int scoreValueMineralsFriendlyFireTechnology = GetInt(statsDic, "m_scoreValueMineralsFriendlyFireTechnology");
                int scoreValueMineralsUsedCurrentEconomy = GetInt(statsDic, "m_scoreValueMineralsUsedCurrentEconomy");
                int scoreValueVespeneLostEconomy = GetInt(statsDic, "m_scoreValueVespeneLostEconomy");
                int scoreValueMineralsUsedCurrentArmy = GetInt(statsDic, "m_scoreValueMineralsUsedCurrentArmy");
                int scoreValueVespeneUsedInProgressArmy = GetInt(statsDic, "m_scoreValueVespeneUsedInProgressArmy");
                int scoreValueVespeneCollectionRate = GetInt(statsDic, "m_scoreValueVespeneCollectionRate");
                int scoreValueMineralsUsedInProgressTechnology = GetInt(statsDic, "m_scoreValueMineralsUsedInProgressTechnology");
                int scoreValueMineralsCollectionRate = GetInt(statsDic, "m_scoreValueMineralsCollectionRate");
                int scoreValueWorkersActiveCount = GetInt(statsDic, "m_scoreValueWorkersActiveCount");
                int scoreValueMineralsUsedInProgressArmy = GetInt(statsDic, "m_scoreValueMineralsUsedInProgressArmy");
                int scoreValueVespeneLostArmy = GetInt(statsDic, "m_scoreValueVespeneLostArmy");
                int scoreValueMineralsKilledEconomy = GetInt(statsDic, "m_scoreValueMineralsKilledEconomy");
                int scoreValueMineralsUsedCurrentTechnology = GetInt(statsDic, "m_scoreValueMineralsUsedCurrentTechnology");
                int scoreValueMineralsKilledArmy = GetInt(statsDic, "m_scoreValueMineralsKilledArmy");
                int scoreValueMineralsLostEconomy = GetInt(statsDic, "m_scoreValueMineralsLostEconomy");
                int scoreValueMineralsCurrent = GetInt(statsDic, "m_scoreValueMineralsCurrent");
                int scoreValueMineralsLostArmy = GetInt(statsDic, "m_scoreValueMineralsLostArmy");
                int scoreValueVespeneKilledArmy = GetInt(statsDic, "m_scoreValueVespeneKilledArmy");
                int scoreValueVespeneKilledTechnology = GetInt(statsDic, "m_scoreValueVespeneKilledTechnology");
                int scoreValueVespeneKilledEconomy = GetInt(statsDic, "m_scoreValueVespeneKilledEconomy");
                int scoreValueMineralsUsedActiveForces = GetInt(statsDic, "m_scoreValueMineralsUsedActiveForces");
                int scoreValueVespeneUsedCurrentArmy = GetInt(statsDic, "m_scoreValueVespeneUsedCurrentArmy");
                int scoreValueMineralsFriendlyFireArmy = GetInt(statsDic, "m_scoreValueMineralsFriendlyFireArmy");
                int scoreValueVespeneUsedActiveForces = GetInt(statsDic, "m_scoreValueVespeneUsedActiveForces");
                int scoreValueVespeneCurrent = GetInt(statsDic, "m_scoreValueVespeneCurrent");
                int scoreValueMineralsLostTechnology = GetInt(statsDic, "m_scoreValueMineralsLostTechnology");
                int scoreValueMineralsUsedInProgressEconomy = GetInt(statsDic, "m_scoreValueMineralsUsedInProgressEconomy");
                int scoreValueMineralsFriendlyFireEconomy = GetInt(statsDic, "m_scoreValueMineralsFriendlyFireEconomy");
                int scoreValueVespeneUsedInProgressTechnology = GetInt(statsDic, "m_scoreValueVespeneUsedInProgressTechnology");
                int scoreValueFoodMade = GetInt(statsDic, "m_scoreValueFoodMade");
                int scoreValueMineralsKilledTechnology = GetInt(statsDic, "m_scoreValueMineralsKilledTechnology");
                int scoreValueVespeneLostTechnology = GetInt(statsDic, "m_scoreValueVespeneLostTechnology");
                int scoreValueVespeneFriendlyFireEconomy = GetInt(statsDic, "m_scoreValueVespeneFriendlyFireEconomy");
                int scoreValueVespeneUsedInProgressEconomy = GetInt(statsDic, "m_scoreValueVespeneUsedInProgressEconomy");
                int scoreValueVespeneUsedCurrentEconomy = GetInt(statsDic, "m_scoreValueVespeneUsedCurrentEconomy");
                int scoreValueVespeneFriendlyFireTechnology = GetInt(statsDic, "m_scoreValueVespeneFriendlyFireTechnology");
                int scoreValueFoodUsed = GetInt(statsDic, "m_scoreValueFoodUsed");
                return new SPlayerStatsEvent
                    (
                        trackerEvent,
                        scoreValueVespeneUsedCurrentTechnology,
                        scoreValueVespeneFriendlyFireArmy,
                        scoreValueMineralsFriendlyFireTechnology,
                        scoreValueMineralsUsedCurrentEconomy,
                        scoreValueVespeneLostEconomy,
                        scoreValueMineralsUsedCurrentArmy,
                        scoreValueVespeneUsedInProgressArmy,
                        scoreValueVespeneCollectionRate,
                        scoreValueMineralsUsedInProgressTechnology,
                        scoreValueMineralsCollectionRate,
                        scoreValueWorkersActiveCount,
                        scoreValueMineralsUsedInProgressArmy,
                        scoreValueVespeneLostArmy,
                        scoreValueMineralsKilledEconomy,
                        scoreValueMineralsUsedCurrentTechnology,
                        scoreValueMineralsKilledArmy,
                        scoreValueMineralsLostEconomy,
                        scoreValueMineralsCurrent,
                        scoreValueMineralsLostArmy,
                        scoreValueVespeneKilledArmy,
                        scoreValueVespeneKilledTechnology,
                        scoreValueVespeneKilledEconomy,
                        scoreValueMineralsUsedActiveForces,
                        scoreValueVespeneUsedCurrentArmy,
                        scoreValueMineralsFriendlyFireArmy,
                        scoreValueVespeneUsedActiveForces,
                        scoreValueVespeneCurrent,
                        scoreValueMineralsLostTechnology,
                        scoreValueMineralsUsedInProgressEconomy,
                        scoreValueMineralsFriendlyFireEconomy,
                        scoreValueVespeneUsedInProgressTechnology,
                        scoreValueFoodMade,
                        scoreValueMineralsKilledTechnology,
                        scoreValueVespeneLostTechnology,
                        scoreValueVespeneFriendlyFireEconomy,
                        scoreValueVespeneUsedInProgressEconomy,
                        scoreValueVespeneUsedCurrentEconomy,
                        scoreValueVespeneFriendlyFireTechnology,
                        scoreValueFoodUsed
                    );
            }
        }
        return new SPlayerStatsEvent
            (
                trackerEvent,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0,
                0
            );
    }
}
