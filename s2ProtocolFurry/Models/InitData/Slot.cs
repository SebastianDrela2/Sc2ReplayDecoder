
namespace s2ProtocolFurry.Models.InitData
{
    public class Slot
    {
        public Slot(int aCEnemyRace, string toonHandle, List<int> rewardOverrides, int? userId, string skin, List<int> commanderMasteryTalents, int aiBuild, int teamId, List<int> rewards, int commanderLevel, int logoIndex, List<string> artifacts, int difficulty, int? tandemLeaderId, int commanderMasteryLevel, int trophyId, int brutalPlusDifficulty, int? racePref, int? tandemId, string hero, string commander, string mount, int handicap, int observe, int aCEnemyWaveType, int control, List<int> licenses, int? colorPref, bool hasSilencePenalty, int workingSetSlotId, List<int> retryMutationIndexes, int selectedCommanderPrestige)
        {
            ACEnemyRace = aCEnemyRace;
            ToonHandle = toonHandle;
            RewardOverrides = rewardOverrides;
            UserId = userId;
            Skin = skin;
            CommanderMasteryTalents = commanderMasteryTalents;
            AiBuild = aiBuild;
            TeamId = teamId;
            Rewards = rewards;
            CommanderLevel = commanderLevel;
            LogoIndex = logoIndex;
            Artifacts = artifacts;
            Difficulty = difficulty;
            TandemLeaderId = tandemLeaderId;
            CommanderMasteryLevel = commanderMasteryLevel;
            TrophyId = trophyId;
            BrutalPlusDifficulty = brutalPlusDifficulty;
            RacePref = racePref;
            TandemId = tandemId;
            Hero = hero;
            Commander = commander;
            Mount = mount;
            Handicap = handicap;
            Observe = observe;
            ACEnemyWaveType = aCEnemyWaveType;
            Control = control;
            Licenses = licenses;
            ColorPref = colorPref;
            HasSilencePenalty = hasSilencePenalty;
            WorkingSetSlotId = workingSetSlotId;
            RetryMutationIndexes = retryMutationIndexes;
            SelectedCommanderPrestige = selectedCommanderPrestige;
        }

        public int ACEnemyRace { get; }
        public string ToonHandle { get; }
        public List<int> RewardOverrides { get; }
        public int? UserId { get; }
        public string Skin { get; }
        public List<int> CommanderMasteryTalents { get; }
        public int AiBuild { get; }
        public int TeamId { get; }
        public List<int> Rewards { get; }
        public int CommanderLevel { get; }
        public int LogoIndex { get; }
        public List<string> Artifacts { get; }
        public int Difficulty { get; }
        public int? TandemLeaderId { get; }
        public int CommanderMasteryLevel { get; }
        public int TrophyId { get; }
        public int BrutalPlusDifficulty { get; }
        public int? RacePref { get; }
        public int? TandemId { get; }
        public string Hero { get; }
        public string Commander { get; }
        public string Mount { get; }
        public int Handicap { get; }
        public int Observe { get; }
        public int ACEnemyWaveType { get; }
        public int Control { get; }
        public List<int> Licenses { get; }
        public int? ColorPref { get; }
        public bool HasSilencePenalty { get; }
        public int WorkingSetSlotId { get; }
        public List<int> RetryMutationIndexes { get; }
        public int SelectedCommanderPrestige { get; }
    }
}