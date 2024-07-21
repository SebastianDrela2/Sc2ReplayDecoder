namespace s2ProtocolFurry.Protocol
{
    public class ProtocolTypeInfo
    {
        public string Type { get; set; }
        public object[] Arguments { get; set; }

        public ProtocolTypeInfo(string type, object[] arguments)
        {
            Type = type;
            Arguments = arguments;
        }

        public override string ToString()
        {
            return $"{Type}: {string.Join(", ", Arguments)}";
        }
    }
}
