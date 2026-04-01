using System;

namespace PatternObserver.GameEvents
{
    public class Collect3ItemsAchievement : ICompletableObserver
    {
        private string _name = "Коллекционер";
        private string _description = "собрано 3 предмета";
        private int _progress;
        public bool IsCompleted { get; private set; } = false;
        private const int TARGET = 3;
        public void OnNotify(GameEventData eventData)
        {
            if (IsCompleted) return;
            if (eventData.Type != GameEventType.ItemCollected) return;

            _progress = Math.Min(_progress + eventData.Value, TARGET);

            if (_progress >= TARGET)
            {
                IsCompleted = true;
                Console.WriteLine($"Достижение получено: {_name} ({_description})");
            }
        }
    }
}
