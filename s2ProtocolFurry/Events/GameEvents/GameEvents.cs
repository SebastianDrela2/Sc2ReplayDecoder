namespace s2ProtocolFurry.Events.GameEvents
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