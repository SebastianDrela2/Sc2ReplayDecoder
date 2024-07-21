namespace s2ProtocolFurry.Events.MessageEvents
{
    internal class PingMessageEvent
    {
        public PingMessageEvent(int recipient, int id, int loop, long x, long y)
        {
            Recipient = recipient;
            Id = id;
            Loop = loop;
            X = x;
            Y = y;
        }

        public int Recipient { get; }
        public int Id { get; }
        public int Loop { get; }
        public long X { get; }
        public long Y { get; }
    }
}