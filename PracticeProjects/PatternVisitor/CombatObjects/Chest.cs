using PatternVisitor.Visitor;

namespace PatternVisitor.CombatObjects
{
    public class Chest : IBattleElement
    {
        public string Contents { get; private set; }
        public int GoldAmount { get; private set; }
        public bool IsOpened { get; private set; }

        public Chest(string contents, int goldAmount, bool isOpened)
        {
            Contents = contents;
            GoldAmount = goldAmount;
            IsOpened = isOpened;
        }

        public void Accept(IBattleVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
