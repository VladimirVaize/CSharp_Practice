using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using SystemIO.Models;

namespace SystemIO.Managers
{
    static internal class SettingsManager
    {
        private static string _baseSettingsDirectory;
        private static string _currentSettingsDirectory;
        private static string _currentSettingsFilePath;

        public static List<GameSettings> Profiles;
        public static int CurrentProfile;

        private static JsonSerializerOptions _jsonOptions;

        static SettingsManager()
        {
            _baseSettingsDirectory = Path.Combine(
                Directory.GetParent(Environment.CurrentDirectory).FullName,
                "Settings");

            Profiles = new List<GameSettings>();

            _jsonOptions = new JsonSerializerOptions
            {
                WriteIndented = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                IncludeFields = true,
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            };

            CurrentProfile = 0;
            UpdateProfilePath(CurrentProfile);
        }

        public static void Initialize()
        {
            try
            {
                if (!Directory.Exists(_baseSettingsDirectory))
                    Directory.CreateDirectory(_baseSettingsDirectory);

                LoadAvailableProfiles();

                if (Profiles.Count == 0)
                {
                    CreateNewProfile();
                }
                else
                {
                    GameSettings loadedSettings = LoadSettingsForProfile(CurrentProfile);

                    if (loadedSettings != null)
                    {
                        Profiles[CurrentProfile] = loadedSettings;
                    }
                    else
                    {
                        throw new Exception($"Ошибка загрузки профиля {CurrentProfile}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка инициализации: {ex.Message}. Создаем профиль по умолчанию.");

                CreateDefaultProfile();
                Console.ReadKey(true);
            }
            ApplySettingsToConsole();
        }

        private static void LoadAvailableProfiles()
        {
            Profiles.Clear();

            if (Directory.Exists(_baseSettingsDirectory))
            {
                var profileDirs = Directory.GetDirectories(_baseSettingsDirectory, "Profile*");

                foreach (var dir in profileDirs)
                {
                    string dirName = Path.GetFileName(dir);
                    if (int.TryParse(dirName.Replace("Profile", ""), out int profileIndex))
                    {
                        if (Profiles.Count <= profileIndex)
                        {
                            var settings = LoadSettingsForProfile(profileIndex);
                            Profiles.Add(settings ?? CreateDefaultSettings());
                        }
                    }
                }
            }

            for (int i = 0; i < Profiles.Count; i++)
            {
                if (Profiles[i] == null)
                {
                    Profiles[i] = CreateDefaultSettings();
                }
            }
        }

        private static GameSettings LoadSettingsForProfile(int profileNumber)
        {
            string profilePath = Path.Combine(_baseSettingsDirectory, $"Profile{profileNumber}");
            string settingsFile = Path.Combine(profilePath, "settings.json");

            try
            {
                if (File.Exists(settingsFile))
                {
                    string json = File.ReadAllText(settingsFile);
                    return JsonSerializer.Deserialize<GameSettings>(json, _jsonOptions);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка загрузки профиля {profileNumber}: {ex.Message}");
            }

            return null;
        }

        private static void UpdateProfilePath(int profileNumber)
        {
            _currentSettingsDirectory = Path.Combine(_baseSettingsDirectory, $"Profile{profileNumber}");
            _currentSettingsFilePath = Path.Combine(_currentSettingsDirectory, "settings.json");
        }

        public static void SaveSettings()
        {
            try
            {
                if (!Directory.Exists(_currentSettingsDirectory))
                    Directory.CreateDirectory(_currentSettingsDirectory);

                CreateBackup();

                string json = JsonSerializer.Serialize(Profiles[CurrentProfile], _jsonOptions);
                File.WriteAllText(_currentSettingsFilePath, json);

                Console.WriteLine($"Настройки профиля {CurrentProfile} сохранены");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка сохранения: {ex.Message}");
            }
        }

        private static void CreateBackup()
        {
            if (File.Exists(_currentSettingsFilePath))
            {
                string backupPath = Path.Combine(_currentSettingsDirectory, "settings.backup.json");
                File.Copy(_currentSettingsFilePath, backupPath, true);
            }
        }

        public static void LoadSettings()
        {
            GameSettings loaded = LoadSettingsForProfile(CurrentProfile);
            if (loaded != null)
            {
                Profiles[CurrentProfile] = loaded;
                Console.WriteLine($"Профиль {CurrentProfile} загружен");
            }
            else
            {
                if (Profiles[CurrentProfile] != null)
                {
                    Console.WriteLine($"Профиль {CurrentProfile} используется из памяти");
                }
                else
                {
                    Console.WriteLine($"Не удалось загрузить профиль {CurrentProfile}");
                }
            }
            ApplySettingsToConsole();
        }

        public static void SwitchToProfile(int profileNumber)
        {
            if (profileNumber >= 0 && profileNumber < Profiles.Count)
            {
                CurrentProfile = profileNumber;
                UpdateProfilePath(profileNumber);

                LoadSettings();
            }
            else if (profileNumber == Profiles.Count)
            {
                CreateNewProfile();
            }
            else
            {
                Console.WriteLine("Некорректный номер профиля");
            }
        }

        public static void CreateNewProfile()
        {
            int newProfileNumber = Profiles.Count;
            Profiles.Add(CreateDefaultSettings());
            SwitchToProfile(newProfileNumber);

            SaveSettings();
            Console.WriteLine($"Создан новый профиль {newProfileNumber}");
        }

        private static GameSettings CreateDefaultSettings()
        {
            return new GameSettings
            {
                PlayerName = "Player",
                Volume = 50,
                IsFullscreen = false,
                BackgroundColor = ConsoleColor.Black,
                Difficulty = DifficultyLevel.Normal,
                FavoriteHeroes = new List<string> { "Warrior", "Mage" }
            };
        }

        private static void CreateDefaultProfile()
        {
            Profiles.Clear();
            Profiles.Add(CreateDefaultSettings());
            CurrentProfile = 0;
            UpdateProfilePath(0);
        }

        private static void ApplySettingsToConsole()
        {
            if (Profiles != null && Profiles.Count > CurrentProfile && Profiles[CurrentProfile] != null)
            {
                Console.BackgroundColor = Profiles[CurrentProfile].BackgroundColor;
                Console.Clear();
            }
        }
        public static void ShowProfileSelection()
        {
            Console.Clear();
            Console.WriteLine("===== ВЫБОР ПРОФИЛЯ =====");

            for (int i = 0; i < Profiles.Count; i++)
            {
                string marker = (i == CurrentProfile) ? " [ТЕКУЩИЙ]" : "";
                string playerName = Profiles[i]?.PlayerName ?? "Безымянный";
                Console.WriteLine($"{i + 1}. Профиль {i}: {playerName}{marker}");
            }

            Console.WriteLine($"{Profiles.Count + 1}. Создать новый профиль");
            Console.WriteLine("0. Назад");

            Console.Write("\nВыберите профиль: ");
            if (int.TryParse(Console.ReadLine(), out int choice))
            {
                if (choice == 0) return;

                choice -= 1;

                if (choice <= Profiles.Count)
                {
                    SwitchToProfile(choice);
                }
                else
                {
                    Console.WriteLine("Неверный выбор");
                }
            }
        }
    }
}
