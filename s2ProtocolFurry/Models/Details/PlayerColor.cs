namespace s2ProtocolFurry.Models.Details
{
    public class PlayerColor
    {
        public PlayerColor(int a, int b, int g, int r)
        {
            A = a;
            B = b;
            G = g;
            R = r;
        }

        public int A { get; }
        public int B { get; }
        public int G { get; }
        public int R { get; }
    }
}