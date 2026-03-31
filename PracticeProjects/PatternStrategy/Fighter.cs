using PatternStrategy.Strategies;
using System;

namespace PatternStrategy
{
    public class Fighter
    {
        public string Name { get; private set; }
        public int BaseDamage { get; private set; }
        public IDamageStrategy DamageStrategy { get; private set; }

        public Fighter(string name, int baseDamage, IDamageStrategy damageStrategy)
        {
            Name = name;
            BaseDamage = baseDamage;
            DamageStrategy = damageStrategy ?? throw new ArgumentNullException(nameof(damageStrategy));
        }

        public void Attack()
        {
            int damage = DamageStrategy.CalculateDamage(BaseDamage);
            string description = DamageStrategy.GetDamageDescription();
            Console.WriteLine($"- Атака! {Name} наносит {damage} урона! {description}");
        }

        public void SetStrategy(IDamageStrategy newStrategy)
        {
            DamageStrategy = newStrategy ?? throw new ArgumentNullException(nameof(newStrategy));
        }
    }
}
