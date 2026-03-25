using System;
using System.Collections.Generic;
using System.Linq;

namespace MergeSort
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ReplayManager replayManager = new ReplayManager();
            replayManager.AddRandomReplays(15);

            Console.WriteLine("=== Не отсортированные элементы ===");
            replayManager.ShowReplaysList();

            Console.WriteLine("\n=== Сортировка по дате (От новых к старым)");
            var dateComparer = Comparer<Replay>.Create((a, b) => b.Date.CompareTo(a.Date));
            replayManager.SortReplays(dateComparer);
            replayManager.ShowReplaysList();

            Console.WriteLine("\n=== Сортировка по длительности (короткие сверху)");
            var durationComparer = Comparer<Replay>.Create((a, b) => a.DurationSeconds.CompareTo(b.DurationSeconds));
            replayManager.SortReplays(durationComparer);
            replayManager.ShowReplaysList();

            Console.WriteLine("\n=== Сортировка по урону (больше урона сверху)");
            var damageComparer = Comparer<Replay>.Create((a, b) => b.DamageDealt.CompareTo(a.DamageDealt));
            replayManager.SortReplays(damageComparer);
            replayManager.ShowReplaysList();

            Console.WriteLine("\n=== Сортировка сначала победы, потом поражения");
            var victoryFirstComparer = Comparer<Replay>.Create((a, b) => b.IsVictory.CompareTo(a.IsVictory));
            replayManager.SortReplays(victoryFirstComparer);
            replayManager.ShowReplaysList();

            Console.WriteLine("\n=== Сортировка комбинированная (сначала победы, потом по урону)");
            var compositeComparer = Comparer<Replay>.Create((a, b) =>
            {
                if (a.IsVictory != b.IsVictory)
                    return b.IsVictory.CompareTo(a.IsVictory);

                return b.DamageDealt.CompareTo(a.DamageDealt);
            });
            replayManager.SortReplays(compositeComparer);
            replayManager.ShowReplaysList();
        }
    }

    public class Replay
    {
        public int Id { get; private set; }
        public DateTime Date { get; private set; }
        public string OpponentName { get; private set; }
        public float DurationSeconds { get; private set; }
        public int DamageDealt { get; private set; }
        public int MaxCombo { get; private set; }
        public bool IsVictory { get; private set; }

        private static int _nextId = 0;
        private static readonly Random _random = new Random();

        public Replay(DateTime date, string opponentName, float durationSeconds, int damageDealt, int maxCombo, bool isVictory)
        {
            Id = _nextId++;
            Date = date;
            OpponentName = opponentName ?? "Bot";
            DurationSeconds = durationSeconds;
            DamageDealt = damageDealt;
            MaxCombo = maxCombo;
            IsVictory = isVictory;
        }

        public static Replay RandomReplay()
        {
            DateTime date = DateTime.Now.AddDays(-_random.Next(30));
            string opponentName = "Bot";
            float durationSeconds = _random.Next(240);
            int damageDealt = _random.Next(5000);
            int maxCombo = _random.Next(25);
            bool isVictory = _random.NextDouble() < 0.5;

            return new Replay(date, opponentName, durationSeconds, damageDealt, maxCombo, isVictory);
        }

        public override string ToString()
        {
            string isVictory = IsVictory ? "Victory" : "Defeat";
            return $"[{Id}] [{Date:d}]: {OpponentName} ({isVictory}) - {DurationSeconds} sec, {DamageDealt} damage, combo - {MaxCombo}";
        }
    }

    public class ReplayManager
    {
        private List<Replay> _replays;
        private MergeSort _mergeSort;

        public ReplayManager()
        {
            _replays = new List<Replay>();
            _mergeSort = new MergeSort();
        }

        public void AddReplay(Replay replay) { _replays.Add(replay); }

        public void AddRandomReplays(int count)
        {
            for (int i = 0; i < count; i++)
                _replays.Add(Replay.RandomReplay());
        }

        public void SortReplays(IComparer<Replay> comparer)
        {
            _replays = _mergeSort.Sort(_replays, comparer);
        }

        public void ShowReplaysList()
        {
            if (_replays.Count == 0)
            {
                Console.WriteLine("Список реплеев пуст");
                return;
            }

            foreach (Replay replay in _replays)
                Console.WriteLine(replay.ToString());
        }
    }

    public class MergeSort
    {
        public List<T> Sort<T>(List<T> list, IComparer<T> comparer)
        {
            if (comparer == null)
                throw new ArgumentNullException(nameof(comparer));

            if (list.Count == 1)
                return list;

            var mid = list.Count / 2;

            var left = list.Take(mid).ToList();
            var right = list.Skip(mid).ToList();

            return Merge(Sort(left, comparer), Sort(right, comparer), comparer);
        }

        private List<T> Merge<T>(List<T> left, List<T> right, IComparer<T> comparer)
        {
            if (comparer == null)
                throw new ArgumentNullException(nameof(comparer));

            var length = left.Count + right.Count;
            var leftPointer = 0;
            var rightPointer = 0;

            var result = new List<T>();

            for (int i = 0; i < length; i++)
            {
                if (leftPointer < left.Count && rightPointer < right.Count)
                {
                    if (comparer.Compare(left[leftPointer], right[rightPointer]) < 0)
                    {
                        result.Add(left[leftPointer]);
                        leftPointer++;
                    }
                    else
                    {
                        result.Add(right[rightPointer]);
                        rightPointer++;
                    }
                }
                else
                {
                    if (rightPointer < right.Count)
                    {
                        result.Add(right[rightPointer]);
                        rightPointer++;
                    }
                    else
                    {
                        result.Add(left[leftPointer]);
                        leftPointer++;
                    }
                }
            }
            return result;
        }
    }
}
