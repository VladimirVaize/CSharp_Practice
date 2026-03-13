using System;
using System.Collections.Generic;
using System.Linq;

namespace UpcastingAndDowncasting
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Item> inventory = new List<Item>()
            {
                new Weapon("Меч", 5, 15),
                new Potion("Малое зелье здоровья", 1, 50),
                new Armor("Стальной щит", 8, 20)
            };

            InventoryInteraction.ShowInventoryItems(inventory);
            InventoryInteraction.UseInventoryItems(inventory);
            InventoryInteraction.ShowInventoryWeight(inventory);
        }
    }

    static class InventoryInteraction
    {
        public static void ShowInventoryItems(List<Item> inventoryItems)
        {
            if (!CheckInventoryForNull(inventoryItems)) return;

            Console.WriteLine("Содержимое инвентаря:");
            foreach (Item item in inventoryItems)
            {
                Console.WriteLine($"- {item.Name}");
            }
        }

        public static void ShowInventoryWeight(List<Item> inventoryItems)
        {
            if (!CheckInventoryForNull(inventoryItems)) return;

            Console.Write("\nОбщий вес инвентаря: ");
            Console.WriteLine(inventoryItems.Sum(item => item.Weight) + " кг.");
        }

        public static void UseInventoryItems(List<Item> inventoryItems)
        {
            if (!CheckInventoryForNull(inventoryItems)) return;

            Console.WriteLine("\nИспользуем предметы:");
            foreach (Item item in inventoryItems)
            {
                if(item != null)
                {
                    item.Use();
                    switch (item)
                    {
                        case Weapon w: Console.WriteLine($"Это оружие! Сила атаки: {w.AttackPower}"); break;
                        case Potion p: Console.WriteLine($"Это зелье! Лечит: {p.HealAmount}"); break;
                        case Armor a: Console.WriteLine($"Это броня! Защита: {a.Defense}"); break;
                        default:
                            Console.WriteLine($"Неизвестный тип предмета: {item.GetType().Name}");
                            break;
                    }
                }
            }
        }

        public static bool CheckInventoryForNull(List<Item> inventoryItems)
        {
            if (inventoryItems == null)
            {
                Console.WriteLine("Инвентарь не существует!");
                return false;
            }
            return true;
        }
    }

    public class Item
    {
        private const int MinWeight = 1;
        public string Name { get; }
        public int Weight { get; }

        public Item(string name, int weight)
        {
            Name = name;
            Weight = Math.Max(weight, MinWeight);
        }

        public virtual void Use()
        {
            Console.WriteLine($"{Name} использован. Но эффекта нет (базовый класс).");
        }
    }

    public class Weapon : Item
    {
        private const int MinAttackPower = 1;
        public int AttackPower { get; }

        public Weapon(string name, int weight, int attackPower) : base(name, weight)
        {
            AttackPower = Math.Max(attackPower, MinAttackPower);
        }

        public override void Use()
        {
            Console.WriteLine($"{Name} наносит {AttackPower} урона.");
        }
    }

    public class Potion : Item
    {
        private const int MinHealAmount = 1;
        public int HealAmount {  get; }

        public Potion(string name, int weight, int healAmount) : base(name, weight)
        {
            HealAmount = Math.Max(healAmount, MinHealAmount);
        }

        public override void Use()
        {
            Console.WriteLine($"{Name} восстанавливает {HealAmount} здоровья.");
        }
    }

    public class Armor : Item
    {
        private const int MinDefense = 1;
        public int Defense {  get; }

        public Armor(string name, int weight, int defense) : base(name, weight)
        {
            Defense = Math.Max(defense, MinDefense);
        }

        public override void Use()
        {
            Console.WriteLine($"{Name} дает {Defense} защиты.");
        }
    }
}
