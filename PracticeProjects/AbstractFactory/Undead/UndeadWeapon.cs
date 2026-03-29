using AbstractFactory.AbstractFactory;
using System;

namespace AbstractFactory.Undead
{
    public class UndeadWeapon : IWeapon
    {
        public string Name { get; } = "Коса";
        public int Damage { get; } = 12;

        // Метод получения информации
        public void ShowInfo()
        {
            Console.WriteLine($"- {Name}: наносит проклятый удар!");
        }

        // Метод вызываемый при атаке
        public void Attack()
        {
            Console.WriteLine($"{Name}: наносит проклятый удар! Урон: {Damage}");
        }
    }
}
