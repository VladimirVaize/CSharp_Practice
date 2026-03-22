using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace ShellSort
{
    internal class Program
    {
        static void Main(string[] args)
        {
            EventQueue eventQueue = new EventQueue();
            try
            {
                for (int i = 0; i < 15; i++)
                {
                    eventQueue.GameEvents.Add(GameEvent.GetRandomEvent());
                }

                Console.WriteLine("\nНеотсортированный список (Unsorted list)");
                eventQueue.DisplayEvents();

                Console.ReadKey(true);

                eventQueue.SortEvents();

                eventQueue.DisplayEvents();

                Console.ReadKey(true);

                eventQueue.AddEvent(GameEvent.GetRandomEvent());

                eventQueue.DisplayEvents();

                Console.ReadKey(true);

                GameEvent nextEvent = eventQueue.GetNextEvent();
                Console.WriteLine($"\nПолучен элемент: [{nextEvent.Id}] - \"{nextEvent.Name}\" с приоритетом {nextEvent.Priority}");
                Console.WriteLine("Финальный отсортированный список");
                eventQueue.DisplayEvents();

                Console.ReadKey(true);

                Console.WriteLine("\nЭлементы с Priority от 75 до 100");
                foreach (GameEvent item in eventQueue.GetEventsByPriorityRange(75, 100))
                {
                    Console.WriteLine($"[{item.Id}] - {item.Name} Priority:{item.Priority}");
                }

                Console.WriteLine("\n=== Проверка обработки ошибок ===");
                Console.WriteLine("\nПопытка добавить событие Null");
                eventQueue.AddEvent(null);

                Console.WriteLine("\nПопытка добавить событие с приоритетом 150");
                eventQueue.AddEvent(new GameEvent("NameOfErrorEvent", "fffff", 150, DateTime.Now));
            }
            catch (Exception ex)
            {
                ConsoleHelper.WriteErrorMessage(ex.Message);
            }
        }
    }

    public class GameEvent
    {
        private static readonly Random _random = new Random();
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public int Priority { get; private set; }
        public DateTime CreatedAt { get; private set; }

        private static int _nextId = 0;

        public GameEvent(string name, string description, int priority, DateTime createdAt)
        {
            if (priority < 1 || priority > 100)
                throw new ArgumentOutOfRangeException(nameof(priority), "Приоритет должен быть в диапазоне 1-100");

            Priority = priority;
            Id = _nextId++;
            Name = name ?? "Unknown";
            Description = description ?? "Unknown";

            CreatedAt = createdAt;
        }

        public static GameEvent GetRandomEvent()
        {
            string[] names = {
                "Атака героя", "Ход босса", "Восстановление здоровья",
                "Ультимейшн героя", "Атака дракона", "Получение опыта",
                "Завершение квеста", "Начало квеста"
            };

            int descriptionLength = _random.Next(15, 40);

            string randName = names[_random.Next(names.Count())];
            StringBuilder randDescription = new StringBuilder(descriptionLength);

            for (int i = 0; i < descriptionLength; i++)
            {
                char randomChar = (char)('a' + _random.Next(26));
                randDescription.Append(randomChar);
            }

            DateTime randCreatedAt = DateTime.Now.AddSeconds(-_random.Next(120));

            return new GameEvent(randName, randDescription.ToString(), _random.Next(1, 101), randCreatedAt);
        }
    }

    public class EventQueue
    {
        public List<GameEvent> GameEvents { get; private set; }

        public EventQueue()
        {
            GameEvents = new List<GameEvent>();
        }

        public void AddEvent(GameEvent newEvent)
        {
            Stopwatch timer = Stopwatch.StartNew();

            int swapsCount = 0;
            int comparisonCount = 0;

            try
            {
                if (newEvent != null)
                {
                    GameEvents.Add(newEvent);

                    var j = GameEvents.Count - 1;

                    while (j > 0)
                    {
                        comparisonCount++;
                        if (ShouldComeFirst(newEvent, GameEvents[j - 1]))
                        {
                            GameEvents[j] = GameEvents[j - 1];
                            j--;
                            swapsCount++;
                        }
                        else
                            break;
                    }
                    GameEvents[j] = newEvent;

                    timer.Stop();

                    Console.WriteLine($"\nДобавлен элемент {newEvent.Name} [{newEvent.Id}]. Время сортировки {timer.Elapsed} " +
                        $"\nКол-во сравнений - {comparisonCount}, обменов - {swapsCount}. (Insertion Sort)");
                }
                else
                    throw new ArgumentNullException(nameof(GameEvent) + " - не может быть Null.");
            }
            catch (Exception ex)
            {
                ConsoleHelper.WriteErrorMessage(ex.Message);
            }
        }

        public GameEvent GetNextEvent()
        {
            try
            {
                if (GameEvents.Count == 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(GetNextEvent) + ": " + nameof(GameEvents) + " не содержит объекты.");
                }
                GameEvent priorityGameEvent = GameEvents[0];
                GameEvents.RemoveAt(0);

                return priorityGameEvent;
            }
            catch (Exception ex)
            {
                ConsoleHelper.WriteErrorMessage(ex.Message);
                return null;
            }
        }

        public void SortEvents()
        {
            var step = GameEvents.Count / 2;
            Stopwatch timer = Stopwatch.StartNew();

            int swapsCount = 0;
            int comparisonCount = 0;

            while (step > 0)
            {
                for (int i = step; i < GameEvents.Count; i++)
                {
                    int j = i;
                    while (j >= step)
                    {
                        comparisonCount++;
                        if (ShouldComeFirst(GameEvents[j], GameEvents[j - step]))
                        {
                            (GameEvents[j], GameEvents[j - step]) = (GameEvents[j - step], GameEvents[j]);

                            j -= step;
                            swapsCount++;
                        }
                        else
                            break;
                    }
                }
                step /= 2;
            }
            timer.Stop();
            Console.WriteLine($"\nСписок отсортирован за {timer.Elapsed}. \nКол-во сравнений - {comparisonCount}, обменов - {swapsCount}. (Shell Sort)");
        }

        private static bool ShouldComeFirst(GameEvent a, GameEvent b)
        {
            return a.Priority > b.Priority || (a.Priority == b.Priority && a.CreatedAt < b.CreatedAt);
        }

        public void DisplayEvents()
        {
            Console.WriteLine(new string('_', 110));
            Console.WriteLine("{0,-5} {1, -1} {2,-25} {3, -1} {4,-40} {5, -1} {6, -10} {7, -1} {8, -15}",
                                "Id", "|", "Name", "|", "Description", "|", "Priority", "|", "CreatedAt");
            for (int i = 0; i < GameEvents.Count; i++)
            {
                Console.WriteLine($"{new string('-', 6)}+{new string('-', 27)}+{new string('-', 42)}+{new string('-', 12)}+{new string('-', 19)}");
                Console.WriteLine("{0,-5} {1, -1} {2,-25} {3, -1} {4,-40} {5, -1} {6, -10} {7, -1} {8, -15}",
                    GameEvents[i].Id, "|", GameEvents[i].Name, "|", GameEvents[i].Description, "|", GameEvents[i].Priority, "|", GameEvents[i].CreatedAt);
            }
            Console.WriteLine(new string('-', 110));
        }

        public List<GameEvent> GetEventsByPriorityRange(int minPriority, int maxPriority)
        {
            if (minPriority < 1 || maxPriority > 100 || minPriority > maxPriority)
                throw new ArgumentException($"Недопустимый диапазон приоритетов: {minPriority}-{maxPriority}. Допустимые значения: 1-100");

            return GameEvents.Where(item => item.Priority >= minPriority && item.Priority <= maxPriority).ToList();
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
