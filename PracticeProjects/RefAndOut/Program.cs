using System;

namespace RefAndOut
{
    internal class Program
    {
        static Random rand = new Random();
        static void Main(string[] args)
        {
            CharacterStats playerStats = new CharacterStats(250);

            Console.WriteLine("=== Исходные характеристики ===");
            playerStats.PrintStats();

            Console.WriteLine("\n=== Применяем зелье силы ===");
            ApplyPotion(ref playerStats, "strength", 23);
            playerStats.PrintStats();

            Console.WriteLine("\n=== Пробуем проклятие интеллекта ===");
            if(TryApplyCurse(playerStats, "intelligence", 13, out CharacterStats cursedStats))
            {
                Console.WriteLine("Проклятие подействовало!");
                playerStats = cursedStats;
            }
            else
            {
                Console.WriteLine("Проклятие не подействовало.");
            }
            playerStats.PrintStats();

            Console.WriteLine("\n=== Меняем местами здоровье и интеллект ===");
            SwapStats(ref playerStats, "health", "intelligence");
            playerStats.PrintStats();
        }

        static void ApplyPotion(ref CharacterStats stats, string statName, int power)
        {
            ref int stat = ref FindStat(ref stats, statName);
            stat += power;
            Console.WriteLine($"Параметр {statName} изменен на {power}. Теперь: {stat}");
        }

        static bool TryApplyCurse(CharacterStats baseStats, string statName, int power, out CharacterStats cursedStats)
        {
            cursedStats = baseStats;

            if (rand.Next(0,2) == 0 && baseStats.Health < 20)
            {
                return false;
            }
            else
            {
                ref int stat = ref FindStat(ref cursedStats, statName);
                stat -= power;

                Console.WriteLine($"Параметр {statName} изменен на {power}. Теперь: {stat}");

                return true;
            }
        }

        static void SwapStats(ref CharacterStats stats, string firstStatName, string secondStatName)
        {
            ref int firstStat = ref FindStat(ref stats, firstStatName);
            ref int secondStat = ref FindStat(ref stats, secondStatName);

            (firstStat, secondStat) = (secondStat,  firstStat);

            Console.WriteLine($"Параметры {firstStatName} и {secondStatName} поменялись местами. Теперь {firstStatName}: {firstStat}, {secondStatName}: {secondStat}");
        }

        static ref int FindStat(ref CharacterStats stats, string statName)
        {
            switch (statName.ToLower())
            {
                case "health":
                    return ref stats.Health;
                case "mana":
                    return ref stats.Mana;
                case "strength":
                    return ref stats.Strength;
                case "agility":
                    return ref stats.Agility;
                case "intelligence":
                    return ref stats.Intelligence;
                default:
                    throw new ArgumentException($"Неизвестная характеристика: {statName}");
            }
        }
    }

    struct CharacterStats
    {
        public int Health;
        public int Mana;
        public int Strength;
        public int Agility;
        public int Intelligence;

        public CharacterStats(int health = 100, int mana = 50, int strength = 10, int agility = 10, int intelligence = 10)
        {
            Health = health;
            Mana = mana;
            Strength = strength;
            Agility = agility;
            Intelligence = intelligence;
        }

        public void PrintStats()
        {
            Console.WriteLine($"Здоровье - {Health}");
            Console.WriteLine($"Мана - {Mana}");
            Console.WriteLine($"Сила - {Strength}");
            Console.WriteLine($"Ловкость - {Agility}");
            Console.WriteLine($"Интеллект - {Intelligence}");
        }
    }
}

