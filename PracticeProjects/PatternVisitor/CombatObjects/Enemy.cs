using PatternVisitor.Visitor;

namespace PatternVisitor.CombatObjects
{
    public class Enemy : IBattleElement
    {
        public string Type { get; private set; }
        public int Health { get; private set; }
        public int Damage { get; private set; }

        public Enemy(string type, int health, int damage)
        {
            Type = type;
            Health = health;
            Damage = damage;
        }

        public void Accept(IBattleVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
