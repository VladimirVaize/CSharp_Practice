using PatternDecorator.Character.Core;

namespace PatternDecorator.Character
{
    public class BaseCharacter : ICharacterStats
    {
        private float _damage;
        private float _attackSpeed;
        private float _magicResist;

        public BaseCharacter(float baseDamage, float baseAttackSpeed, float baseMagicResist)
        {
            _damage = baseDamage;
            _attackSpeed = baseAttackSpeed;
            _magicResist = baseMagicResist;
        }

        public float GetDamage() => _damage;
        public float GetAttackSpeed() => _attackSpeed;
        public float GetMagicResist() => _magicResist;
    }
}
