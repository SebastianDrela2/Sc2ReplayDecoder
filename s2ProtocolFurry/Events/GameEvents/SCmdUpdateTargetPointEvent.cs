﻿namespace s2ProtocolFurry.Events.GameEvents
{
    internal class SCmdUpdateTargetPointEvent
    {
        public SCmdUpdateTargetPointEvent(GameEvent gameEvent, long x, long y, long z)
        {
            GameEvent = gameEvent;
            X = x;
            Y = y;
            Z = z;
        }

        public GameEvent GameEvent { get; }
        public long X { get; }
        public long Y { get; }
        public long Z { get; }
    }
}