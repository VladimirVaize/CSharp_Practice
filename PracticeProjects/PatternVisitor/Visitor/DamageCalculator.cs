using PatternVisitor.CombatObjects;

namespace PatternVisitor.Visitor
{
    public class DamageCalculator : IBattleVisitor
    {
        private int _totalDamage;

        public void Reset() => _totalDamage = 0;
        public int TotalDamage() => _totalDamage;

        public void Visit(Player player) => _totalDamage += player.Level * 10;
        public void Visit(Enemy enemy) => _totalDamage += enemy.Damage;
        public void Visit(Trap trap)
        {
            if (trap.IsTriggered)
                _totalDamage += trap.Damage;
        }
        public void Visit(Chest chest) { }
    }
}
