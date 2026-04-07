using EntryPoint.Server;
using System;

namespace EntryPoint
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Game Server";
            Console.ForegroundColor = ConsoleColor.Green;

            Console.WriteLine($"Получено аргументов: {args.Length}");
            for (int i = 0; i < args.Length; i++)
            {
                Console.WriteLine($"  args[{i}] = '{args[i]}'");
            }

            using (var entryPoint = new GameServerEntryPoint())
            {
                var config = entryPoint.ParseArguments(args);

                if (config == null)
                {
                    return;
                }

                if (entryPoint.InitializeServices(config))
                {
                    entryPoint.RunServerLoop();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Ошибка инициализации сервера. Нажмите любую клавишу для выхода...");
                    Console.ResetColor();
                    Console.ReadKey();
                }
            }

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\nНажмите любую клавишу для выхода...");
            Console.ResetColor();
            Console.ReadKey();
        }
    }
}
