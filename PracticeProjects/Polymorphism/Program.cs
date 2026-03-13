using System;
using System.Collections.Generic;

namespace Polymorphism
{
    internal class Program
    {
        static void Main(string[] args)
        {
            QuestJournal questJournal = new QuestJournal();

            KillQuest goblinHunting = new KillQuest("Охота на гоблинов", 100, "Гоблин", 3);
            KillQuest trollHunting = new KillQuest("Охота на троллей", 350, "Тролль", 2);
            CollectQuest theHerbalist = new CollectQuest("Травник", 10, "Трава", 2);
            CollectQuest mushroomPicker = new CollectQuest("Грибник", 23, "Гриб", 4);
            ExplorationQuest theDarkForest = new ExplorationQuest("Темный лес", 75, "Темный лес");
            ExplorationQuest everest = new ExplorationQuest("Эверест", 125, "Заснеженые горы");

            questJournal.AddQuest(goblinHunting);
            questJournal.AddQuest(trollHunting);
            questJournal.AddQuest(theHerbalist);
            questJournal.AddQuest(mushroomPicker);
            questJournal.AddQuest(theDarkForest);
            questJournal.AddQuest(everest);

            GameManager.Game(questJournal);
        }
    }

    public static class GameManager
    {
        public static void Game(QuestJournal questJournal)
        {
            bool isPlaying = true;
            while (isPlaying)
            {
                DrawMenu();
                if (int.TryParse(Console.ReadLine(), out int choice))
                {
                    switch (choice)
                    {
                        case 1:
                            Console.Write("Какой враг убит: ");
                            string enemyType = Console.ReadLine();
                            foreach (Quest quest in questJournal.Quests)
                            {
                                if (quest is KillQuest killQuest)
                                    killQuest.EnemyKilled(enemyType);
                            }
                            break;
                        case 2:
                            Console.Write("Какой предмет найден: ");
                            string itemName = Console.ReadLine();
                            foreach (Quest quest in questJournal.Quests)
                            {
                                if (quest is CollectQuest collectQuest)
                                    collectQuest.ItemCollected(itemName);
                            }
                            break;
                        case 3:
                            Console.Write("Какую локацию вы посетили: ");
                            string locationName = Console.ReadLine();
                            foreach (Quest quest in questJournal.Quests)
                            {
                                if (quest is ExplorationQuest explorationQuest)
                                    explorationQuest.LocationReached(locationName);
                            }
                            break;
                        case 4:
                            foreach (Quest quest in questJournal.Quests)
                                quest.ShowInfo();
                            break;
                        case 5: isPlaying = false; break;
                        default:
                            Console.WriteLine("Не корректный ввод!");
                            break;
                    }
                    questJournal.UpdateAllQuests();
                }
            }
        }

        public static void DrawMenu()
        {
            Console.WriteLine("\n===Main Menu===");
            Console.WriteLine("1. Убить врага");
            Console.WriteLine("2. Найти предмет");
            Console.WriteLine("3. Посетить локацию");
            Console.WriteLine("4. Показать список квестов");
            Console.WriteLine("5. Выйти из игры");
            Console.Write("Ваш выбор: ");
        }
    }

    public class QuestJournal
    {
        public readonly List<Quest> Quests = new List<Quest>();

        public void AddQuest(Quest quest)
        {
            Quests.Add(quest);
        }

        public void UpdateAllQuests()
        {
            foreach (Quest quest in Quests)
            {
                quest.CheckProgress();
            }
        }
    }

    public abstract class Quest
    {
        public string QuestName { get; private set; }
        public bool IsCompleted { get; private set; }
        public int RewardGold { get; private set; }

        public Quest(string questName, int rewardGold)
        {
            QuestName = questName;
            RewardGold = rewardGold;
        }

        public abstract void CheckProgress();

        protected void CompleteQuest()
        {
            if (!IsCompleted)
            {
                IsCompleted = true;
                Console.WriteLine($"Квест '{QuestName}' выполнен! Получено {RewardGold} золота!");
            }
        }

        public virtual void ShowInfo()
        {
            if (IsCompleted)
                Console.WriteLine(" (Квест завершен)");
            else Console.WriteLine();
        }
    }

    public class KillQuest : Quest
    {
        public string targetEnemyType { get; private set; }
        public int requiredKills { get; private set; }
        public int currentKills { get; private set; }

        public KillQuest(string questName, int rewardGold, string targetEnemyType, int requiredKills) : base(questName, rewardGold)
        {
            this.targetEnemyType = targetEnemyType;
            this.requiredKills = requiredKills;
            currentKills = 0;
        }

        public override void CheckProgress()
        {
            if (currentKills >= requiredKills)
                CompleteQuest();
        }

        public void EnemyKilled(string enemyType)
        {
            if (enemyType == targetEnemyType && !IsCompleted)
            {
                currentKills++;
                Console.WriteLine($"Убито {targetEnemyType}: {currentKills}/{requiredKills}");
            }
        }

        public override void ShowInfo()
        {
            Console.Write($"{QuestName}: Убить {targetEnemyType}. Прогресс: {currentKills}/{requiredKills}");
            base.ShowInfo();
        }
    }

    public class CollectQuest : Quest
    {
        public string targetItemName { get; private set; }
        public int requiredItems { get; private set; }
        public int currentItems { get; private set; }

        public CollectQuest(string questName, int rewardGold, string itemName, int requiredItems) : base(questName, rewardGold)
        {
            targetItemName = itemName;
            this.requiredItems = requiredItems;
            currentItems = 0;
        }

        public void ItemCollected(string itemName)
        {
            if (itemName == targetItemName && !IsCompleted)
            {
                currentItems++;
                Console.WriteLine($"Собрано {targetItemName}: {currentItems}/{requiredItems}");
            }
        }

        public override void CheckProgress()
        {
            if (currentItems >= requiredItems)
                CompleteQuest();
        }

        public override void ShowInfo()
        {
            Console.Write($"{QuestName}: Собрать {targetItemName}. Прогресс: {currentItems}/{requiredItems}");
            base.ShowInfo();
        }
    }

    public class ExplorationQuest : Quest
    {
        public string targetLocation { get; private set; }

        bool locationVisited = false;

        public ExplorationQuest(string questName, int rewardGold, string targetLocation) : base(questName, rewardGold)
        {
            this.targetLocation = targetLocation;
        }

        public void LocationReached(string locationName)
        {

            if (locationName == targetLocation && !IsCompleted)
            {
                locationVisited = true;
                Console.WriteLine($"Посещена локация: {targetLocation}");
            }
        }

        public override void CheckProgress()
        {
            if (locationVisited)
                CompleteQuest();
        }

        public override void ShowInfo()
        {
            string status = locationVisited ? "[Посещено]" : "[Не посещено]";
            Console.Write($"{QuestName}: Посетить локацию {targetLocation} {status}");
            base.ShowInfo();
        }
    }
}
