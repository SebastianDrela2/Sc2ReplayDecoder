namespace s2ProtocolFurry.Models.InitData
{
    public class UserInitialData
    {
        public UserInitialData(string mount, string skin, int observe, int? teamPref, string toonHandle, long combinedRaceLevels, int highestLeague, string clanTag, bool testMap, bool testAuto, bool examine, int testType, bool customInterface, string clanLogo, string name, int? racePreference, int randomSeed, string hero, long? scaledRating)
        {
            Mount = mount;
            Skin = skin;
            Observe = observe;
            TeamPref = teamPref;
            ToonHandle = toonHandle;
            CombinedRaceLevels = combinedRaceLevels;
            HighestLeague = highestLeague;
            ClanTag = clanTag;
            TestMap = testMap;
            TestAuto = testAuto;
            Examine = examine;
            TestType = testType;
            CustomInterface = customInterface;
            ClanLogo = clanLogo;
            Name = name;
            RacePreference = racePreference;
            RandomSeed = randomSeed;
            Hero = hero;
            ScaledRating = scaledRating;
        }

        public string Mount { get; }
        public string Skin { get; }
        public int Observe { get; }
        public int? TeamPref { get; }
        public string ToonHandle { get; }
        public long CombinedRaceLevels { get; }
        public int HighestLeague { get; }
        public string ClanTag { get; }
        public bool TestMap { get; }
        public bool TestAuto { get; }
        public bool Examine { get; }
        public int TestType { get; }
        public bool CustomInterface { get; }
        public string ClanLogo { get; }
        public string Name { get; }
        public int? RacePreference { get; }
        public int RandomSeed { get; }
        public string Hero { get; }
        public long? ScaledRating { get; }
    }
}