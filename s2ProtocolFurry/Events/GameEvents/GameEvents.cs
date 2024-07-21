
namespace s2ProtocolFurry.Models.GameEvents
{
    public class GameEvents
    {
        public GameEvents(List<GameEvent> gameevents)
        {
            Gameevents = gameevents;
        }

        public List<GameEvent> Gameevents { get; }
    }
}