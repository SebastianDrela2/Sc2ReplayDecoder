namespace s2ProtocolFurry.Events.GameEvents
{
    internal class SBankSignatureEvent
    {
        public SBankSignatureEvent(GameEvent gameEvent, string toonHandle, List<int> signature)
        {
            GameEvent = gameEvent;
            ToonHandle = toonHandle;
            Signature = signature;
        }

        public GameEvent GameEvent { get; }
        public string ToonHandle { get; }
        public List<int> Signature { get; }
    }
}