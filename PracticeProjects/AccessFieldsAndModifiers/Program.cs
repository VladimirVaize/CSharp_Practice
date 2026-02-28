using System;
using System.Collections.Generic;

namespace AccessFieldsAndModifiers
{
    internal class Program
    {
        static void Main(string[] args)
        {
            GameManager.StartGame();
        }
    }

    static class GameManager
    {
        private static readonly Random _random = new Random();
        private const int MinDamage = 1;
        private const int MaxDamage = 50;

        private static List<Armor> _armors = new List<Armor>
        {
            new Armor("Шлем", 4, 10, 2),
            new Armor("Нагрудник", 15, 60, 13),
            new Armor("Поножи", 10, 50, 7)
        };
        public static IReadOnlyList<Armor> Armors => _armors.AsReadOnly();

        public static void StartGame()
        {
            ShowArmorsStat();

            foreach (Armor armor in _armors)
            {
                armor.TakeDamage(_random.Next(MinDamage, MaxDamage + 1));
            }

            ShowArmorsStat();

            _armors[1].Repair();

            ShowArmorsStat();
        }

        public static void ShowArmorsStat()
        {
            foreach (Armor armor in _armors)
            {
                armor.ShowStat();
            }
            Console.WriteLine();
        }
    }

    public class Armor
    {
        private string _name;
        private int _durability;
        private int _maxDurability;
        private int _defense;

        private const int MinDurability = 0;

        public Armor(string name, int durability, int maxDurability, int defense)
        {
            _name = name;
            _maxDurability = maxDurability <= 0 ? 1 : maxDurability;
            _durability = durability <= 0 ? 1 : Math.Min(durability, _maxDurability);
            _defense = defense <= 0 ? 1 : defense;
        }

        public string Name
        {
            get { return _name; }
        }
        public int Defense
        {
            get { return _defense; }
        }
        public int Durability
        {
            get { return _durability; }
        }
        public int MaxDurability
        {
            get { return _maxDurability; }
        }

        public void TakeDamage(int damageAmount)
        {
            if (damageAmount < 0)
            {
                Console.WriteLine("Ошибка: урон не может быть отрицательным!");
                return;
            }
            if (damageAmount == 0)
            {
                Console.WriteLine($"{Name} не получил урона.");
                return;
            }

            Console.WriteLine($"{Name} получает {damageAmount} урона.");
            if (damageAmount > 0)
                _durability = Durability - damageAmount > MinDurability ? Durability - damageAmount : MinDurability;

            Console.WriteLine($"Теперь прочность {Name}: {Durability}/{MaxDurability}");
        }

        public void Repair()
        {
            if (Durability >= MaxDurability)
            {
                Console.WriteLine($"{Name} не нуждается в починке.");
                return;
            }

            bool wasBroken = Durability <= 0;
            Console.WriteLine($"Ремонтируем {Name}...");

            if (wasBroken)
            {
                _maxDurability = (int)Math.Ceiling(MaxDurability * 0.9f);
                _maxDurability = Math.Max(MaxDurability, 1);
                Console.WriteLine($"Предмет был сломан! Максимальная прочность уменьшена до {MaxDurability}");
            }

            _durability = MaxDurability;
            Console.WriteLine($"{Name} полностью восстановлен!");
        }

        public void ShowStat()
        {
            Console.WriteLine($"{Name} - Прочность: {Durability}/{MaxDurability}, защита: {Defense}");
        }
    }
}
