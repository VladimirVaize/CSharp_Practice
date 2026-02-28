using System;

namespace ClassesAndObjects
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Backpack backpack = new Backpack(3);
            Weapon longSword = new Weapon("Длинный меч", 12);
            Weapon dagger = new Weapon("Кинжал", 4);

            backpack.AddWeapon(longSword);
            backpack.AddWeapon(dagger);

            MainGame.Game(backpack);
        }
    }

    static class MainGame
    {
        private static readonly Random _random = new Random();

        public static void Game(Backpack backpack)
        {
            bool isPlaying = true;
            while (isPlaying)
            {
                Console.WriteLine("\nНажмите ПРОБЕЛ чтобы найти сундук, или ESC для выхода...");
                var key = Console.ReadKey(true).Key;

                Console.Clear();
                if (key == ConsoleKey.Escape)
                {
                    isPlaying = false;
                    Console.WriteLine("Игра завершена. Спасибо за игру!");
                    break;
                }

                if (key != ConsoleKey.Spacebar)
                    continue;

                Weapon foundWeapon = FindRandomWeapon();

                bool isFoundWeapon = true;
                while (isFoundWeapon)
                {
                    Console.WriteLine("\n1. Положить в рюкзак");
                    Console.WriteLine("2. Улучшить");
                    Console.WriteLine("3. Выбросить");
                    Console.Write("Ваш выбор: ");

                    string input = Console.ReadLine();
                    if (int.TryParse(input, out int choice))
                    {
                        switch (choice)
                        {
                            case 1: backpack.AddWeapon(foundWeapon); isFoundWeapon = false; break;
                            case 2: foundWeapon.Upgrade(); break;
                            case 3: isFoundWeapon = false; break;
                            default: Console.WriteLine("Неверный ввод! Введите 1, 2 или 3."); break;
                        }
                    }
                    else
                        Console.WriteLine("Ошибка! Введите число.");
                }
            }
        }

        static Weapon FindRandomWeapon()
        {
            Weapon newWeapon = new Weapon(GetRandomWeaponName());
            Console.WriteLine($"Найден сундук с {newWeapon.Name}. Уровень: {newWeapon.Level}, Урон: {newWeapon.Damage}");
            return newWeapon;
        }

        static string GetRandomWeaponName()
        {
            string[] _weaponsName = { "Кинжал", "Топор", "Арбалет", "Огненный шар" };

            return _weaponsName[_random.Next(_weaponsName.Length)];
        }
    }

    class Weapon
    {
        private static readonly Random _random = new Random();

        private const int _minStartDamage = 0;
        private const int _maxStartDamage = 30;
        private const int _minIncreaseDamage = 3;
        private const int _maxIncreaseDamage = 8;

        public string Name { get; private set; }
        public int Damage { get; private set; }
        public int Level { get; private set; }

        public Weapon(string name, int damage = 3)
        {
            Name = name;
            int baseDamage = damage < 1 ? 3 : damage;
            Damage = baseDamage + _random.Next(_minStartDamage, _maxStartDamage + 1);
            Level = 1;
        }

        public void Upgrade()
        {
            Level++;
            Damage += _random.Next(_minIncreaseDamage, _maxIncreaseDamage + 1);
            Console.WriteLine($"Ваш {Name} улучшен! Текущий уровень: {Level}, Урон: {Damage}");
        }
    }

    class Backpack
    {
        public Weapon[] Weapons { get; private set; }
        public int Capacity { get; private set; }

        public Backpack(int capacity)
        {
            Capacity = capacity;
            Weapons = new Weapon[Capacity];
        }

        public void AddWeapon(Weapon newWeapon)
        {
            for (int i = 0; i < Weapons.Length; i++)
            {
                if (Weapons[i] == null)
                {
                    Weapons[i] = newWeapon;
                    Console.WriteLine($"Вы положили {newWeapon.Name} в рюкзак");
                    return;
                }
            }
            Console.WriteLine("В рюкзаке нет свободного места!");
            Console.WriteLine("Что делать?");
            Console.WriteLine("1. Не брать.");
            Console.WriteLine("2. Заменить на предмет из рюкзака.");
            Console.Write("Ваш выбор: ");
            if (int.TryParse(Console.ReadLine(), out int choice))
            {
                switch (choice)
                {
                    case 1: Console.WriteLine($"Вы не стали брать {newWeapon.Name}."); break;
                    case 2: ReplaceWeapon(newWeapon); break;
                    default: Console.WriteLine("Неверный ввод!"); break;
                }
            }
            else
                Console.WriteLine("Ошибка! Введите число.");
        }

        public void ReplaceWeapon(Weapon newWeapon)
        {
            ShowAllWeapons();

            Console.Write("Какое оружие заменить? (Введите номер): ");

            if (int.TryParse(Console.ReadLine(), out int choice))
            {
                if (choice >= 1 && choice <= Weapons.Length)
                {
                    Weapons[choice - 1] = newWeapon;
                    Console.WriteLine($"Вы положили {newWeapon.Name} в рюкзак");
                }
                else
                    Console.WriteLine("Такого слота не существует!");
            }
            else
                Console.WriteLine("Не корректный ввод!");
        }

        public void ShowAllWeapons()
        {
            for (int i = 0; i < Weapons.Length; i++)
            {
                Console.Write($"{i + 1}: ");
                if (Weapons[i] != null)
                    Console.WriteLine($"{Weapons[i].Name}. Уровень: {Weapons[i].Level}, Урон: {Weapons[i].Damage}");
                else
                    Console.WriteLine("[Пусто]");
            }
        }
    }
}
