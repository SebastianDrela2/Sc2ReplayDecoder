namespace s2ProtocolFurry.Protocol
{
    public class Protocol92440 : IProtocol
    {
        public List<KeyValuePair<string, object>> TypeInfos { get; } = new List<KeyValuePair<string, object>>
        {
            new KeyValuePair<string, object>(
            "_int",
            new List<Tuple<int, int>> { new Tuple<int, int>(0, 7) }
        ),  // 0

            new KeyValuePair<string, object>(
            "_int",
            new List<Tuple<int, int>> { new Tuple<int, int>(0, 4) }
        ),  // 1

            new KeyValuePair<string, object>(
            "_int",
            new List<Tuple<int, int>> { new Tuple<int, int>(0, 5) }
        ),  // 2

            new KeyValuePair<string, object>(
            "_int",
            new List<Tuple<int, int>> { new Tuple<int, int>(0, 6) }
        ),  // 3

            new KeyValuePair<string, object>(
            "_int",
            new List<Tuple<int, int>> { new Tuple<int, int>(0, 14) }
        ),  // 4

            new KeyValuePair<string, object>(
            "_int",
            new List<Tuple<int, int>> { new Tuple<int, int>(0, 22) }
        ),  // 5

            new KeyValuePair<string, object>(
            "_int",
            new List<Tuple<int, int>> { new Tuple<int, int>(0, 32) }
        ),  // 6

            new KeyValuePair<string, object>(
            "_choice",
            new Tuple<Tuple<int, int>, Dictionary<int, Tuple<string, int>>>(
                new Tuple<int, int>(0, 2),
                new Dictionary<int, Tuple<string, int>>
                {
                    { 0, new Tuple<string, int>("m_uint6", 3) },
                    { 1, new Tuple<string, int>("m_uint14", 4) },
                    { 2, new Tuple<string, int>("m_uint22", 5) },
                    { 3, new Tuple<string, int>("m_uint32", 6) }
                }
            )
        ),  // 7

            new KeyValuePair<string, object>(
            "_struct",
            new List<Tuple<string, int, int>>
            {
                new Tuple<string, int, int>("m_userId", 2, -1)
            }
        ),  // 8

            new KeyValuePair<string, object>(
            "_blob",
            new List<Tuple<int, int>> { new Tuple<int, int>(0, 8) }
        ),  // 9

            new KeyValuePair<string, object>(
            "_int",
            new List<Tuple<int, int>> { new Tuple<int, int>(0, 8) }
        ),  // 10

            new KeyValuePair<string, object>(
            "_struct",
            new List<Tuple<string, int, int>>
            {
                new Tuple<string, int, int>("m_flags", 10, 0),
                new Tuple<string, int, int>("m_major", 10, 1),
                new Tuple<string, int, int>("m_minor", 10, 2),
                new Tuple<string, int, int>("m_revision", 10, 3),
                new Tuple<string, int, int>("m_build", 6, 4),
                new Tuple<string, int, int>("m_baseBuild", 6, 5)
            }
        ),  // 11

            new KeyValuePair<string, object>(
            "_int",
            new List<Tuple<int, int>> { new Tuple<int, int>(0, 3) }
        ),  // 12

            new KeyValuePair<string, object>(
            "_bool",
            new List<object>()
        ),  // 13

            new KeyValuePair<string, object>(
            "_array",
            new Tuple<Tuple<int, int>, int>(new Tuple<int, int>(16, 0), 10)
        ),  // 14

            new KeyValuePair<string, object>(
            "_optional",
            new List<int> { 14 }
        ),  // 15

            new KeyValuePair<string, object>(
            "_blob",
            new List<Tuple<int, int>> { new Tuple<int, int>(16, 0) }
        ),  // 16

            new KeyValuePair<string, object>(
            "_struct",
            new List<Tuple<string, int, int>>
            {
                new Tuple<string, int, int>("m_dataDeprecated", 15, 0),
                new Tuple<string, int, int>("m_data", 16, 1)
            }
        ),  // 17

            new KeyValuePair<string, object>(
            "_struct",
            new List<Tuple<string, int, int>>
            {
                new Tuple<string, int, int>("m_signature", 9, 0),
                new Tuple<string, int, int>("m_version", 11, 1),
                new Tuple<string, int, int>("m_type", 12, 2),
                new Tuple<string, int, int>("m_elapsedGameLoops", 6, 3),
                new Tuple<string, int, int>("m_useScaledTime", 13, 4),
                new Tuple<string, int, int>("m_ngdpRootKey", 17, 5),
                new Tuple<string, int, int>("m_dataBuildNum", 6, 6),
                new Tuple<string, int, int>("m_replayCompatibilityHash", 17, 7),
                new Tuple<string, int, int>("m_ngdpRootKeyIsDevData", 13, 8)
            }
        ),  // 18

            new KeyValuePair<string, object>(
            "_fourcc",
            new List<object>()
        ),  // 19

            new KeyValuePair<string, object>(
            "_blob",
            new List<Tuple<int, int>> { new Tuple<int, int>(0, 7) }
        ),  // 20

            new KeyValuePair<string, object>(
            "_int",
            new List<Tuple<int, int>> { new Tuple<int, int>(0, 64) }
        ),  // 21

            new KeyValuePair<string, object>(
            "_struct",
            new List<Tuple<string, int, int>>
            {
                new Tuple<string, int, int>("m_region", 10, 0),
                new Tuple<string, int, int>("m_programId", 19, 1),
                new Tuple<string, int, int>("m_realm", 6, 2),
                new Tuple<string, int, int>("m_name", 20, 3),
                new Tuple<string, int, int>("m_id", 21, 4)
            }
        ),  // 22

            new KeyValuePair<string, object>(
            "_struct",
            new List<Tuple<string, int, int>>
            {
                new Tuple<string, int, int>("m_a", 10, 0),
                new Tuple<string, int, int>("m_r", 10, 1),
                new Tuple<string, int, int>("m_g", 10, 2),
                new Tuple<string, int, int>("m_b", 10, 3)
            }
        ),  // 23

            new KeyValuePair<string, object>(
            "_int",
            new List<Tuple<int, int>> { new Tuple<int, int>(0, 2) }
        ),  // 24

            new KeyValuePair<string, object>(
            "_optional",
            new List<int> { 10 }
        ),  // 25

            new KeyValuePair<string, object>(
            "_struct",
            new List<Tuple<string, int, int>>
            {
                new Tuple<string, int, int>("m_name", 9, 0),
                new Tuple<string, int, int>("m_toon", 22, 1),
                new Tuple<string, int, int>("m_race", 9, 2),
                new Tuple<string, int, int>("m_color", 23, 3),
                new Tuple<string, int, int>("m_control", 10, 4),
                new Tuple<string, int, int>("m_teamId", 1, 5),
                new Tuple<string, int, int>("m_handicap", 6, 6),
                new Tuple<string, int, int>("m_observe", 24, 7),
                new Tuple<string, int, int>("m_result", 24, 8),
                new Tuple<string, int, int>("m_workingSetSlotId", 25, 9),
                new Tuple<string, int, int>("m_hero", 9, 10)
            }
        ),  // 26

            new KeyValuePair<string, object>(
            "_array",
            new Tuple<Tuple<int, int>, int>(new Tuple<int, int>(0, 5), 26)
        ),  // 27

            new KeyValuePair<string, object>(
            "_optional",
            new List<int> { 27 }
        ),  // 28

            new KeyValuePair<string, object>(
            "_blob",
            new List<Tuple<int, int>> { new Tuple<int, int>(0, 10) }
        ),  // 29
            new KeyValuePair<string, object>(
            "_blob",
            new List<Tuple<int, int>> { new Tuple<int, int>(0, 11) }
        ),  // 30

        new KeyValuePair<string, object>(
            "_struct",
            new List<Tuple<string, int, int>>
            {
                new Tuple<string, int, int>("m_file", 30, 0)
            }
        ),  // 31

        new KeyValuePair<string, object>(
            "_int",
            new List<Tuple<long, int>>
            {
                new Tuple<long, int>(-9223372036854775808L, 64)
            }
        ),  // 32

        new KeyValuePair<string, object>(
            "_optional",
            new List<int> { 13 }
        ),  // 33

        new KeyValuePair<string, object>(
            "_blob",
            new List<Tuple<int, int>> { new Tuple<int, int>(0, 12) }
        ),  // 34

        new KeyValuePair<string, object>(
            "_blob",
            new List<Tuple<int, int>> { new Tuple<int, int>(40, 0) }
        ),  // 35

        new KeyValuePair<string, object>(
            "_array",
            new Tuple<Tuple<int, int>, int>(new Tuple<int, int>(0, 6), 35)
        ),  // 36

        new KeyValuePair<string, object>(
            "_optional",
            new List<int> { 36 }
        ),  // 37

        new KeyValuePair<string, object>(
            "_array",
            new Tuple<Tuple<int, int>, int>(new Tuple<int, int>(0, 6), 30)
        ),  // 38

        new KeyValuePair<string, object>(
            "_optional",
            new List<int> { 38 }
        ),  // 39

        new KeyValuePair<string, object>(
            "_struct",
            new List<Tuple<string, int, int>>
            {
                new Tuple<string, int, int>("m_playerList", 28, 0),
                new Tuple<string, int, int>("m_title", 29, 1),
                new Tuple<string, int, int>("m_difficulty", 9, 2),
                new Tuple<string, int, int>("m_thumbnail", 31, 3),
                new Tuple<string, int, int>("m_isBlizzardMap", 13, 4),
                new Tuple<string, int, int>("m_timeUTC", 32, 5),
                new Tuple<string, int, int>("m_timeLocalOffset", 32, 6),
                new Tuple<string, int, int>("m_restartAsTransitionMap", 33, 16),
                new Tuple<string, int, int>("m_disableRecoverGame", 13, 17),
                new Tuple<string, int, int>("m_description", 34, 7),
                new Tuple<string, int, int>("m_imageFilePath", 30, 8),
                new Tuple<string, int, int>("m_campaignIndex", 10, 15),
                new Tuple<string, int, int>("m_mapFileName", 30, 9),
                new Tuple<string, int, int>("m_cacheHandles", 37, 10),
                new Tuple<string, int, int>("m_miniSave", 13, 11),
                new Tuple<string, int, int>("m_gameSpeed", 12, 12),
                new Tuple<string, int, int>("m_defaultDifficulty", 3, 13),
                new Tuple<string, int, int>("m_modPaths", 39, 14)
            }
        ),  // 40

        new KeyValuePair<string, object>(
            "_optional",
            new List<int> { 9 }
        ),  // 41

        new KeyValuePair<string, object>(
            "_optional",
            new List<int> { 35 }
        ),  // 42

        new KeyValuePair<string, object>(
            "_optional",
            new List<int> { 6 }
        ),  // 43

        new KeyValuePair<string, object>(
            "_struct",
            new List<Tuple<string, int, int>>
            {
                new Tuple<string, int, int>("m_race", 25, -1)
            }
        ),  // 44

        new KeyValuePair<string, object>(
            "_struct",
            new List<Tuple<string, int, int>>
            {
                new Tuple<string, int, int>("m_team", 25, -1)
            }
        ),  // 45

        new KeyValuePair<string, object>(
            "_blob",
            new List<Tuple<int, int>> { new Tuple<int, int>(0, 9) }
        ),  // 46

        new KeyValuePair<string, object>(
            "_int",
            new List<Tuple<int, int>>
            {
                new Tuple<int, int>(-2147483648, 32)
            }
        ),  // 47

        new KeyValuePair<string, object>(
            "_optional",
            new List<int> { 47 }
        ),  // 48

        new KeyValuePair<string, object>(
            "_struct",
            new List<Tuple<string, int, int>>
            {
                new Tuple<string, int, int>("m_name", 9, -19),
                new Tuple<string, int, int>("m_clanTag", 41, -18),
                new Tuple<string, int, int>("m_clanLogo", 42, -17),
                new Tuple<string, int, int>("m_highestLeague", 25, -16),
                new Tuple<string, int, int>("m_combinedRaceLevels", 43, -15),
                new Tuple<string, int, int>("m_randomSeed", 6, -14),
                new Tuple<string, int, int>("m_racePreference", 44, -13),
                new Tuple<string, int, int>("m_teamPreference", 45, -12),
                new Tuple<string, int, int>("m_testMap", 13, -11),
                new Tuple<string, int, int>("m_testAuto", 13, -10),
                new Tuple<string, int, int>("m_examine", 13, -9),
                new Tuple<string, int, int>("m_customInterface", 13, -8),
                new Tuple<string, int, int>("m_testType", 6, -7),
                new Tuple<string, int, int>("m_observe", 24, -6),
                new Tuple<string, int, int>("m_hero", 46, -5),
                new Tuple<string, int, int>("m_skin", 46, -4),
                new Tuple<string, int, int>("m_mount", 46, -3),
                new Tuple<string, int, int>("m_toonHandle", 20, -2),
                new Tuple<string, int, int>("m_scaledRating", 48, -1)
            }
        ),  // 49

        new KeyValuePair<string, object>(
            "_array",
            new Tuple<Tuple<int, int>, int>(new Tuple<int, int>(0, 5), 49)
        ),  // 50

        new KeyValuePair<string, object>(
            "_struct",
            new List<Tuple<string, int, int>>
            {
                new Tuple<string, int, int>("m_lockTeams", 13, -16),
                new Tuple<string, int, int>("m_teamsTogether", 13, -15),
                new Tuple<string, int, int>("m_advancedSharedControl", 13, -14),
                new Tuple<string, int, int>("m_randomRaces", 13, -13),
                new Tuple<string, int, int>("m_battleNet", 13, -12),
                new Tuple<string, int, int>("m_amm", 13, -11),
                new Tuple<string, int, int>("m_competitive", 13, -10),
                new Tuple<string, int, int>("m_practice", 13, -9),
                new Tuple<string, int, int>("m_cooperative", 13, -8),
                new Tuple<string, int, int>("m_noVictoryOrDefeat", 13, -7),
                new Tuple<string, int, int>("m_heroDuplicatesAllowed", 13, -6),
                new Tuple<string, int, int>("m_fog", 24, -5),
                new Tuple<string, int, int>("m_observers", 24, -4),
                new Tuple<string, int, int>("m_userDifficulty", 24, -3),
                new Tuple<string, int, int>("m_clientDebugFlags", 21, -2),
                new Tuple<string, int, int>("m_buildCoachEnabled", 13, -1)
            }
        ),

        new KeyValuePair<string, object>(
            "_int",
            new List<Tuple<int, int>>
            {
                new Tuple<int, int>(1, 4)
            }
        ),  // 52

        new KeyValuePair<string, object>(
            "_int",
            new List<Tuple<int, int>>
            {
                new Tuple<int, int>(1, 8)
            }
        ),  // 53

        new KeyValuePair<string, object>(
            "_bitarray",
            new List<Tuple<int, int>>
            {
                new Tuple<int, int>(0, 6)
            }
        ),  // 54

        new KeyValuePair<string, object>(
            "_bitarray",
            new List<Tuple<int, int>>
            {
                new Tuple<int, int>(0, 8)
            }
        ),  // 55

        new KeyValuePair<string, object>(
            "_bitarray",
            new List<Tuple<int, int>>
            {
                new Tuple<int, int>(0, 2)
            }
        ),  // 56

        new KeyValuePair<string, object>(
            "_struct",
            new List<Tuple<string, int, int>>
            {
                new Tuple<string, int, int>("m_allowedColors", 54, -6),
                new Tuple<string, int, int>("m_allowedRaces", 55, -5),
                new Tuple<string, int, int>("m_allowedDifficulty", 54, -4),
                new Tuple<string, int, int>("m_allowedControls", 55, -3),
                new Tuple<string, int, int>("m_allowedObserveTypes", 56, -2),
                new Tuple<string, int, int>("m_allowedAIBuilds", 55, -1)
            }
        ),  // 57

        new KeyValuePair<string, object>(
            "_array",
            new Tuple<Tuple<int, int>, int>(new Tuple<int, int>(0, 5), 57)
        ),  // 58

        new KeyValuePair<string, object>(
            "_struct",
            new List<Tuple<string, int, int>>
            {
                new Tuple<string, int, int>("m_randomValue", 6, -28),
                new Tuple<string, int, int>("m_gameCacheName", 29, -27),
                new Tuple<string, int, int>("m_gameOptions", 51, -26),
                new Tuple<string, int, int>("m_gameSpeed", 12, -25),
                new Tuple<string, int, int>("m_gameType", 12, -24),
                new Tuple<string, int, int>("m_maxUsers", 2, -23),
                new Tuple<string, int, int>("m_maxObservers", 2, -22),
                new Tuple<string, int, int>("m_maxPlayers", 2, -21),
                new Tuple<string, int, int>("m_maxTeams", 52, -20),
                new Tuple<string, int, int>("m_maxColors", 3, -19),
                new Tuple<string, int, int>("m_maxRaces", 53, -18),
                new Tuple<string, int, int>("m_maxControls", 10, -17),
                new Tuple<string, int, int>("m_mapSizeX", 10, -16),
                new Tuple<string, int, int>("m_mapSizeY", 10, -15),
                new Tuple<string, int, int>("m_mapFileSyncChecksum", 6, -14),
                new Tuple<string, int, int>("m_mapFileName", 30, -13),
                new Tuple<string, int, int>("m_mapAuthorName", 9, -12),
                new Tuple<string, int, int>("m_modFileSyncChecksum", 6, -11),
                new Tuple<string, int, int>("m_slotDescriptions", 58, -10),
                new Tuple<string, int, int>("m_defaultDifficulty", 3, -9),
                new Tuple<string, int, int>("m_defaultAIBuild", 10, -8),
                new Tuple<string, int, int>("m_cacheHandles", 36, -7),
                new Tuple<string, int, int>("m_hasExtensionMod", 13, -6),
                new Tuple<string, int, int>("m_hasNonBlizzardExtensionMod", 13, -5),
                new Tuple<string, int, int>("m_isBlizzardMap", 13, -4),
                new Tuple<string, int, int>("m_isPremadeFFA", 13, -3),
                new Tuple<string, int, int>("m_isCoopMode", 13, -2),
                new Tuple<string, int, int>("m_isRealtimeMode", 13, -1)
            }
        ),  // 59

        new KeyValuePair<string, object>(
            "_optional",
            new List<int> { 1 }
        ),  // 60

        new KeyValuePair<string, object>(
            "_optional",
            new List<int> { 2 }
        ),  // 61

        new KeyValuePair<string, object>(
            "_struct",
            new List<Tuple<string, int, int>>
            {
                new Tuple<string, int, int>("m_color", 61, -1)
            }
        ),  // 62

        new KeyValuePair<string, object>(
            "_array",
            new Tuple<Tuple<int, int>, int>(new Tuple<int, int>(0, 4), 46)
        ),  // 63

        new KeyValuePair<string, object>(
            "_array",
            new Tuple<Tuple<int, int>, int>(new Tuple<int, int>(0, 17), 6)
        ),  // 64

        new KeyValuePair<string, object>(
            "_array",
            new Tuple<Tuple<int, int>, int>(new Tuple<int, int>(0, 16), 6)
        ),  // 65

        new KeyValuePair<string, object>(
            "_array",
            new Tuple<Tuple<int, int>, int>(new Tuple<int, int>(0, 3), 6)
        ),  // 66

        new KeyValuePair<string, object>(
            "_struct",
            new List<Tuple<string, int, int>>
            {
                new Tuple<string, int, int>("m_key", 6, -2),
                new Tuple<string, int, int>("m_rewards", 64, -1)
            }
        ),  // 67

        new KeyValuePair<string, object>(
            "_array",
            new Tuple<Tuple<int, int>, int>(new Tuple<int, int>(0, 17), 67)
        ),  // 68
        new KeyValuePair<string, object>(
            "_struct",
            new List<Tuple<string, int, int>>
            {
                new Tuple<string, int, int>("m_control", 10, -32),
                new Tuple<string, int, int>("m_userId", 60, -31),
                new Tuple<string, int, int>("m_teamId", 1, -30),
                new Tuple<string, int, int>("m_colorPref", 62, -29),
                new Tuple<string, int, int>("m_racePref", 44, -28),
                new Tuple<string, int, int>("m_difficulty", 3, -27),
                new Tuple<string, int, int>("m_aiBuild", 10, -26),
                new Tuple<string, int, int>("m_handicap", 6, -25),
                new Tuple<string, int, int>("m_observe", 24, -24),
                new Tuple<string, int, int>("m_logoIndex", 6, -23),
                new Tuple<string, int, int>("m_hero", 46, -22),
                new Tuple<string, int, int>("m_skin", 46, -21),
                new Tuple<string, int, int>("m_mount", 46, -20),
                new Tuple<string, int, int>("m_artifacts", 63, -19),
                new Tuple<string, int, int>("m_workingSetSlotId", 25, -18),
                new Tuple<string, int, int>("m_rewards", 64, -17),
                new Tuple<string, int, int>("m_toonHandle", 20, -16),
                new Tuple<string, int, int>("m_licenses", 65, -15),
                new Tuple<string, int, int>("m_tandemLeaderId", 60, -14),
                new Tuple<string, int, int>("m_commander", 46, -13),
                new Tuple<string, int, int>("m_commanderLevel", 6, -12),
                new Tuple<string, int, int>("m_hasSilencePenalty", 13, -11),
                new Tuple<string, int, int>("m_tandemId", 60, -10),
                new Tuple<string, int, int>("m_commanderMasteryLevel", 6, -9),
                new Tuple<string, int, int>("m_commanderMasteryTalents", 66, -8),
                new Tuple<string, int, int>("m_trophyId", 6, -7),
                new Tuple<string, int, int>("m_rewardOverrides", 68, -6),
                new Tuple<string, int, int>("m_brutalPlusDifficulty", 6, -5),
                new Tuple<string, int, int>("m_retryMutationIndexes", 66, -4),
                new Tuple<string, int, int>("m_aCEnemyRace", 6, -3),
                new Tuple<string, int, int>("m_aCEnemyWaveType", 6, -2),
                new Tuple<string, int, int>("m_selectedCommanderPrestige", 6, -1)
            }
        ),  // 69

        new KeyValuePair<string, object>(
            "_array",
            new Tuple<Tuple<int, int>, int>(new Tuple<int, int>(0, 5), 69)
        ),  // 70

        new KeyValuePair<string, object>(
            "_struct",
            new List<Tuple<string, int, int>>
            {
                new Tuple<string, int, int>("m_phase", 12, -11),
                new Tuple<string, int, int>("m_maxUsers", 2, -10),
                new Tuple<string, int, int>("m_maxObservers", 2, -9),
                new Tuple<string, int, int>("m_slots", 70, -8),
                new Tuple<string, int, int>("m_randomSeed", 6, -7),
                new Tuple<string, int, int>("m_hostUserId", 60, -6),
                new Tuple<string, int, int>("m_isSinglePlayer", 13, -5),
                new Tuple<string, int, int>("m_pickedMapTag", 10, -4),
                new Tuple<string, int, int>("m_gameDuration", 6, -3),
                new Tuple<string, int, int>("m_defaultDifficulty", 3, -2),
                new Tuple<string, int, int>("m_defaultAIBuild", 10, -1)
            }
        ),  // 71

        new KeyValuePair<string, object>(
            "_struct",
            new List<Tuple<string, int, int>>
            {
                new Tuple<string, int, int>("m_userInitialData", 50, -3),
                new Tuple<string, int, int>("m_gameDescription", 59, -2),
                new Tuple<string, int, int>("m_lobbyState", 71, -1)
            }
        ),  // 72

        new KeyValuePair<string, object>(
            "_struct",
            new List<Tuple<string, int, int>>
            {
                new Tuple<string, int, int>("m_syncLobbyState", 72, -1)
            }
        ),  // 73

        new KeyValuePair<string, object>(
            "_struct",
            new List<Tuple<string, int, int>>
            {
                new Tuple<string, int, int>("m_name", 20, -6)
            }
        ),  // 74

        new KeyValuePair<string, object>(
            "_blob",
            new Tuple<int, int>(0, 6)
        ),  // 75

        new KeyValuePair<string, object>(
            "_struct",
            new List<Tuple<string, int, int>>
            {
                new Tuple<string, int, int>("m_name", 75, -6)
            }
        ),  // 76

        new KeyValuePair<string, object>(
            "_struct",
            new List<Tuple<string, int, int>>
            {
                new Tuple<string, int, int>("m_name", 75, -8),
                new Tuple<string, int, int>("m_type", 6, -7),
                new Tuple<string, int, int>("m_data", 20, -6)
            }
        ),  // 77

        new KeyValuePair<string, object>(
            "_struct",
            new List<Tuple<string, int, int>>
            {
                new Tuple<string, int, int>("m_type", 6, -8),
                new Tuple<string, int, int>("m_name", 75, -7),
                new Tuple<string, int, int>("m_data", 34, -6)
            }
        ),  // 78

        new KeyValuePair<string, object>(
            "_array",
            new Tuple<Tuple<int, int>, int>(new Tuple<int, int>(0, 5), 10)
        ),  // 79

        new KeyValuePair<string, object>(
            "_struct",
            new List<Tuple<string, int, int>>
            {
                new Tuple<string, int, int>("m_signature", 79, -7),
                new Tuple<string, int, int>("m_toonHandle", 20, -6)
            }
        ),  // 80

        new KeyValuePair<string, object>(
            "_struct",
            new List<Tuple<string, int, int>>
            {
                new Tuple<string, int, int>("m_gameFullyDownloaded", 13, -19),
                new Tuple<string, int, int>("m_developmentCheatsEnabled", 13, -18),
                new Tuple<string, int, int>("m_testCheatsEnabled", 13, -17),
                new Tuple<string, int, int>("m_multiplayerCheatsEnabled", 13, -16),
                new Tuple<string, int, int>("m_syncChecksummingEnabled", 13, -15),
                new Tuple<string, int, int>("m_isMapToMapTransition", 13, -14),
                new Tuple<string, int, int>("m_debugPauseEnabled", 13, -13),
                new Tuple<string, int, int>("m_useGalaxyAsserts", 13, -12),
                new Tuple<string, int, int>("m_platformMac", 13, -11),
                new Tuple<string, int, int>("m_cameraFollow", 13, -10),
                new Tuple<string, int, int>("m_baseBuildNum", 6, -9),
                new Tuple<string, int, int>("m_buildNum", 6, -8),
                new Tuple<string, int, int>("m_versionFlags", 6, -7),
                new Tuple<string, int, int>("m_hotkeyProfile", 46, -6)
            }
        ),  // 81

        new KeyValuePair<string, object>(
            "_struct",
            new List<Tuple<string, int, int>>()
        ),  // 82

        new KeyValuePair<string, object>(
            "_int",
            new Tuple<int, int>(0, 16)
        ),  // 83
         new KeyValuePair<string, object>(
            "_struct",
            new List<Tuple<string, int, int>>
            {
                new Tuple<string, int, int>("x", 83, -2),
                new Tuple<string, int, int>("y", 83, -1)
            }
        ),  // 84

        new KeyValuePair<string, object>(
            "_struct",
            new List<Tuple<string, int, int>>
            {
                new Tuple<string, int, int>("m_which", 12, -7),
                new Tuple<string, int, int>("m_target", 84, -6)
            }
        ),  // 85

        new KeyValuePair<string, object>(
            "_struct",
            new List<Tuple<string, int, int>>
            {
                new Tuple<string, int, int>("m_fileName", 30, -10),
                new Tuple<string, int, int>("m_automatic", 13, -9),
                new Tuple<string, int, int>("m_overwrite", 13, -8),
                new Tuple<string, int, int>("m_name", 9, -7),
                new Tuple<string, int, int>("m_description", 29, -6)
            }
        ),  // 86

        new KeyValuePair<string, object>(
            "_struct",
            new List<Tuple<string, int, int>>
            {
                new Tuple<string, int, int>("m_sequence", 6, -6)
            }
        ),  // 87

        new KeyValuePair<string, object>(
            "_struct",
            new List<Tuple<string, int, int>>
            {
                new Tuple<string, int, int>("x", 47, -2),
                new Tuple<string, int, int>("y", 47, -1)
            }
        ),  // 88

        new KeyValuePair<string, object>(
            "_struct",
            new List<Tuple<string, int, int>>
            {
                new Tuple<string, int, int>("m_point", 88, -4),
                new Tuple<string, int, int>("m_time", 47, -3),
                new Tuple<string, int, int>("m_verb", 29, -2),
                new Tuple<string, int, int>("m_arguments", 29, -1)
            }
        ),  // 89

        new KeyValuePair<string, object>(
            "_struct",
            new List<Tuple<string, int, int>>
            {
                new Tuple<string, int, int>("m_data", 89, -6)
            }
        ),  // 90

        new KeyValuePair<string, object>(
            "_int",
            new Tuple<int, int>(0, 27)
        ),  // 91

        new KeyValuePair<string, object>(
            "_struct",
            new List<Tuple<string, int, int>>
            {
                new Tuple<string, int, int>("m_abilLink", 83, -3),
                new Tuple<string, int, int>("m_abilCmdIndex", 2, -2),
                new Tuple<string, int, int>("m_abilCmdData", 25, -1)
            }
        ),  // 92

        new KeyValuePair<string, object>(
            "_optional",
            new Tuple<int>(92)
        ),  // 93

        new KeyValuePair<string, object>(
            "_null",
            null
        ),  // 94

        new KeyValuePair<string, object>(
            "_int",
            new Tuple<int, int>(0, 20)
        ),  // 95

        new KeyValuePair<string, object>(
            "_struct",
            new List<Tuple<string, int, int>>
            {
                new Tuple<string, int, int>("x", 95, -3),
                new Tuple<string, int, int>("y", 95, -2),
                new Tuple<string, int, int>("z", 47, -1)
            }
        ),  // 96

        new KeyValuePair<string, object>(
            "_struct",
            new List<Tuple<string, int, int>>
            {
                new Tuple<string, int, int>("m_targetUnitFlags", 83, -7),
                new Tuple<string, int, int>("m_timer", 10, -6),
                new Tuple<string, int, int>("m_tag", 6, -5),
                new Tuple<string, int, int>("m_snapshotUnitLink", 83, -4),
                new Tuple<string, int, int>("m_snapshotControlPlayerId", 60, -3),
                new Tuple<string, int, int>("m_snapshotUpkeepPlayerId", 60, -2),
                new Tuple<string, int, int>("m_snapshotPoint", 96, -1)
            }
        ),  // 97

        new KeyValuePair<string, object>(
            "_choice",
            new Tuple<int, Dictionary<int, Tuple<string, object>>>(0, new Dictionary<int, Tuple<string, object>>
            {
                { 0, new Tuple<string, object>("None", 94) },
                { 1, new Tuple<string, object>("TargetPoint", 96) },
                { 2, new Tuple<string, object>("TargetUnit", 97) },
                { 3, new Tuple<string, object>("Data", 6) }
            })
        ),  // 98

        new KeyValuePair<string, object>(
            "_int",
            new Tuple<int, int>(1, 32)
        ),  // 99

        new KeyValuePair<string, object>(
            "_struct",
            new List<Tuple<string, int, int>>
            {
                new Tuple<string, int, int>("m_cmdFlags", 91, -11),
                new Tuple<string, int, int>("m_abil", 93, -10),
                new Tuple<string, int, int>("m_data", 98, -9),
                new Tuple<string, int, int>("m_sequence", 99, -8),
                new Tuple<string, int, int>("m_otherUnit", 43, -7),
                new Tuple<string, int, int>("m_unitGroup", 43, -6)
            }
        ), // 100
        new KeyValuePair<string, object>(
            "_int",
            new { Min = 0, Max = 9 }  // Represents an integer with a range from 0 to 9
        ),  // 101

        new KeyValuePair<string, object>(
            "_bitarray",
            new { Size = 9 }  // Represents a bit array with size 9
        ),  // 102

        new KeyValuePair<string, object>(
            "_array",
            new { Count = 9, ItemType = 101 }  // Represents an array with 9 items of type _int
        ),  // 103

         new KeyValuePair<string, object>(
            "_choice",
            new
            {
                Index = 0,
                MaxValue = 2,
                Options = new Dictionary<int, Tuple<string, object>>
                {
                    { 0, Tuple.Create("None", (object)94) },
                    { 1, Tuple.Create("Mask", (object)102) },
                    { 2, Tuple.Create("OneIndices", (object)103) },
                    { 3, Tuple.Create("ZeroIndices", (object)103) }
                }
            }
        ),  // 104

        new KeyValuePair<string, object>(
            "_struct",
            new Dictionary<string, object>
            {
                { "m_unitLink", 83 },
                { "m_subgroupPriority", 10 },
                { "m_intraSubgroupPriority", 10 },
                { "m_count", 101 }
            }
        ),  // 105

        new KeyValuePair<string, object>(
            "_array",
            new { Count = 9, ItemType = 105 }  // Represents an array with 9 items of type _struct
        ),  // 106

        new KeyValuePair<string, object>(
            "_array",
            new { Count = 9, ItemType = 6 }  // Represents an array with 9 items of type _bitarray
        ),  // 107

        new KeyValuePair<string, object>(
            "_struct",
            new Dictionary<string, object>
            {
                { "m_subgroupIndex", 101 },
                { "m_removeMask", 104 },
                { "m_addSubgroups", 106 },
                { "m_addUnitTags", 107 }
            }
        ),  // 108

        new KeyValuePair<string, object>(
            "_struct",
            new Dictionary<string, object>
            {
                { "m_controlGroupId", 1 },
                { "m_delta", 108 }
            }
        ),  // 109

        new KeyValuePair<string, object>(
            "_struct",
            new Dictionary<string, object>
            {
                { "m_controlGroupIndex", 1 },
                { "m_controlGroupUpdate", 12 },
                { "m_mask", 104 }
            }
        ),  // 110

        new KeyValuePair<string, object>(
            "_struct",
            new Dictionary<string, object>
            {
                { "m_count", 101 },
                { "m_subgroupCount", 101 },
                { "m_activeSubgroupIndex", 101 },
                { "m_unitTagsChecksum", 6 },
                { "m_subgroupIndicesChecksum", 6 },
                { "m_subgroupsChecksum", 6 }
            }
        ),  // 111

        new KeyValuePair<string, object>(
            "_struct",
            new Dictionary<string, object>
            {
                { "m_controlGroupId", 1 },
                { "m_selectionSyncData", 111 }
            }
        ),  // 112

        new KeyValuePair<string, object>(
            "_array",
            new { Count = 3, ItemType = 47 }  // Represents an array with 3 items of type _struct
        ),  // 113

        new KeyValuePair<string, object>(
            "_struct",
            new Dictionary<string, object>
            {
                { "m_recipientId", 1 },
                { "m_resources", 113 }
            }
        ),  // 114

        new KeyValuePair<string, object>(
            "_struct",
            new Dictionary<string, object>
            {
                { "m_chatMessage", 29 }
            }
        ),  // 115

        new KeyValuePair<string, object>(
            "_int",
            new { Min = -128, Max = 8 }  // Represents an integer with a range from -128 to 8
        ),  // 116

        new KeyValuePair<string, object>(
            "_struct",
            new Dictionary<string, object>
            {
                { "x", 47 },
                { "y", 47 },
                { "z", 47 }
            }
        ),  // 117

        new KeyValuePair<string, object>(
            "_struct",
            new Dictionary<string, object>
            {
                { "m_beacon", 116 },
                { "m_ally", 116 },
                { "m_flags", 116 },
                { "m_build", 116 },
                { "m_targetUnitTag", 6 },
                { "m_targetUnitSnapshotUnitLink", 83 },
                { "m_targetUnitSnapshotUpkeepPlayerId", 116 },
                { "m_targetUnitSnapshotControlPlayerId", 116 },
                { "m_targetPoint", 117 }
            }
        ),  // 118

        new KeyValuePair<string, object>(
            "_struct",
            new Dictionary<string, object>
            {
                { "m_speed", 12 }
            }
        ),  // 119

        new KeyValuePair<string, object>(
            "_struct",
            new Dictionary<string, object>
            {
                { "m_delta", 116 }
            }
        ),  // 120
        new KeyValuePair<string, object>(
            "_struct",
            new
            {
                Fields = new[]
                {
                    new { Name = "m_point", Type = 88, Index = -14 },
                    new { Name = "m_unit", Type = 6, Index = -13 },
                    new { Name = "m_unitLink", Type = 83, Index = -12 },
                    new { Name = "m_unitControlPlayerId", Type = 60, Index = -11 },
                    new { Name = "m_unitUpkeepPlayerId", Type = 60, Index = -10 },
                    new { Name = "m_unitPosition", Type = 96, Index = -9 },
                    new { Name = "m_unitIsUnderConstruction", Type = 13, Index = -8 },
                    new { Name = "m_pingedMinimap", Type = 13, Index = -7 },
                    new { Name = "m_option", Type = 47, Index = -6 }
                }
            }
        ),  // 121

        new KeyValuePair<string, object>(
            "_struct",
            new
            {
                Fields = new[]
                {
                    new { Name = "m_verb", Type = 29, Index = -7 },
                    new { Name = "m_arguments", Type = 29, Index = -6 }
                }
            }
        ),  // 122

        new KeyValuePair<string, object>(
            "_struct",
            new
            {
                Fields = new[]
                {
                    new { Name = "m_alliance", Type = 6, Index = -7 },
                    new { Name = "m_control", Type = 6, Index = -6 }
                }
            }
        ),  // 123

        new KeyValuePair<string, object>(
            "_struct",
            new
            {
                Fields = new[]
                {
                    new { Name = "m_unitTag", Type = 6, Index = -6 }
                }
            }
        ),  // 124

        new KeyValuePair<string, object>(
            "_struct",
            new
            {
                Fields = new[]
                {
                    new { Name = "m_unitTag", Type = 6, Index = -7 },
                    new { Name = "m_flags", Type = 10, Index = -6 }
                }
            }
        ),  // 125

        new KeyValuePair<string, object>(
            "_struct",
            new
            {
                Fields = new[]
                {
                    new { Name = "m_conversationId", Type = 47, Index = -7 },
                    new { Name = "m_replyId", Type = 47, Index = -6 }
                }
            }
        ),  // 126

        new KeyValuePair<string, object>(
            "_optional",
            20
        ),  // 127

        new KeyValuePair<string, object>(
            "_struct",
            new
            {
                Fields = new[]
                {
                    new { Name = "m_gameUserId", Type = 1, Index = -6 },
                    new { Name = "m_observe", Type = 24, Index = -5 },
                    new { Name = "m_name", Type = 9, Index = -4 },
                    new { Name = "m_toonHandle", Type = 127, Index = -3 },
                    new { Name = "m_clanTag", Type = 41, Index = -2 },
                    new { Name = "m_clanLogo", Type = 42, Index = -1 }
                }
            }
        ),  // 128

        new KeyValuePair<string, object>(
            "_array",
            new
            {
                Count = 5,
                ItemType = 128
            }
        ),  // 129

        new KeyValuePair<string, object>(
            "_int",
            new
            {
                MinValue = 0,
                MaxValue = 1
            }
        ),  // 130

        new KeyValuePair<string, object>(
            "_struct",
            new
            {
                Fields = new[]
                {
                    new { Name = "m_userInfos", Type = 129, Index = -7 },
                    new { Name = "m_method", Type = 130, Index = -6 }
                }
            }
        ),  // 131

        new KeyValuePair<string, object>(
            "_struct",
            new
            {
                Fields = new[]
                {
                    new { Name = "m_purchaseItemId", Type = 47, Index = -6 }
                }
            }
        ),  // 132

        new KeyValuePair<string, object>(
            "_struct",
            new
            {
                Fields = new[]
                {
                    new { Name = "m_difficultyLevel", Type = 47, Index = -6 }
                }
            }
        ),  // 133

        new KeyValuePair<string, object>(
            "_choice",
            new
            {
                Index = 0,
                MaxValue = 3,
                Options = new Dictionary<int, Tuple<string, object>>
                {
                    { 0, Tuple.Create("None", (object)94) },
                    { 1, Tuple.Create("Checked", (object)13) },
                    { 2, Tuple.Create("ValueChanged", (object)6) },
                    { 3, Tuple.Create("SelectionChanged", (object)47) },
                    { 4, Tuple.Create("TextChanged", (object)30) },
                    { 5, Tuple.Create("MouseButton", (object)6) }
                }
            }
        ),  // 134

        new KeyValuePair<string, object>(
            "_struct",
            new
            {
                Fields = new[]
                {
                    new { Name = "m_controlId", Type = 47, Index = -8 },
                    new { Name = "m_eventType", Type = 47, Index = -7 },
                    new { Name = "m_eventData", Type = 134, Index = -6 }
                }
            }
        ),  // 135

        new KeyValuePair<string, object>(
            "_struct",
            new
            {
                Fields = new[]
                {
                    new { Name = "m_soundHash", Type = 6, Index = -7 },
                    new { Name = "m_length", Type = 6, Index = -6 }
                }
            }
        ),  // 136

        new KeyValuePair<string, object>(
            "_array",
            new
            {
                Count = 7,
                ItemType = 6
            }
        ),  // 137

        new KeyValuePair<string, object>(
            "_struct",
            new
            {
                Fields = new[]
                {
                    new { Name = "m_soundHash", Type = 137, Index = -2 },
                    new { Name = "m_length", Type = 137, Index = -1 }
                }
            }
        ),  // 138

        new KeyValuePair<string, object>(
            "_struct",
            new
            {
                Fields = new[]
                {
                    new { Name = "m_syncInfo", Type = 138, Index = -6 }
                }
            }
        ),  // 139

        new KeyValuePair<string, object>(
            "_struct",
            new
            {
                Fields = new[]
                {
                    new { Name = "m_queryId", Type = 83, Index = -8 },
                    new { Name = "m_lengthMs", Type = 6, Index = -7 },
                    new { Name = "m_finishGameLoop", Type = 6, Index = -6 }
                }
            }
        ),  // 140

        new KeyValuePair<string, object>(
            "_struct",
            new
            {
                Fields = new[]
                {
                    new { Name = "m_queryId", Type = 83, Index = -7 },
                    new { Name = "m_lengthMs", Type = 6, Index = -6 }
                }
            }
        ),  // 141

        new KeyValuePair<string, object>(
            "_struct",
            new
            {
                Fields = new[]
                {
                    new { Name = "m_animWaitQueryId", Type = 83, Index = -6 }
                }
            }
        ),  // 142

        new KeyValuePair<string, object>(
            "_struct",
            new
            {
                Fields = new[]
                {
                    new { Name = "m_sound", Type = 6, Index = -6 }
                }
            }
        ),  // 143

        new KeyValuePair<string, object>(
            "_struct",
            new
            {
                Fields = new[]
                {
                    new { Name = "m_transmissionId", Type = 47, Index = -7 },
                    new { Name = "m_thread", Type = 6, Index = -6 }
                }
            }
        ),  // 144
        new KeyValuePair<string, object>(
            "_struct",
            new
            {
                Fields = new[]
                {
                    new { Name = "m_transmissionId", Type = 47, Index = -6 }
                }
            }
        ),  // 145

        new KeyValuePair<string, object>(
            "_optional",
            84
        ),  // 146

        new KeyValuePair<string, object>(
            "_optional",
            83
        ),  // 147

        new KeyValuePair<string, object>(
            "_optional",
            116
        ),  // 148

        new KeyValuePair<string, object>(
            "_struct",
            new
            {
                Fields = new[]
                {
                    new { Name = "m_target", Type = 146, Index = -11 },
                    new { Name = "m_distance", Type = 147, Index = -10 },
                    new { Name = "m_pitch", Type = 147, Index = -9 },
                    new { Name = "m_yaw", Type = 147, Index = -8 },
                    new { Name = "m_reason", Type = 148, Index = -7 },
                    new { Name = "m_follow", Type = 13, Index = -6 }
                }
            }
        ),  // 149

        new KeyValuePair<string, object>(
            "_struct",
            new
            {
                Fields = new[]
                {
                    new { Name = "m_skipType", Type = 130, Index = -6 }
                }
            }
        ),  // 150

        new KeyValuePair<string, object>(
            "_int",
            new
            {
                MinValue = 0,
                MaxValue = 11
            }
        ),  // 151

        new KeyValuePair<string, object>(
            "_struct",
            new
            {
                Fields = new[]
                {
                    new { Name = "x", Type = 151, Index = -2 },
                    new { Name = "y", Type = 151, Index = -1 }
                }
            }
        ),  // 152

        new KeyValuePair<string, object>(
            "_struct",
            new
            {
                Fields = new[]
                {
                    new { Name = "m_button", Type = 6, Index = -10 },
                    new { Name = "m_down", Type = 13, Index = -9 },
                    new { Name = "m_posUI", Type = 152, Index = -8 },
                    new { Name = "m_posWorld", Type = 96, Index = -7 },
                    new { Name = "m_flags", Type = 116, Index = -6 }
                }
            }
        ),  // 153

        new KeyValuePair<string, object>(
            "_struct",
            new
            {
                Fields = new[]
                {
                    new { Name = "m_posUI", Type = 152, Index = -8 },
                    new { Name = "m_posWorld", Type = 96, Index = -7 },
                    new { Name = "m_flags", Type = 116, Index = -6 }
                }
            }
        ),  // 154

        new KeyValuePair<string, object>(
            "_struct",
            new
            {
                Fields = new[]
                {
                    new { Name = "m_achievementLink", Type = 83, Index = -6 }
                }
            }
        ),  // 155

        new KeyValuePair<string, object>(
            "_struct",
            new
            {
                Fields = new[]
                {
                    new { Name = "m_hotkey", Type = 6, Index = -7 },
                    new { Name = "m_down", Type = 13, Index = -6 }
                }
            }
        ),  // 156

        new KeyValuePair<string, object>(
            "_struct",
            new
            {
                Fields = new[]
                {
                    new { Name = "m_abilLink", Type = 83, Index = -8 },
                    new { Name = "m_abilCmdIndex", Type = 2, Index = -7 },
                    new { Name = "m_state", Type = 116, Index = -6 }
                }
            }
        ),  // 157

        new KeyValuePair<string, object>(
            "_struct",
            new
            {
                Fields = new[]
                {
                    new { Name = "m_soundtrack", Type = 6, Index = -6 }
                }
            }
        ),  // 158

        new KeyValuePair<string, object>(
            "_struct",
            new
            {
                Fields = new[]
                {
                    new { Name = "m_planetId", Type = 47, Index = -6 }
                }
            }
        ),  // 159

        new KeyValuePair<string, object>(
            "_struct",
            new
            {
                Fields = new[]
                {
                    new { Name = "m_key", Type = 116, Index = -7 },
                    new { Name = "m_flags", Type = 116, Index = -6 }
                }
            }
        ),  // 160
        new KeyValuePair<string, object>(
            "_struct",
            new
            {
                Fields = new[]
                {
                    new { Name = "m_resources", Type = 113, Index = -6 }
                }
            }
        ),  // 161

        new KeyValuePair<string, object>(
            "_struct",
            new
            {
                Fields = new[]
                {
                    new { Name = "m_fulfillRequestId", Type = 47, Index = -6 }
                }
            }
        ),  // 162

        new KeyValuePair<string, object>(
            "_struct",
            new
            {
                Fields = new[]
                {
                    new { Name = "m_cancelRequestId", Type = 47, Index = -6 }
                }
            }
        ),  // 163

        new KeyValuePair<string, object>(
            "_struct",
            new
            {
                Fields = new[]
                {
                    new { Name = "m_error", Type = 47, Index = -7 },
                    new { Name = "m_abil", Type = 93, Index = -6 }
                }
            }
        ),  // 164

        new KeyValuePair<string, object>(
            "_struct",
            new
            {
                Fields = new[]
                {
                    new { Name = "m_researchItemId", Type = 47, Index = -6 }
                }
            }
        ),  // 165

        new KeyValuePair<string, object>(
            "_struct",
            new
            {
                Fields = new[]
                {
                    new { Name = "m_mercenaryId", Type = 47, Index = -6 }
                }
            }
        ),  // 166

        new KeyValuePair<string, object>(
            "_struct",
            new
            {
                Fields = new[]
                {
                    new { Name = "m_battleReportId", Type = 47, Index = -7 },
                    new { Name = "m_difficultyLevel", Type = 47, Index = -6 }
                }
            }
        ),  // 167

        new KeyValuePair<string, object>(
            "_struct",
            new
            {
                Fields = new[]
                {
                    new { Name = "m_battleReportId", Type = 47, Index = -6 }
                }
            }
        ),  // 168

        new KeyValuePair<string, object>(
            "_struct",
            new
            {
                Fields = new[]
                {
                    new { Name = "m_decrementSeconds", Type = 47, Index = -6 }
                }
            }
        ),  // 169

        new KeyValuePair<string, object>(
            "_struct",
            new
            {
                Fields = new[]
                {
                    new { Name = "m_portraitId", Type = 47, Index = -6 }
                }
            }
        ),  // 170

        new KeyValuePair<string, object>(
            "_struct",
            new
            {
                Fields = new[]
                {
                    new { Name = "m_functionName", Type = 20, Index = -6 }
                }
            }
        ),  // 171

        new KeyValuePair<string, object>(
            "_struct",
            new
            {
                Fields = new[]
                {
                    new { Name = "m_result", Type = 47, Index = -6 }
                }
            }
        ),  // 172

        new KeyValuePair<string, object>(
            "_struct",
            new
            {
                Fields = new[]
                {
                    new { Name = "m_gameMenuItemIndex", Type = 47, Index = -6 }
                }
            }
        ),  // 173

        new KeyValuePair<string, object>(
            "_int",
            new
            {
                MinValue = -32768,
                MaxValue = 16
            }
        ),  // 174

        new KeyValuePair<string, object>(
            "_struct",
            new
            {
                Fields = new[]
                {
                    new { Name = "m_wheelSpin", Type = 174, Index = -7 },
                    new { Name = "m_flags", Type = 116, Index = -6 }
                }
            }
        ),  // 175

        new KeyValuePair<string, object>(
            "_struct",
            new
            {
                Fields = new[]
                {
                    new { Name = "m_purchaseCategoryId", Type = 47, Index = -6 }
                }
            }
        ),  // 176

        new KeyValuePair<string, object>(
            "_struct",
            new
            {
                Fields = new[]
                {
                    new { Name = "m_button", Type = 83, Index = -6 }
                }
            }
        ),  // 177

        new KeyValuePair<string, object>(
            "_struct",
            new
            {
                Fields = new[]
                {
                    new { Name = "m_cutsceneId", Type = 47, Index = -7 },
                    new { Name = "m_bookmarkName", Type = 20, Index = -6 }
                }
            }
        ),  // 178

        new KeyValuePair<string, object>(
            "_struct",
            new
            {
                Fields = new[]
                {
                    new { Name = "m_cutsceneId", Type = 47, Index = -6 }
                }
            }
        ),  // 179

        new KeyValuePair<string, object>(
            "_struct",
            new
            {
                Fields = new[]
                {
                    new { Name = "m_cutsceneId", Type = 47, Index = -8 },
                    new { Name = "m_conversationLine", Type = 20, Index = -7 },
                    new { Name = "m_altConversationLine", Type = 20, Index = -6 }
                }
            }
        ),  // 180

        new KeyValuePair<string, object>(
            "_struct",
            new
            {
                Fields = new[]
                {
                    new { Name = "m_cutsceneId", Type = 47, Index = -7 },
                    new { Name = "m_conversationLine", Type = 20, Index = -6 }
                }
            }
        ),  // 181

        new KeyValuePair<string, object>(
            "_struct",
            new
            {
                Fields = new[]
                {
                    new { Name = "m_leaveReason", Type = 1, Index = -6 }
                }
            }
        ),  // 182

        new KeyValuePair<string, object>(
            "_struct",
            new
            {
                Fields = new[]
                {
                    new { Name = "m_observe", Type = 24, Index = -12 },
                    new { Name = "m_name", Type = 9, Index = -11 },
                    new { Name = "m_toonHandle", Type = 127, Index = -10 },
                    new { Name = "m_clanTag", Type = 41, Index = -9 },
                    new { Name = "m_clanLogo", Type = 42, Index = -8 },
                    new { Name = "m_hijack", Type = 13, Index = -7 },
                    new { Name = "m_hijackCloneGameUserId", Type = 60, Index = -6 }
                }
            }
        ),  // 183

        new KeyValuePair<string, object>(
            "_optional",
            99
        ),  // 184
        new KeyValuePair<string, object>(
            "_struct",
            new
            {
                Fields = new[]
                {
                    new { Name = "m_state", Type = 24, Index = -7 },
                    new { Name = "m_sequence", Type = 184, Index = -6 }
                }
            }
        ),  // 185

        new KeyValuePair<string, object>(
            "_struct",
            new
            {
                Fields = new[]
                {
                    new { Name = "m_target", Type = 96, Index = -6 }
                }
            }
        ),  // 186

        new KeyValuePair<string, object>(
            "_struct",
            new
            {
                Fields = new[]
                {
                    new { Name = "m_target", Type = 97, Index = -6 }
                }
            }
        ),  // 187

        new KeyValuePair<string, object>(
            "_struct",
            new
            {
                Fields = new[]
                {
                    new { Name = "m_catalog", Type = 10, Index = -9 },
                    new { Name = "m_entry", Type = 83, Index = -8 },
                    new { Name = "m_field", Type = 9, Index = -7 },
                    new { Name = "m_value", Type = 9, Index = -6 }
                }
            }
        ),  // 188

        new KeyValuePair<string, object>(
            "_struct",
            new
            {
                Fields = new[]
                {
                    new { Name = "m_index", Type = 6, Index = -6 }
                }
            }
        ),  // 189

        new KeyValuePair<string, object>(
            "_struct",
            new
            {
                Fields = new[]
                {
                    new { Name = "m_shown", Type = 13, Index = -6 }
                }
            }
        ),  // 190

        new KeyValuePair<string, object>(
            "_struct",
            new
            {
                Fields = new[]
                {
                    new { Name = "m_syncTime", Type = 6, Index = -6 }
                }
            }
        ),  // 191

        new KeyValuePair<string, object>(
            "_struct",
            new
            {
                Fields = new[]
                {
                    new { Name = "m_recipient", Type = 12, Index = -3 },
                    new { Name = "m_string", Type = 30, Index = -2 }
                }
            }
        ),  // 192

        new KeyValuePair<string, object>(
            "_struct",
            new
            {
                Fields = new[]
                {
                    new { Name = "m_recipient", Type = 12, Index = -3 },
                    new { Name = "m_point", Type = 88, Index = -2 }
                }
            }
        ),  // 193

        new KeyValuePair<string, object>(
            "_struct",
            new
            {
                Fields = new[]
                {
                    new { Name = "m_progress", Type = 47, Index = -2 }
                }
            }
        ),  // 194

        new KeyValuePair<string, object>(
            "_struct",
            new
            {
                Fields = new[]
                {
                    new { Name = "m_status", Type = 24, Index = -2 }
                }
            }
        ),  // 195

        new KeyValuePair<string, object>(
            "_struct",
            new
            {
                Fields = new[]
                {
                    new { Name = "m_scoreValueMineralsCurrent", Type = 47, Index = 0 },
                    new { Name = "m_scoreValueVespeneCurrent", Type = 47, Index = 1 },
                    new { Name = "m_scoreValueMineralsCollectionRate", Type = 47, Index = 2 },
                    new { Name = "m_scoreValueVespeneCollectionRate", Type = 47, Index = 3 },
                    new { Name = "m_scoreValueWorkersActiveCount", Type = 47, Index = 4 },
                    new { Name = "m_scoreValueMineralsUsedInProgressArmy", Type = 47, Index = 5 },
                    new { Name = "m_scoreValueMineralsUsedInProgressEconomy", Type = 47, Index = 6 },
                    new { Name = "m_scoreValueMineralsUsedInProgressTechnology", Type = 47, Index = 7 },
                    new { Name = "m_scoreValueVespeneUsedInProgressArmy", Type = 47, Index = 8 },
                    new { Name = "m_scoreValueVespeneUsedInProgressEconomy", Type = 47, Index = 9 },
                    new { Name = "m_scoreValueVespeneUsedInProgressTechnology", Type = 47, Index = 10 },
                    new { Name = "m_scoreValueMineralsUsedCurrentArmy", Type = 47, Index = 11 },
                    new { Name = "m_scoreValueMineralsUsedCurrentEconomy", Type = 47, Index = 12 },
                    new { Name = "m_scoreValueMineralsUsedCurrentTechnology", Type = 47, Index = 13 },
                    new { Name = "m_scoreValueVespeneUsedCurrentArmy", Type = 47, Index = 14 },
                    new { Name = "m_scoreValueVespeneUsedCurrentEconomy", Type = 47, Index = 15 },
                    new { Name = "m_scoreValueVespeneUsedCurrentTechnology", Type = 47, Index = 16 },
                    new { Name = "m_scoreValueMineralsLostArmy", Type = 47, Index = 17 },
                    new { Name = "m_scoreValueMineralsLostEconomy", Type = 47, Index = 18 },
                    new { Name = "m_scoreValueMineralsLostTechnology", Type = 47, Index = 19 },
                    new { Name = "m_scoreValueVespeneLostArmy", Type = 47, Index = 20 },
                    new { Name = "m_scoreValueVespeneLostEconomy", Type = 47, Index = 21 },
                    new { Name = "m_scoreValueVespeneLostTechnology", Type = 47, Index = 22 },
                    new { Name = "m_scoreValueMineralsKilledArmy", Type = 47, Index = 23 },
                    new { Name = "m_scoreValueMineralsKilledEconomy", Type = 47, Index = 24 },
                    new { Name = "m_scoreValueMineralsKilledTechnology", Type = 47, Index = 25 },
                    new { Name = "m_scoreValueVespeneKilledArmy", Type = 47, Index = 26 },
                    new { Name = "m_scoreValueVespeneKilledEconomy", Type = 47, Index = 27 },
                    new { Name = "m_scoreValueVespeneKilledTechnology", Type = 47, Index = 28 },
                    new { Name = "m_scoreValueFoodUsed", Type = 47, Index = 29 },
                    new { Name = "m_scoreValueFoodMade", Type = 47, Index = 30 },
                    new { Name = "m_scoreValueMineralsUsedActiveForces", Type = 47, Index = 31 },
                    new { Name = "m_scoreValueVespeneUsedActiveForces", Type = 47, Index = 32 },
                    new { Name = "m_scoreValueMineralsFriendlyFireArmy", Type = 47, Index = 33 },
                    new { Name = "m_scoreValueMineralsFriendlyFireEconomy", Type = 47, Index = 34 },
                    new { Name = "m_scoreValueMineralsFriendlyFireTechnology", Type = 47, Index = 35 },
                    new { Name = "m_scoreValueVespeneFriendlyFireArmy", Type = 47, Index = 36 },
                    new { Name = "m_scoreValueVespeneFriendlyFireEconomy", Type = 47, Index = 37 },
                    new { Name = "m_scoreValueVespeneFriendlyFireTechnology", Type = 47, Index = 38 }
                }
            }
        ),  // 196

        new KeyValuePair<string, object>(
            "_struct",
            new
            {
                Fields = new[]
                {
                    new { Name = "m_playerId", Type = 1, Index = 0 },
                    new { Name = "m_stats", Type = 196, Index = 1 }
                }
            }
        ),  // 197

        new KeyValuePair<string, object>(
            "_optional",
            29
        ),  // 198
        new KeyValuePair<string, object>(
            "_struct",
            new
            {
                Fields = new[]
                {
                    new { Name = "m_unitTagIndex", Type = 6, Index = 0 },
                    new { Name = "m_unitTagRecycle", Type = 6, Index = 1 },
                    new { Name = "m_unitTypeName", Type = 29, Index = 2 },
                    new { Name = "m_controlPlayerId", Type = 1, Index = 3 },
                    new { Name = "m_upkeepPlayerId", Type = 1, Index = 4 },
                    new { Name = "m_x", Type = 10, Index = 5 },
                    new { Name = "m_y", Type = 10, Index = 6 },
                    new { Name = "m_creatorUnitTagIndex", Type = 43, Index = 7 },
                    new { Name = "m_creatorUnitTagRecycle", Type = 43, Index = 8 },
                    new { Name = "m_creatorAbilityName", Type = 198, Index = 9 }
                }
            }
        ),  // 199

        new KeyValuePair<string, object>(
            "_struct",
            new
            {
                Fields = new[]
                {
                    new { Name = "m_unitTagIndex", Type = 6, Index = 0 },
                    new { Name = "m_unitTagRecycle", Type = 6, Index = 1 },
                    new { Name = "m_killerPlayerId", Type = 60, Index = 2 },
                    new { Name = "m_x", Type = 10, Index = 3 },
                    new { Name = "m_y", Type = 10, Index = 4 },
                    new { Name = "m_killerUnitTagIndex", Type = 43, Index = 5 },
                    new { Name = "m_killerUnitTagRecycle", Type = 43, Index = 6 }
                }
            }
        ),  // 200

        new KeyValuePair<string, object>(
            "_struct",
            new
            {
                Fields = new[]
                {
                    new { Name = "m_unitTagIndex", Type = 6, Index = 0 },
                    new { Name = "m_unitTagRecycle", Type = 6, Index = 1 },
                    new { Name = "m_controlPlayerId", Type = 1, Index = 2 },
                    new { Name = "m_upkeepPlayerId", Type = 1, Index = 3 }
                }
            }
        ),  // 201

        new KeyValuePair<string, object>(
            "_struct",
            new
            {
                Fields = new[]
                {
                    new { Name = "m_unitTagIndex", Type = 6, Index = 0 },
                    new { Name = "m_unitTagRecycle", Type = 6, Index = 1 },
                    new { Name = "m_unitTypeName", Type = 29, Index = 2 }
                }
            }
        ),  // 202

        new KeyValuePair<string, object>(
            "_struct",
            new
            {
                Fields = new[]
                {
                    new { Name = "m_playerId", Type = 1, Index = 0 },
                    new { Name = "m_upgradeTypeName", Type = 29, Index = 1 },
                    new { Name = "m_count", Type = 47, Index = 2 }
                }
            }
        ),  // 203

        new KeyValuePair<string, object>(
            "_struct",
            new
            {
                Fields = new[]
                {
                    new { Name = "m_unitTagIndex", Type = 6, Index = 0 },
                    new { Name = "m_unitTagRecycle", Type = 6, Index = 1 },
                    new { Name = "m_unitTypeName", Type = 29, Index = 2 },
                    new { Name = "m_controlPlayerId", Type = 1, Index = 3 },
                    new { Name = "m_upkeepPlayerId", Type = 1, Index = 4 },
                    new { Name = "m_x", Type = 10, Index = 5 },
                    new { Name = "m_y", Type = 10, Index = 6 }
                }
            }
        ),  // 204

        new KeyValuePair<string, object>(
            "_struct",
            new
            {
                Fields = new[]
                {
                    new { Name = "m_unitTagIndex", Type = 6, Index = 0 },
                    new { Name = "m_unitTagRecycle", Type = 6, Index = 1 }
                }
            }
        ),  // 205

        new KeyValuePair<string, object>(
            "_array",
            new
            {
                ItemType = 47,
                Length = 10
            }
        ),  // 206

        new KeyValuePair<string, object>(
            "_struct",
            new
            {
                Fields = new[]
                {
                    new { Name = "m_firstUnitIndex", Type = 6, Index = 0 },
                    new { Name = "m_items", Type = 206, Index = 1 }
                }
            }
        ),  // 207

        new KeyValuePair<string, object>(
            "_struct",
            new
            {
                Fields = new[]
                {
                    new { Name = "m_playerId", Type = 1, Index = 0 },
                    new { Name = "m_type", Type = 6, Index = 1 },
                    new { Name = "m_userId", Type = 43, Index = 2 },
                    new { Name = "m_slotId", Type = 43, Index = 3 }
                }
            }
        )  // 208
    };
     }
}
