using System;

namespace PatternObserver.GameEvents
{
    public class UIPopupObserver : IObserver
    {
        public void OnNotify(GameEventData eventData)
        {
            Console.WriteLine($"[UI] Событие: {eventData.Type} | {eventData.TargetName} | кол-во: {eventData.Value}");
        }
    }
}
