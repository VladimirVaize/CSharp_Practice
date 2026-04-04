using System;
using System.Collections.Generic;

namespace PatternFacade.Subsystems
{
    public class ConfigLoader
    {
        private Dictionary<string, string> _config = new Dictionary<string, string>();

        public bool LoadConfig(out string errorMessage)
        {
            errorMessage = "";
            Console.WriteLine("[Config] Загрузка конфигурации...");

            try
            {
                _config["game_version"] = "1.2.3";
                _config["language"] = "ru";
                _config["max_players"] = "100";
                return true;
            }
            catch (Exception ex)
            {
                errorMessage = $"Ошибка загрузки конфига: {ex.Message}";
                return false;
            }
        }

        public string GetConfigValue(string key) => _config.TryGetValue(key, out string value) ? value : "unknown";
    }
}
