using AbstractFactory.AbstractFactory;
using System;

namespace AbstractFactory.Human
{
    public class HumanWeapon : IWeapon
    {
        public string Name { get; } = "Меч";
        public int Damage { get; } = 15;

        // Метод получения информации
        public void ShowInfo()
        {
            Console.WriteLine($"- {Name}: наносит рубящий удар!");
        }

        // Метод вызываемый при атаке
        public void Attack()
        {
            Console.WriteLine($"{Name}: наносит рубящий удар! Урон: {Damage}");
        }
    }
}
