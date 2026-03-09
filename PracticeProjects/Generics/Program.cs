using System;
using System.Collections.Generic;

namespace Generics
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const int MIN_DAMAGE = 5;
            const int MAX_DAMAGE = 15;
            const int MAX_X = 80;
            const int MAX_Y = 20;

            ObjectPool<Bullet> bulletPool = new ObjectPool<Bullet>(
                    createFunc: () => new Bullet(),
                    resetAction: (bullet) => bullet.Reset(),
                    initialPoolSize: 5
                );

            Console.WriteLine("Пул создан. Начинаем тест:\n");

            Console.WriteLine("Получаем 3 пули из пула:");
            Bullet bullet1 = bulletPool.Get();
            Bullet bullet2 = bulletPool.Get();
            Bullet bullet3 = bulletPool.Get();

            Random rand = new Random();
            bullet1.Activate(rand.Next(MIN_DAMAGE, MAX_DAMAGE + 1), rand.Next(0, MAX_X + 1), rand.Next(0, MAX_Y + 1));
            bullet2.Activate(rand.Next(MIN_DAMAGE, MAX_DAMAGE + 1), rand.Next(0, MAX_X + 1), rand.Next(0, MAX_Y + 1));
            bullet3.Activate(rand.Next(MIN_DAMAGE, MAX_DAMAGE + 1), rand.Next(0, MAX_X + 1), rand.Next(0, MAX_Y + 1));

            bullet1.Show();
            bullet2.Show();
            bullet3.Show();

            Console.WriteLine("\nВозвращаем 2 пули в пул:");
            bulletPool.Return(bullet1);
            bulletPool.Return(bullet2);

            Console.WriteLine("\nПолучаем еще 6 пуль (4 из пула, 2 новых):");
            Bullet bullet4 = bulletPool.Get();
            Bullet bullet5 = bulletPool.Get();
            Bullet bullet6 = bulletPool.Get();
            Bullet bullet7 = bulletPool.Get();
            Bullet bullet8 = bulletPool.Get();
            Bullet bullet9 = bulletPool.Get();

            bullet4.Show();
            bullet5.Show();
            bullet6.Show();
            bullet7.Show();
            bullet8.Show();
            bullet9.Show();

            Console.WriteLine("\nВозвращаем 2 пули в пул:");
            bulletPool.Return(bullet4);
            bulletPool.Return(bullet5);

            bulletPool.ShowPoolInfo();
        }
    }

    public class ObjectPool<T>
    {
        private Queue<T> _objectPool = new Queue<T>();
        public int TotalCreated { get; private set; }

        private Func<T> _createFunc { get; }
        private Action<T> _resetAction { get; }

        public ObjectPool(Func<T> createFunc, Action<T> resetAction, int initialPoolSize)
        {
            _createFunc = createFunc;
            _resetAction = resetAction;
            for (int i = 0; i < initialPoolSize; i++)
            {
                T newItem = createFunc();
                _resetAction(newItem);
                _objectPool.Enqueue(newItem);
                TotalCreated++;
            }
        }

        public T Get()
        {
            if (_objectPool.Count > 0)
            {
                T item = _objectPool.Dequeue();

                _resetAction(item);
                return item;
            }
            else
            {
                T newItem = _createFunc();
                _resetAction(newItem);
                TotalCreated++;
                return newItem;
            }
        }

        public void Return(T item)
        {
            _resetAction(item);
            _objectPool.Enqueue(item);
        }

        public void ShowPoolInfo()
        {
            Console.WriteLine($"\nОбъектов в пуле: {_objectPool.Count}.\nВсего новых объектов создано: {TotalCreated}");
        }
    }

    public class Bullet
    {
        public int Damage;
        public int X, Y;
        public bool IsActive;

        public void Reset()
        {
            X = 0; Y = 0;
            Damage = 1;
            IsActive = false;
        }

        public void Activate(int damage, int x, int y)
        {
            Damage = damage;
            X = x;
            Y = y;
            IsActive = true;
        }

        public void Show()
        {
            Console.WriteLine($"Пуля: урон {Damage}, позиция ({X},{Y}), активна: {IsActive.ToString()}");
        }
    }
}
