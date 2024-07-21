namespace s2ProtocolFurry.Models.Details
{
    public class Toon
    {
        public Toon(int id, string programId, int realm, int region)
        {
            Id = id;
            ProgramId = programId;
            Realm = realm;
            Region = region;
        }

        public int Id { get; }
        public string ProgramId { get; }
        public int Realm { get; }
        public int Region { get; }
    }
}