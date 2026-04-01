using PatternObserver.GameEvents;

namespace PatternObserver
{
    internal class Program
    {
        static void Main(string[] args)
        {
            EventManager manager = new EventManager();

            UIPopupObserver uIPopup = new UIPopupObserver();
            Kill10MonstersAchievement kill10Monsters = new Kill10MonstersAchievement();
            FirstBloodAchievement firstBlood = new FirstBloodAchievement();
            Collect3ItemsAchievement collect3Items = new Collect3ItemsAchievement();

            manager.Subscribe(GameEventType.MonsterKilled, uIPopup);
            manager.Subscribe(GameEventType.ItemCollected, uIPopup);
            manager.Subscribe(GameEventType.LevelCompleted, uIPopup);

            manager.Subscribe(GameEventType.MonsterKilled, kill10Monsters);
            manager.Subscribe(GameEventType.MonsterKilled, firstBlood);
            manager.Subscribe(GameEventType.ItemCollected, collect3Items);

            manager.Notify(new GameEventData { Type = GameEventType.MonsterKilled, TargetName = "Гоблин", Value = 4 });
            manager.Notify(new GameEventData { Type = GameEventType.MonsterKilled, TargetName = "Орк", Value = 3 });
            manager.Notify(new GameEventData { Type = GameEventType.MonsterKilled, TargetName = "Тролль", Value = 2 });
            manager.Notify(new GameEventData { Type = GameEventType.ItemCollected, TargetName = "Зелье", Value = 1 });
            manager.Notify(new GameEventData { Type = GameEventType.ItemCollected, TargetName = "Меч", Value = 1 });
            manager.Notify(new GameEventData { Type = GameEventType.ItemCollected, TargetName = "Щит", Value = 1 });
            manager.Notify(new GameEventData { Type = GameEventType.MonsterKilled, TargetName = "Дракон", Value = 1 });
        }
    }
}
