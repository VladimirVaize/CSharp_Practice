using PatternFacade.Facade;
using System;

namespace PatternFacade
{
    internal class Program
    {
        static void Main(string[] args)
        {
            GameFacade game = new GameFacade();

            GameResult result = game.StartGame();

            if (result.IsSuccess)
            {
                Console.WriteLine($"\n=== ИГРА ЗАПУЩЕНА ===");
                if (result.LoadedSave != null)
                {
                    Console.WriteLine($"Загружен персонаж: {result.LoadedSave.PlayerName}");
                    Console.WriteLine($"Уровень: {result.LoadedSave.Level}, Здоровье: {result.LoadedSave.Health}");
                }
                else
                {
                    Console.WriteLine("Новая игра (сохранение не найдено)");
                }

                Console.WriteLine("\n[ИГРА] Нажмите Enter для выхода...");
                Console.ReadLine();

                game.Shutdown();
            }
            else
            {
                Console.WriteLine($"\n!!! ОШИБКА ЗАПУСКА: {result.ErrorMessage}");
            }
        }
    }
}
