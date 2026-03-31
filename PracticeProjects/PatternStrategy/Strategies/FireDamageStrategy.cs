namespace PatternStrategy.Strategies
{
    public class FireDamageStrategy : IDamageStrategy
    {
        public int CalculateDamage(int baseDamage)
        {
            if (baseDamage > 0)
                return (int)(baseDamage * 1.2f);
            else return 0;
        }

        public string GetDamageDescription() => "(ОГОНЬ!)";
    }
}
