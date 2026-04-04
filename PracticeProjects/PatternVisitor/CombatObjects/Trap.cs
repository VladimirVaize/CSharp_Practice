using PatternVisitor.Visitor;

namespace PatternVisitor.CombatObjects
{
    public class Trap : IBattleElement
    {
        public string TrapName { get; private set; }
        public int Damage { get; private set; }
        public bool IsTriggered { get; private set; }

        public Trap(string name, int damage, bool isTriggered)
        {
            TrapName = name;
            Damage = damage;
            IsTriggered = isTriggered;
        }

        public void Accept(IBattleVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
