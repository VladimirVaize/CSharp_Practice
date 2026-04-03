using PatternDecorator.Character.Core;

namespace PatternDecorator.Character
{
    public class MagicShieldBuff : BuffDecorator
    {
        public MagicShieldBuff(ICharacterStats stats) : base(stats) { }

        public override float GetMagicResist() => _stats.GetMagicResist() + 20f;
    }
}
