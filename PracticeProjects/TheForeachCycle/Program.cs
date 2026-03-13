using System;
using System.Collections.Generic;

namespace TheForeachCycle
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Player player = new Player();
            player.Inventory.Add(new Coin(5));
            player.Inventory.Add(new HealthPotion(30, 5));
            player.Inventory.Add(new Key());
            player.Inventory.Add(new Item("Сапог"));
            player.Inventory.Add(new Item("Меч"));
            player.Inventory.Add(new Item("Палка", true, 6));

            bool isPlayed = true;

            while(isPlayed)
            {
                DisplayMenu();

                if (int.TryParse(Console.ReadLine(), out int choice))
                {
                    switch (choice)
                    {
                        case 1: DisplayInventory(player); break;
                        case 2:
                            DisplayInventory(player);
                            UseItem(player); 
                            break;
                        case 3: isPlayed = false; break;
                        default: ShowMessage("Неизвестное действие!", ConsoleColor.Red); break;
                    }
                }
                else
                    ShowMessage("Ошибка ввода! Введите число.", ConsoleColor.Red);
                Console.ReadKey();
            }
        }

        public static void DisplayMenu()
        {
            Console.Clear();
            Console.WriteLine("===МЕНЮ===\n");
            Console.WriteLine("1. Показать инвентарь.");
            Console.WriteLine("2. Использовать предмет.");
            Console.WriteLine("3. Выход.");
            Console.Write("\nВыберите действие: ");
        }

        public static void DisplayInventory(Player player)
        {
            Console.Clear();
            int i = 1;
            Console.WriteLine("===Инвентарь===\n");

            foreach(Item item in player.Inventory)
            {
                Console.Write($"{i}. {item.Name}");
                if (item.IsStackable)
                    Console.WriteLine($" (x{item.Count})");
                else
                    Console.WriteLine();
                i++;
            }
        }

        public static void UseItem(Player player)
        {
            Console.Write("Введите номер предмета: ");

            if (int.TryParse(Console.ReadLine(), out int choice))
            {
                if (choice < 1 || choice > player.Inventory.Count)
                {
                    ShowMessage("Неверный номер предмета!", ConsoleColor.Red);
                    return;
                }

                Item selectedItem = player.Inventory[choice - 1];

                if (!selectedItem.CanBeUsed)
                {
                    selectedItem.Use();
                    return;
                }

                if (selectedItem is HealthPotion potion)
                {
                    
                    if (player.Health == player.MaxHealth)
                        Console.WriteLine($"У вас уже максимальное HP: {player.Health}/{player.MaxHealth}");
                    else
                    {
                        selectedItem.Use();
                        int neededHeal = player.MaxHealth - player.Health;
                        int actualHeal = Math.Min(potion.HealPower, neededHeal);
                        player.Health += actualHeal;

                        Console.WriteLine($"Ваше HP: {player.Health}/{player.MaxHealth}");
                    }
                }
                else
                    selectedItem.Use();

                if (selectedItem.IsStackable && selectedItem.Count > 1)
                    selectedItem.Count--;
                else
                    player.Inventory.RemoveAt(choice - 1);
            }
            else
                ShowMessage("Ошибка ввода! Введите число.", ConsoleColor.Red);
        }

        public static void ShowMessage(string massage, ConsoleColor color = ConsoleColor.White)
        {
            Console.ForegroundColor = color;
            Console.WriteLine("\n\n" + massage);
            Console.ResetColor();
            Console.ReadKey();
        }
    }

    public class Player
    {
        public int Health { get; set; }
        public int MaxHealth { get; }
        public List<Item> Inventory { get; }

        public Player(int health = 20, int maxHealth = 100)
        {
            Health = Math.Min(health, maxHealth);

            Inventory = new List<Item>();
            MaxHealth = maxHealth;
        }
    }

    public class Item
    {
        public string Name { get; }
        public bool IsStackable { get; }
        public int Count { get; set; }

        public virtual bool CanBeUsed => true;

        public Item(string name, bool isStackable = false, int count = 1)
        {
            Name = name;
            IsStackable = isStackable;
            if(isStackable)
                Count = count;
            else
                Count = 1;
        }

        public virtual void Use()
        {
            Console.WriteLine($"Вы использовали {Name}");
        }
    }

    public class HealthPotion : Item
    {
        public int HealPower;

        public HealthPotion(int healPower = 10, int count = 1) : base("Зелье здоровья", true, count) 
        { 
            HealPower = healPower;
        }

        public override void Use()
        {
            Console.WriteLine($"Вы использовали {Name}. Восстановлено {HealPower} HP.");
        }
    }

    public class Coin : Item
    {
        public Coin(int count = 1) : base("Монета", true, count) { }

        public override bool CanBeUsed => false;

        public override void Use()
        {
            Console.WriteLine("Монету нельзя использовать, это валюта.");
        }
    }

    public class Key : Item
    {
        public Key() : base("Ключ") { }

        public override void Use()
        {
            Console.WriteLine($"Вы использовали {Name} для открытия двери.");
        }
    }
}
