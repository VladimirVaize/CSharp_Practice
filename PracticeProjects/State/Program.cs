using System;
using System.Threading;

namespace State
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Enemy goblin = new Enemy("Гоблин", 100, 5, 2, 20, 2);
            Player hero = new Player("Jon", 100, 4, 4);

            Console.WriteLine("=== Игровая сессия началась ===");
            Console.WriteLine($"Враг: {goblin.Name} (Здоровье: {goblin.Health})");
            Console.WriteLine($"Игрок: {hero.Name} (Здоровье: {hero.Health})");
            Console.WriteLine($"Начальная позиция игрока: ({hero.X}, {hero.Y})");
            Console.WriteLine($"Начальная позиция врага: ({goblin.X}, {goblin.Y})");
            Console.WriteLine("================================\n");

            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine($"\n--- Итерация {i + 1} ---");

                goblin.Update(hero);

                float distance = hero.GetDistanceTo(goblin);
                Console.WriteLine($"Игрок: ({hero.X:F1}, {hero.Y:F1}) | Враг: ({goblin.X:F1}, {goblin.Y:F1})");
                Console.WriteLine($"Расстояние до игрока: {distance:F2}");
                Console.WriteLine($"Текущее состояние врага: {goblin.CurrentStateName()}");

                Thread.Sleep(1500);

                if (i % 3 == 0 && i < 10 && hero.Health > 0)
                {
                    float newX = hero.X - 1;
                    float newY = hero.Y - 1;

                    hero.MoveTo(newX, newY);
                    Console.WriteLine($"Игрок переместился на ({newX:F1}, {newY:F1})");
                }
                else if (i == 10)
                {
                    Console.WriteLine("\n=== Игровая сессия завершена ===");
                    Console.WriteLine($"Итоговое здоровье игрока: {hero.Health}");
                    Console.WriteLine($"Итоговое здоровье врага: {goblin.Health}");
                }
            }
        }
    }
}
