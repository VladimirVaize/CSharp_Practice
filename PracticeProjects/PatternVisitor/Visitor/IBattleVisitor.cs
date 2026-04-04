using PatternVisitor.CombatObjects;

namespace PatternVisitor.Visitor
{
    public interface IBattleVisitor
    {
        void Visit(Player player);
        void Visit(Enemy enemy);
        void Visit(Trap trap);
        void Visit(Chest chest);
    }
}
