using System;

namespace ArrayExpansion
{
    internal class Program
    {
        static Hero Player = new Hero("АРТУР");
        static Random rand = new Random();
        static void Main(string[] args)
        {
            bool isPlaying = true;

            while (isPlaying)
            {
                Console.Clear();
                DisplayInventory();
                DisplayMenu();

                if(int.TryParse(Console.ReadLine(), out int choise))
                {
                    switch (choise)
                    {
                        case 1:
                            switch(rand.Next(0,5))
                            {
                                case 0: ExpandInventory(1); break;
                                case 1:
                                    int randomIndex = rand.Next(0,Player.Inventory.Length);
                                    if (Player.Inventory[randomIndex] != null)
                                        RemoveItem(randomIndex);
                                    else
                                        ShowMessage("Ничего не произошло.");
                                    break;
                                case 2:
                                case 3:
                                case 4:
                                    Item newItem = new Item(rand.Next(0, Item.itemName.Length), rand.Next(1, 5));
                                    AddItem(newItem);
                                    break;
                                default:
                                    ShowMessage("Ничего не произошло.");
                                    break;
                            }
                            break;
                        case 2:
                            Console.Write("Введите номер слота инвентаря: ");
                            if(int.TryParse(Console.ReadLine(), out int index))
                                RemoveItem(index - 1);
                            break;
                        case 3:
                            Console.Write("Введите название предмета: ");
                            string findItemName = Console.ReadLine();
                            int foundIndex = FindItem(findItemName);

                            if (foundIndex >= 0)
                                ShowMessage($"{findItemName} найден в слоте {foundIndex + 1}");
                            else ShowWarning($"Предмет {findItemName} не найден.");
                            break;
                        case 4: isPlaying = false; break;
                        default: ShowWarning("Неизвестное действие!"); break;
                    }
                }
            }

        }

        static void DisplayMenu()
        {
            Console.WriteLine($"=== ГЛАВНОЕ МЕНЮ ===\n");
            Console.WriteLine("1 - На поиски приключений");
            Console.WriteLine("2 - Выбросить предмет");
            Console.WriteLine("3 - Найти предмет в инвентаре");
            Console.WriteLine("4 - Выход");

            Console.Write("\nВыберите действие: ");
        }

        static void DisplayInventory()
        {
            Console.WriteLine($"=== ИНВЕНТАРЬ ГЕРОЯ \"{Player.Name}\" ===\n");
            Console.WriteLine($"Имя: {Player.Name}\nРазмер инвентаря: {Player.InventorySize}");
            for (int i = 0; i < Player.InventorySize; i++)
            {
                if (Player.Inventory[i] != null)
                    Console.WriteLine($"Слот ({i + 1}): {Player.Inventory[i].Name}" +
                        $" | Кол-во: {Player.Inventory[i].Value}" +
                        $" | Вес: {Player.Inventory[i].Weight}");
                else
                    Console.WriteLine($"Слот ({i + 1}): [Null]");
            }
            Console.WriteLine();
        }

        static void ExpandInventory(int additionalSlots = 3)
        {
            Player.Expand(additionalSlots);
            ShowMessage($"Инвентарь расширен! Новый размер: {Player.InventorySize}");
        }

        static void AddItem(Item newItem)
        {
            bool isFull = true;

            int existingItemIndex = FindItem(newItem.Name);

            if (existingItemIndex >= 0)
            {
                Player.Inventory[existingItemIndex].Value += newItem.Value;
                Player.Inventory[existingItemIndex].Weight += newItem.Weight;
                ShowMessage($"Добавлен предмет: {newItem.Name} " +
                    $"в кол-ве: {newItem.Value}. Вес: {newItem.Weight}");
                return;
            }


            for (int i = 0; i < Player.Inventory.Length; i++)
            {
                if (Player.Inventory[i] == null)
                {
                    isFull = false;
                    break;
                }
            }

            if (isFull)
                ExpandInventory();

            for (int i = 0; i < Player.Inventory.Length; i++)
            {
                if(Player.Inventory[i] == null)
                {
                    Player.AddItem(newItem, i);
                    break;
                }
            }

            ShowMessage($"Добавлен предмет: {newItem.Name} " +
                        $"в кол-ве: {newItem.Value}. Вес: {newItem.Weight}");
        }

        static void RemoveItem(int index)
        {
            if(index < 0 || index >= Player.Inventory.Length)
            {
                ShowWarning("Неверный индекс!");
                return;
            }

            if (Player.Inventory[index] == null)
            {
                ShowWarning("Эта ячейка уже пуста!");
                return;
            }

            string removedItemName = Player.Inventory[index].Name;

            for(int i = index; i < Player.Inventory.Length - 1; i++)
            {
                Player.Inventory[i] = Player.Inventory[i + 1];
            }

            Player.RemoveItemAt(Player.Inventory.Length - 1);

            ShowMessage($"Предмет '{removedItemName}' удален. Инвентарь сдвинут.");
        }

        static int FindItem(string itemName)
        {
            for(int i = 0; i < Player.Inventory.Length; i++)
            {
                if (Player.Inventory[i] != null && Player.Inventory[i].Name == itemName)
                    return i;
            }
            return -1;
        }

        static void ShowMessage(string message)
        {
            Console.WriteLine("\n\n" + message);
            Console.ReadKey();
        }

        static void ShowWarning(string message)
        {
            Console.WriteLine("\n\n" + message);
            Console.ReadKey();
        }
    }

    public class Hero
    {
        public string Name { get; }
        public Item[] Inventory { get; set; }
        public int InventorySize => Inventory.Length;

        public Hero (string name, int inventorySize = 5, Item[] inventory = null)
        {
            Name = name;
            Inventory = new Item[inventorySize];

            if (inventory != null)
            {
                int copyLength = Math.Min(inventory.Length, inventorySize);
                
                for(int i = 0; i < copyLength; i++)
                    Inventory[i] = inventory[i];
            }
        }

        public void RemoveItemAt(int index)
        {
            Inventory[index] = null;
        }
        public void AddItem(Item item, int index)
        {
            if (index < 0 || index >= Inventory.Length)
                throw new ArgumentOutOfRangeException();

            Inventory[index] = item;
        }
        public void Expand(int slots)
        {
            Item[] newInventory = new Item[Inventory.Length + slots];
            Array.Copy(Inventory, newInventory, Inventory.Length);

            Inventory = newInventory;
        }
    }

    public class Item
    {
        public string Name { get; }
        public int Weight { get; set; }
        public int Value { get; set; }

        public static string[] itemName = new string[] { "Яблоко", "Банан", "Мечь", "Кружка", "Лопата", "Хлеб", "Беляш", "Кирка", "Топор", "Заяц", "Бревно", "Гриб" };
        public static int[] itemWeight = new int[] { 1, 1, 5, 2, 6, 2, 1, 8, 6, 4, 12, 1 };

        public Item(int index, int value)
        {
            Name = itemName[index];
            Weight = itemWeight[index] * value;
            Value = value;
        }
    }
}
