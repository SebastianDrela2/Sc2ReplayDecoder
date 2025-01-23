using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;
using s2ProtocolFurry.Events;
using s2ProtocolFurry.Events.TrackerEvents;

namespace s2ProtocolFurry.Parse;

public static partial class Parse
{
    public static IEnumerable<T> Tracker<Transformer, T>(Transformer transformer, IEnumerable<Dictionary<string, object>> eventDicList)
        where Transformer : IEventTransformer<T>
    {
        return eventDicList.Select(obj =>
        {
            EventHeader header = new () {
                PlayerId = GetInt(obj, "m_playerId"),
                Bits = GetInt(obj, "_bits"),
                Gameloop = GetUInt(obj, "_gameloop")
            };
            return GetString(obj, "_event") switch
            {
                "NNet.Replay.Tracker.SPlayerSetupEvent" => transformer.Transform(header, GetSPlayerSetupEvent(obj)),
                "NNet.Replay.Tracker.SPlayerStatsEvent" => transformer.Transform(header, GetSPlayerStatsEvent(obj)),
                "NNet.Replay.Tracker.SUnitBornEvent" => transformer.Transform(header, GetSUnitBornEvent(obj)),
                "NNet.Replay.Tracker.SUnitDiedEvent" => transformer.Transform(header, GetSUnitDiedEvent(obj)),
                "NNet.Replay.Tracker.SUnitOwnerChangeEvent" => transformer.Transform(header, GetSUnitOwnerChangeEvent(obj)),
                "NNet.Replay.Tracker.SUnitPositionsEvent" => transformer.Transform(header, GetSUnitPositionsEvent(obj)),
                "NNet.Replay.Tracker.SUnitTypeChangeEvent" => transformer.Transform(header, GetSUnitTypeChangeEvent(obj)),
                "NNet.Replay.Tracker.SUpgradeEvent" => transformer.Transform(header, GetSUpgradeEvent(obj)),
                "NNet.Replay.Tracker.SUnitInitEvent" => transformer.Transform(header, GetSUnitInitEvent(obj)),
                "NNet.Replay.Tracker.SUnitDoneEvent" => transformer.Transform(header, GetSUnitDoneEvent(obj)),
                var evType => transformer.Tranform_Default(header, evType),
            };
        });
    }
    internal static void SetTrackerEventsUnitConnections(TrackerEvents trackerEvents)
    {
        trackerEvents.SUnitBornEvents.Data.Select(x => x.SUnitDiedEvent = trackerEvents.SUnitDiedEvents.Data.FirstOrDefault(f => f.UnitIndex == x.UnitIndex));
        trackerEvents.SUnitInitEvents.Data.Select(x => x.SUnitDiedEvent = trackerEvents.SUnitDiedEvents.Data.FirstOrDefault(f => f.UnitIndex == x.UnitIndex));
        trackerEvents.SUnitInitEvents.Data.Select(x => x.SUnitDoneEvent = trackerEvents.SUnitDoneEvents.Data.FirstOrDefault(f => f.UnitIndex == x.UnitIndex));
        trackerEvents.SUnitDiedEvents.Data.Select(x => x.KillerUnitBornEvent = trackerEvents.SUnitBornEvents.Data.FirstOrDefault(f => f.UnitTagIndex == x.KillerUnitTagIndex && f.UnitTagRecycle == x.KillerUnitTagRecycle));
        trackerEvents.SUnitDiedEvents.Data.Select(x => x.KillerUnitInitEvent = trackerEvents.SUnitInitEvents.Data.FirstOrDefault(f => f.UnitTagIndex == x.KillerUnitTagIndex && f.UnitTagRecycle == x.KillerUnitTagRecycle));
    }

    private static SUnitDoneEvent GetSUnitDoneEvent(Dictionary<string, object> dic) => new()
    {
        UnitTagIndex = GetInt(dic, "m_unitTagIndex"),
        UnitTagRecycle = GetInt(dic, "m_unitTagRecycle"),
    };

    private static SUnitInitEvent GetSUnitInitEvent(Dictionary<string, object> dic) => new()
    {
        UnitTagIndex = GetInt(dic, "m_unitTagIndex"),
        UnitTagRecycle = GetInt(dic, "m_unitTagRecycle"),
        UnitTypeName = GetString(dic, "m_unitTypeName"),
        X = GetInt(dic, "m_controlPlayerId"),
        Y = GetInt(dic, "m_x"),
        UpkeepPlayerId = GetInt(dic, "m_y"),
        ControlPlayerId = GetInt(dic, "m_upkeepPlayerId"),
    };

