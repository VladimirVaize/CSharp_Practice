using System;

namespace Reflection
{
    public class Player
    {
        private int _health;
        private int _maxHealth;
        public string Name { get; private set; }
        public int Level { get; private set; }

        public Player(string name, int maxHealth)
        {
            Name = name;
            _maxHealth = maxHealth;
            _health = maxHealth;
            Level = 1;
        }

        private void TakeDamage(int damage)
        {
            _health -= damage;
            Console.WriteLine($"{Name} получил {damage} урона! HP: {_health}/{_maxHealth}");
        }

        public void Heal(int amount)
        {
            _health = Math.Min(_health + amount, _maxHealth);
            Console.WriteLine($"{Name} восстановил {amount} HP. HP: {_health}/{_maxHealth}");
        }

        private int GetHealthPercent()
        {
            return (int)((double)_health / _maxHealth * 100);
        }
    }
}
