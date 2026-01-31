using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace CSharpPraktice
{
    internal class Program
    {
        private static Random rand = new Random();
        static void Main(string[] args)
        {
            List<Enemy> enemies = new List<Enemy>
            {
                new Goblin(),
                new SkeletWarrior(),
                new KingOfWolf()
            };

            int totalGold = 0;


            Console.WriteLine("Выберите врага: ");
            Console.WriteLine("1 - Гоблин");
            Console.WriteLine("2 - Скелет-воин");
            Console.WriteLine("3 - Король волков");
            Console.WriteLine("4 - Все");
            Console.SetCursorPosition(16, 0);

            if (int.TryParse(Console.ReadLine(), out int choice))
            {
                Console.Clear();

                if (choice == 4)
                {
                    foreach (var enemy in enemies)
                    {
                        LootResult result = SimulateLootDrop(enemy);
                        result.PrintResult();
                        totalGold += result.Gold;
                        Console.WriteLine();
                    }
                }
                else if (choice >= 1 && choice <= enemies.Count)
                {
                    LootResult result = SimulateLootDrop(enemies[choice - 1]);
                    result.PrintResult();
                    totalGold = result.Gold;
                }
                else
                {
                    Console.WriteLine("Вы ввели не верные данные");
                }
                Console.WriteLine($"\nВсего за сессию добыто золота: {totalGold}");
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Введите число от 1 до 4");
            }

        }

        static LootResult SimulateLootDrop(Enemy enemy)
        {
            int inventorySize = rand.Next(1, 4);
            List<string> lootItems = new List<string>();
            int gold = 0;



            for (int i = 0; i < inventorySize; i++)
            {
                string loot = GetRandomLootItem(enemy.LootPool, enemy.DropChance, enemy.TotalWeight);
                lootItems.Add(loot);

                if (loot.Contains("Золотая монета"))
                {
                    Match findNumber = Regex.Match(loot, @"\d+");
                    if (findNumber.Success)
                        gold += int.Parse(findNumber.Value);
                }
            }


            return new LootResult(enemy.Name, inventorySize, lootItems, gold);
        }

        static string GetRandomLootItem(string[] lootPool, int[] dropChance, int totalWeight)
        {
            if (lootPool.Length != dropChance.Length)
                throw new ArgumentException("Массивы lootPool и dropChance должны иметь одинаковую длину");

            if (lootPool.Length == 0)
                throw new ArgumentException("Массив lootPool не может быть пустым");

            int randomNumber = rand.Next(0, totalWeight);

            int currentRange = 0;

            for (int i = 0; i < lootPool.Length; i++)
            {
                currentRange += dropChance[i];
                if (randomNumber < currentRange)
                    return lootPool[i];
            }
            throw new InvalidOperationException("Не удалось выбрать предмет. Проверьте массивы lootPool и dropChance.");
        }
    }

    public class LootResult
    {
        public string EnemyName { get; }
        public int InventorySlots { get; }
        public List<string> Items { get; }
        public int Gold { get; }

        public LootResult(string enemyName, int inventorySlots, List<string> items, int gold)
        {
            EnemyName = enemyName;
            InventorySlots = inventorySlots;
            Items = items;
            Gold = gold;
        }

        public void PrintResult()
        {
            Console.WriteLine($"=== Победа над монстром: {EnemyName} ===");
            Console.WriteLine($"Слотов лута: {InventorySlots}");
            Console.WriteLine("Выпало:");

            foreach (var item in Items)
            {
                Console.WriteLine($"- {item}");
            }
            Console.WriteLine($"Золота в этой добыче: {Gold}");
        }
    }

    public abstract class Enemy
    {
        public string Name { get; }
        public string[] LootPool { get; }
        public int[] DropChance { get; }
        public int TotalWeight { get; }

        public Enemy(string name, string[] lootPool, int[] dropChance)
        {
            if (lootPool.Length != dropChance.Length)
                throw new ArgumentException("Массивы lootPool и dropChance должны иметь одинаковую длину");

            if (lootPool.Length == 0)
                throw new ArgumentException("Массив lootPool не может быть пустым");

            Name = name;
            LootPool = lootPool;
            DropChance = dropChance;
            TotalWeight = dropChance.Sum();
        }
    }

    public class Goblin : Enemy
    {
        public Goblin()
            : base(
                  "Гоблин",
                  new[] { "Золотая монета (1)", "Ржавый кинжал", "Кусок хлеба", "Слабая лечебная трава", "Ничего" },
                  new[] { 50, 15, 30, 10, 5 }
            )
        {

        }
    }

    public class SkeletWarrior : Enemy
    {
        public SkeletWarrior()
            : base(
                  "Скелет-воин",
                  new[] { "Золотая монета (5)", "Груда костей", "Сломаный щит", "Ром", "Лук из костей", "Ничего" },
                  new[] { 35, 10, 30, 5, 1, 5 }
            )
        {

        }
    }

    public class KingOfWolf : Enemy
    {
        public KingOfWolf()
            : base(
                  "Король волков",
                  new[] { "Золотая монета (36)", "Зуб короля волков", "Шкура короля волков", "Ничего" },
                  new[] { 13, 5, 2, 80 }
            )
        {

        }
    }
}