    private static SUpgradeEvent GetSUpgradeEvent(Dictionary<string, object> dic) => new()
    {
        Count = GetInt(dic, "m_count"),
        UpgradeTypeName = GetString(dic, "m_upgradeTypeName"),
    };

    private static SUnitTypeChangeEvent GetSUnitTypeChangeEvent(Dictionary<string, object> dic) => new()
    {
        UnitTagIndex = GetInt(dic, "m_unitTagIndex"),
        UnitTagRecycle = GetInt(dic, "m_unitTagRecycle"),
        UnitTypeName = GetString(dic, "m_unitTypeName"),
    };

    private static SUnitPositionsEvent GetSUnitPositionsEvent(Dictionary<string, object> dic) => new()
    {
        FirstUnitIndex = GetInt(dic, "m_firstUnitIndex"),
        Ints = dic.GetValueOrDefault("m_items") is IEnumerable<object> items ? [.. items.OfType<int>()] : [],
    };

    private static SUnitOwnerChangeEvent GetSUnitOwnerChangeEvent(Dictionary<string, object> dic) => new()
    {
        UnitTagIndex = GetInt(dic, "m_unitTagIndex"),
        UnitTagRecycle = GetInt(dic, "m_unitTagRecycle"),
        ControlPlayerId = GetInt(dic, "m_controlPlayerId"),
        UpkeepPlayerId = GetInt(dic, "m_upkeepPlayerId"),
    };

    private static SUnitDiedEvent GetSUnitDiedEvent(Dictionary<string, object> dic) => new()
    {
        UnitTagIndex = GetInt(dic, "m_unitTagIndex"),
        UnitTagRecycle = GetInt(dic, "m_unitTagRecycle"),
        KillerPlayerId = GetNullableInt(dic, "m_killerPlayerId"),
        X = GetInt(dic, "m_x"),
        Y = GetInt(dic, "m_y"),
        KillerUnitTagRecycle = GetNullableInt(dic, "m_killerUnitTagRecycle"),
        KillerUnitTagIndex = GetNullableInt(dic, "m_killerUnitTagIndex"),
    };

    private static SUnitBornEvent GetSUnitBornEvent(Dictionary<string, object> dic) => new()
    {
        UnitTagIndex = GetInt(dic, "m_unitTagIndex"),
        UnitTagRecycle = GetInt(dic, "m_unitTagRecycle"),
        CreatorAbilityName = GetNullableString(dic, "m_creatorAbilityName"),
        CreatorUnitTagRecycle = GetNullableInt(dic, "m_creatorUnitTagRecycle"),
        ControlPlayerId = GetInt(dic, "m_controlPlayerId"),
        X = GetInt(dic, "m_x"),
        Y = GetInt(dic, "m_y"),
        UpkeepPlayerId = GetInt(dic, "m_upkeepPlayerId"),
        UnitTypeName = GetString(dic, "m_unitTypeName"),
        CreatorUnitTagIndex = GetNullableInt(dic, "m_creatorUnitTagIndex"),
    };

    private static SPlayerSetupEvent GetSPlayerSetupEvent(Dictionary<string, object> dic) => new()
    {
        Type = GetInt(dic, "m_type"),
        UserId = GetNullableInt(dic, "m_userId"),
        SlotId = GetInt(dic, "m_slotId")
    };

