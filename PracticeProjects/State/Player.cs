using System;

namespace State
{
    public class Player
    {
        public string Name { get; private set; }
        public float Health { get; private set; }
        public float X { get; private set; }
        public float Y { get; private set; }
        public bool IsAlive => Health > 0;

        public Player(string name, float health, float x, float y)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Health = health;
            X = x;
            Y = y;
        }

        public void TakeDamage(float damage)
        {
            Health -= Math.Max(damage, 0);
            Health = Math.Max(Health, 0);
        }

        public void MoveTo(float x, float y)
        {
            X = x; Y = y;
        }

        public float GetDistanceTo(Enemy enemy)
        {
            double deltaX = enemy.X - X;
            double deltaY = enemy.Y - Y;

            return (float)Math.Sqrt(deltaX * deltaX + deltaY * deltaY);
        }
    }
}
