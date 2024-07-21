namespace s2ProtocolFurry.Events.GameEvents
{
    internal class STriggerChatMessageEvent
    {
        public STriggerChatMessageEvent(GameEvent gameEvent, string chatMessage)
        {
            GameEvent = gameEvent;
            ChatMessage = chatMessage;
        }

        public GameEvent GameEvent { get; }
        public string ChatMessage { get; }
    }
}