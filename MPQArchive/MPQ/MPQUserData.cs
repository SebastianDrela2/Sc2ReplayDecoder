namespace MPQArchive.MPQ;

public class MPQUserData
{
    public uint Magic = 0x1A_51_50_4D;
    public uint UserDataSize;         // Maximum size of the user data
    public uint HeaderPosition;       // Position of the MPQ header, relative to the begin of the shunt 
    public byte[] Content;

    public uint UserDataHeaderSize { get; internal set; }
}    
