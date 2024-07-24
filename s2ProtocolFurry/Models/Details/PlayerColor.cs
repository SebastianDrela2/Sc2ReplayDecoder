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

        public PlayerColor()
        {

        }

        public int A { get; set; }
        public int B { get; set; }
        public int G { get; set; }
        public int R { get; set; }
    }
}