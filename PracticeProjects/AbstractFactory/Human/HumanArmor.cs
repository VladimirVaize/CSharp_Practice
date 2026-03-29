using AbstractFactory.AbstractFactory;
using System;

namespace AbstractFactory.Human
{
    public class HumanArmor : IArmor
    {
        public string Name { get; } = "Кожаная броня";
        public int Protection { get; } = 10;

        // Метод получения информации
        public void ShowInfo()
        {
            Console.WriteLine($"- {Name}: защищает от легких ударов.");
        }

        // Метод вызываемый при получении урона
        public void Defend()
        {
            Console.WriteLine($"{Name}: защищает от легких ударов. Защита: {Protection}");
        }
    }
}
