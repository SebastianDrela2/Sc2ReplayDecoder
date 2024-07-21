﻿namespace s2ProtocolFurry.Events.GameEvents
{
    internal class SBankValueEvent
    {
        public SBankValueEvent(GameEvent gameEvent, string name, string data, int type)
        {
            GameEvent = gameEvent;
            Name = name;
            Data = data;
            Type = type;
        }

        public GameEvent GameEvent { get; }
        public string Name { get; }
        public string Data { get; }
        public int Type { get; }
    }
}