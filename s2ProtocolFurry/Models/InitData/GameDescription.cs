namespace s2ProtocolFurry.Models.InitData;

public class GameDescription
{
    public GameDescription(int maxRaces,
                           int maxTeams,
                           bool hasExtensionMod,
                           int maxColors,
                           bool isBlizzardMap,
                           GameOptions gameOptions,
                           int defaultDifficulty,
                           bool isCoopMode,
                           string mapFileName,
                           int defaultAIBuild,
                           int gameType,
                           bool hasNonBlizzardExtensionMod,
                           long randomValue,
                           int maxObservers,
                           bool isRealtimeMode,
                           int maxUsers,
                           long modFileSyncChecksum,
                           long mapFileSyncChecksum,
                           int maxPlayers,
                           ICollection<string> cacheHandles,
                           int gameSpeed,
                           int maxControls,
                           string gameCacheName,
                           string mapAuthorName,
                           ICollection<SlotDescription> slotDescriptions,
                           int mapSizeY,
                           int mapSizeX,
                           bool isPremadeFFA)
    {
        MaxRaces = maxRaces;
        MaxTeams = maxTeams;
        HasExtensionMod = hasExtensionMod;
        MaxColors = maxColors;
        IsBlizzardMap = isBlizzardMap;
        GameOptions = gameOptions;
        DefaultDifficulty = defaultDifficulty;
        IsCoopMode = isCoopMode;
        MapFileName = mapFileName;
        DefaultAIBuild = defaultAIBuild;
        GameType = gameType;
        HasNonBlizzardExtensionMod = hasNonBlizzardExtensionMod;
        RandomValue = randomValue;
        MaxObservers = maxObservers;
        IsRealtimeMode = isRealtimeMode;
        MaxUsers = maxUsers;
        ModFileSyncChecksum = modFileSyncChecksum;
        MapFileSyncChecksum = mapFileSyncChecksum;
        MaxPlayers = maxPlayers;
        CacheHandles = cacheHandles;
        GameSpeed = gameSpeed;
        MaxControls = maxControls;
        GameCacheName = gameCacheName;
        MapAuthorName = mapAuthorName;
        SlotDescriptions = slotDescriptions;
        MapSizeY = mapSizeY;
        MapSizeX = mapSizeX;
        IsPremadeFFA = isPremadeFFA;
    }

    public int MaxRaces { get; init; }
    /// <summary>InitData GameDescription MaxTeams</summary>
    ///
    public int MaxTeams { get; init; }
    /// <summary>InitData GameDescription HasExtensionMod</summary>
    ///
    public bool HasExtensionMod { get; init; }
    /// <summary>InitData GameDescription MaxColors</summary>
    ///
    public int MaxColors { get; init; }
    /// <summary>InitData GameDescription IsBlizzardMap</summary>
    ///
    public bool IsBlizzardMap { get; init; }
    /// <summary>InitData GameDescription GameOptions</summary>
    ///
    public GameOptions GameOptions { get; init; }
    /// <summary>InitData GameDescription DefaultDifficulty</summary>
    ///
    public int DefaultDifficulty { get; init; }
    /// <summary>InitData GameDescription IsCoopMode</summary>
    ///
    public bool IsCoopMode { get; init; }
    /// <summary>InitData GameDescription MapFileName</summary>
    ///
    public string MapFileName { get; init; }
    /// <summary>InitData GameDescription DefaultAIBuild</summary>
    ///
    public int DefaultAIBuild { get; init; }
    /// <summary>InitData GameDescription GameType</summary>
    ///
    public int GameType { get; init; }
    /// <summary>InitData GameDescription HasNonBlizzardExtensionMod</summary>
    ///
    public bool HasNonBlizzardExtensionMod { get; init; }
    /// <summary>InitData GameDescription RandomValue</summary>
    ///
    public long RandomValue { get; init; }
    /// <summary>InitData GameDescription MaxObservers</summary>
    ///
    public int MaxObservers { get; init; }
    /// <summary>InitData GameDescription IsRealtimeMode</summary>
    ///
    public bool IsRealtimeMode { get; init; }
    /// <summary>InitData GameDescription MaxUsers</summary>
    ///
    public int MaxUsers { get; init; }
    /// <summary>InitData GameDescription ModFileSyncChecksum</summary>
    ///
    public long ModFileSyncChecksum { get; init; }
    /// <summary>InitData GameDescription MapFileSyncChecksum</summary>
    ///
    public long MapFileSyncChecksum { get; init; }
    /// <summary>InitData GameDescription MaxPlayers</summary>
    ///
    public int MaxPlayers { get; init; }
    /// <summary>InitData GameDescription CacheHandles</summary>
    ///
    public ICollection<string> CacheHandles { get; init; }
    /// <summary>InitData GameDescription GameSpeed</summary>
    ///
    public int GameSpeed { get; init; }
    /// <summary>InitData GameDescription MaxControls</summary>
    ///
    public int MaxControls { get; init; }
    /// <summary>InitData GameDescription GameCacheName</summary>
    ///
    public string GameCacheName { get; init; }
    /// <summary>InitData GameDescription MapAuthorName</summary>
    ///
    public string MapAuthorName { get; init; }
    /// <summary>InitData GameDescription SlotDescriptions</summary>
    ///
    public ICollection<SlotDescription> SlotDescriptions { get; init; }
    /// <summary>InitData GameDescription MapSizeY</summary>
    ///
    public int MapSizeY { get; init; }
    /// <summary>InitData GameDescription MapSizeX</summary>
    ///
    public int MapSizeX { get; init; }
    /// <summary>InitData GameDescription IsPremadeFFA</summary>
    ///
    public bool IsPremadeFFA { get; init; }
}