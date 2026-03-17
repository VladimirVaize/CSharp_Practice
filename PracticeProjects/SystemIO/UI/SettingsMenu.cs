using System;
using SystemIO.Managers;
using SystemIO.Models;

namespace SystemIO.UI
{
    internal class SettingsMenu
    {
        public static void ShowSettingsMenu()
        {
            bool isSettingsMenu = true;
            while (isSettingsMenu)
            {
                Console.Clear();
                Console.WriteLine("===== НАСТРОЙКИ ИГРЫ =====");

                var settings = SettingsManager.Profiles[SettingsManager.CurrentProfile];

                Console.WriteLine($"1. Имя игрока: {settings.PlayerName}");
                Console.WriteLine($"2. Громкость: {settings.Volume}");
                Console.WriteLine($"3. Полноэкранный режим: {(settings.IsFullscreen ? "Да" : "Нет")}");
                Console.WriteLine($"4. Цвет фона: {settings.BackgroundColor}");
                Console.WriteLine($"5. Сложность: {settings.Difficulty}");
                Console.WriteLine($"6. Любимые герои: {string.Join(", ", settings.FavoriteHeroes)}");
                Console.WriteLine("0. Выход в главное меню");
                Console.WriteLine("Любое другое число - Выбрать другой профиль");

                Console.Write("\nВыберите пункт для изменения: ");
                if (int.TryParse(Console.ReadLine(), out int choiceSettings))
                {
                    switch (choiceSettings)
                    {
                        case 0:
                            isSettingsMenu = false; break;
                        case 1:
                            ChangePlayerName();
                            break;
                        case 2:
                            ChangeVolume();
                            break;
                        case 3:
                            ToggleFullscreen();
                            break;
                        case 4:
                            ChangeBackgroundColor();
                            break;
                        case 5:
                            ChangeDifficulty();
                            break;
                        case 6:
                            ChangeFavoriteHeroes();
                            break;
                        default:
                            SettingsManager.ShowProfileSelection();
                            break;
                    }

                    if (choiceSettings != 0)
                    {
                        AskToSave();
                    }
                }
                else
                {
                    Console.WriteLine("Ошибка: введите число!");
                }
            }
        }

        private static void ChangePlayerName()
        {
            Console.Write("\nВведите новое имя игрока: ");

            string newName = Console.ReadLine();

            if (newName != "" && newName.Length <= 20)
            {
                SettingsManager.Profiles[SettingsManager.CurrentProfile].PlayerName = newName;
                Console.WriteLine("Имя изменено!");
                return;
            }

            Console.WriteLine("Имя не должно быть пустым и длиной не более 20 символов!");
        }

        private static void ChangeVolume()
        {
            Console.Write("\nВведите громкость (0-100): ");

            if (int.TryParse(Console.ReadLine(), out int newVolume))
            {
                if (newVolume >= 0 && newVolume <= 100)
                {
                    SettingsManager.Profiles[SettingsManager.CurrentProfile].Volume = newVolume;
                    Console.WriteLine("Громкость изменена!");
                }
                else
                    Console.WriteLine("Ошибка: громкость должна быть от 0 до 100!");
            }
            else
            {
                Console.WriteLine("Ошибка: введите число!");
            }
        }

        private static void ToggleFullscreen()
        {
            SettingsManager.Profiles[SettingsManager.CurrentProfile].IsFullscreen = !SettingsManager.Profiles[SettingsManager.CurrentProfile].IsFullscreen;
            Console.WriteLine($"Полноэкранный режим: {(SettingsManager.Profiles[SettingsManager.CurrentProfile].IsFullscreen ? "Да" : "Нет")}");
        }

        private static void ChangeBackgroundColor()
        {
            Console.WriteLine("\nДоступные цвета:");

            var colors = Enum.GetValues(typeof(ConsoleColor));
            int index = 1;

            foreach (ConsoleColor color in colors)
            {
                Console.WriteLine($"{index}. {color}");
                index++;
            }

            Console.Write("Выберите цвет (введите номер): ");

            if (int.TryParse(Console.ReadLine(), out int colorIndex) && colorIndex > 0 && colorIndex <= colors.Length)
            {
                ConsoleColor newColor = (ConsoleColor)colors.GetValue(colorIndex - 1);
                SettingsManager.Profiles[SettingsManager.CurrentProfile].BackgroundColor = newColor;

                Console.BackgroundColor = newColor;
                Console.Clear();

                Console.WriteLine("Цвет фона изменен!");
            }
            else
            {
                Console.WriteLine("Ошибка: неверный выбор!");
            }
        }

        private static void ChangeDifficulty()
        {
            Console.WriteLine("\nДоступные уровни сложности:");

            var difficulties = Enum.GetValues(typeof(DifficultyLevel));
            int index = 1;

            foreach (DifficultyLevel diff in difficulties)
            {
                Console.WriteLine($"{index}. {diff}");
                index++;
            }

            Console.Write("Выберите сложность (введите номер): ");

            if (int.TryParse(Console.ReadLine(), out int diffIndex) && diffIndex > 0 && diffIndex <= difficulties.Length)
            {
                DifficultyLevel newDiff = (DifficultyLevel)difficulties.GetValue(diffIndex - 1);
                SettingsManager.Profiles[SettingsManager.CurrentProfile].Difficulty = newDiff;
                Console.WriteLine("Сложность изменена!");
            }
            else
            {
                Console.WriteLine("Ошибка: неверный выбор!");
            }
        }

        private static void ChangeFavoriteHeroes()
        {
            var heroes = SettingsManager.Profiles[SettingsManager.CurrentProfile].FavoriteHeroes;

            Console.WriteLine($"\nТекущие любимые герои: {string.Join(", ", heroes)}");
            Console.WriteLine("1. Добавить героя");
            Console.WriteLine("2. Удалить героя");
            Console.WriteLine("3. Очистить список");
            Console.Write("Выберите действие: ");

            if (int.TryParse(Console.ReadLine(), out int choice))
            {
                switch (choice)
                {
                    case 1:
                        Console.Write("Введите имя героя: ");
                        string newHero = Console.ReadLine();
                        if (!string.IsNullOrWhiteSpace(newHero))
                        {
                            heroes.Add(newHero);
                            Console.WriteLine($"Герой {newHero} добавлен!");
                        }
                        break;
                    case 2:
                        Console.Write("Введите имя героя для удаления: ");
                        string heroToRemove = Console.ReadLine();
                        if (heroes.Remove(heroToRemove))
                        {
                            Console.WriteLine($"Герой {heroToRemove} удален!");
                        }
                        else
                        {
                            Console.WriteLine($"Герой {heroToRemove} не найден!");
                        }
                        break;
                    case 3:
                        heroes.Clear();
                        Console.WriteLine("Список героев очищен!");
                        break;
                    default:
                        Console.WriteLine("Неверный выбор!");
                        break;
                }
            }
        }

        private static void AskToSave()
        {
            Console.Write("\nСохранить изменения? (Y/N): ");
            string answer = Console.ReadLine()?.ToUpper();

            if (answer == "Y")
            {
                try
                {
                    SettingsManager.SaveSettings();
                    Console.WriteLine("Настройки сохранены!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка при сохранении: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("Изменения отменены. Загружаем последние сохраненные настройки...");
                SettingsManager.LoadSettings();
            }
            Console.WriteLine("Нажмите любую клавишу для продолжения...");
            Console.ReadKey();
        }
    }
}
