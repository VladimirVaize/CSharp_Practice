using AbstractFactory.AbstractFactory;
using System;

namespace AbstractFactory.Elf
{
    public class ElfWeapon : IWeapon
    {
        public string Name { get; } = "Лук";
        public int Damage { get; } = 7;

        // Метод получения информации
        public void ShowInfo()
        {
            Console.WriteLine($"- {Name}: выпускает стрелу с расстояния!");
        }

        // Метод вызываемый при атаке
        public void Attack()
        {
            Console.WriteLine($"{Name}: выпускает стрелу с расстояния! Урон: {Damage}");
        }
    }
}
