using AbstractFactory.AbstractFactory;
using System;

namespace AbstractFactory.Orc
{
    public class OrcConsumable : IConsumable
    {
        public string Name { get; } = "Зелье ярости";
        public int Power { get; } = 50;

        // Метод получения информации
        public void ShowInfo()
        {
            Console.WriteLine($"- {Name}: увеличивает урон на {Power}% на 10 секунд");
        }

        // Метод вызываемый при использовании предмета
        public void Use()
        {
            Console.WriteLine($"Увеличивает урон на {Power}% на 10 секунд.");
        }
    }
}
