using System.Numerics;

namespace s2ProtocolFurry.Models.InitData
{
    public class SlotDescription
    {
        public SlotDescription(KeyValuePair<int, BigInteger> allowedRaces, KeyValuePair<int, BigInteger> allowedColors, KeyValuePair<int, BigInteger> allowedAIBuilds, KeyValuePair<int, BigInteger> allowedDifficulty, KeyValuePair<int, BigInteger> allowedObserveTypes, KeyValuePair<int, BigInteger> allowedControls)
        {
            AllowedRaces = allowedRaces;
            AllowedColors = allowedColors;
            AllowedAIBuilds = allowedAIBuilds;
            AllowedDifficulty = allowedDifficulty;
            AllowedObserveTypes = allowedObserveTypes;
            AllowedControls = allowedControls;
        }

        public KeyValuePair<int, BigInteger> AllowedRaces { get; }
        public KeyValuePair<int, BigInteger> AllowedColors { get; }
        public KeyValuePair<int, BigInteger> AllowedAIBuilds { get; }
        public KeyValuePair<int, BigInteger> AllowedDifficulty { get; }
        public KeyValuePair<int, BigInteger> AllowedObserveTypes { get; }
        public KeyValuePair<int, BigInteger> AllowedControls { get; }
    }
}