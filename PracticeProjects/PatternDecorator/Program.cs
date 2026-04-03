using PatternDecorator.Character;
using PatternDecorator.Character.Core;
using System;

namespace PatternDecorator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ICharacterStats character = new BaseCharacter(10, 1, 5);

            Console.WriteLine("=== Базовый персонаж ===");
            Console.WriteLine("Урон: " + character.GetDamage());
            Console.WriteLine("Скорость атаки: " + character.GetAttackSpeed());
            Console.WriteLine("Сопротивление магии: " + character.GetMagicResist());

            Console.WriteLine("\n=== После баффа Берсерка ===");
            ICharacterStats berserkCharacter = new BerserkBuff(character);

            Console.WriteLine("Урон: " + berserkCharacter.GetDamage());
            Console.WriteLine("Скорость атаки: " + berserkCharacter.GetAttackSpeed());
            Console.WriteLine("Сопротивление магии: " + berserkCharacter.GetMagicResist());

            Console.WriteLine("\n=== После баффа Скорости ===");
            ICharacterStats hasteCharacter = new HasteBuff(berserkCharacter);

            Console.WriteLine("Урон: " + hasteCharacter.GetDamage());
            Console.WriteLine("Скорость атаки: " + hasteCharacter.GetAttackSpeed());
            Console.WriteLine("Сопротивление магии: " + hasteCharacter.GetMagicResist());

            Console.WriteLine("\n=== После баффа Магического Щита ===");
            ICharacterStats magicShieldCharacter = new MagicShieldBuff(hasteCharacter);

            Console.WriteLine("Урон: " + magicShieldCharacter.GetDamage());
            Console.WriteLine("Скорость атаки: " + magicShieldCharacter.GetAttackSpeed());
            Console.WriteLine("Сопротивление магии: " + magicShieldCharacter.GetMagicResist());

            Console.WriteLine("\n=== После баффа Двойного Урона ===");
            ICharacterStats doubleDamageCharacter = new DoubleDamageBuff(magicShieldCharacter);

            Console.WriteLine("Урон: " + doubleDamageCharacter.GetDamage());
            Console.WriteLine("Скорость атаки: " + doubleDamageCharacter.GetAttackSpeed());
            Console.WriteLine("Сопротивление магии: " + doubleDamageCharacter.GetMagicResist());
        }
    }
}
