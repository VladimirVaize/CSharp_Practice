using PatternFacade.Subsystems;
using System;

namespace PatternFacade.Facade
{
    public class GameFacade
    {
        private GameLogger _gameLogger;
        private LicenseValidator _licenseValidator;
        private ConfigLoader _configLoader;
        private NetworkService _networkService;
        private SaveSystem _saveSystem;
        private GameSessionTimer _gameSessionTimer;

        private bool _isGameSessionTimerStarted = false;
        private bool _isNetworkServiceStarted = false;
        private bool _isGameLoggerStarted = false;

        public GameFacade()
        {
            _gameLogger = new GameLogger();
            _licenseValidator = new LicenseValidator();
            _configLoader = new ConfigLoader();
            _networkService = new NetworkService();
            _saveSystem = new SaveSystem();
            _gameSessionTimer = new GameSessionTimer();
        }

        public GameResult StartGame()
        {
            try
            {
                _gameLogger?.Initialize();
                _isGameLoggerStarted = true;

                if (!_licenseValidator.ValidateLicense(out string licenseErrorMessage))
                {
                    _gameLogger?.Log(licenseErrorMessage);
                    Shutdown();
                    return GameResult.Failure(licenseErrorMessage);
                }
                else
                    _gameLogger?.Log("Лицензия валидна");

                if (!_configLoader.LoadConfig(out string configErrorMessage))
                {
                    _gameLogger?.Log(configErrorMessage);
                    Shutdown();
                    return GameResult.Failure(configErrorMessage);
                }
                else
                    _gameLogger?.Log($"Конфигурация загружена (версия: {_configLoader.GetConfigValue("game_version")})");

                if (!_networkService.ConnectToServer(out string networkErrorMessage))
                {
                    _gameLogger?.Log(networkErrorMessage);
                    Shutdown();
                    return GameResult.Failure(networkErrorMessage);
                }
                else
                {
                    _isNetworkServiceStarted = true;
                    _gameLogger?.Log("Подключение к серверу успешно");
                }

                bool hasSave = _saveSystem.LoadLastSave(out string playerName, out int level, out float health);

                if (!hasSave)
                    _gameLogger?.Log($"Сохранение не найдено");
                else
                    _gameLogger?.Log($"Загружено сохранение: {playerName} (уровень {level})");

                _gameSessionTimer?.StartTimer();
                _isGameSessionTimerStarted = true;

                _gameLogger.Log("Игра успешно запущена!");

                return GameResult.Success(hasSave ? new SaveData { PlayerName = playerName, Level = level, Health = health } : null);
            }
            catch (Exception ex)
            {
                _gameLogger?.Log($"Критическая ошибка: {ex.Message}");
                return GameResult.Failure(ex.Message);
            }
        }

        public void Shutdown()
        {
            if (_isGameSessionTimerStarted)
            {
                _gameSessionTimer?.StopTimer();
                _isGameSessionTimerStarted = false;
            }
            if (_isNetworkServiceStarted)
            {
                _networkService.Disconnect();
                _isNetworkServiceStarted = false;
            }
            if (_isGameLoggerStarted)
            {
                _gameLogger.Shutdown();
                _isGameLoggerStarted = false;
            }
        }
    }
}
