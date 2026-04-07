using System;
using System.IO;
using System.Text.Json;
using System.Threading;

namespace EntryPoint.Server
{
    public class GameServerEntryPoint : IDisposable
    {
        private ServerConfig _config;
        private bool _isRunning;
        private bool _disposed;

        public ServerConfig ParseArguments(string[] args)
        {
            var config = new ServerConfig();

            config.Mode = "dev";
            config.Port = 7777;
            config.Debug = true;

            try
            {
                for (int i = 0; i < args.Length; i++)
                {
                    switch (args[i].ToLower())
                    {
                        case "--mode":
                            if (i + 1 < args.Length)
                            {
                                config.Mode = args[i + 1].ToLower();
                                i++;
                            }
                            break;

                        case "--port":
                            if (i + 1 < args.Length && int.TryParse(args[i + 1], out int port))
                            {
                                config.Port = port;
                                i++;
                            }
                            else if (i + 1 < args.Length)
                            {
                                Logger.Log($"Ошибка парсинга порта: {args[i + 1]}");
                            }
                            break;

                        case "--debug":
                            if (i + 1 < args.Length && bool.TryParse(args[i + 1], out bool debug))
                            {
                                config.Debug = debug;
                                i++;
                            }
                            break;

                        case "--config":
                            if (i + 1 < args.Length)
                            {
                                config.ConfigPath = args[i + 1];
                                i++;
                            }
                            break;

                        case "--help":
                            ShowHelp();
                            return null;

                        default:
                            Logger.Log($"Неизвестный аргумент: {args[i]}");
                            break;
                    }
                }

                if (args.Length == 0 || !Array.Exists(args, a => a.ToLower() == "--mode"))
                {
                    Logger.Log("Режим не указан. Используется режим разработки (dev)");
                }

                bool portSpecified = Array.Exists(args, a => a.ToLower() == "--port");
                if (!portSpecified)
                {
                    switch (config.Mode)
                    {
                        case "dev":
                            config.Port = 7777;
                            break;
                        case "test":
                            config.Port = 8888;
                            break;
                        case "prod":
                            config.Port = 8080;
                            break;
                    }
                }

                bool debugSpecified = Array.Exists(args, a => a.ToLower() == "--debug");
                if (!debugSpecified)
                {
                    config.Debug = config.Mode == "dev";
                }
            }
            catch (Exception ex)
            {
                Logger.Log($"Ошибка при парсинге аргументов: {ex.Message}");
            }

            return config;
        }

        private void ShowHelp()
        {
            Console.WriteLine("Использование: GameServer.exe [опции]\n");
            Console.WriteLine("Опции:");
            Console.WriteLine("  --mode <dev|test|prod>    Режим запуска (по умолчанию: dev)");
            Console.WriteLine("  --port <число>           Порт сервера");
            Console.WriteLine("  --debug <true|false>     Режим отладки");
            Console.WriteLine("  --config <путь>          Путь к JSON конфигурации");
            Console.WriteLine("  --help                   Показать эту справку");
            Console.WriteLine("\nПорты по умолчанию:");
            Console.WriteLine("  dev:  7777");
            Console.WriteLine("  test: 8888");
            Console.WriteLine("  prod: 8080");
        }

        public bool InitializeServices(ServerConfig config)
        {
            if (config == null)
            {
                Logger.Log("Ошибка: конфигурация не загружена");
                return false;
            }

            _config = config;

            ServiceLocator.Register(config);

            Logger.Log($"=== ЗАПУСК СЕРВЕРА ===");
            Logger.Log($"Режим: {config.Mode.ToUpper()}");
            Logger.Log($"Порт: {config.Port}");
            Logger.Log($"Режим отладки: {(config.Debug ? "ВКЛ" : "ВЫКЛ")}");

            if (config.Mode == "dev")
            {
                Logger.Log("ВНИМАНИЕ: Режим разработки. Логирование включено.");
            }

            if (config.Debug)
            {
                Logger.OnLog += DebugLogHandler;
                Logger.Log("Отладочное логирование активировано");
            }

            if (!string.IsNullOrEmpty(config.ConfigPath))
            {
                LoadJsonConfig(config.ConfigPath);
            }

            Logger.Log("Инициализация сетевых сервисов...");
            Thread.Sleep(1000);

            Logger.Log("Загрузка игровых модулей...");
            Thread.Sleep(500);

            Logger.Log("Сервер успешно инициализирован");

            return true;
        }

