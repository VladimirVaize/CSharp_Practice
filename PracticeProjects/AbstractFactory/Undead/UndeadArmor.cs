using AbstractFactory.AbstractFactory;
using System;

namespace AbstractFactory.Undead
{
    public class UndeadArmor : IArmor
    {
        public string Name { get; } = "Гнилая броня";
        public int Protection { get; } = 4;

        // Метод получения информации
        public void ShowInfo()
        {
            Console.WriteLine($"- {Name}: отпугивает врагов своим видом.");
        }

        // Метод вызываемый при получении урона
        public void Defend()
        {
            Console.WriteLine($"{Name}: отпугивает врагов своим видом. Защита: {Protection}");
        }
    }
}
