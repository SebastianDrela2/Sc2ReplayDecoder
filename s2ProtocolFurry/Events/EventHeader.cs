namespace s2ProtocolFurry.Events;

public record struct EventHeader(int PlayerId, int Bits, uint Gameloop);
