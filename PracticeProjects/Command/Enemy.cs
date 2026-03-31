using System;

namespace Command
{
    public class Enemy
    {
        public int Health { get; set; }

        public Enemy(int health)
        {
            Health = Math.Max(1, health);
        }

        public void TakeDamage(int damage)
        {
            Health -= Math.Min(Health, damage);
        }
    }
}
