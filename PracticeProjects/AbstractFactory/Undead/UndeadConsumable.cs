using AbstractFactory.AbstractFactory;
using System;

namespace AbstractFactory.Undead
{
    public class UndeadConsumable : IConsumable
    {
        public string Name { get; } = "Зелье распада";
        public int Power { get; } = 10;

        // Метод получения информации
        public void ShowInfo()
        {
            Console.WriteLine($"- {Name}: наносит {Power} урона врагу в течении 5 сек.");
        }

        // Метод вызываемый при использовании предмета
        public void Use()
        {
            Console.WriteLine($"Наносит {Power} урона врагу в течении 5 сек.");
        }
    }
}
