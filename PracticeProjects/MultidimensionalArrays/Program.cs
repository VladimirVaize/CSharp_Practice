using System;
using System.Collections.Generic;

namespace MultidimensionalArrays
{
    internal class Program
    {
        public static int[,] gameBoard;
        public static Random rand = new Random();
        public static int movesCount = 0;
        static void Main(string[] args)
        {
            Console.Clear();
            Console.WriteLine("Выберите размер поля: ");
            Console.WriteLine("1 - 4x4\n2 - 6x6\n3 - 8x8\n4 - 9x9");
            Console.SetCursorPosition(22, 0);

            if(!int.TryParse(Console.ReadLine(), out int choice))
            {
                Console.Clear();
                Console.WriteLine("Ошибка: нужно ввести число!");
                Console.ReadKey();
                return;
            }

            switch(choice)
            {
                case 1:
                    GenerateRandomBoard(4, ref gameBoard);
                    break;
                case 2:
                    GenerateRandomBoard(6, ref gameBoard);
                    break;
                case 3:
                    GenerateRandomBoard(8, ref gameBoard);
                    break;
                case 4:
                    GenerateRandomBoard(9, ref gameBoard);
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine("Вы ввели не верный вариант!");
                    return;
            }

            bool isGameWon = false;
            while (!isGameWon)
            {
                if(gameBoard == null)
                {
                    Console.WriteLine("Ошибка инициализации игры!");
                    return;
                }
                else
                {
                    Console.Clear();
                    PrintBoard(gameBoard);
                    ChooseTiles(gameBoard);
                    isGameWon = CheckForWin(gameBoard);
                }
            }

            int totalTiles = gameBoard.GetLength(0) * gameBoard.GetLength(1);
            Console.Clear();
            PrintBoard(gameBoard);

            Console.WriteLine($"\nПОБЕДА!!!");
            Console.WriteLine($"Размер поля: {gameBoard.GetLength(0)}x{gameBoard.GetLength(1)}");
            Console.WriteLine($"Количество ходов: {movesCount}");
            Console.WriteLine($"Эффективность: {(double)movesCount / totalTiles:F2} ходов на плитку");
            Console.ReadKey();

        }

        static void GenerateRandomBoard(int boardSize, ref int[,] board)
        {
            board = new int[boardSize, boardSize];

            for (int i = 0; i < boardSize; i++)
            {
                HashSet<int> usedInRow = new HashSet<int>();
                for(int j = 0; j < boardSize; j++)
                {
                    int randomNum;
                    do
                    {
                        randomNum = rand.Next(0, boardSize);
                    }
                    while (usedInRow.Contains(randomNum));

                    usedInRow.Add(randomNum);
                    board[i, j] = randomNum + 1;
                }
            }
        }

        static void PrintBoard(int[,] board)
        {
            ConsoleColor[] tileColors = new ConsoleColor[]
            {
                ConsoleColor.Red, ConsoleColor.Blue, ConsoleColor.Green,
                ConsoleColor.Yellow, ConsoleColor.White, ConsoleColor.Magenta,
                ConsoleColor.Cyan, ConsoleColor.Gray, ConsoleColor.DarkYellow
            };

            Console.Clear();
            Console.WriteLine($"=== TileSwap Puzzle === Ходов: {movesCount}");
            Console.WriteLine("Команды: R - рестарт, Q - выход\n");

            Console.Write(" ");
            for(int i = 0; i < board.GetLength(0); i++)
            {
                if (i == 0)
                    Console.Write($"| {i + 1} ");
                else
                    Console.Write($" {i + 1} ");
            }
            Console.WriteLine();
            for (int i = 0; i < board.GetLength(0); i++)
            {
                if (i == 0)
                    Console.Write("-+-");
                Console.Write("---");
            }
            Console.WriteLine();

            for (int i = 0; i < board.GetLength(0); i++)
            {
                

                for (int j = 0; j < board.GetLength(1); j++)
                {
                    if (j == 0)
                        Console.Write($"{i + 1}|");

                    Console.ForegroundColor = tileColors[board[i, j] - 1];

                    Console.Write($" {board[i, j]} ");
                }
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine();
            }
        }

        static void ChooseTiles(int[,] board)
        {
            int row1, col1, row2, col2;

            Console.WriteLine("\nВведите координаты или команду (R/Q):");
            Console.WriteLine("\nПервая плитка:");

            if(!TryReadCoordinate("Строка: ", out row1, board.GetLength(0)) ||
               !TryReadCoordinate("Столбец: ", out col1, board.GetLength(1)))
            {
                Console.WriteLine("Некорректный ввод! Попробуйте снова.");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("\nВторая плитка:");
            if(!TryReadCoordinate("Строка: ", out row2, board.GetLength(0)) ||
               !TryReadCoordinate("Столбец: ", out col2, board.GetLength(1)))
            {
                Console.WriteLine("Некорректный ввод! Попробуйте снова.");
                Console.ReadKey();
                return;
            }

            if (row1 == row2 && col1 == col2)
            {
                Console.WriteLine("Выбрана одна и та же позиция!");
                Console.ReadKey();
            }
            else
            {
                SwapTiles(row1, col1, row2, col2, board);
            }
        }

        static bool TryReadCoordinate(string prompt, out int coord, int maxSize)
        {
            Console.Write(prompt);
            string input = Console.ReadLine();

            if(input.ToUpper() == "Q") Environment.Exit(0);
            if(input.ToUpper() == "R") RestartGame();

            if (int.TryParse(input, out coord))
            {
                coord--;
                return coord >= 0 && coord < maxSize;
            }
            return false;
        }

        static void SwapTiles(int row1, int col1, int row2, int col2, int[,] board)
        {
            if(AreTilesAdjacent(row1, col1, row2, col2))
            {
                int temp = board[row1, col1];
                board[row1, col1] = board[row2, col2];
                board[row2, col2] = temp;

                movesCount++;
            }
            else
            {
                Console.WriteLine("Ошибка: можно менять только соседние плитки (по горизонтали или вертикали)!");
                Console.ReadKey();
            }
        }

        static bool AreTilesAdjacent(int row1, int col1, int row2, int col2)
        {
            int rowDiff = Math.Abs(row1 - row2);
            int colDiff = Math.Abs(col1 - col2);
            return (rowDiff == 1 && colDiff == 0) || (rowDiff == 0 && colDiff == 1);
        }

        static bool CheckForWin(int[,] board)
        {
            int rows = board.GetLength(0);
            int cols = board.GetLength(1);

            for(int i = 0; i < rows; i++)
            {
                HashSet<int> rowValues = new HashSet<int>();
                for (int j = 0; j < cols; j++)
                {
                    if(rowValues.Contains(board[i, j]))
                    {
                        return false;
                    }
                    rowValues.Add(board[i, j]);
                }
            }

            for(int j = 0; j < cols; j++)
            {
                HashSet<int> colValues = new HashSet<int>();
                for(int i = 0; i < rows; i++)
                {
                    if(colValues.Contains(board[i, j]))
                    {
                        return false;
                    }
                    colValues.Add(board[i, j]);
                }
            }
            return true;
        }

        static void RestartGame()
        {
            movesCount = 0;
            gameBoard = null;
            Console.Clear();
            Main(new string[0]);
        }
    }
}
