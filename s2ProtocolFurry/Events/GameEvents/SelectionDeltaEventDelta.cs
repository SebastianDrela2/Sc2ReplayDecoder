using s2ProtocolFurry.NNetGame;

namespace s2ProtocolFurry.Events.GameEvents
{
    internal class SelectionDeltaEventDelta
    {
        public SelectionDeltaEventDelta(List<int> addUnitTags, List<SelectionDeltaEventDeltaSubGroup> subgroups, List<int> zeroIndices, int subgroupIndex)
        {
            AddUnitTags = addUnitTags;
            Subgroups = subgroups;
            ZeroIndices = zeroIndices;
            SubgroupIndex = subgroupIndex;
        }

        public List<int> AddUnitTags { get; }
        public List<SelectionDeltaEventDeltaSubGroup> Subgroups { get; }
        public List<int> ZeroIndices { get; }
        public int SubgroupIndex { get; }
    }
}