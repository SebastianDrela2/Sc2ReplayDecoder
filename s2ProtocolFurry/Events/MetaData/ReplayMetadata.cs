using s2ProtocolFurry.Events.MetaData.s2protocol.NET.Models;

namespace s2ProtocolFurry.Events.MetaData;

public sealed record ReplayMetadata
{
    public ReplayMetadata()
    {

    }

    public ReplayMetadata(
        string baseBuild,
        string dataBuild,
        string dataVersion,
        int duration,
        Version gameVersion,
        bool isNotAvailable,
        string title,
        ICollection<MetadataPlayer> players)
    {
        BaseBuild = baseBuild;
        DataBuild = dataBuild;
        DataVersion = dataVersion;
        Duration = duration;
        GameVersion = gameVersion;
        IsNotAvailable = isNotAvailable;
        Title = title;
        Players = players;
    }
    
    public string BaseBuild { get; init; }
    
    public string DataBuild { get; init; }
    
    public string DataVersion { get; init; }
    
    public int Duration { get; init; }
    
    public Version GameVersion { get; init; }
    
    public bool IsNotAvailable { get; init; }
    
    public string Title { get; init; }
   
    public ICollection<MetadataPlayer> Players { get; init; }
}
