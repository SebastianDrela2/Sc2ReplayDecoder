using s2ProtocolFurry.Events.TrackerEvents;

namespace s2ProtocolFurry.Parse;

public static partial class Parse
{
    public static TrackerEvents Tracker(IEnumerable<Dictionary<string, object>> eventDicList)
    {
        List<TrackerEvent> trackerevents = new();

        foreach (var eventDic in eventDicList)
        {
            TrackerEvent trackerEvent = GetTrackerEvent(eventDic);

            TrackerEvent detailEvent = trackerEvent.EventType switch
            {
                TrackerEventType.SPlayerSetupEvent => GetSPlayerSetupEvent(eventDic, trackerEvent),
                TrackerEventType.SPlayerStatsEvent => GetSPlayerStatsEvent(eventDic, trackerEvent),
                TrackerEventType.SUnitBornEvent => GetSUnitBornEvent(eventDic, trackerEvent),
                TrackerEventType.SUnitDiedEvent => GetSUnitDiedEvent(eventDic, trackerEvent),
                TrackerEventType.SUnitOwnerChangeEvent => GetSUnitOwnerChangeEvent(eventDic, trackerEvent),
                TrackerEventType.SUnitPositionsEvent => GetSUnitPositionsEvent(eventDic, trackerEvent),
                TrackerEventType.SUnitTypeChangeEvent => GetSUnitTypeChangeEvent(eventDic, trackerEvent),
                TrackerEventType.SUpgradeEvent => GetSUpgradeEvent(eventDic, trackerEvent),
                TrackerEventType.SUnitInitEvent => GetSUnitInitEvent(eventDic, trackerEvent),
                TrackerEventType.SUnitDoneEvent => GetSUnitDoneEvent(eventDic, trackerEvent),
                _ => GetUnknownEvent(eventDic, trackerEvent)
            };
            trackerevents.Add(detailEvent);
        }

        return new TrackerEvents(trackerevents);
    }

    internal static void SetTrackerEventsUnitConnections(TrackerEvents trackerEvents)
    {
        foreach (SUnitInitEvent x in trackerEvents.SUnitInitEvents)
        {
            x.SUnitDiedEvent = trackerEvents.SUnitDiedEvents.GetValueOrDefault(x.UnitIndex);
        }
        foreach (SUnitInitEvent x in trackerEvents.SUnitInitEvents)
        {
            x.SUnitDiedEvent = trackerEvents.SUnitDiedEvents.GetValueOrDefault(x.UnitIndex);
            x.SUnitDoneEvent = trackerEvents.SUnitDoneEvents.GetValueOrDefault(x.UnitIndex);
        }
        foreach (var (_, x) in trackerEvents.SUnitDiedEvents)
        {
            x.KillerUnitBornEvent = trackerEvents.SUnitBornEvents.FirstOrDefault(f => f.UnitTagIndex == x.KillerUnitTagIndex && f.UnitTagRecycle == x.KillerUnitTagRecycle);
            x.KillerUnitInitEvent = trackerEvents.SUnitInitEvents.FirstOrDefault(f => f.UnitTagIndex == x.KillerUnitTagIndex && f.UnitTagRecycle == x.KillerUnitTagRecycle);
        }
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
