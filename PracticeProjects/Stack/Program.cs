using System;
using System.Collections.Generic;

namespace Stack
{
    internal class Program
    {
        static void Main(string[] args)
        {
            GameHistory gameHistory = new GameHistory();

            bool isPlaying = true;

            while (isPlaying)
            {
                Console.WriteLine($"Текущие свитки: {gameHistory.ScrollCount}");
                Console.WriteLine("Команды: [F]ind, [U]se, [D]rop, [Z] - Undo, [Q] - Exit");
                Console.Write("Ваш выбор: ");

                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.F:
                        gameHistory.FoundScroll();
                        break;
                    case ConsoleKey.U:
                        gameHistory.UseScroll();
                        break;
                    case ConsoleKey.D:
                        gameHistory.DropScroll();
                        break;
                    case ConsoleKey.Z:
                        gameHistory.Undo();
                        break;
                    case ConsoleKey.Q:
                        gameHistory.PrintHistory();
                        isPlaying = false;
                        break;
                    default:
                        Console.Write("\nНекорректный ввод!\n");
                        break;
                }
            }
        }
    }

    public class GameHistory
    {
        private Random rand = new Random();
        private Stack<string> _actions = new Stack<string>();
        private int _scrollCount = 0;

        private const string FoundAction = "Found a scroll";
        private const string SuccessUseAction = "Successfully used scroll!";
        private const string FailedUseAction = "Failed to use scroll";
        private const string DropAction = "Dropped the scroll";

        private bool HasScrolls() => _scrollCount > 0;
        private string GenerateUseResult => rand.Next(2) == 1 ? SuccessUseAction : FailedUseAction;

        private void ChangeScroll(int changeNum)
        {
            _scrollCount += changeNum;
        }

        private void AddAction(string action)
        {
            _actions.Push(action);
        }

        public int ScrollCount => _scrollCount;

        public void PrintHistory()
        {
            Console.WriteLine("\nИстория действий:");

            foreach(string action in _actions.ToArray())
            {
                Console.WriteLine(action); 
            }
        }

        public void FoundScroll()
        {
            AddAction(FoundAction);
            ChangeScroll(1);
            Console.WriteLine("\nВы нашли свиток!\n");
        }

        public void Undo()
        {
            if (_actions.Count > 0)
            {
                string lastActions = _actions.Pop();
                Console.Write($"\nОтменено действие: {lastActions}\n");

                if (lastActions == FoundAction)
                {
                    ChangeScroll(-1);
                }
                else if (lastActions == SuccessUseAction || lastActions == FailedUseAction || lastActions == DropAction)
                {
                    ChangeScroll(1);
                }
            }
        }

        public void UseScroll()
        {
            if (!HasScrolls())
            {
                Console.Write("\nУ вас нет свитков.\n");
                return;
            }

            string result = GenerateUseResult;
            AddAction(result);
            ChangeScroll(-1);

            Console.Write($"\nВы использовали свиток! {(result == SuccessUseAction ? "(Удачно)" : "(Не удачно)")}\n");
        }

        public void DropScroll()
        {
            if (HasScrolls())
            {
                AddAction(DropAction);
                ChangeScroll(-1);
                Console.WriteLine("\nВы выбросили свиток!\n");
            }
            else
            {
                Console.Write("\nУ вас нету свитков.\n");
            }
        }
    }
}
