namespace PatternStrategy.Strategies
{
    public class GlancingDamageStrategy : IDamageStrategy
    {
        public int CalculateDamage(int baseDamage)
        {
            if (baseDamage > 0)
                return baseDamage / 2;
            else return 0;
        }

        public string GetDamageDescription() => "";
    }
}
