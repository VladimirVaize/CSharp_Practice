using AbstractFactory.AbstractFactory;
using System;

namespace AbstractFactory.Elf
{
    public class ElfConsumable : IConsumable
    {
        public string Name { get; } = "Эликсир маны";
        public int Power { get; } = 30;

        // Метод получения информации
        public void ShowInfo()
        {
            Console.WriteLine($"- {Name}: восстанавливает {Power} MP.");
        }

        // Метод вызываемый при использовании предмета
        public void Use()
        {
            Console.WriteLine($"Восстанавливает {Power} MP.");
        }
    }
}
