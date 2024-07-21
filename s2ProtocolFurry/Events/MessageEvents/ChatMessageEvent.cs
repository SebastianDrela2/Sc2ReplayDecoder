namespace s2ProtocolFurry.Events.MessageEvents
{
    internal class ChatMessageEvent
    {
        public ChatMessageEvent(int recipient, int id, string msg, int loop)
        {
            Recipient = recipient;
            Id = id;
            Msg = msg;
            Loop = loop;
        }

        public int Recipient { get; }
        public int Id { get; }
        public string Msg { get; }
        public int Loop { get; }
    }
}