    private static SPlayerStatsEvent GetSPlayerStatsEvent(Dictionary<string, object> dic) =>
        dic.GetValueOrDefault("m_stats") is Dictionary<string, object> statsDic ? new()
        {
            ScoreValueVespeneUsedCurrentTechnology = GetInt(statsDic, "m_scoreValueVespeneUsedCurrentTechnology"),
            ScoreValueVespeneFriendlyFireArmy = GetInt(statsDic, "m_scoreValueVespeneFriendlyFireArmy"),
            ScoreValueMineralsFriendlyFireTechnology = GetInt(statsDic, "m_scoreValueMineralsFriendlyFireTechnology"),
            ScoreValueMineralsUsedCurrentEconomy = GetInt(statsDic, "m_scoreValueMineralsUsedCurrentEconomy"),
            ScoreValueVespeneLostEconomy = GetInt(statsDic, "m_scoreValueVespeneLostEconomy"),
            ScoreValueMineralsUsedCurrentArmy = GetInt(statsDic, "m_scoreValueMineralsUsedCurrentArmy"),
            ScoreValueVespeneUsedInProgressArmy = GetInt(statsDic, "m_scoreValueVespeneUsedInProgressArmy"),
            ScoreValueVespeneCollectionRate = GetInt(statsDic, "m_scoreValueVespeneCollectionRate"),
            ScoreValueMineralsUsedInProgressTechnology = GetInt(statsDic, "m_scoreValueMineralsUsedInProgressTechnology"),
            ScoreValueMineralsCollectionRate = GetInt(statsDic, "m_scoreValueMineralsCollectionRate"),
            ScoreValueWorkersActiveCount = GetInt(statsDic, "m_scoreValueWorkersActiveCount"),
            ScoreValueMineralsUsedInProgressArmy = GetInt(statsDic, "m_scoreValueMineralsUsedInProgressArmy"),
            ScoreValueVespeneLostArmy = GetInt(statsDic, "m_scoreValueVespeneLostArmy"),
            ScoreValueMineralsKilledEconomy = GetInt(statsDic, "m_scoreValueMineralsKilledEconomy"),
            ScoreValueMineralsUsedCurrentTechnology = GetInt(statsDic, "m_scoreValueMineralsUsedCurrentTechnology"),
            ScoreValueMineralsKilledArmy = GetInt(statsDic, "m_scoreValueMineralsKilledArmy"),
            ScoreValueMineralsLostEconomy = GetInt(statsDic, "m_scoreValueMineralsLostEconomy"),
            ScoreValueMineralsCurrent = GetInt(statsDic, "m_scoreValueMineralsCurrent"),
            ScoreValueMineralsLostArmy = GetInt(statsDic, "m_scoreValueMineralsLostArmy"),
            ScoreValueVespeneKilledArmy = GetInt(statsDic, "m_scoreValueVespeneKilledArmy"),
            ScoreValueVespeneKilledTechnology = GetInt(statsDic, "m_scoreValueVespeneKilledTechnology"),
            ScoreValueVespeneKilledEconomy = GetInt(statsDic, "m_scoreValueVespeneKilledEconomy"),
            ScoreValueMineralsUsedActiveForces = GetInt(statsDic, "m_scoreValueMineralsUsedActiveForces"),
            ScoreValueVespeneUsedCurrentArmy = GetInt(statsDic, "m_scoreValueVespeneUsedCurrentArmy"),
            ScoreValueMineralsFriendlyFireArmy = GetInt(statsDic, "m_scoreValueMineralsFriendlyFireArmy"),
            ScoreValueVespeneUsedActiveForces = GetInt(statsDic, "m_scoreValueVespeneUsedActiveForces"),
            ScoreValueVespeneCurrent = GetInt(statsDic, "m_scoreValueVespeneCurrent"),
            ScoreValueMineralsLostTechnology = GetInt(statsDic, "m_scoreValueMineralsLostTechnology"),
            ScoreValueMineralsUsedInProgressEconomy = GetInt(statsDic, "m_scoreValueMineralsUsedInProgressEconomy"),
            ScoreValueMineralsFriendlyFireEconomy = GetInt(statsDic, "m_scoreValueMineralsFriendlyFireEconomy"),
            ScoreValueVespeneUsedInProgressTechnology = GetInt(statsDic, "m_scoreValueVespeneUsedInProgressTechnology"),
            ScoreValueFoodMade = GetInt(statsDic, "m_scoreValueFoodMade"),
            ScoreValueMineralsKilledTechnology = GetInt(statsDic, "m_scoreValueMineralsKilledTechnology"),
            ScoreValueVespeneLostTechnology = GetInt(statsDic, "m_scoreValueVespeneLostTechnology"),
            ScoreValueVespeneFriendlyFireEconomy = GetInt(statsDic, "m_scoreValueVespeneFriendlyFireEconomy"),
            ScoreValueVespeneUsedInProgressEconomy = GetInt(statsDic, "m_scoreValueVespeneUsedInProgressEconomy"),
            ScoreValueVespeneUsedCurrentEconomy = GetInt(statsDic, "m_scoreValueVespeneUsedCurrentEconomy"),
            ScoreValueVespeneFriendlyFireTechnology = GetInt(statsDic, "m_scoreValueVespeneFriendlyFireTechnology"),
            ScoreValueFoodUsed = GetInt(statsDic, "m_scoreValueFoodUsed")
        } : new();
}
