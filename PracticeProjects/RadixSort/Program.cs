using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace RadixSort
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Dungeon dungeon = new Dungeon();

            Console.WriteLine("=== Генерация подземелья (20 комнат) ===");
            dungeon.AddRandomRooms(20);
            Console.WriteLine("До сортировки:");
            dungeon.ShowDungeon();

            Console.WriteLine("\nПосле сортировки (Radix sort):");
            dungeon.SortRoomsByDepthRadix();
            dungeon.ShowDungeon();


            var BenchmarkValue = 100000;
            Console.WriteLine($"\n=== Бенчмарк ({BenchmarkValue:N0} комнат) ===");
            dungeon.StartRadixSortBenchmark(BenchmarkValue);
            dungeon.StartDefaultSortBenchmark(BenchmarkValue);

            /*=== Бенчмарк (10 000 000 комнат) ===
              Radix sort: 4326 ms.
              List.Sort: 10290 ms.*/
        }
    }

    public class Room
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public int Depth { get; private set; }
        public int EnemyCount { get; private set; }
        public bool IsVisited { get; private set; }

        private static int _nextId = 0;
        private static readonly Random _random = new Random();

        public Room(string name, int depth, int enemyCount, bool isVisited)
        {
            Id = _nextId++;
            Name = name;
            Depth = depth;
            EnemyCount = enemyCount;
            IsVisited = isVisited;
        }

        public static Room RandomRoom()
        {
            int nameLength = _random.Next(2, 7);
            StringBuilder randName = new StringBuilder(nameLength);

            for (int i = 0; i < nameLength; i++)
            {
                char randomChar = (char)('a' + _random.Next(26));
                randName.Append(randomChar);
            }

            int randDepth = _random.Next(1000);
            int randEnemyCount = _random.Next(25);

            return new Room(randName.ToString(), randDepth, randEnemyCount, false);
        }

        public override string ToString()
        {
            return $"{Name} (Глубина: {Depth}, Врагов: {EnemyCount})";
        }
    }

    public class Dungeon
    {
        private List<Room> _rooms;
        private const int DigitBase = 10;

        public Dungeon() { _rooms = new List<Room>(); }

        public void AddRoom(Room room)
        {
            if (room == null)
                throw new ArgumentNullException(nameof(room));

            _rooms.Add(room);
        }

        public void AddRandomRooms(int value)
        {
            for (int i = 0; i < value; i++)
                _rooms.Add(Room.RandomRoom());
        }

        public void ClearRooms() { _rooms.Clear(); }

        // Сортирует комнаты по глубине используя LSD Radix Sort.
        // Сложность: O(n * k), где n - количество элементов, k - количество разрядов.
        // Память: O(n + base) для временных списков.
        public void SortRoomsByDepthRadix()
        {
            var groups = new List<List<Room>>();

            for (int i = 0; i < DigitBase; i++)
                groups.Add(new List<Room>());

            int length = GetMaxLength();


            for (int step = 0; step < length; step++)
            {
                foreach (var room in _rooms)
                {
                    var value = GetDigit(room.Depth, step);
                    groups[value].Add(room);
                }

                _rooms.Clear();

                foreach (var group in groups)
                {
                    foreach (var room in group)
                        _rooms.Add(room);
                }

                foreach (var group in groups)
                    group.Clear();
            }
        }

        // Сортирует комнаты используя встроенную сортировку List.Sort (QuickSort).
        public void DefaultListSort()
        {
            _rooms.Sort((room1, room2) => room1.Depth.CompareTo(room2.Depth));
        }

        // Возвращает количество разрядов у максимальной глубины.
        private int GetMaxLength()
        {
            if (_rooms.Count == 0) return 0;

            int maxDepth = _rooms.Max(room => room.Depth);
            return maxDepth == 0 ? 1 : (int)Math.Log10(maxDepth) + 1;
        }

        // Возвращает цифру числа в указанной позиции (0 - единицы, 1 - десятки и т.д.).
        public int GetDigit(int number, int digitPosition)
        {
            return (number / (int)Math.Pow(DigitBase, digitPosition)) % DigitBase;
        }

        public void ShowDungeon()
        {
            foreach (var room in _rooms)
                Console.WriteLine(room.ToString());
        }

        public void StartRadixSortBenchmark(int count)
        {
            ClearRooms();
            AddRandomRooms(count);

            Stopwatch benchmarkTimer = new Stopwatch();
            benchmarkTimer.Start();

            SortRoomsByDepthRadix();

            benchmarkTimer.Stop();
            Console.WriteLine($"Radix sort: {benchmarkTimer.ElapsedMilliseconds} ms.");
        }

        public void StartDefaultSortBenchmark(int count)
        {
            ClearRooms();
            AddRandomRooms(count);

            Stopwatch benchmarkTimer = new Stopwatch();
            benchmarkTimer.Start();

            DefaultListSort();

            benchmarkTimer.Stop();
            Console.WriteLine($"List.Sort: {benchmarkTimer.ElapsedMilliseconds} ms.");
        }
    }
}
