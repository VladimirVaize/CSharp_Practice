using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace InsertionSort
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Leaderboard leaderboard = new Leaderboard();

            leaderboard.AddPlayer(Player.GetRandomPlayer());
            leaderboard.AddPlayer(Player.GetRandomPlayer());
            leaderboard.AddPlayer(Player.GetRandomPlayer());
            leaderboard.AddPlayer(Player.GetRandomPlayer());
            leaderboard.AddPlayer(Player.GetRandomPlayer());

            Console.WriteLine("\nОтсортированный список");

            leaderboard.PrintLeaderboard();

            Console.WriteLine("\nДобавление нового элемента");

            leaderboard.AddPlayer(Player.GetRandomPlayer());
            leaderboard.AddPlayer(Player.GetRandomPlayer());
            leaderboard.AddPlayer(Player.GetRandomPlayer());

            leaderboard.PrintLeaderboard();

            leaderboard.UpdatePlayerXP(6, 20);

            leaderboard.PrintLeaderboard();

            Console.WriteLine("\nТоп 5 игроков:");
            leaderboard.PrintTopPlayers(5);
        }
    }

    public class Player
    {
        private static readonly Random _rand = new Random();
        public int Id { get; private set; }
        public string Name { get; private set; }
        public int TotalXP { get; private set; }
        public DateTime LastPlayed { get; private set; }

        private static int _nextId = 0;

        public Player(string name, int totalXP, DateTime lastPlayed)
        {
            Id = _nextId++;
            Name = name ?? "Unknown";
            TotalXP = Math.Max(totalXP, 0);
            LastPlayed = lastPlayed;
        }

        public void AddXP(int additionalXP)
        {
            if (additionalXP > 0)
                TotalXP += additionalXP;
            else
                Console.WriteLine("Нельзя добавить меньше 1 опыта!");
        }

        public static Player GetRandomPlayer()
        {
            string[] names = new string[] { "Rinrinr", "Nid", "Zuni", "IdeaCraft", "DimaStar",
                "DimaPro", "DimaKing", "DimaLord", "DimaAce", "FaceCraft",
                "FaceMirror", "FaceHunter", "TopTier", "TopDog", "TopPlayer",
                "TopPriority", "Pussycat", "Pussy Power", "DickDoodle", "Softie"
            };

            string name = names[_rand.Next(names.Length)];
            int totalXP = _rand.Next(100);
            DateTime nowDateTime = DateTime.Now;
            DateTime lastPlayed = nowDateTime.AddDays(-_rand.Next(365 * 10) / 3.0); // Просто случайная дата

            return new Player(name, totalXP, lastPlayed);

        }
    }

    public class Leaderboard
    {
        List<Player> players = new List<Player>();

        public void AddPlayer(Player player)
        {
            Stopwatch timer = Stopwatch.StartNew();
            if (player != null)
            {
                players.Add(player);
                InsertIntoSorted(player);
                timer.Stop();

                Console.WriteLine($"Добавлен элемент {player.Name}. Время сортировки {timer.Elapsed}");
            }
            else
                Console.WriteLine("Player не может быть Null");
        }

        public void UpdatePlayerXP(int playerId, int additionalXP)
        {
            for (int i = 0; i < players.Count; i++)
            {
                if (players[i].Id == playerId)
                {
                    Player foundPlayer = players[i];
                    int oldXP = players[i].TotalXP;
                    players.RemoveAt(i);
                    foundPlayer.AddXP(additionalXP);

                    Console.WriteLine($"Добавление XP игроку {foundPlayer.Name}. {oldXP} -> {foundPlayer.TotalXP}");

                    AddPlayer(foundPlayer);
                    return;
                }
            }
            Console.WriteLine($"Игрок с ID {playerId} не найден");
        }

        /*public void Sort() // Сортировка всего List<Player>. (Просто для примера)
        {
            for (int i = 1; i < players.Count; i++)
            {
                var temp = players[i];
                var j = i;

                while (j > 0 && temp.TotalXP > players[j - 1].TotalXP)
                {
                    players[j] = players[j - 1];
                    j--;
                }
                players[j] = temp;
            }
        }*/

        public void InsertIntoSorted(Player sortablePlayer)
        {
            var j = players.Count - 1;

            while (j > 0 && sortablePlayer.TotalXP > players[j - 1].TotalXP)
            {
                players[j] = players[j - 1];
                j--;
            }
            players[j] = sortablePlayer;
        }

        public void PrintTopPlayers(int count)
        {
            int topCount = Math.Min(count, players.Count);
            Console.WriteLine(new string('-', 50));
            Console.WriteLine("{0,-5} {1,-12} {2,-8} {3, -17}", "Id", "Name", "XP", "Дата последней игры");
            for (int i = 0; i < topCount; i++)
            {
                Console.WriteLine("{0,-5} {1,-12} {2,-8} {3, -17}", players[i].Id, players[i].Name, players[i].TotalXP, players[i].LastPlayed);
            }
            Console.WriteLine(new string('-', 50));
        }

        public void PrintLeaderboard()
        {
            Console.WriteLine(new string('-', 50));
            Console.WriteLine("{0,-5} {1,-12} {2,-8} {3, -17}", "Id", "Name", "XP", "Дата последней игры");
            for (int i = 0; i < players.Count; i++)
            {
                Console.WriteLine("{0,-5} {1,-12} {2,-8} {3, -17}", players[i].Id, players[i].Name, players[i].TotalXP, players[i].LastPlayed);
            }
            Console.WriteLine(new string('-', 50));
        }
    }
}
