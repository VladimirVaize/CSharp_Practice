namespace AbstractFactory.AbstractFactory
{
    public interface IWeapon
    {
        string Name { get; }
        int Damage { get; }

        void ShowInfo();
        void Attack();
    }
}
