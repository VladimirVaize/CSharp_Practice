namespace AbstractFactory.AbstractFactory
{
    public interface IConsumable
    {
        string Name { get; }
        int Power { get; }

        void ShowInfo();
        void Use();
    }
}
