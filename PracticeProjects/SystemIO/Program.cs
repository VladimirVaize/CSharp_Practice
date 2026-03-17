using SystemIO.Managers;
using SystemIO.UI;

namespace SystemIO
{
    internal class Program
    {
        static void Main(string[] args)
        {
            SettingsManager.Initialize();
            SettingsManager.ShowProfileSelection();
            SettingsMenu.ShowSettingsMenu();
        }
    }
}
