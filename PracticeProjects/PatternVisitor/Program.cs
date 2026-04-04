using PatternVisitor.CombatObjects;
using PatternVisitor.Visitor;
using System;
using System.Collections.Generic;

namespace PatternVisitor
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<IBattleElement> elements = new List<IBattleElement>();

            BattleLogger logger = new BattleLogger();
            DamageCalculator damageCalculator = new DamageCalculator();

            elements.Add(new Chest("Золотой браслет", 150, false));
            elements.Add(new Chest("Сладки рулет", 1200, true));
            elements.Add(new Chest("Кинжал", 25, false));
            elements.Add(new Enemy("Dragon", 300, 45));
            elements.Add(new Enemy("Goblin", 20, 5));
            elements.Add(new Enemy("Orc", 50, 17));
            elements.Add(new Player("Gandalf", 120, 5));
            elements.Add(new Player("Hagrid", 100, 3));
            elements.Add(new Player("Kazuma", 30, 10));
            elements.Add(new Trap("Огненная яма", 50, true));
            elements.Add(new Trap("Сетка", 5, true));
            elements.Add(new Trap("Яма с шипами", 70, true));

            damageCalculator.Reset();
            foreach (var element in elements)
            {
                element.Accept(logger);
                element.Accept(damageCalculator);
            }

            Console.WriteLine($"Общий потенциальный урон: {damageCalculator.TotalDamage()}");
        }
    }
}
