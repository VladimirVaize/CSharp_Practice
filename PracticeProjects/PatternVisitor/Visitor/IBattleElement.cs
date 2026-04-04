namespace PatternVisitor.Visitor
{
    public interface IBattleElement
    {
        void Accept(IBattleVisitor visitor);
    }
}
