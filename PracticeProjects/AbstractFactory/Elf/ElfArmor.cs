using AbstractFactory.AbstractFactory;
using System;

namespace AbstractFactory.Elf
{
    public class ElfArmor : IArmor
    {
        public string Name { get; } = "Легкая броня";
        public int Protection { get; } = 5;

        // Метод получения информации
        public void ShowInfo()
        {
            Console.WriteLine($"- {Name}: позволяет уклоняться от атак.");
        }

        // Метод вызываемый при получении урона
        public void Defend()
        {
            Console.WriteLine($"{Name}: позволяет уклоняться от атак. Защита: {Protection}");
        }
    }
}