        private void LoadJsonConfig(string configPath)
        {
            try
            {
                if (File.Exists(configPath))
                {
                    string jsonContent = File.ReadAllText(configPath);

                    ServerConfig jsonConfig = JsonSerializer.Deserialize<ServerConfig>(jsonContent);

                    if (jsonConfig != null)
                    {
                        if (_config.MaxPlayers == 100)
                            _config.MaxPlayers = jsonConfig.MaxPlayers;

                        if (_config.ServerName == "Default Server")
                            _config.ServerName = jsonConfig.ServerName;

                        if (_config.TickRate == 20.0f)
                            _config.TickRate = jsonConfig.TickRate;

                        Logger.Log($"Загружена конфигурация из {configPath}");
                        Logger.Log($"   - Имя сервера: {_config.ServerName}");
                        Logger.Log($"   - Макс. игроков: {_config.MaxPlayers}");
                        Logger.Log($"   - TickRate: {_config.TickRate}");
                    }
                }
                else
                {
                    Logger.Log($"Файл конфигурации не найден: {configPath}. Используются значения по умолчанию.");
                }
            }
            catch (FileNotFoundException ex)
            {
                Logger.Log($"JSON файл не найден: {ex.Message}. Используются значения по умолчанию.");
            }
            catch (JsonException ex)
            {
                Logger.Log($"Ошибка парсинга JSON: {ex.Message}. Используются значения по умолчанию.");
            }
            catch (IOException ex)
            {
                Logger.Log($"Ошибка чтения файла: {ex.Message}. Используются значения по умолчанию.");
            }
            catch (Exception ex)
            {
                Logger.Log($"Непредвиденная ошибка при загрузке конфигурации: {ex.Message}");
            }
        }

        private void DebugLogHandler(string message)
        {
            if (message.Contains("Обработан запрос"))
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("[DEBUG] ");
                Console.ResetColor();
            }
        }

        public void RunServerLoop()
        {
            if (_config == null)
            {
                Logger.Log("Ошибка: сервер не инициализирован");
                return;
            }

            _isRunning = true;
            int requestCount = 0;

            Console.WriteLine();
            Logger.Log($"СЕРВЕР ЗАПУЩЕН на порту {_config.Port}");
            Logger.Log("Нажмите ESC для остановки или любую другую клавишу для имитации запроса клиента");
            Console.WriteLine();

            try
            {
                while (_isRunning)
                {
                    if (Console.KeyAvailable)
                    {
                        var key = Console.ReadKey(true);

                        if (key.Key == ConsoleKey.Escape)
                        {
                            Logger.Log("Получена команда остановки...");
                            break;
                        }
                        else
                        {
                            requestCount++;
                            HandleClientRequest(requestCount);
                        }
                    }
                    else
                    {
                        Thread.Sleep(100);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Log($"Критическая ошибка в основном цикле: {ex.Message}");
            }
            finally
            {
                Shutdown();
            }
        }

        private void HandleClientRequest(int requestId)
        {
            try
            {
                Logger.Log($"Обработан запрос #{requestId}");

                if (_config.Debug)
                {
                    Console.WriteLine($"   └─ Порт: {_config.Port}, Режим: {_config.Mode}");
                }

                Thread.Sleep(50);
            }
            catch (Exception ex)
            {
                Logger.Log($"Ошибка при обработке запроса #{requestId}: {ex.Message}");
            }
        }

        private void Shutdown()
        {
            _isRunning = false;

            Logger.Log("Инициировано завершение работы сервера...");

            if (_config?.Debug == true)
            {
                Logger.OnLog -= DebugLogHandler;
            }

            Logger.Log("Закрытие сетевых соединений...");
            Thread.Sleep(300);

            Logger.Log("Сохранение состояния...");
            Thread.Sleep(200);

            Logger.Log("Сервер остановлен. Ресурсы освобождены.");
        }

        public void Dispose()
        {
            if (!_disposed)
            {
                if (_isRunning)
                {
                    Shutdown();
                }

                if (ServiceLocator.Has<ServerConfig>())
                {
                    // В реальном приложении здесь была бы очистка ресурсов
                }

                _disposed = true;
                GC.SuppressFinalize(this);
            }
        }

        ~GameServerEntryPoint()
        {
            Dispose();
        }
    }
}
