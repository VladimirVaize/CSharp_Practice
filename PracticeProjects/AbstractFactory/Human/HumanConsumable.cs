using AbstractFactory.AbstractFactory;
using System;

namespace AbstractFactory.Human
{
    public class HumanConsumable : IConsumable
    {
        public string Name { get; } = "Целебное зелье";
        public int Power { get; } = 50;

        // Метод получения информации
        public void ShowInfo()
        {
            Console.WriteLine($"- {Name}: восстанавливает {Power} HP.");
        }

        // Метод вызываемый при использовании предмета
        public void Use()
        {
            Console.WriteLine($"Восстанавливает {Power} HP.");
        }
    }
}
