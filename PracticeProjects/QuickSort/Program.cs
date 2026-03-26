using System;
using System.Collections.Generic;

namespace Quicksort
{
    internal class Program
    {
        static void Main(string[] args)
        {
            RenderQueue renderQueue = new RenderQueue();

            try
            {
                renderQueue.AddRandomObjects(10);

                Console.WriteLine("=== ДО СОРТИРОВКИ (порядок добавления) ===");
                renderQueue.Render();

                Console.WriteLine("\n=== ПОСЛЕ СОРТИРОВКИ (порядок отрисовки: от дальних к ближним) ===");
                renderQueue.SortByDepth();
                renderQueue.Render();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }

    public class RenderObject
    {
        public string Name { get; private set; }
        public float Depth { get; private set; }

        private static readonly Random _random = new Random();

        public RenderObject(string name, float depth)
        {
            Name = name;
            Depth = depth;
        }

        public static RenderObject RandomRenderObject()
        {
            string name = "RandomName" + _random.Next(50);
            int depth = _random.Next(100);

            return new RenderObject(name, depth);
        }

        public override string ToString()
        {
            return $"{Name} (Depth: {Depth})";
        }
    }

    public class RenderQueue
    {
        private List<RenderObject> _renderObjects;

        public RenderQueue()
        {
            _renderObjects = new List<RenderObject>();
        }

        public void AddObject(RenderObject obj)
        {
            if( obj == null)
                throw new ArgumentNullException(nameof(obj));

            _renderObjects.Add(obj);
        }

        public void AddRandomObjects(int count)
        {
            for (int i = 0; i < count; i++)
                AddObject(RenderObject.RandomRenderObject());
        }

        public void SortByDepth()
        {
            if (_renderObjects == null || _renderObjects.Count == 0)
            {
                Console.WriteLine("Нет объектов для сортировки.");
                return;
            }

            var depthComparer = Comparer<RenderObject>.Create((a, b) =>
            {
                if (a == null && b == null) return 0;
                if (a == null) return -1;
                if (b == null) return 1;
                return b.Depth.CompareTo(a.Depth);
            });

            try
            {
                Sort.QuickSort(_renderObjects, 0, _renderObjects.Count - 1, depthComparer);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при сортировке: {ex.Message}");
            }
        }

        public void Render()
        {
            for (int i = 0; i < _renderObjects.Count; i++)
                Console.WriteLine($"{i}. {_renderObjects[i].ToString()}");
        }
    }

    public static class Sort
    {
        public static void QuickSort<T>(List<T> list, int left, int right, IComparer<T> comparer)
        {
            if (left >= right)
                return;

            var pivot = Partition(list, left, right, comparer);

            QuickSort(list, left, pivot - 1, comparer);
            QuickSort(list, pivot + 1, right, comparer);
        }

        private static int Partition<T>(List<T> list, int left, int right, IComparer<T> comparer)
        {
            var pointer = left;

            for (int i = left; i <= right; i++)
            {
                if (comparer.Compare(list[i], list[right]) < 0)
                {
                    (list[pointer], list[i]) = (list[i], list[pointer]);
                    pointer++;
                }
            }
            (list[pointer], list[right]) = (list[right], list[pointer]);
            return pointer;
        }
    }
}
