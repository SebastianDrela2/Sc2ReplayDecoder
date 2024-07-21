namespace s2ProtocolFurry.Protocol
{
    internal interface IProtocol
    {
        List<KeyValuePair<string, object>> TypeInfos { get; }
    }
}
