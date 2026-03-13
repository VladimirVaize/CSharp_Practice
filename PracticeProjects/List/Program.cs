using System;
using System.Collections.Generic;

namespace List
{
    internal class Program
    {
        static List<Enemy> enemies = new List<Enemy>();
        static void Main(string[] args)
        {
            bool isPlaying = true;

            while (isPlaying)
            {
                switch (MainMenu())
                {
                    case 1: EnemyManager.CreateNewEnemy(enemies); break;
                    case 2: EnemyManager.AttackEnemies(enemies); break;
                    case 3: EnemyManager.ShowEnemiesList(enemies); break;
                    case 4: EnemyManager.RemoveDeadEnemies(enemies); break;
                    case 5: isPlaying = false; break;
                    default: ConsoleHelper.ShowMessage("Не корректный ввод!", ConsoleColor.Red); break;
                }
            }
        }

        static int MainMenu()
        {
            Console.Clear();
            Console.WriteLine("\n=== Менеджер Врагов ===");
            Console.WriteLine("1. Добавить врага");
            Console.WriteLine("2. Атаковать всех врагов");
            Console.WriteLine("3. Показать список врагов");
            Console.WriteLine("4. Удалить мёртвых врагов");
            Console.WriteLine("5. Выход");
            Console.Write("\nВаш выбор: ");
            if (int.TryParse(Console.ReadLine(), out int choice))
                return choice;
            else
            {
                ConsoleHelper.ShowMessage("Не корректный ввод!", ConsoleColor.Red);
                return 5;
            }
        }
    }

    public static class ConsoleHelper
    {
        public static void ShowMessage(string message, ConsoleColor color = ConsoleColor.White)
        {
            Console.ForegroundColor = color;
            Console.WriteLine("\n\n" + message);
            Console.ResetColor();
            Console.ReadKey();
        }
    }

    public class EnemyManager
    {
        private const int PlayerDamage = 20;
        private const int MinHealth = 30;
        private const int MaxHealth = 80;
        private const int MinDamage = 5;
        private const int MaxDamage = 20;

        static Random rand = new Random();
        public static void CreateNewEnemy(List<Enemy> enemies)
        {
            string enemyName;
            int enemyHealth = 50;
            int enemyDamage = 10;

            bool randGen = false;

            Console.WriteLine("\n--- Добавление врага ---");

            Console.Write("Введите имя (или 'random'): ");
            enemyName = Console.ReadLine();
            if (enemyName == "random")
            {
                GenerateRandomEnemyStats(out enemyName, out enemyHealth, out enemyDamage);
                randGen = true;

            }

            if (!randGen)
            {
                Console.Write("Введите здоровье: ");
                if (int.TryParse(Console.ReadLine(), out int health))
                    if (health > 0)
                        enemyHealth = health;
                    else
                        enemyHealth = health * -1;
                else
                {
                    GenerateRandomEnemyStats(out enemyName, out enemyHealth, out enemyDamage);
                    randGen = true;
                    ConsoleHelper.ShowMessage("Не корректный ввод!", ConsoleColor.Red);
                }
            }

            if (!randGen)
            {
                Console.Write("Введите урон: ");
                if (int.TryParse(Console.ReadLine(), out int damage))
                    if (damage > 0)
                        enemyDamage = damage;
                    else
                        enemyDamage = damage * -1;
                else
                {
                    GenerateRandomEnemyStats(out enemyName, out enemyHealth, out enemyDamage);
                    randGen = true;
                    ConsoleHelper.ShowMessage("Не корректный ввод!", ConsoleColor.Red);
                }
            }

            enemies.Add(new Enemy(enemyName, enemyHealth, enemyDamage));

            ConsoleHelper.ShowMessage($"Сгенерирован враг: {enemyName} ({enemyHealth} HP, {enemyDamage} урона)\n\n");
        }

        static void GenerateRandomEnemyStats(out string name, out int health, out int damage)
        {
            string[] names = { "Гоблин", "Орк", "Скелет", "Демон", "Зомби", "Дракон", "Зомби-рыцарь", "Гопник" };

            name = names[rand.Next(0, names.Length)];
            health = rand.Next(MinHealth, MaxHealth + 1);
            damage = rand.Next(MinDamage, MaxDamage + 1);
        }

        public static void AttackEnemies(List<Enemy> enemies)
        {
            if (enemies.Count == 0)
            {
                ConsoleHelper.ShowMessage("Список врагов пуст!", ConsoleColor.Yellow);
                return;
            }

            Console.WriteLine("--- АТАКА! ---");

            foreach (Enemy enemy in enemies)
            {
                if (enemy.Health > 0)
                {
                    enemy.Health -= PlayerDamage;

                    Console.Write($"Игрок бьет {enemy.Name} на {PlayerDamage} урона! У {enemy.Name} осталось {enemy.Health} HP.");
                }
                else
                    Console.Write(enemy.Name);
                if (enemy.Health <= 0) Console.WriteLine(" (Враг мертв)");
                else Console.WriteLine();
            }

            Console.ReadLine();
        }

        public static void ShowEnemiesList(List<Enemy> enemies)
        {
            Console.WriteLine("\n--- Список врагов ---");
            if (enemies.Count == 0)
            {
                ConsoleHelper.ShowMessage("Врагов нет", ConsoleColor.Yellow);
            }
            else
            {
                for (int i = 0; i < enemies.Count; i++)
                {
                    Console.Write($"Враг {i + 1}: {enemies[i].Name} ({enemies[i].Health} HP, {enemies[i].Damage} урона)");
                    if (enemies[i].Health <= 0)
                        Console.WriteLine(" (Враг мертв)");
                    else Console.WriteLine();
                }
            }
            Console.ReadLine();
        }

        public static void RemoveDeadEnemies(List<Enemy> enemies)
        {
            int deadEnemiesCount = 0;
            for (int i = enemies.Count - 1; i >= 0; i--)
            {
                if (enemies[i].Health <= 0)
                {
                    enemies.RemoveAt(i);
                    deadEnemiesCount++;
                }
            }
            if (deadEnemiesCount > 0)
                ConsoleHelper.ShowMessage($"Мёртвые враги (в кол-ве: {deadEnemiesCount}) удалены с поля боя.", ConsoleColor.Green);
            else
                ConsoleHelper.ShowMessage("На поле боя нет мёртвых врагов.", ConsoleColor.Green);
        }
    }

    public class Enemy
    {
        public string Name { get; }
        public int Health { get; set; }
        public int Damage { get; }

        public Enemy(string name, int health, int damage)
        {
            Name = name;
            Health = health;
            Damage = damage;
        }
    }
}
