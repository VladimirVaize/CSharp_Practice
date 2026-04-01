using System;

namespace PatternObserver.GameEvents
{
    public class Kill10MonstersAchievement : ICompletableObserver
    {
        private string _name = "Истребитель";
        private string _description = "убито 10 монстров";
        private int _progress;
        private const int TARGET = 10;
        public bool IsCompleted { get; private set; } = false;
        public void OnNotify(GameEventData eventData)
        {
            if (IsCompleted) return;
            if (eventData.Type != GameEventType.MonsterKilled) return;

            _progress = Math.Min(_progress + eventData.Value, TARGET);

            if (_progress >= TARGET)
            {
                IsCompleted = true;
                Console.WriteLine($"Достижение получено: {_name} ({_description})");
            }
        }
    }
}
