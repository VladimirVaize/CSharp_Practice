using System;

namespace Command
{
    public class Player
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Health { get; set; }

        public Player(int x, int y, int health)
        {
            X = x;
            Y = y;
            Health = Math.Max(1, health);
        }

        public void Move(int dx, int dy)
        {
            X += dx;
            Y += dy;
        }

        public void Heal(int healPower)
        {
            Health += Math.Max(0, healPower);
        }
    }
}
