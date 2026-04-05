using System;

namespace Reflection
{
    public class Quest
    {
        private string _title;
        private bool _isCompleted;
        public int RewardGold { get; set; }

        public Quest(string title, int rewardGold)
        {
            _title = title;
            RewardGold = rewardGold;
            _isCompleted = false;
        }

        private void Complete()
        {
            _isCompleted = true;
            Console.WriteLine($"Квест '{_title}' завершен! Получено золота: {RewardGold}");
        }
    }
}
