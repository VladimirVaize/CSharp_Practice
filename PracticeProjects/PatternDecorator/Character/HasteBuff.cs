using PatternDecorator.Character.Core;

namespace PatternDecorator.Character
{
    public class HasteBuff : BuffDecorator
    {
        public HasteBuff(ICharacterStats stats) : base(stats) { }

        public override float GetAttackSpeed() => _stats.GetAttackSpeed() * 1.3f;
    }
}
