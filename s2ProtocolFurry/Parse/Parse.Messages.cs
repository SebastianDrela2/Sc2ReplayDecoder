using s2ProtocolFurry.Events.MessageEvents;

namespace s2ProtocolFurry.Parse
{
    public static partial class Parse
    {
        public static void SetMessages(IEnumerable<Dictionary<string, object>> dicList, Sc2Replay replay)
        {
            List<ChatMessageEvent> messages = [];
            List<PingMessageEvent> pings = [];

            foreach (var dic in dicList)
            {
                var _event = GetString(dic, "_event");
                if (_event == "NNet.Game.SChatMessage")
                {
                    var recipient = GetInt(dic, "m_recipient");
                    var id = GetChatMessageId(dic);
                    var msg = GetString(dic, "m_string");
                    var loop = GetInt(dic, "_gameloop");
                    messages.Add(new ChatMessageEvent(recipient, id, msg, loop));
                }
                else if (_event == "NNet.Game.SPingMessage")
                {
                    var recipient = GetInt(dic, "m_recipient");
                    var id = GetChatMessageId(dic);
                    var loop = GetInt(dic, "_gameloop");
                    (var x, var y) = GetXYCoords(dic);
                    pings.Add(new(recipient, id, loop, x, y));
                }
            }
            replay.ChatMessages = messages;
            replay.PingMessages = pings;
        }

        private static int GetChatMessageId(Dictionary<string, object> dic)
        {
            if (dic.ContainsKey("_userid"))
            {
                if (dic["_userid"] is Dictionary<string, object> usrdic)
                {
                    return GetInt(usrdic, "m_userId");
                }
            }
            return 0;
        }

        private static (long, long) GetXYCoords(Dictionary<string, object> pydic)
        {
            if (pydic.ContainsKey("m_point"))
            {
                if (pydic["m_point"] is Dictionary<string, object> coorddic)
                {
                    var x = GetBigInt(coorddic, "x");
                    var y = GetBigInt(coorddic, "y");
                    return (x, y);
                }
            }
            return (0, 0);
        }
    }
}
