using PatternDecorator.Character.Core;

namespace PatternDecorator.Character
{
    public class BerserkBuff : BuffDecorator
    {
        public BerserkBuff(ICharacterStats stats) : base(stats) { }

        public override float GetDamage() => _stats.GetDamage() * 1.5f;

        public override float GetMagicResist() => _stats.GetMagicResist() * 0.5f;
    }
}
