using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace HeapSort
{
    internal class Program
    {
        static void Main(string[] args)
        {
            QuestJournal journal = new QuestJournal();

            try
            {
                Stopwatch stopwatch = new Stopwatch();

                stopwatch.Start();

                journal.AddQuest(new Quest("Спасти деревню", QuestStatus.Active, 8, 3, 500));
                journal.AddQuest(new Quest("Найти лекарство", QuestStatus.Active, 5, 1, 200));
                journal.AddQuest(new Quest("Победить дракона", QuestStatus.Active, 10, 5, 1000));
                journal.AddQuest(new Quest("Собрать грибы", QuestStatus.Completed, 2, 1, 50));
                journal.AddQuest(new Quest("Доставить письмо", QuestStatus.Failed, 3, 2, 80));

                Console.WriteLine("=== Все квесты (отсортировано по приоритету) ===");
                journal.DisplayQuests();

                Console.WriteLine("\n=== Прошел игровой день (обновляем срочность) ===");
                journal.UpdateQuestUrgency(3, 2);
                journal.DisplayQuests();

                Console.WriteLine("\n=== Удаляем выполненный квест ===");
                journal.RemoveQuest(4);
                journal.DisplayQuests();

                stopwatch.Stop();

                Console.WriteLine($"\nОбщее время выполнения: {stopwatch.Elapsed}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }

    public enum QuestStatus
    {
        Active,
        Completed,
        Failed
    }

    public class Quest
    {
        public int Id { get; private set; }
        public string Title { get; private set; }
        public QuestStatus Status { get; private set; }
        public int BasePriority { get; private set; }
        public int Urgency { get; private set; }
        public int Reward { get; private set; }
        public int DynamicPriority { get; private set; }

        private static int _nextId = 1;

        public Quest(string title, QuestStatus status, int basePriority, int urgency, int reward)
        {
            Id = _nextId++;
            Title = title;
            Status = status;
            BasePriority = basePriority;
            Urgency = urgency;
            Reward = reward;

            int urgencyValue = Urgency > 0 ? Urgency : 1;
            DynamicPriority = BasePriority + (100 / urgencyValue) + (Reward / 100);
        }

        public void SetUrgency(int newUrgency)
        {
            Urgency = newUrgency;
            DynamicPriority = BasePriority + (100 / Urgency) + (Reward / 100);
        }

        public override string ToString()
        {
            return $"[{Id}] [{Status}] {Title} - Приоритет: Базовый - {BasePriority}. Динамический - {DynamicPriority}. Срочность - {Urgency}. Награда - {Reward}";
        }
    }

    public class QuestJournal
    {
        public List<Quest> Quests { get; private set; }

        public QuestJournal()
        {
            Quests = new List<Quest>();
        }

        public void AddQuest(Quest quest)
        {
            Quests.Add(quest);
            Sort();
        }

        public void RemoveQuest(int id)
        {
            var removedQuest = Quests.FirstOrDefault(item => item.Id == id);

            if (removedQuest != null)
                Quests.Remove(removedQuest);
            else
                Console.WriteLine($"Элемент с Id {id} не найден.");
        }

        public void UpdateQuestUrgency(int id, int newUrgency)
        {
            var quest = Quests.Find(item => item.Id == id);
            if (quest != null)
            {
                string title = quest.Title;
                quest.SetUrgency(newUrgency);
                Sort();
                Console.WriteLine($"Обновлен квест: {title}");
            }
            else
                Console.WriteLine($"Квест с Id {id} не найден.");
        }

        public void DisplayQuests()
        {
            if (Quests.Count == 0)
            {
                Console.WriteLine("Нет доступных квестов.");
                return;
            }

            foreach (Quest quest in Quests)
            {
                Console.WriteLine(quest.ToString());
            }
        }

        public void Sort()
        {
            if (Quests.Count <= 1)
            {
                Console.WriteLine("Сортировка не требуется (0 или 1 элемент).");
                return;
            }

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            var heap = new Heap(Quests);
            var sorted = heap.Order();

            Quests = sorted.ToList();

            stopwatch.Stop();
            Console.WriteLine($"Время сортировки: {stopwatch.Elapsed}");
        }
    }

    class Heap
    {
        private List<Quest> items = new List<Quest>();
        public int Count => items.Count;

        public Quest Peek()
        {
            if (Count > 0)
                return items[0];
            else
                return null;
        }

        public Heap() { }

        public Heap(List<Quest> items)
        {
            this.items.AddRange(items);
            for (int i = Count; i >= 0; i--)
            {
                Sort(i);
            }
        }

        public void Add(Quest item)
        {
            items.Add(item);

            var currentIndex = Count - 1;
            var parentIndex = GetParentIndex(currentIndex);

            while (currentIndex > 0 && items[parentIndex].DynamicPriority < items[currentIndex].DynamicPriority)
            {
                Swap(currentIndex, parentIndex);

                currentIndex = parentIndex;
                parentIndex = GetParentIndex(currentIndex);
            }
        }

        public Quest GetMax()
        {
            var result = items[0];
            items[0] = items[Count - 1];
            items.RemoveAt(Count - 1);
            Sort(0);
            return result;
        }

        private void Sort(int curentIndex)
        {
            int maxIndex = curentIndex;
            int leftIndex;
            int rightIndex;

            while (curentIndex < Count)
            {
                leftIndex = 2 * curentIndex + 1;
                rightIndex = 2 * curentIndex + 2;

                if (leftIndex < Count && items[leftIndex].DynamicPriority > items[maxIndex].DynamicPriority)
                    maxIndex = leftIndex;

                if (rightIndex < Count && items[rightIndex].DynamicPriority > items[maxIndex].DynamicPriority)
                    maxIndex = rightIndex;

                if (maxIndex == curentIndex)
                    break;

                Swap(curentIndex, maxIndex);
                curentIndex = maxIndex;
            }
        }

        private void Swap(int currentIndex, int parentIndex)
        {
            Quest temp = items[currentIndex];
            items[currentIndex] = items[parentIndex];
            items[parentIndex] = temp;
        }

        private int GetParentIndex(int currentIndex)
        {
            return (currentIndex - 1) / 2;
        }

        public List<Quest> Order()
        {
            var result = new List<Quest>();

            while (Count > 0)
            {
                result.Add(GetMax());
            }
            return result;
        }
    }
}
