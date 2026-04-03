using PatternDecorator.Character.Core;

namespace PatternDecorator.Character
{
    public class DoubleDamageBuff : BuffDecorator
    {
        public DoubleDamageBuff(ICharacterStats stats) : base(stats) { }

        public override float GetDamage()
        {
            if (_stats.GetDamage() < 30)
                return _stats.GetDamage() * 2f;

            return _stats.GetDamage();
        }
    }
}
