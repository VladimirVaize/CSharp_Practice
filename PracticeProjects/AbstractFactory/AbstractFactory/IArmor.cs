namespace AbstractFactory.AbstractFactory
{
    public interface IArmor
    {
        string Name { get; }
        int Protection { get; }

        void ShowInfo();
        void Defend();
    }
}
