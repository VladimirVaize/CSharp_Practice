using System;
using System.Collections.Generic;

namespace Structures
{
    internal class Program
    {
        static Random rand = new Random();
        static void Main(string[] args)
        {
            Spaceship spaceship = new Spaceship("Шустрый", 100, 25);
            Player player = new Player(spaceship);

            player.AddCargo(new CargoItem("Детали", 4, 16));
            player.AddCargo(new CargoItem("Метал", 2, 40));
            player.AddCargo(new CargoItem("Молоко", 16, 3));

            player.ShowInfo();

            for (int i = 0; i <= 3; i++)
            {
                DamageReport damageReport = CombatCalculator.CalculateDamage(player.Spaceship.Speed, rand);
                damageReport.PrintReport();
                if (damageReport.DamageDealt > 0)
                {
                    player.TakeDamage(damageReport.DamageDealt);
                }
                if (player.Spaceship.Health <= 0)
                {
                    Console.WriteLine("Корабль уничтожен!");
                    break;
                }
            }

            player.ShowInfo();
        }
    }

    public static class CombatCalculator
    {
        private const double CriticalHitChance = 0.3;
        public static DamageReport CalculateDamage(float shipSpeed, Random rnd)
        {
            int baseDamage = 10 + (int)shipSpeed;
            bool isCriticalHit = false;

            if (rnd.NextDouble() <= CriticalHitChance)
            {
                baseDamage *= 2;
                isCriticalHit = true;
            }


            return new DamageReport(baseDamage, isCriticalHit);
        }
    }

    public struct Spaceship
    {
        public string ShipName { get; set; }
        public int Health { get; set; }
        public int MaxHealth { get; set; }
        public float Speed { get; set; }

        public Spaceship(string shipName, int maxHealth, float speed)
        {
            ShipName = shipName;
            MaxHealth = Math.Max(maxHealth, 1);
            Health = MaxHealth;
            Speed = speed;
        }
    }

    public struct CargoItem
    {
        private string itemName;
        private float _pricePerUnit;

        public int Quantity { get; set; }
        public string ItemName => itemName;
        public float PricePerUnit => _pricePerUnit;

        public CargoItem(string name, int quantity, float pricePerUnit)
        {
            itemName = name;
            Quantity = quantity;
            _pricePerUnit = pricePerUnit;
        }
    }

    public class Player
    {
        private Spaceship _spaceShip;
        private List<CargoItem> _cargoItems;

        public Spaceship Spaceship => _spaceShip;
        public List<CargoItem> CargoItems => _cargoItems;

        public Player(Spaceship spaceship)
        {
            _spaceShip = spaceship;
            _cargoItems = new List<CargoItem>();
        }
        public void TakeDamage(int damage)
        {
            _spaceShip.Health = Math.Max(0, _spaceShip.Health - damage);
        }

        public void AddCargo(CargoItem newCargo)
        {
            int index = _cargoItems.FindIndex(item => item.ItemName == newCargo.ItemName);

            if (index != -1)
            {
                CargoItem existingItem = _cargoItems[index];
                existingItem.Quantity += newCargo.Quantity;
                _cargoItems[index] = existingItem;
            }
            else
            {
                _cargoItems.Add(newCargo);
            }
        }

        public void ShowInfo()
        {
            Console.WriteLine($"У корабля {_spaceShip.ShipName} здоровье: {_spaceShip.Health}/{_spaceShip.MaxHealth}");
            Console.WriteLine($"На корабле {_spaceShip.ShipName} {_cargoItems.Count} видов груза:");

            foreach (CargoItem item in _cargoItems)
            {
                Console.WriteLine($"{item.ItemName}: Кол-во: {item.Quantity}. Цена за шт: {item.PricePerUnit}");
            }
        }
    }

    public struct DamageReport
    {
        public int DamageDealt;
        public bool IsCriticalHit;

        public DamageReport(int damageDealt, bool isCriticalHit)
        {
            DamageDealt = damageDealt;
            IsCriticalHit = isCriticalHit;
        }

        public void PrintReport()
        {
            if (IsCriticalHit)
                Console.ForegroundColor = ConsoleColor.Green;
            else
                Console.ForegroundColor = ConsoleColor.Red;

            Console.WriteLine($"Нанесено {DamageDealt} урона.");
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
