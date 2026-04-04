using PatternVisitor.Visitor;

namespace PatternVisitor.CombatObjects
{
    public class Player : IBattleElement
    {
        public string Name { get; private set; }
        public int Health { get; private set; }
        public int Level { get; private set; }

        public Player(string name, int health, int level)
        {
            Name = name;
            Health = health;
            Level = level;
        }

        public void Accept(IBattleVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
