using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace Serialization
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                SaveSystem saveSystem = new SaveSystem();

                var xmlFormatter = new XmlSerializer(typeof(GameState));

                List<Item> starterItems = new List<Item>
                {
                    new Item("Лопата", ItemRarity.Common),
                    new Item("Кирка", ItemRarity.Common),
                    new Item("Меч", ItemRarity.Common),
                    new Item("Сливочное пиво", ItemRarity.Rare)
                };

                Player player = new Player("Bobby", 100, 50, 1200, starterItems);
                GameState gameState = new GameState(player, "Лес");

                GameState loadedGame;

                string saveDirectory = "Saves";

                bool isPlaying = true;
                while (isPlaying)
                {
                    Console.Clear();

                    Console.WriteLine("1 - Сохранить игру");
                    Console.WriteLine("2 - Загрузить игру");
                    Console.WriteLine("3 - Список сохранений");
                    Console.WriteLine("4 - Удалить сохранение");
                    Console.WriteLine("5 - Выход");
                    Console.Write("Введите значение: ");

                    if (int.TryParse(Console.ReadLine(), out int choice))
                    {
                        switch (choice)
                        {
                            case 1:
                                Console.Write("Введите имя сохранения: ");
                                string saveName = Console.ReadLine();
                                saveSystem.SaveGame(gameState, saveName, xmlFormatter);
                                Console.WriteLine("Сохранение выполнено успешно!");
                                break;
                            case 2:
                                Console.Write("Введите имя сохранения для загрузки: ");
                                string loadName = Console.ReadLine();
                                loadedGame = saveSystem.LoadGame(loadName, xmlFormatter);
                                gameState = loadedGame;
                                Console.WriteLine("Загрузка выполнена успешно!");
                                break;
                            case 3:
                                Console.WriteLine("\nСписок сохранений:");
                                foreach (string saveFile in saveSystem.GetAllSaveFiles(saveDirectory))
                                {
                                    Console.WriteLine(saveFile);
                                }
                                break;
                            case 4:
                                Console.Write("Введите имя файла: ");
                                saveSystem.DeleteSave(Console.ReadLine());
                                break;
                            case 5:
                                isPlaying = false;
                                break;
                        }
                    }

                    Console.WriteLine("\nНажмите для продолжения...");
                    Console.ReadKey(false);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }
    }

    public enum ItemRarity
    {
        Common,
        Rare,
        Epic,
        Legendary
    }

    [Serializable]
    public class Player
    {
        public string Name { get; set; }
        public int Level { get; set; }
        public int Experience { get; set; }
        public int Health { get; set; }
        public int MaxHealth { get; set; }
        public int Mana { get; set; }
        public int MaxMana { get; set; }
        public float Gold { get; set; }
        public List<Item> Inventory { get; set; } = new List<Item>();

        public Player() { } // Для XML-сериализации

        public Player(string name, int maxHealth, int maxMana, float gold, List<Item> inventory)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Имя игрока не может быть пустым");

            Name = name;

            if (maxHealth > 0)
            {
                Health = maxHealth;
                MaxHealth = maxHealth;
            }
            else throw new ArgumentException("Максимальное значение здоровья не может быть меньше одного");

            if (maxMana > 0)
            {
                Mana = maxMana;
                MaxMana = maxMana;
            }
            else throw new ArgumentException("Максимальное значение маны не может быть меньше одного");

            Gold = gold;

            Inventory = inventory;
            Level = 1;
            Experience = 0;
        }
    }

    [Serializable]
    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ItemRarity Rarity { get; set; }

        private static int _nextId = 0;

        public Item() { } // Для XML-сериализации

        public Item(string name, ItemRarity rarity)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Имя предмета не может быть пустым");

            Id = _nextId++;
            Name = name;
            Rarity = rarity;
        }
    }

    [Serializable]
    public class GameState
    {
        public Player CurrentPlayer { get; set; }
        public string CurrentLocation { get; set; }
        public DateTime SaveTime { get; set; }
        public int PlayTimeInMinutes { get; set; }
        public List<string> UnlockedLocations { get; set; } = new List<string>();

        [XmlIgnore]
        public Dictionary<string, bool> QuestProgress { get; set; } = new Dictionary<string, bool>();

        // Для XML-сериализации
        [XmlArray("Quests")]
        [XmlArrayItem("Quest")]
        public List<SerializableKeyValuePair<string, bool>> SerializableQuestProgress
        {
            get
            {
                return QuestProgress.Select(kvp => new SerializableKeyValuePair<string, bool>(kvp.Key, kvp.Value)).ToList();
            }
            set
            {
                QuestProgress = value.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
            }
        }


        [NonSerialized]
        private DateTime _startTime;

        private GameState() { } // Для XML-сериализации

        public GameState(Player player, string location)
        {
            CurrentPlayer = player;
            CurrentLocation = location;

            _startTime = DateTime.Now;
        }

        public void UpdatePlayTime()
        {
            PlayTimeInMinutes = (int)(DateTime.Now - _startTime).TotalMinutes;
        }

        public void SetSaveTime()
        {
            SaveTime = DateTime.Now;
        }

        public DateTime GetStartTime() { return _startTime; }

        public void RestoreStartTime()
        {
            _startTime = SaveTime.AddMinutes(-PlayTimeInMinutes);
        }
    }

    // Для XML-сериализации
    public class SerializableKeyValuePair<TKey, TValue>
    {
        public TKey Key { get; set; }
        public TValue Value { get; set; }

        public SerializableKeyValuePair() { }
        public SerializableKeyValuePair(TKey key, TValue value)
        {
            Key = key;
            Value = value;
        }
    }

    public class SaveSystem
    {
        public void SaveGame(GameState gameState, string saveFileName, XmlSerializer xmlFormatter)
        {
            gameState.UpdatePlayTime();
            gameState.SetSaveTime();

            string saveDirectory = "Saves";
            if (!Directory.Exists(saveDirectory))
                Directory.CreateDirectory(saveDirectory);

            string filePath = Path.Combine(saveDirectory, $"{saveFileName}.xml");

            using (var file = new FileStream(filePath, FileMode.OpenOrCreate))
            {
                xmlFormatter.Serialize(file, gameState);
                Console.WriteLine("Данные сохранены");
            }
        }

        public GameState LoadGame(string saveFileName, XmlSerializer xmlFormatter)
        {
            string saveDirectory = "Saves";
            string filePath = Path.Combine(saveDirectory, $"{saveFileName}.xml");

            if (!File.Exists(filePath))
                throw new FileNotFoundException($"Файл сохранения не найден: {filePath}");

            using (var file = new FileStream(filePath, FileMode.OpenOrCreate))
            {
                var newGameState = xmlFormatter.Deserialize(file) as GameState;

                if (newGameState == null)
                    throw new Exception("Ошибка загрузки данных");

                newGameState.RestoreStartTime();
                newGameState.UpdatePlayTime();

                Console.WriteLine("Данные загружены:");
                Console.WriteLine($"Игрок: {newGameState.CurrentPlayer.Name}");
                Console.WriteLine($"Локация: {newGameState.CurrentLocation}");
                Console.WriteLine($"Время сохранения: {newGameState.SaveTime}");
                Console.WriteLine($"Время с первого входа в игру: {newGameState.PlayTimeInMinutes} минут");
                Console.WriteLine($"Время начала игры: {newGameState.GetStartTime()}");

                return newGameState;
            }
        }

        public List<string> GetAllSaveFiles(string saveDirectory)
        {
            if (!Directory.Exists(saveDirectory))
                return new List<string>();

            return Directory.GetFiles(saveDirectory, "*.xml").Select(Path.GetFileName).ToList();
        }

        public void DeleteSave(string saveFileName)
        {
            if (string.IsNullOrWhiteSpace(saveFileName))
            {
                Console.WriteLine("Имя файла не может быть пустым");
                return;
            }

            if (!saveFileName.EndsWith(".xml"))
                saveFileName += ".xml";

            string filePath = Path.Combine("Saves", saveFileName);
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
                Console.WriteLine($"Сохранение {saveFileName} удалено");
            }
            else
                Console.WriteLine($"Файл {saveFileName} не найден");
        }
    }
}
