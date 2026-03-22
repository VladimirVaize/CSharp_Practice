using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace TreeSort
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

            var tree = new Tree(Quests);
            var sorted = tree.Inorder();

            Quests = sorted.ToList();

            stopwatch.Stop();
            Console.WriteLine($"Время сортировки: {stopwatch.Elapsed}");
        }
    }

    class QuestNode
    {
        public Quest Data { get; private set; }
        public QuestNode Left { get; private set; }
        public QuestNode Right { get; private set; }

        public QuestNode(Quest data)
        {
            Data = data;
        }

        public void Add(Quest data)
        {
            if (data.DynamicPriority > Data.DynamicPriority)
            {
                if (Left == null)
                    Left = new QuestNode(data);
                else
                    Left.Add(data);
            }
            else
            {
                if (Right == null)
                    Right = new QuestNode(data);
                else
                    Right.Add(data);
            }
        }

        public override string ToString()
        {
            return Data.ToString();
        }
    }

    class Tree
    {
        public QuestNode Root { get; private set; }
        public int Count { get; private set; }

        public Tree() { }

        public Tree(IEnumerable<Quest> items)
        {
            foreach (var item in items)
                Add(item);
        }

        public void Add(Quest data)
        {
            if (Root == null)
            {
                Root = new QuestNode(data);
                Count = 1;
                return;
            }

            Root.Add(data);
            Count++;
        }

        public List<Quest> Inorder()
        {
            if (Root == null)
                return new List<Quest>();

            return Inorder(Root);
        }

        private List<Quest> Inorder(QuestNode node)
        {
            var list = new List<Quest>();
            if (node != null)
            {
                if (node.Left != null)
                    list.AddRange(Inorder(node.Left));

                list.Add(node.Data);

                if (node.Right != null)
                    list.AddRange(Inorder(node.Right));
            }

            return list;
        }
    }
}
