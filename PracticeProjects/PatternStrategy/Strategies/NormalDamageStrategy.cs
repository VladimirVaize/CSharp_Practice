namespace PatternStrategy.Strategies
{
    public class NormalDamageStrategy : IDamageStrategy
    {
        public int CalculateDamage(int baseDamage)
        {
            if (baseDamage > 0)
                return baseDamage;
            else return 0;
        }

        public string GetDamageDescription() => "";
    }
}
