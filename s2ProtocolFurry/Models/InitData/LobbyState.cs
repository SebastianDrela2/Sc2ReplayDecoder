
namespace s2ProtocolFurry.Models.InitData
{
    public class LobbyState
    {
        public LobbyState(int maxUser, List<Slot> slots, int defaultDifficulty, bool isSinglePlayer, int phase, int? hostUserId, int maxObs, int defaultAIBuild, int pickedMapTag, long randomSeed, int gameDuration)
        {
            MaxUser = maxUser;
            Slots = slots;
            DefaultDifficulty = defaultDifficulty;
            IsSinglePlayer = isSinglePlayer;
            Phase = phase;
            HostUserId = hostUserId;
            MaxObs = maxObs;
            DefaultAIBuild = defaultAIBuild;
            PickedMapTag = pickedMapTag;
            RandomSeed = randomSeed;
            GameDuration = gameDuration;
        }

        public int MaxUser { get; }
        public List<Slot> Slots { get; }
        public int DefaultDifficulty { get; }
        public bool IsSinglePlayer { get; }
        public int Phase { get; }
        public int? HostUserId { get; }
        public int MaxObs { get; }
        public int DefaultAIBuild { get; }
        public int PickedMapTag { get; }
        public long RandomSeed { get; }
        public int GameDuration { get; }
    }
}