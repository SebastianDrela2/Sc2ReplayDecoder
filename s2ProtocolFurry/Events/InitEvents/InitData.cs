using s2ProtocolFurry.Models.InitData;

namespace s2ProtocolFurry.Events.InitEvents
{
    public class InitData
    {
        public InitData(List<UserInitialData> userInitialDatas, LobbyState lobbyState, GameDescription gameDescription)
        {
            UserInitialDatas = userInitialDatas;
            LobbyState = lobbyState;
            GameDescription = gameDescription;
        }

        public List<UserInitialData> UserInitialDatas { get; }
        public LobbyState LobbyState { get; }
        public GameDescription GameDescription { get; }
    }
}