using s2ProtocolFurry.Models.Details;

namespace s2ProtocolFurry.Parse
{
    public static partial class Parse
    {
        public static Details Details(Dictionary<string, object> details)
        {
            var campaignIndex = GetInt(details, "m_campaignIndex");
            var defaultDiff = GetInt(details, "m_defaultDifficulty");
            var desc = GetString(details, "m_description");
            var diff = GetString(details, "m_difficulty");
            var disableRec = GetBool(details, "m_disableRecoverGame");
            var speed = GetInt(details, "m_gameSpeed");
            var image = GetString(details, "m_imageFilePath");
            var isBlizzard = GetBool(details, "m_isBlizzardMap");
            var mapName = GetString(details, "m_mapFileName");
            var mini = GetBool(details, "m_miniSave");
            var restart = GetBool(details, "m_restartAsTransitionMap");
            var offset = GetBigInt(details, "m_timeLocalOffset");
            var time = GetBigInt(details, "m_timeUTC");
            var title = GetString(details, "m_title");
            var players = GetDetailsPlayers(details);

            return new Details(campaignIndex,
                               defaultDiff,
                               desc,
                               diff,
                               disableRec,
                               speed,
                               image,
                               isBlizzard,
                               mapName,
                               mini,
                               restart,
                               offset,
                               time,
                               title,
                               players);
        }

        private static List<DetailsPlayer> GetDetailsPlayers(Dictionary<string, object> details)
        {
            List<DetailsPlayer> players = new();
            if (details.TryGetValue("m_playerList", out var playerListObj) && playerListObj is ICollection<object> playerList)
            {
                foreach (var playerObj in playerList)
                {
                    if (playerObj is Dictionary<string, object> playerDic)
                    {
                        var color = GetColor(playerDic);
                        var control = GetInt(playerDic, "m_control");
                        var handicap = GetInt(playerDic, "m_handicap");
                        var hero = GetString(playerDic, "m_hero");
                        var name = GetString(playerDic, "m_name");
                        var observe = GetInt(playerDic, "m_observe");
                        var race = GetString(playerDic, "m_race");
                        var result = GetInt(playerDic, "m_result");
                        var team = GetInt(playerDic, "m_teamId");
                        var toon = GetToon(playerDic);
                        var slot = GetInt(playerDic, "m_workingSetSlotId");
                        players.Add(new DetailsPlayer(color,
                                                      control,
                                                      handicap,
                                                      hero,
                                                      name,
                                                      observe,
                                                      race,
                                                      result,
                                                      team,
                                                      toon,
                                                      slot));
                    }
                }
            }

            return players;
        }

        private static Toon GetToon(Dictionary<string, object> details)
        {
            if (details.TryGetValue("m_toon", out var toonObj) && toonObj is Dictionary<string, object> toonDic)
            {
                return new Toon(GetInt(toonDic, "m_id"), GetString(toonDic, "m_programId"), GetInt(toonDic, "m_realm"), GetInt(toonDic, "m_region"));
            }
            return new Toon(0, "", 0, 0);
        }

        private static PlayerColor GetColor(Dictionary<string, object> details)
        {
            if (details.TryGetValue("m_color", out var colorObj) && colorObj is Dictionary<string, object> colorDic)
            {
                return new PlayerColor(GetInt(colorDic, "m_a"), GetInt(colorDic, "m_b"), GetInt(colorDic, "m_g"), GetInt(colorDic, "m_r"));
            }
            return new PlayerColor(0, 0, 0, 0);
        }
    }
}
