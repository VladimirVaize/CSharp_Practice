namespace PatternObserver.GameEvents
{
    public interface IObserver
    {
        void OnNotify(GameEventData eventData);
    }
}
