namespace Reflection
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var player = new Player("Геральт", 100);
            var quest = new Quest("Убить грифона", 500);

            GameDebugger.DumpObject(player, "Игрок");
            GameDebugger.DumpObject(quest, "Квест");

            GameDebugger.InvokeMethod(player, "TakeDamage", 30);
            GameDebugger.InvokeMethod(player, "Heal", 10);
            GameDebugger.InvokeMethod(player, "GetHealthPercent");
            GameDebugger.InvokeMethod(quest, "Complete");

            GameDebugger.InvokeMethod(player, "FlyToMoon");
        }
    }
}
