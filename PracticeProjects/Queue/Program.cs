using System;
using System.Collections.Generic;
using System.Threading;

namespace Queue
{
    internal class Program
    {
        static Random rand = new Random();
        static void Main(string[] args)
        {
            GameLoop();
        }

        static void GameLoop()
        {
            SpawnManager spawnManager = new SpawnManager();

            const int MinHealth = 30;
            const int MaxHealth = 80;
            const int MinDamage = 5;
            const int MaxDamage = 25;

            string[] enemiesName = { "Гоблин", "Орк", "Скелет", "Демон", "Зомби" };

            for(int i = rand.Next(4, 9); i > 0;  i--)
            {
                spawnManager.AddEnemyToQueue(new Enemy(
                                            enemiesName[rand.Next(0, enemiesName.Length)], 
                                            rand.Next(MinHealth, MaxHealth + 1), 
                                            rand.Next(MinDamage, MaxDamage + 1)
                                            ));
            }

            Console.WriteLine($"Засада! Враг поджидает в очереди: {spawnManager.GetEnemyQueueCount()}\n");

            while (spawnManager.GetEnemyQueueCount() > 0)
            {
                spawnManager.SpawnNextEnemy();
                spawnManager.ShowQueueStatus();
                Thread.Sleep(1500);
            }
        }
    }

    public class SpawnManager
    {
        private Queue<Enemy> _enemyQueue = new Queue<Enemy>();

        public int GetEnemyQueueCount()
        {
            return _enemyQueue.Count;
        }

        public void AddEnemyToQueue(Enemy enemy)
        {
            _enemyQueue.Enqueue(enemy);
        }

        public void SpawnNextEnemy()
        {
            Enemy enemy = _enemyQueue.Dequeue();
            enemy.SayPhrase();
            Console.WriteLine($"[{enemy.Name}] появился! Нанесено урона: {enemy.Damage}");
        }

        public void ShowQueueStatus()
        {
            if(GetEnemyQueueCount() > 0)
            {
                Console.WriteLine($"Осталось врагов:  {GetEnemyQueueCount()}\n");
            }
        }
    }

    public class Enemy
    {
        public string Name { get; }
        public int Health { get; }
        public int Damage { get; }

        public Enemy(string name, int health, int damage)
        {
            Name = name;
            Health = health;
            Damage = damage;
        }

        public void SayPhrase()
        {
            Console.WriteLine($"Я {Name} выхожу из тени!");
        }
    }
}
