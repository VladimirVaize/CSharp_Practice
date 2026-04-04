using System;

namespace PatternFacade.Subsystems
{
    public class SaveSystem
    {
        public bool LoadLastSave(out string playerName, out int level, out float health)
        {
            Console.WriteLine("[SaveSystem] Загрузка последнего сохранения...");

            bool hasSave = new Random().Next(0, 3) > 0;

            if (hasSave)
            {
                playerName = "Герой";
                level = 5;
                health = 85.5f;
                return true;
            }

            playerName = "";
            level = 1;
            health = 100f;
            return false;
        }
    }
}
