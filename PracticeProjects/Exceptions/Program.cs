using System;
using System.Collections.Generic;
using System.Linq;

namespace Exceptions
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Inventory inventory = new Inventory();
                CraftingSystem craftingSystem = new CraftingSystem();
                EventHandler eventHandler = new EventHandler();
                eventHandler.SubscribeToCraftingSystem(craftingSystem);

                Item woodStick = new Item("Палка", ItemRarity.Common);
                Item stone = new Item("Камень", ItemRarity.Common);
                Item rope = new Item("Верёвка", ItemRarity.Common);
                Item diamond = new Item("Алмаз", ItemRarity.Epic);


                inventory.AddItem(woodStick);
                inventory.AddItem(woodStick);
                inventory.AddItem(woodStick);

                inventory.AddItem(stone);
                inventory.AddItem(stone);

                inventory.AddItem(rope);

                inventory.AddItem(diamond);

                Dictionary<Item, int> axeIngredients = new Dictionary<Item, int>();
                axeIngredients.Add(woodStick, 2);
                axeIngredients.Add(stone, 1);
                axeIngredients.Add(rope, 1);

                Dictionary<Item, int> diamondPendant = new Dictionary<Item, int>();
                diamondPendant.Add(rope, 1);
                diamondPendant.Add(diamond, 1);

                Recipe stoneAxeRecipe = new Recipe(new Item("Каменный топор", ItemRarity.Common), axeIngredients);
                Recipe diamondRecipe = new Recipe(new Item("Бриллиантовый кулон", ItemRarity.Epic), diamondPendant);

                craftingSystem.AddRecipe(stoneAxeRecipe);
                craftingSystem.AddRecipe(diamondRecipe);

                while (true)
                {
                    Console.WriteLine("\nВаш инвентарь:");
                    foreach (var item in inventory.Items)
                        Console.WriteLine($"- {item.Name}");

                    Console.Write("\nЧто крафтим? (или 'exit' для выхода): ");
                    string input = Console.ReadLine();

                    if (input?.ToLower() == "exit")
                        break;

                    if (string.IsNullOrWhiteSpace(input))
                    {
                        Console.WriteLine("Введите название предмета!");
                        continue;
                    }

                    Recipe recipe = craftingSystem.FindRecipeByName(input);

                    if (recipe == null)
                    {
                        Console.WriteLine($"Рецепт '{input}' не найден!");
                        continue;
                    }

                    craftingSystem.Craft(inventory, recipe);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Критическая ошибка в игре: {ex.Message}");
            }
        }
    }

    public enum ItemRarity
    {
        Common,
        Rare,
        Epic,
        Legendary
    }

    public class EventHandler
    {
        public void SubscribeToCraftingSystem(CraftingSystem craftingSystem)
        {
            craftingSystem.OnItemCrafted += (item) => Console.WriteLine($"Вы скрафтили {item.Name}!");
        }
    }

    public class Item
    {
        public int Id { get; }
        public string Name { get; }
        public ItemRarity Rarity { get; }

        private static int _nextId = 0;

        public Item(string name, ItemRarity rarity)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Имя предмета не может быть пустым");

            Id = _nextId++;
            Name = name;
            Rarity = rarity;
        }
    }

    public class Recipe
    {
        public Item Result { get; }
        public Dictionary<Item, int> Ingredients { get; }

        public Recipe(Item result, Dictionary<Item, int> ingredients)
        {
            if (result == null)
            {
                throw new ArgumentNullException("Результат не может быть Null");
            }
            if (ingredients.Count <= 0)
            {
                throw new ArgumentNullException("Список ингредиентов не может быть пустым");
            }
            foreach (Item item in ingredients.Keys)
            {
                if (item == null)
                {
                    throw new ArgumentNullException("Ингредиент не может быть Null");
                }
            }

            Result = result;
            Ingredients = ingredients;
        }
    }

    public class Inventory
    {
        public List<Item> Items { get; } = new List<Item>();

        public void AddItem(Item item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));
            Items.Add(item);
        }

        public void RemoveItem(Item item, int count)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));
            if (count <= 0)
                throw new ArgumentException("Количество должно быть положительным");

            for (int i = 0; i < count; i++)
            {
                if (!Items.Remove(item))
                    throw new InvalidOperationException("Недостаточно предметов для удаления");
            }
        }

        public bool HasItems(Dictionary<Item, int> requiredItems)
        {
            var inventoryCounts = Items.GroupBy(item => item.Id).ToDictionary(g => g.Key, g => g.Count());

            foreach (var reqItem in requiredItems)
            {
                int requiredId = reqItem.Key.Id;
                int requiredCount = reqItem.Value;

                inventoryCounts.TryGetValue(requiredId, out int availableCount);

                if (availableCount < requiredCount)
                    return false;
            }

            return true;
        }
    }

    public class CraftingSystem
    {
        public event Action<Item> OnItemCrafted;
        private List<Recipe> _availableRecipes = new List<Recipe>();

        public void AddRecipe(Recipe recipe)
        {
            _availableRecipes.Add(recipe);
        }

        public Recipe FindRecipeByName(string name)
        {
            return _availableRecipes.FirstOrDefault(r =>
                r.Result.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }

        public void Craft(Inventory inventory, Recipe recipe)
        {
            try
            {
                if (recipe == null)
                    throw new ArgumentNullException(nameof(recipe));

                if (inventory == null)
                    throw new ArgumentNullException(nameof(inventory));

                if (!inventory.HasItems(recipe.Ingredients))
                {
                    var missingItems = GetMissingItems(inventory, recipe);
                    throw new InvalidOperationException($"Не хватает: {string.Join(", ", missingItems)}");
                }

                foreach (var item in recipe.Ingredients)
                {
                    inventory.RemoveItem(item.Key, item.Value);
                }
                inventory.AddItem(recipe.Result);
                OnItemCrafted?.Invoke(recipe.Result);
            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine($"Ошибка данных: {ex.Message}");
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Нельзя скрафтить: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Критическая ошибка: {ex.Message}");
            }
        }

        public List<string> GetMissingItems(Inventory inventory, Recipe recipe)
        {
            List<string> missingItems = new List<string>();
            var inventoryCounts = inventory.Items.GroupBy(i => i.Id).ToDictionary(g => g.Key, g => g.Count());
            foreach (var ingredient in recipe.Ingredients)
            {
                int requiredId = ingredient.Key.Id;
                int requiredCount = ingredient.Value;

                inventoryCounts.TryGetValue(requiredId, out int availableCount);

                if (availableCount < requiredCount)
                {
                    missingItems.Add($"{ingredient.Key.Name} (нужно {requiredCount}, есть {availableCount})");
                }
            }
            return missingItems;
        }
    }
}
