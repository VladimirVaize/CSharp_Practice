using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace LINQ
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Inventory inventory = new Inventory();

            inventory.AddItem(new Item("Меч \"Excalibur\"", ItemType.Weapon, 5, 500));
            inventory.AddItem(new Item("Ржавый Меч", ItemType.Weapon, 1, 10, true));
            inventory.AddItem(new Item("Большое Зелье Лечения", ItemType.Potion, 3, 50));
            inventory.AddItem(new Item("Латы Дракона", ItemType.Armor, 5, 300, true));
            inventory.AddItem(new Item("Шкура Крысы", ItemType.Material, 1, 2));
            inventory.AddItem(new Item("Шкура Медведя", ItemType.Material, 5, 150));
            inventory.AddItem(new Item("Кинжал Вора", ItemType.Weapon, 2, 50));
            inventory.AddItem(new Item("Плащ Вора", ItemType.Armor, 2, 35));
            inventory.AddItem(new Item("Моментальное Зелье Исцеления", ItemType.Potion, 5, 600));
            inventory.AddItem(new Item("Яд \"Мороз\"", ItemType.Potion, 3, 200));
            inventory.AddItem(new Item("Яд \"Пламя\"", ItemType.Potion, 4, 250));
            inventory.AddItem(new Item("Корона 7ми Королей", ItemType.Armor, 5, 1500, true));
            inventory.AddItem(new Item("Бревно Дуба", ItemType.Material, 1, 10));
            inventory.AddItem(new Item("Сталь", ItemType.Material, 2, 25));
            inventory.AddItem(new Item("Адамантий", ItemType.Material, 5, 450));
            inventory.AddItem(new Item("Дубина Троля", ItemType.Weapon, 1, 25));
            inventory.AddItem(new Item("Зуб Волка", ItemType.Material, 1, 5));

            inventory.FindMostExpensiveItem();
            inventory.GetAllItemsOfType(ItemType.Weapon);
            inventory.GetAllItemsOfType(ItemType.Potion);
            inventory.GetLegendaryItems();
            inventory.CalculateAverageValue();
            inventory.GetItemsSortedByName();
            inventory.GetUniqueItemTypes();
            inventory.GetTop3RarestItems();
            inventory.CheckIfItemExists("Бревно Дуба");
            inventory.CheckIfItemExists("Бревно Березы");
            inventory.GetEquippedItems();
            inventory.GroupItemsByType();
            inventory.GetItemsByRarityAndValue(3, 500);
        }
    }

    public enum ItemType
    {
        Weapon,
        Armor,
        Potion,
        Material
    }

    public class Item
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public ItemType Type { get; private set; }
        public int Rarity { get; private set; }
        public float Value { get; private set; }
        public bool IsEquipped { get; private set; }

        private static int _nextId = 0;

        public Item(string name, ItemType type, int rarity, float value, bool isEquipped = false)
        {
            Id = _nextId++;
            Name = name;
            Type = type;
            Rarity = rarity;
            Value = value;
            IsEquipped = isEquipped;
        }
    }

    public class Inventory
    {
        List<Item> items = new List<Item>();

        public void AddItem(Item newItem)
        {
            items.Add(newItem);
        }

        public Item FindMostExpensiveItem()
        {
            var mostExpensiveItem = items.OrderByDescending(item => item.Value).FirstOrDefault();

            if (mostExpensiveItem == null)
            {
                Console.WriteLine("\nИнвентарь пуст!");
                return null;
            }

            Console.WriteLine($"\nСамый дорогой предмет: {mostExpensiveItem.Name}");

            Thread.Sleep(2000);
            return mostExpensiveItem;
        }

        public List<Item> GetAllItemsOfType(ItemType type)
        {
            var allItemsOfType = items.Where(item => item.Type == type).ToList();

            Console.WriteLine($"\nПредметы типа {type}:");
            foreach (var item in allItemsOfType)
            {
                Console.WriteLine($"- {item.Name}");
            }

            Thread.Sleep(2000);
            return allItemsOfType;
        }

        public List<Item> GetLegendaryItems()
        {
            var legendaryItems = items.Where(item => item.Rarity == 5).ToList();

            Console.WriteLine("\nСамые редкие предметы:");
            foreach (var item in legendaryItems)
            {
                Console.WriteLine($"- {item.Name}");
            }

            Thread.Sleep(2000);
            return legendaryItems;
        }

        public float CalculateAverageValue()
        {
            var averageValue = items.Average(item => item.Value);

            Console.WriteLine($"\nСредняя стоимость предметов: {averageValue}");

            Thread.Sleep(2000);
            return averageValue;
        }

        public List<Item> GetItemsSortedByName()
        {
            var itemsSortedByName = items.OrderBy(item => item.Name).ToList();

            Console.WriteLine("\nПредметы отсортированные по имени:");
            foreach (var item in itemsSortedByName)
            {
                Console.WriteLine($"- {item.Name}");
            }

            Thread.Sleep(2000);
            return itemsSortedByName;
        }

        public List<ItemType> GetUniqueItemTypes()
        {
            var uniqueItemTypes = items.Select(item => item.Type).Distinct().ToList();

            Console.WriteLine("\nТипы предметов, которые есть в инвенторе: ");
            foreach (var item in uniqueItemTypes)
            {
                Console.WriteLine($"- {item}");
            }

            Thread.Sleep(2000);
            return uniqueItemTypes;
        }

        public List<Item> GetTop3RarestItems()
        {
            var top3RarestItems = items.OrderByDescending(item => item.Rarity).Take(3).ToList();

            Console.WriteLine("\nТоп 3 самых редких предмета:");
            foreach (var item in top3RarestItems)
            {
                Console.WriteLine($"- {item.Name}");
            }

            Thread.Sleep(2000);
            return top3RarestItems;
        }

        public bool CheckIfItemExists(string itemName)
        {
            var isItemExists = items.Any(item => item.Name == itemName);

            if (isItemExists)
            {
                Console.WriteLine($"\n{itemName} есть в инвентаре.");

                Thread.Sleep(2000);
                return true;
            }
            else
            {
                Console.WriteLine($"\n{itemName} нету в инвентаре.");

                Thread.Sleep(2000);
                return false;
            }
        }

        public List<Item> GetEquippedItems()
        {
            var equippedItems = items.Where(item => item.IsEquipped == true).ToList();

            Console.WriteLine($"\nЭкипированные предмены:");
            foreach (var item in equippedItems)
            {
                Console.WriteLine($"- {item.Name}");
            }

            Thread.Sleep(2000);
            return equippedItems;
        }

        public void GroupItemsByType()
        {
            var groupedItems = items.GroupBy(item => item.Type).ToList();

            Console.WriteLine("\nГруппы предметов:");
            foreach (var sortItemsGroup in groupedItems)
            {
                Thread.Sleep(2000);
                Console.WriteLine($"\nГруппа {sortItemsGroup.Key} (кол-во: {sortItemsGroup.Count()}):");
                foreach (var item in sortItemsGroup)
                {
                    Console.WriteLine($"- {item.Name}");
                }
            }
        }

        public List<Item> GetItemsByRarityAndValue(int minRarity, float minValue)
        {
            var itemsByRarityAndValue = items.Where(item => item.Rarity >= minRarity && item.Value >= minValue).ToList();

            Console.WriteLine($"\nПредметы с редкостью >= {minRarity} и стоимостью >= {minValue}:");
            foreach (var item in itemsByRarityAndValue)
            {
                Console.WriteLine($"- {item.Name}");
            }

            Thread.Sleep(2000);
            return itemsByRarityAndValue;
        }
    }
}
