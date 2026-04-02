using PatternComposite.Composite;
using System;

namespace PatternComposite
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Group swordsmen = new Group();
            Group archers = new Group();

            swordsmen.Add(new Unit("Рыцарь", 100));
            swordsmen.Add(new Unit("Солдат", 80));
            swordsmen.Add(new Unit("Солдат", 80));

            archers.Add(new Unit("Лучник", 60));
            archers.Add(new Unit("Лучник", 60));
            archers.Add(new Unit("Маг", 50));

            Group army = new Group();

            army.Add(swordsmen);
            army.Add(archers);

            Console.WriteLine($"Общее здоровье армии: {army.GetTotalHealth()}");

            Console.WriteLine("\nНаносим 30 урона по всей армии:\n");

            army.TakeDamage(30);

            Console.WriteLine($"\nОбщее здоровье армии: {army.GetTotalHealth()}");

            Console.WriteLine("\nНаносим 30 урона по отряду мечников:\n");

            swordsmen.TakeDamage(30);

            Console.WriteLine($"\nОбщее здоровье отряда: {swordsmen.GetTotalHealth()}");

            Console.WriteLine("\nНаносим 30 урона по отряду лучников:\n");

            archers.TakeDamage(30);

            Console.WriteLine($"\nОбщее здоровье отряда: {archers.GetTotalHealth()}");
            Console.WriteLine($"Общее здоровье армии: {army.GetTotalHealth()}");
        }
    }
}
