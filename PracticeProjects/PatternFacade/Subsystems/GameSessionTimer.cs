using System;

namespace PatternFacade.Subsystems
{
    public class GameSessionTimer
    {
        private DateTime _startTime;

        public void StartTimer()
        {
            _startTime = DateTime.Now;
            Console.WriteLine($"[Timer] Сессия началась в {_startTime:HH:mm:ss}");
        }

        public void StopTimer()
        {
            var duration = DateTime.Now - _startTime;
            Console.WriteLine($"[Timer] Сессия длилась {duration.TotalSeconds:F1} секунд");
        }
    }
}
