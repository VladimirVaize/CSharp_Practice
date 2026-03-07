using System;
using System.Threading;

namespace Enum
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Enemy enemy = new Enemy("Гоблин", 25);

            bool isPlaying = true;
            int tickCount = 1;

            Console.WriteLine($"Создан враг: {enemy.Name} (Здоровье: {enemy.Health}). Начальное состояние: {enemy.CurrentState}.");

            while (isPlaying)
            {
                Console.WriteLine($"\n--- ТИК {tickCount} ---");
                enemy.UpdateState();
                Console.Write("Введите новую дистанцию до игрока (или '0' для выхода): ");
                if (int.TryParse(Console.ReadLine(), out int newDistance))
                {
                    if (newDistance == 0 || enemy.Health <= 0)
                    {
                        isPlaying = false;
                        return;
                    }
                    enemy.TakeDamage(5);
                    enemy.SetPlayerDistance(newDistance);
                }
                tickCount++;
                Thread.Sleep(1000);
            }
        }
    }

    public enum EnemyState
    {
        Patrol,
        Chase,
        Attack
    }

    public class Enemy
    {
        public string Name { get; }

        private int _health;
        private int _playerDistance;
        private EnemyState _currentState;

        public int Health => _health;
        public int PlayerDistance => _playerDistance;
        public EnemyState CurrentState => _currentState;

        public Enemy(string name, int health, int playerDistance = 15)
        {
            Name = name;
            _health = Math.Max(health, 1);
            _playerDistance = Math.Max(playerDistance, 1);
            _currentState = EnemyState.Patrol;
        }

        public void SetPlayerDistance(int distance)
        {
            _playerDistance = Math.Max(distance, 1);
        }

        public void TakeDamage(int damage)
        {
            _health = Math.Min(_health - damage, _health);
            _health = Math.Max(_health, 0);
        }

        public void UpdateState()
        {
            switch (_currentState)
            {
                case EnemyState.Patrol:
                    if (_playerDistance <= 5)
                    {
                        _currentState = EnemyState.Chase;
                        Console.WriteLine($"{Name} заметил игрока! Переходит в режим преследования!");
                        UpdateState();
                        return;
                    }
                    Console.WriteLine($"{Name} патрулирует территорию... (Дистанция до игрока: {_playerDistance} м)");
                    break;
                case EnemyState.Chase:
                    if (_playerDistance <= 1)
                    {
                        _currentState = EnemyState.Attack;
                        UpdateState();
                        return;
                    }
                    else if (_playerDistance > 5)
                    {
                        _currentState = EnemyState.Patrol;
                        UpdateState();
                        return;
                    }
                    Console.WriteLine($"{Name} преследует игрока! (Дистанция: {_playerDistance} м)");
                    break;
                case EnemyState.Attack:
                    if (_playerDistance > 1)
                    {
                        _currentState = EnemyState.Chase;
                        UpdateState();
                        return;
                    }
                    Console.WriteLine($"{Name} АТАКУЕТ игрока! (Дистанция: {_playerDistance} м)");
                    break;
            }
        }
    }
}
