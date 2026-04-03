using PatternDecorator.Character.Core;
using System;

namespace PatternDecorator.Character
{
    public abstract class BuffDecorator : ICharacterStats
    {
        protected ICharacterStats _stats;

        public BuffDecorator(ICharacterStats characterStats)
        {
            _stats = characterStats ?? throw new ArgumentNullException(nameof(characterStats));
        }

        public virtual float GetDamage() => _stats.GetDamage();
        public virtual float GetAttackSpeed() => _stats.GetAttackSpeed();
        public virtual float GetMagicResist() => _stats.GetMagicResist();
    }
}
