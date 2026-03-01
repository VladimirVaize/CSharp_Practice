using System;
using System.Collections.Generic;

namespace Interfaces
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Player player = new Player("R2D2", 100);
            List<IInteractable> interactObjects = new List<IInteractable>
            {
                new FuelTank(),
                new SupplyCrate(),
                new EnemyDrone()
            };

            GameLoop(player, interactObjects);
        }

        static void GameLoop(Player player, List<IInteractable> interactObjects)
        {
            foreach (var interactable in interactObjects)
            {
                interactable.Interact(player);
                Console.WriteLine();
            }
            player.ShowStats();
        }
    }

    public interface IInteractable
    {
        void Interact(Player player);
    }

    public class Player
    {
        private string _name;
        private int _health;
        private int _xp;
        private int _damage = 12;

        public Player(string name, int health)
        {
            _name = name;
            _health = Math.Max(health, 1);
            _xp = 0;
        }

        public string Name { get { return _name; } }
        public int Health { get { return _health; } }
        public int XP { get { return _xp; } }
        public int Damage { get { return _damage; } }

        public void TakeDamage(int damage)
        {
            _health = Math.Min(_health - damage, _health);
            _health = Math.Max(_health, 0);

            Console.WriteLine($"Игрок {_name} получил урон: {damage}. Здоровье: {_health}.");
        }

        public void AddXP(int amount)
        {
            if (amount <= 0) return;
            _xp += amount;

            Console.WriteLine($"Игрок {_name} получил {amount} опыта!");
        }

        public void ShowStats()
        {
            Console.WriteLine($"Игрок: {_name}. Здоровье: {_health}. Опыт: {_xp}.");
        }
    }

    public class FuelTank : IInteractable
    {
        private int _damage = 20;

        public void Interact(Player player)
        {
            Console.WriteLine("Бздыщ! Цистерна взорвалась!");
            player.TakeDamage(_damage);
        }
    }

    public class SupplyCrate : IInteractable
    {
        private int _xpReward = 50;

        public void Interact(Player player)
        {
            Console.WriteLine($"*Вы нашли припасы! +{_xpReward} XP*");
            player.AddXP(_xpReward);
        }
    }

    public class EnemyDrone : IInteractable
    {
        private int _health = 30;
        private int _damage = 10;

        public void Interact(Player player)
        {
            Console.WriteLine($"Игрок {player.Name} атакует дрон!");
            TakeDamage(player.Damage);

            if (_health <= 0)
                Console.WriteLine("Дрон уничтожен");
            else
            {
                Console.WriteLine("Дрон контратакует!");
                player.TakeDamage(_damage);
            }
        }

        public void TakeDamage(int damage)
        {
            _health = Math.Min(_health - damage, _health);
            _health = Math.Max(_health, 0);

            Console.WriteLine($"Дрон получил урон: {damage}. Здоровье: {_health}.");
        }
    }
}
