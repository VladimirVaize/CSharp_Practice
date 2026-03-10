using System;
using System.Collections.Generic;

namespace DelegatesAndEvents
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Player player = new Player("Герой");
            Inventory inventory = new Inventory();
            AchievementSystem achievementSystem = new AchievementSystem();

            Monster dragon = new Monster("Dragon", 200, 500, 1000);
            Monster goblin1 = new Monster("Гоблин", 30, 20, 50);
            Monster goblin2 = new Monster("Гоблин", 30, 20, 50);
            Monster goblin3 = new Monster("Гоблин", 30, 20, 50);
            Monster goblin4 = new Monster("Гоблин", 30, 20, 50);

            achievementSystem.SubscribeToPlayer(player);
            achievementSystem.SubscribeToInventory(inventory);

            achievementSystem.SubscribeToMonster(dragon);
            achievementSystem.SubscribeToMonster(goblin1);
            achievementSystem.SubscribeToMonster(goblin2);
            achievementSystem.SubscribeToMonster(goblin3);
            achievementSystem.SubscribeToMonster(goblin4);

            Console.WriteLine("=== Начало игры ===");

            inventory.AddItem(new Item("Меч", ItemType.Weapon, 1, 10));
            inventory.AddItem(new Item("Зелье", ItemType.Potion, 1, 5));

            player.GainExperience(50);
            player.GainExperience(150);

            player.AddGold(500);
            player.AddGold(600);

            goblin1.TakeDamage(30);
            goblin2.TakeDamage(30);
            goblin3.TakeDamage(30);
            goblin4.TakeDamage(30);
            dragon.TakeDamage(200);

            inventory.AddItem(new Item("Щит", ItemType.Armor, 1, 20));
            inventory.AddItem(new Item("Копьё", ItemType.Weapon, 1, 10));
            inventory.AddItem(new Item("Хлеб", ItemType.Food, 1, 2));

            player.TakeDamage(30);
            player.Heal(10);
            player.TakeDamage(70);

            Monster tempMonster = new Monster("Временный", 10, 5, 5);
            achievementSystem.SubscribeToMonster(tempMonster);
            tempMonster.TakeDamage(10);
            achievementSystem.UnsubscribeMonster(tempMonster);
            tempMonster = null;
        }
    }

    public enum ItemType
    {
        Weapon,
        Potion,
        Armor,
        Food
    }

    public class AchievementSystem
    {
        private int _totalDamageTaken = 0;
        private int _monstersKilled = 0;
        private int _itemsAdded = 0;
        private int _experienceAdded = 0;
        private HashSet<string> _unlockedAchievements = new HashSet<string>();

        private Dictionary<Monster, Action<Monster>> _monsterHandlers = new Dictionary<Monster, Action<Monster>>();

        public void SubscribeToPlayer(Player player)
        {
            player.OnExperienceGained += (gainedExperience) =>
            {
                _experienceAdded += gainedExperience;
                CheckExperienceAchievements(gainedExperience);
            };
            player.OnLeveledUp += (newLevel) => CheckLevelUpAchievements(newLevel);
            player.OnGoldChanged += (totalGold) => CheckGoldAchievements(totalGold);

            player.OnDamageTaken += (damage) =>
            {
                _totalDamageTaken += damage;
                CheckDamageAchievements(_totalDamageTaken);

                if (player.Health <= player.MaxHealth * 0.1 && damage > 0)
                {
                    CheckLowHealthDamageAchievements();
                }
            };
        }

        public void SubscribeToInventory(Inventory inventory)
        {
            inventory.OnItemAdded += (item) => CheckItemAchievements(item);
        }

        public void SubscribeToMonster(Monster monster)
        {
            Action<Monster> handler = (defeatedMonster) =>
            {
                _monstersKilled++;
                CheckMonsterAchievements(defeatedMonster, _monstersKilled);
            };

            _monsterHandlers[monster] = handler;
            monster.OnMonsterDefeated += handler;
        }

        public void UnsubscribeMonster(Monster monster)
        {
            if (_monsterHandlers.TryGetValue(monster, out Action<Monster> handler))
            {
                monster.OnMonsterDefeated -= handler;
                _monsterHandlers.Remove(monster);
            }
        }

        private void CheckExperienceAchievements(int gainedExperience)
        {
            if (_experienceAdded >= 100 && !_unlockedAchievements.Contains("Умный"))
            {
                UnlockAchievement("Умный", "Получите суммарно 100 опыта");
            }

            if (gainedExperience >= 150 && !_unlockedAchievements.Contains("Ученый"))
            {
                UnlockAchievement("Ученый", "Получите 150 опыта за раз");
            }
        }

        private void CheckLevelUpAchievements(int newLevel)
        {
            if (newLevel >= 2 && !_unlockedAchievements.Contains("Новичок"))
            {
                UnlockAchievement("Новичок", "Достигните 2-го уровня");
            }

            if (newLevel >= 5 && !_unlockedAchievements.Contains("Опытный игрок"))
            {
                UnlockAchievement("Опытный игрок", "Достигните 5-го уровня");
            }
        }

        private void CheckGoldAchievements(int totalGold)
        {
            if (totalGold >= 1000 && !_unlockedAchievements.Contains("Богач"))
            {
                UnlockAchievement("Богач", "Накопите 1000 золота");
            }
        }

        private void CheckMonsterAchievements(Monster monster, int totalKilled)
        {
            if (totalKilled >= 5 && !_unlockedAchievements.Contains("Охотник"))
            {
                UnlockAchievement("Охотник", "Убейте 5 монстров");
            }

            if (monster.Name == "Dragon" && !_unlockedAchievements.Contains("Охотник на драконов"))
            {
                UnlockAchievement("Охотник на драконов", "Убейте дракона");
            }
        }

        private void CheckDamageAchievements(int totalDamage)
        {
            if (totalDamage >= 100 && !_unlockedAchievements.Contains("Берсерк"))
            {
                UnlockAchievement("Берсерк", "Получите 100 единиц урона суммарно");
            }
        }

        private void CheckLowHealthDamageAchievements()
        {
            if (!_unlockedAchievements.Contains("Живучий"))
            {
                UnlockAchievement("Живучий", "Получите урон, но выживите с HP меньше 10%");
            }
        }

        private void CheckItemAchievements(Item item)
        {
            _itemsAdded++;
            if (_itemsAdded >= 5 && !_unlockedAchievements.Contains("Коллекционер"))
            {
                UnlockAchievement("Коллекционер", "Собрать 5 предметов");
            }
        }

        private void UnlockAchievement(string name, string description)
        {
            _unlockedAchievements.Add(name);
            Console.WriteLine($"ДОСТИЖЕНИЕ РАЗБЛОКИРОВАНО: {name} ({description})");
        }
    }

    public class Player
    {
        public event Action<int> OnExperienceGained;
        public event Action<int> OnLeveledUp;
        public event Action<int> OnDamageTaken;
        public event Action<int> OnGoldChanged;

        public string Name { get; private set; }
        public int Level { get; private set; }
        public int Experience { get; private set; }
        public int Health { get; private set; }
        public int MaxHealth { get; private set; } = 100;
        public int Gold { get; private set; }

        private int _expPerLevel = 25;

        public Player(string name)
        {
            Name = name;
            Level = 1;
            Experience = 0;
            Health = MaxHealth;
            Gold = 0;
        }

        public void GainExperience(int amount)
        {
            Experience += Math.Max(amount, 0);

            OnExperienceGained?.Invoke(amount);

            while (Experience >= _expPerLevel)
            {
                Experience -= _expPerLevel;
                Level++;

                OnLeveledUp?.Invoke(Level);
            }
        }

        public void TakeDamage(int amount)
        {
            if (amount <= 0)
                return;

            Health = Math.Max(Health - amount, 0);

            OnDamageTaken?.Invoke(amount);
        }

        public void Heal(int amount)
        {
            if (amount <= 0)
                return;

            Health += amount;
        }

        public void AddGold(int amount)
        {
            if (amount <= 0)
                return;

            Gold += amount;

            OnGoldChanged?.Invoke(Gold);
        }
    }

    public class Monster
    {
        public event Action<Monster> OnMonsterDefeated;

        public string Name { get; private set; }
        public int Health { get; private set; }
        public int RewardExperience { get; private set; }
        public int RewardGold { get; private set; }

        public Monster(string name, int health, int rewardExperience, int rewardGold)
        {
            Name = name;
            Health = Math.Max(health, 1);
            RewardExperience = Math.Max(rewardExperience, 1);
            RewardGold = Math.Max(rewardGold, 1);
        }

        public void TakeDamage(int amount)
        {
            if (amount <= 0)
                return;

            Health = Math.Max(Health - amount, 0);

            if (Health <= 0)
            {
                OnMonsterDefeated?.Invoke(this);
            }
        }
    }

    public class Inventory
    {
        public event Action<Item> OnItemAdded;

        private List<Item> items = new List<Item>();

        public void AddItem(Item item)
        {
            if (item == null)
                return;

            items.Add(item);
            OnItemAdded?.Invoke(item);
        }
    }

    public class Item
    {
        public string Name { get; private set; }
        public ItemType Type { get; private set; }
        public int Rarity { get; private set; }
        public int Value { get; private set; }

        public Item(string name, ItemType type, int rarity, int value)
        {
            Name = name;
            Type = type;
            Rarity = Math.Max(rarity, 1);
            Value = Math.Max(value, 1);
        }
    }
}
