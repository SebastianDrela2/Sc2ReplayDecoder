namespace s2ProtocolFurry.Models.Details
{
    public class DetailsPlayer
    {
        public DetailsPlayer(PlayerColor color, int control, int handicap, string hero, string name, int observe, string race, int result, int team, Toon toon, int slot)
        {
            Color = color;
            Control = control;
            Handicap = handicap;
            Hero = hero;
            Name = name;
            Observe = observe;
            Race = race;
            Result = result;
            Team = team;
            Toon = toon;
            Slot = slot ;
            if (name != null && name.Contains("<sp/>", StringComparison.Ordinal))
            {
                var ents = name.Split("<sp/>");
                Name = ents[1];
                ClanName = ents[0].Length > 8 ? ents[0][4..^4] : null;
            }
            else
            {
                Name = name ?? "";
            }
        }

        public PlayerColor Color { get; }
        public int Control { get; }
        public int Handicap { get; }
        public string Hero { get; }
        public string Name { get; }
        public int Observe { get; }
        public string Race { get; }
        public int Result { get; }
        public int Team { get; }
        public Toon Toon { get; }
        public int Slot { get; }
        public string? ClanName { get; init; }
    }
}