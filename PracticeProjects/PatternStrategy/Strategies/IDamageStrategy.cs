namespace PatternStrategy.Strategies
{
    public interface IDamageStrategy
    {
        int CalculateDamage(int baseDamage);
        string GetDamageDescription();
    }
}
