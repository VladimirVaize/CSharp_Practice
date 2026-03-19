using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BubbleSort
{
    // Не пишите всё в одном namespace! (.cs файле)
    // Этот код просто пример реализации темы!
    internal class Program
    {
        static void Main(string[] args)
        {
            Leaderboard leaderboard = new Leaderboard();
            
            var loadRecords = leaderboard.LoadFromFile();

            if (loadRecords != null)
            {
                leaderboard.records = loadRecords;
            }
            else
            {
                leaderboard.AddRandomRecord(5);
            }

            bool isPlaying = true;

            while (isPlaying)
            {
                Console.Clear();
                Console.WriteLine("=== LEADERBOARD SYSTEM ===");
                Console.WriteLine("1. Добавить новый рекорд");
                Console.WriteLine("2. Показать топ-10 (сортировка по очкам)");
                Console.WriteLine("3. Показать все рекорды по дате");
                Console.WriteLine("4. Сохранить и выйти");
                Console.Write("Выберите действие: ");

                if (int.TryParse(Console.ReadLine(), out int choice))
                {
                    switch (choice)
                    {
                        case 1:
                            Console.Write("\nВведите кол-во новых рекордов: ");

                            if (int.TryParse(Console.ReadLine(), out int count))
                            {
                                leaderboard.AddRandomRecord(count);
                                Console.WriteLine($"Добавлено {count} новых рекордов.\n");
                            }
                            else
                                Console.WriteLine("Нужно ввести число!\n");
                            break;
                        case 2:
                            Console.WriteLine($"\nТоп 10 по очкам:\n");

                            leaderboard.DisplayTop(10);
                            break;
                        case 3:
                            Console.WriteLine($"\nСортировка по дате:\n");

                            leaderboard.SortByDateBubble();
                            leaderboard.ShowAllRecords();
                            break;
                        case 4:
                            leaderboard.SaveToFile();
                            isPlaying = false;
                            break;
                        default:

                            break;
                    }
                }

                Console.WriteLine("\nНажмите кнопку чтобы продолжить...");
                Console.ReadKey(true);
            }
        }
    }

    public class GameRecord
    {
        public string PlayerName { get; }
        public int Score { get; }
        public DateTime Date { get; }
        public TimeSpan PlayTime { get; }

        public GameRecord(string playerName, int score, DateTime date, TimeSpan playTime)
        {
            PlayerName = playerName != null ? playerName : "Player";
            Score = score > 0 ? score : 0;
            Date = date;
            PlayTime = playTime;
        }

        public override string ToString()
        {
            int years = (int)(PlayTime.TotalDays / 365);
            int days = (int)(PlayTime.TotalDays - years * 365);

            return $"[{Date:dd.MM.yyyy}] {PlayerName}: {Score}. [{years}y, {days}d]";
        }
    }

    public class Leaderboard
    {
        private readonly Random _rand = new Random();
        private JsonSerializerOptions _jsonOptions = new JsonSerializerOptions
        {
            WriteIndented = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            IncludeFields = true,
            Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
        };

        public List<GameRecord> records { get; set; } = new List<GameRecord>();

        public void AddRecord(GameRecord record)
        {
            if (record == null)
                throw new ArgumentNullException(nameof(record));

            records.Add(record);
        }

        public void AddRandomRecord(int count = 1)
        {
            if (count <= 0)
                throw new ArgumentOutOfRangeException("Не допустимое значение при создании новых рекордов: " + nameof(AddRandomRecord));

            string[] names = new string[] { "Rinrinr", "Nid", "Zuni", "IdeaCraft", "DimaStar",
                "DimaPro", "DimaKing", "DimaLord", "DimaAce", "FaceCraft",
                "FaceMirror", "FaceHunter", "TopTier", "TopDog", "TopPlayer",
                "TopPriority", "Pussycat", "Pussy Power", "DickDoodle", "Softie"
            };

            for (int i = 0; i < count; i++)
            {
                string name = names[_rand.Next(names.Length)];
                int score = _rand.Next(1000);
                DateTime nowDateTime = DateTime.Now;
                DateTime date = nowDateTime.AddDays(-_rand.Next(365) * (score / 100.0)); // Вычисление не имеющее логики :D
                TimeSpan playTime = nowDateTime.Subtract(date);

                AddRecord(new GameRecord(name, score, date, playTime));
            }
        }

        public void ShowAllRecords()
        {
            for (int i = 0; i < records.Count; i++)
            {
                Console.WriteLine($"[{i + 1}] {records[i].ToString()}");
            }
        }

        public void SortByScoreBubble()
        {
            var count = records.Count;

            for (int i = 0; i < count; i++)
            {
                for (int j = 0; j < count - 1 - i; j++)
                {
                    if (records[j].Score < records[j + 1].Score)
                    {
                        var temp = records[j];
                        records[j] = records[j + 1];
                        records[j + 1] = temp;
                    }
                }
            }
        }

        public void SortByDateBubble()
        {
            var count = records.Count;

            for (int i = 0; i < count; i++)
            {
                for (int j = 0; j < count - 1 - i; j++)
                {
                    if (records[j].Date < records[j + 1].Date)
                    {
                        var temp = records[j];
                        records[j] = records[j + 1];
                        records[j + 1] = temp;
                    }
                }
            }
        }

        public void DisplayTop(int count)
        {
            if (count <= 0)
                throw new ArgumentOutOfRangeException("Не допустимое значение: " + nameof(DisplayTop));

            SortByScoreBubble();

            int itemsToShow = Math.Min(count, records.Count);
            for (int i = 0; i < itemsToShow; i++)
            {
                Console.WriteLine($"[{i + 1}] {records[i].ToString()}");
            }

            if (records.Count < count)
                Console.WriteLine($"\n(Всего записей: {records.Count})");
        }

        public void SaveToFile()
        {
            try
            {
                string saveDirectory = "Saves";
                if (!Directory.Exists(saveDirectory))
                    Directory.CreateDirectory(saveDirectory);

                string filePath = Path.Combine(saveDirectory, "save.json");

                string json = JsonSerializer.Serialize(records, _jsonOptions);
                File.WriteAllText(filePath, json);

                Console.WriteLine("Данные сохранены в JSON");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public List<GameRecord> LoadFromFile()
        {
            try
            {
                string filePath = Path.Combine("Saves", "save.json");

                if (!File.Exists(filePath))
                    throw new FileNotFoundException($"Файл сохранения не найден: {filePath}");

                string json = File.ReadAllText(filePath);
                var leaderboard = JsonSerializer.Deserialize<List<GameRecord>>(json, _jsonOptions);

                if (leaderboard == null)
                    throw new Exception("Ошибка загрузки данных");

                return leaderboard;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
    }
}
