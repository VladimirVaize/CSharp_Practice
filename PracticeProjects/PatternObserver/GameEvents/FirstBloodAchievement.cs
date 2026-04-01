using System;

namespace PatternObserver.GameEvents
{
    public class FirstBloodAchievement : ICompletableObserver
    {
        private string _name = "Первая кровь";
        public bool IsCompleted { get; private set; } = false;
        public void OnNotify(GameEventData eventData)
        {
            if (IsCompleted) return;
            if (eventData.Type != GameEventType.MonsterKilled) return;

            IsCompleted = true;
            Console.WriteLine($"Достижение получено: {_name}");
        }
    }
}
