using System;

namespace s2ProtocolFurry.Models.InitData
{
    public class GameOptions
    {
        public GameOptions(bool competitive,
                       bool practice,
                       bool lockTeams,
                       bool amm,
                       bool battleNet,
                       int fog,
                       bool noVictoryOrDefeat,
                       bool heroDuplicatesAllowed,
                       int userDifficulty,
                       bool advancedSharedControl,
                       bool cooperative,
                       long clientDebugFlags,
                       int observers,
                       bool teamsTogether,
                       bool randomRaces,
                       bool buildCoachEnabled)
        {
            Competitive = competitive;
            Practice = practice;
            LockTeams = lockTeams;
            Amm = amm;
            BattleNet = battleNet;
            Fog = fog;
            NoVictoryOrDefeat = noVictoryOrDefeat;
            HeroDuplicatesAllowed = heroDuplicatesAllowed;
            UserDifficulty = userDifficulty;
            AdvancedSharedControl = advancedSharedControl;
            Cooperative = cooperative;
            ClientDebugFlags = clientDebugFlags;
            Observers = observers;
            TeamsTogether = teamsTogether;
            RandomRaces = randomRaces;
            BuildCoachEnabled = buildCoachEnabled;
        }

        /// <summary>InitData GameOptions Competitive</summary>
        ///
        public bool Competitive { get; init; }
        /// <summary>InitData GameOptions Practice</summary>
        ///
        public bool Practice { get; init; }
        /// <summary>InitData GameOptions LockTeams</summary>
        ///
        public bool LockTeams { get; init; }
        /// <summary>InitData GameOptions Amm</summary>
        ///
        public bool Amm { get; init; }
        /// <summary>InitData GameOptions BattleNet</summary>
        ///
        public bool BattleNet { get; init; }
        /// <summary>InitData GameOptions Fog</summary>
        ///
        public int Fog { get; init; }
        /// <summary>InitData GameOptions NoVictoryOrDefeat</summary>
        ///
        public bool NoVictoryOrDefeat { get; init; }
        /// <summary>InitData GameOptions HeroDuplicatesAllowed</summary>
        ///
        public bool HeroDuplicatesAllowed { get; init; }
        /// <summary>InitData GameOptions UserDifficulty</summary>
        ///
        public int UserDifficulty { get; init; }
        /// <summary>InitData GameOptions AdvancedSharedControl</summary>
        ///
        public bool AdvancedSharedControl { get; init; }
        /// <summary>InitData GameOptions Cooperative</summary>
        ///
        public bool Cooperative { get; init; }
        /// <summary>InitData GameOptions ClientDebugFlags</summary>
        ///
        public long ClientDebugFlags { get; init; }
        /// <summary>InitData GameOptions Observers</summary>
        ///
        public int Observers { get; init; }
        /// <summary>InitData GameOptions TeamsTogether</summary>
        ///
        public bool TeamsTogether { get; init; }
        /// <summary>InitData GameOptions RandomRaces</summary>
        ///
        public bool RandomRaces { get; init; }
        /// <summary>InitData GameOptions BuildCoachEnabled</summary>
        ///
        public bool BuildCoachEnabled { get; init; }
    }
}