using System;
using System.Collections.Generic;
using System.Linq;

namespace SelectionSort
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var tavern = new TavernService();

            try
            {
                tavern.AddOrder(new Order(1, "Арагорн", "Жаркое", 45m, 20, true));
                tavern.AddOrder(new Order(2, "Леголас", "Эльфийский хлеб", 25m, 5, false));
                tavern.AddOrder(new Order(3, "Гимли", "Медовуха", 30m, 2, true));
                tavern.AddOrder(new Order(4, "Гэндальф", "Эльфийское вино", 60m, 10, false));

                tavern.DisplayOrders("Все заказы");

                tavern.SortOrders((a, b) => b.Price.CompareTo(a.Price));
                tavern.DisplayOrders("Сортировка по цене (дорогие первыми)");

                tavern.SortOrders((a, b) => a.PreparationTime.CompareTo(b.PreparationTime));
                tavern.DisplayOrders("Сортировка по времени (быстрые первыми)");

                tavern.SortOrders((a, b) => a.CustomerName.CompareTo(b.CustomerName));
                tavern.DisplayOrders("Сортировка по имени клиента");

                tavern.SortOrders((a, b) =>
                {
                    if (a.IsCompleted != b.IsCompleted)
                        return b.IsCompleted.CompareTo(a.IsCompleted);

                    return b.Price.CompareTo(a.Price);
                });
                tavern.DisplayOrders("Сортировка сначала по статусу, потом по цене");
            }
            catch (Exception ex)
            {
                ConsoleHelper.WriteErrorMessage(ex.Message);
            }
        }
    }

    public class Order
    {
        public int Id { get; private set; }
        public string CustomerName { get; private set; }
        public string DishName { get; private set; }
        public decimal Price { get; private set; }
        public int PreparationTime { get; private set; }
        public DateTime OrderTime { get; private set; }
        public bool IsCompleted { get; private set; }

        public Order(int id, string customerName, string dishName, decimal price, int preparationTime, bool isCompleted)
        {
            Id = id;
            CustomerName = customerName ?? throw new ArgumentNullException(nameof(customerName));
            DishName = dishName ?? throw new ArgumentNullException(nameof(dishName));
            Price = price > 0 ? price : throw new ArgumentException("Price must be positive");
            PreparationTime = preparationTime > 0 ? preparationTime : throw new ArgumentException("Preparation time must be positive");
            OrderTime = DateTime.Now;
            IsCompleted = isCompleted;
        }

        public void Complete() { IsCompleted = true; }
    }

    public class TavernService
    {
        private const int TableWidth = 81;
        private List<Order> _orders;

        public TavernService() { _orders = new List<Order>(); }

        public void AddOrder(Order order) { _orders.Add(order); }

        public void CompleteOrder(int id)
        {
            var order = _orders.FirstOrDefault(o => o.Id == id);
            if (order == null)
                throw new ArgumentException($"Order with ID {id} not found");

            order.Complete();
        }

        public void SortOrders(Func<Order, Order, int> comparer)
        {
            if (comparer == null)
                throw new ArgumentNullException(nameof(comparer));

            int minIndex = 0;

            for (int i = 0; i < _orders.Count - 1; i++)
            {
                minIndex = i;

                for (int j = i + 1; j < _orders.Count; j++)
                {
                    if (comparer(_orders[j], _orders[minIndex]) < 0)
                        minIndex = j;
                }
                if (minIndex != i)
                    (_orders[i], _orders[minIndex]) = (_orders[minIndex], _orders[i]);
            }
        }

        public void DisplayOrders(string title)
        {
            if(title == null)
                title = string.Empty;

            Console.WriteLine($"\n=== {title} ===");
            Console.WriteLine(new string('_', TableWidth));
            Console.WriteLine("{0,-5} {1, -1} {2,-15} {3, -1} {4,-15} {5, -1} {6, -5} {7, -1} {8, -15} {9, -1} {10, -10}",
                                "Id", "|", "CustomerName", "|", "DishName", "|", "Price", "|", "PreparationTime", "|", "IsCompleted");
            foreach (var order in _orders)
            {
                Console.WriteLine($"{new string('-', 6)}+{new string('-', 17)}+{new string('-', 17)}+{new string('-', 7)}+{new string('-', 17)}+{new string('-', 12)}");

                Console.WriteLine("{0,-5} {1, -1} {2,-15} {3, -1} {4,-15} {5, -1} {6, -5} {7, -1} {8, -15} {9, -1} {10, -10}",
                    "ID: " + order.Id, "|", order.CustomerName, "|", order.DishName, "|", order.Price + "$", "|", order.PreparationTime + "m", "|", order.IsCompleted ? "Выполнен" : "Ожидает");
            }
            Console.WriteLine(new string('-', TableWidth));
        }
    }

    public static class ConsoleHelper
    {
        public static void WriteErrorMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ResetColor();
            Console.ReadKey(true);
        }
    }
}
