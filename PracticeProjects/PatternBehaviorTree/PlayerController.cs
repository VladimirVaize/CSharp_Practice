using PatternBehaviorTree.Auxiliary;
using System;

namespace PatternBehaviorTree
{
    public class PlayerController
    {
        public Vector2 Position { get; set; }

        public PlayerController(Vector2 startPosition)
        {
            Position = startPosition;
        }

        public void HandleInput()
        {
            if (!Console.KeyAvailable)
                return;

            var key = Console.ReadKey(true).Key;
            var newPosition = Position;

            switch (key)
            {
                case ConsoleKey.W:
                    newPosition = new Vector2(Position.X, Position.Y - 1);
                    break;
                case ConsoleKey.S:
                    newPosition = new Vector2(Position.X, Position.Y + 1);
                    break;
                case ConsoleKey.A:
                    newPosition = new Vector2(Position.X - 1, Position.Y);
                    break;
                case ConsoleKey.D:
                    newPosition = new Vector2(Position.X + 1, Position.Y);
                    break;
                default:
                    return;
            }

            Position = newPosition;
            Console.WriteLine($"\nИгрок переместился на {Position}");
        }

        public void ShowPosition()
        {
            Console.WriteLine($"Игрок на позиции: {Position}");
        }
    }
}
