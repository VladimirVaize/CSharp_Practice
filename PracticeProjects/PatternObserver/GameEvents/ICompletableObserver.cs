namespace PatternObserver.GameEvents
{
    public interface ICompletableObserver : IObserver
    {
        bool IsCompleted { get; }
    }
}
