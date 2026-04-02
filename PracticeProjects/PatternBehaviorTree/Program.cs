using PatternBehaviorTree.Auxiliary;
using System;
using System.Collections.Generic;
using System.Threading;

namespace PatternBehaviorTree
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== СИСТЕМА ИИ ОХРАННИКА (BEHAVIOR TREE) ===\n");

            var patrolPoints = new List<Vector2>
            {
                new Vector2(3, 0),
                new Vector2(3, 3),
                new Vector2(0, 3),
                new Vector2(0, 0)
            };

            var guard = new GuardAI(new Vector2(0, 0), patrolPoints);
            var player = new PlayerController(new Vector2(5, 5));

            guard.Player = player;

            Console.WriteLine("Игра началась!");
            Console.WriteLine("Управление: W/A/S/D - движение игрока");
            Console.WriteLine("Нажмите ESC для выхода\n");

            int tick = 0;
            bool isRunning = true;

            while (isRunning)
            {
                tick++;
                Console.WriteLine($"\n--- Тик {tick} ---");

                player.HandleInput();

                guard.Update();

                Console.WriteLine($"Охранник: {guard.Position}");
                player.ShowPosition();

                if (guard.CanSeePlayer)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("ОХРАННИК ВИДИТ ИГРОКА!");
                    Console.ResetColor();
                }
                else if (guard.IsAlerted)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("ОХРАННИК В ТРЕВОГЕ!");
                    Console.ResetColor();
                }

                Thread.Sleep(1000);

                if (Console.KeyAvailable)
                {
                    var exitKey = Console.ReadKey(true);
                    if (exitKey.Key == ConsoleKey.Escape)
                    {
                        isRunning = false;
                        Console.WriteLine("\nИгра завершена.");
                    }
                }
            }
        }
    }
}
