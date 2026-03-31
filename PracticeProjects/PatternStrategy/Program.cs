using PatternStrategy.Strategies;
using System;

namespace PatternStrategy
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IDamageStrategy normalDamageStrategy = new NormalDamageStrategy();
            IDamageStrategy criticalDamageStrategy = new CriticalDamageStrategy();
            IDamageStrategy glancingDamageStrategy = new GlancingDamageStrategy();
            IDamageStrategy fireDamageStrategy = new FireDamageStrategy();

            Fighter fighter = new Fighter("Григорий", 10, normalDamageStrategy);

            Console.WriteLine("=== БОЙ НАЧИНАЕТСЯ ===");
            Console.WriteLine($"\nРыцарь {fighter.Name} (Обычный удар)");
            fighter.Attack();
            fighter.Attack();

            Console.WriteLine($"\nРыцарь {fighter.Name} активирует КРИТИЧЕСКИЙ УДАР!");
            fighter.SetStrategy(criticalDamageStrategy);
            fighter.Attack();
            fighter.Attack();

            Console.WriteLine($"\nРыцарь {fighter.Name} ослаблен (Скользящий удар)");
            fighter.SetStrategy(glancingDamageStrategy);
            fighter.Attack();
            fighter.Attack();

            Console.WriteLine($"\nРыцарь {fighter.Name} активирует ОГНЕННЫЙ УДАР!");
            fighter.SetStrategy(fireDamageStrategy);
            fighter.Attack();
            fighter.Attack();
        }
    }
}
