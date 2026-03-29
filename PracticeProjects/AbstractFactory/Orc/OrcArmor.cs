using AbstractFactory.AbstractFactory;
using System;

namespace AbstractFactory.Orc
{
    public class OrcArmor : IArmor
    {
        public string Name { get; } = "Тяжелая броня";
        public int Protection { get; } = 45;

        // Метод получения информации
        public void ShowInfo()
        {
            Console.WriteLine($"- {Name}: поглощает большую часть урона.");
        }

        // Метод вызываемый при получении урона
        public void Defend()
        {
            Console.WriteLine($"{Name}: поглощает большую часть урона. Защита: {Protection}");
        }
    }
}
