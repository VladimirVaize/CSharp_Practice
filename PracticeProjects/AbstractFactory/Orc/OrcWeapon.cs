using AbstractFactory.AbstractFactory;
using System;

namespace AbstractFactory.Orc
{
    public class OrcWeapon : IWeapon
    {
        public string Name { get; } = "Топор";
        public int Damage { get; } = 25;

        // Метод получения информации
        public void ShowInfo()
        {
            Console.WriteLine($"- {Name}: наносит мощный рубящий удар!");
        }

        // Метод вызываемый при атаке
        public void Attack()
        {
            Console.WriteLine($"{Name}: наносит мощный рубящий удар! Урон: {Damage}");
        }
    }
}
