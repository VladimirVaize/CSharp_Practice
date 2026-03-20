using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace CocktailSort
{
    // Не пишите всё в одном namespace! (.cs файле)
    // Этот код просто пример реализации темы!
    internal class Program
    {
        static void Main(string[] args)
        {
            RenderSystem renderSystem = new RenderSystem();

            renderSystem.AddObject(new GameObject("Дерево", 1.2f, GameObjectType.Tree));
            renderSystem.AddObject(new GameObject("Дерево", 3.7f, GameObjectType.Tree));

            renderSystem.AddObject(new GameObject("Камень", 5.1f, GameObjectType.Stone));
            renderSystem.AddObject(new GameObject("Камень", 7.3f, GameObjectType.Stone));
            renderSystem.AddObject(new GameObject("Камень", 2.8f, GameObjectType.Stone));

            renderSystem.AddObject(new GameObject("Игрок", 4.5f, GameObjectType.Player));

            renderSystem.AddObject(new GameObject("NPC", 6.2f, GameObjectType.NPC));
            renderSystem.AddObject(new GameObject("NPC", 8.0f, GameObjectType.NPC));

            renderSystem.AddObject(new GameObject("Предмет", 9.1f, GameObjectType.Item));
            renderSystem.AddObject(new GameObject("Предмет", 0.5f, GameObjectType.Item));
            renderSystem.AddObject(new GameObject("Предмет", 5.9f, GameObjectType.Item));

            renderSystem.Shuffle();

            renderSystem.DisplayOrder("Неотсортированное состояние");

            renderSystem.CocktailSort();
            renderSystem.DisplayOrder("Отсортированное состояние");

            renderSystem.SimulateMovement();
            renderSystem.DisplayOrder("Почти отсортированное состояние (до сортировки)");

            renderSystem.CocktailSort();
            renderSystem.DisplayOrder("После повторной сортировки");
        }
    }

    public enum GameObjectType
    {
        Player,
        Tree,
        Stone,
        NPC,
        Item
    }

    public class GameObject
    {
        public int Id { get; private set; }
        public string Name { get; set; }
        public float Y { get; set; }
        public GameObjectType Type { get; set; }

        private static int _nextId = 0;

        public GameObject(string name, float y, GameObjectType type)
        {
            Id = _nextId++;
            Name = name ?? "Unknown";
            Y = y;
            Type = type;
        }

        public override string ToString()
        {
            return $"[ID:{Id}] {Type}:{Name} (Y={Y:F2})";
        }
    }

    public class RenderSystem
    {
        private List<GameObject> _objects;

        public RenderSystem()
        {
            _objects = new List<GameObject>();
        }

        public void AddObject(GameObject obj)
        {
            _objects.Add(obj);
        }

        public void CocktailSort()
        {
            int left = 0;
            int right = _objects.Count - 1;

            int swapsCount = 0;
            int comparisonCount = 0;

            Stopwatch sortTimer = new Stopwatch();

            sortTimer.Start();

            while (left < right)
            {
                bool swapped = false;
                for (int i = left; i < right; i++)
                {
                    if (_objects[i].Y > _objects[i + 1].Y)
                    {
                        Swap(i, i + 1);

                        swapsCount++;
                        swapped = true;
                    }
                    comparisonCount++;
                }
                right--;

                if (!swapped)
                    break;

                swapped = false;
                for (int i = right; i > left; i--)
                {
                    if (_objects[i].Y < _objects[i - 1].Y)
                    {
                        Swap(i, i - 1);

                        swapsCount++;
                        swapped = true;
                    }
                    comparisonCount++;
                }
                left++;

                if (!swapped)
                    break;
            }

            sortTimer.Stop();

            Console.WriteLine($"\nCocktailSort: кол-во сравнений - {comparisonCount}, обменов - {swapsCount}. Время сортировки: {sortTimer.ElapsedTicks} тиков.");
        }

        public void Swap(int i, int j)
        {
            var temp = _objects[i];
            _objects[i] = _objects[j];
            _objects[j] = temp;
        }

        public void DisplayOrder(string title)
        {
            Console.WriteLine($"\n{title}");
            Console.WriteLine(new string('-', 50));

            for (int i = 0; i < _objects.Count; i++)
            {
                Console.WriteLine($"{i,2}. {_objects[i]}");
            }

            Console.WriteLine(new string('-', 50));
        }

        public void Shuffle()
        {
            Random rnd = new Random();
            for (int i = 0; i < _objects.Count; i++)
            {
                int j = rnd.Next(i, _objects.Count);
                var temp = _objects[i];
                _objects[i] = _objects[j];
                _objects[j] = temp;
            }
        }

        public void SimulateMovement()
        {
            Random rnd = new Random();

            for (int i = 0; i < _objects.Count; i++)
            {
                if (_objects[i].Type == GameObjectType.Player || _objects[i].Type == GameObjectType.NPC)
                {
                    float oldY = _objects[i].Y;
                    float delta = (float)(rnd.NextDouble() * 2 - 1) * 0.5f;
                    _objects[i].Y = Math.Max(0, Math.Min(10, _objects[i].Y + delta));

                    Console.WriteLine($"{_objects[i].Type} переместился: Y {oldY:F2} -> {_objects[i].Y:F2}");
                }
            }
        }
    }
}
