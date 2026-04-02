using System;

namespace PatternBehaviorTree.Auxiliary
{
    public class Vector2
    {
        public int X;
        public int Y;

        public Vector2(int x, int y)
        {
            X = x;
            Y = y;
        }

        public static double Distance(Vector2 a, Vector2 b)
        {
            int dx = a.X - b.X;
            int dy = a.Y - b.Y;
            return Math.Sqrt(dx * dx + dy * dy);
        }

        public override string ToString()
        {
            return $"[{X}, {Y}]";
        }
    }
}
