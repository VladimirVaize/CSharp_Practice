using System;

namespace PatternComposite.Composite
{
    public class Unit : IGameEntity
    {
        public string Name { get; private set; }
        public int Health { get; private set; }

        public Unit(string name, int health)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Health = Math.Max(health, 1);
        }

        public void TakeDamage(int damage)
        {
            if (damage <= 0)
                return;

            int healthBeforeDamage = Health;

            Health = Math.Max(0, Health - damage);
            Console.WriteLine($"Юнит {Name} получил {Math.Min(healthBeforeDamage, damage)} урона. Осталось здоровья: {Health}");
        }

        public int GetTotalHealth()
        {
            return Health;
        }
    }
}
