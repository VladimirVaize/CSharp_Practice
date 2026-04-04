using PatternVisitor.CombatObjects;
using System;

namespace PatternVisitor.Visitor
{
    public class BattleLogger : IBattleVisitor
    {
        public void Visit(Player player) => Console.WriteLine($"[LOG] Игрок {player.Name} (Уровень {player.Level}) имеет {player.Health} HP.");
        public void Visit(Enemy enemy) => Console.WriteLine($"[LOG] Враг {enemy.Type} (Урон {enemy.Damage}) имеет {enemy.Health} HP.");
        public void Visit(Trap trap) => Console.WriteLine($"[LOG] Ловушка \"{trap.TrapName}\" нанесла {trap.Damage} урона. Активирована: {trap.IsTriggered}");
        public void Visit(Chest chest) => Console.WriteLine($"[LOG] Сундук содержит \"{chest.Contents}\" и {chest.GoldAmount} золота. Открыт: {chest.IsOpened}");
    }
}
