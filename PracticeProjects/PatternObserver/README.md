# Pattern Observer - Система Достижений для RPG

[![C#](https://img.shields.io/badge/C%23-12-blue.svg)](https://docs.microsoft.com/en-us/dotnet/csharp/)
[![.NET](https://img.shields.io/badge/.NET-8.0-purple.svg)](https://dotnet.microsoft.com/)

Демонстрация паттерна **Наблюдатель (Observer)** на примере игровой системы достижений. Проект показывает, как реализовать слабосвязанную архитектуру, где игровые события автоматически уведомляют заинтересованные компоненты (достижения, UI) без прямых зависимостей между ними.

---

## 🎯 Описание паттерна

**Наблюдатель (Observer)** — поведенческий паттерн, который определяет механизм подписки, позволяющий одним объектам (наблюдателям) следить и реагировать на события, происходящие в других объектах (издателях).

### Когда использовать:
- Когда изменение состояния одного объекта требует изменения других, и вы не хотите, чтобы объекты знали друг о друге
- Когда количество наблюдателей может меняться во время выполнения
- Когда нужно реализовать систему событий с минимальной связанностью

### В игровой разработке:
- Системы достижений
- Обновление UI при изменении здоровья/опыта
- Звуковые уведомления
- Системы квестов

---

## 🔧 Схема работы

```
┌─────────────────────────────────────────────────────────────────────────────┐
│                        ПАТТЕРН НАБЛЮДАТЕЛЬ                                  │
├─────────────────────────────────────────────────────────────────────────────┤
│                                                                             │
│ ┌─────────────────────┐                       ┌─────────────────────────┐   │
│ │      ИЗДАТЕЛЬ       │                       │       НАБЛЮДАТЕЛЬ       │   │
│ │   (EventManager)    │                       │       (IObserver)       │   │
│ │                     │                       │                         │   │
│ │  - subscribers      │◄──────────────────────│    + OnNotify(data)     │   │
│ │  + Subscribe()      │       подписка        └───────────┬─────────────┘   │
│ │  + Unsubscribe()    │                                   ▲                 │
│ │  + Notify()         │                                   │                 │
│ └──────────┬──────────┘                                   │ реализует       │
│            │                                              │                 │
│            │ уведомление                     ┌────────────┴─────────────┐   │
│            ▼                                 │                          │   │
│ ┌──────────────────────────────────────────┐ │ ┌─────────────────────┐  │   │
│ │             ИГРОВЫЕ СОБЫТИЯ              │ │ │     ICompletable    │  │   │
│ │                                          │ │ │       Observer      │  │   │
│ │ ┌────────────────────────────────────┐   │ │ │                     │  │   │
│ │ │ GameEventType.MonsterKilled        │   │ │ │ + IsCompleted {get} │  │   │
│ │ │ GameEventType.ItemCollected        │   │ │ └──────────┬──────────┘  │   │
│ │ │ GameEventType.LevelCompleted       │   │ │            ▲             │   │
│ │ └────────────────────────────────────┘   │ │            │             │   │
│ └──────────────────────────────────────────┘ │   ┌────────┴────────┐    │   │
│                                              │   │ Достижения      │    │   │ 
│                                              │   │                 │    │   │
│                                              │   │ • FirstBlood    │    │   │
│                                              │   │ • Kill10Monsters│    │   │
│                                              │   │ • Collect3Items │    │   │
│                                              │   └─────────────────┘    │   │
│                                              │ ┌─────────────────────┐  │   │
│                                              │ │   UIPopupObserver   │  │   │
│                                              │ │     (обычный)       │  │   │
│                                              │ └─────────────────────┘  │   │
│                                              └──────────────────────────┘   │
└─────────────────────────────────────────────────────────────────────────────┘

                               ПОТОК ДАННЫХ

┌──────────┐      ┌─────────────────┐      ┌─────────────────────────────┐
│ Игрок    │────▶│ EventManager     │────▶│ Все подписанные наблюдатели │
│ убивает  │      │ .Notify()       │      │ получают OnNotify(eventData)│
│ монстра  │      └─────────────────┘      └─────────────────────────────┘
└──────────┘                                              │
                                                          ▼
                                        ┌───────────────────────────────────┐
                                        │ Kill10MonstersAchievement         │
                                        │ progress += 1                     │
                                        │ if progress >= 10 → получено!     │
                                        └───────────────────────────────────┘
                                        ┌───────────────────────────────────┐
                                        │ FirstBloodAchievement             │
                                        │ → получено! (первое убийство)     │
                                        └───────────────────────────────────┘
                                        ┌───────────────────────────────────┐
                                        │ UIPopupObserver                   │
                                        │ → выводит уведомление в UI        │
                                        └───────────────────────────────────┘
```

---

## 📁 Структура проекта

```
PatternObserver/
├── Program.cs # Точка входа, демонстрация работы
└── GameEvents/
  ├── IObserver.cs # Базовый интерфейс наблюдателя
  ├── ICompletableObserver.cs # Наблюдатель с признаком завершения
  ├── EventManager.cs # Издатель (Subject)
  ├── GameEventType.cs # Типы игровых событий (enum)
  ├── GameEventData.cs # Данные о событии
  ├── Kill10MonstersAchievement.cs # Достижение "Истребитель"
  ├── FirstBloodAchievement.cs # Достижение "Первая кровь"
  ├── Collect3ItemsAchievement.cs # Достижение "Коллекционер"
  └── UIPopupObserver.cs # UI уведомления
```

### Описание классов

| Класс/Интерфейс | Описание |
|-----------------|----------|
| `IObserver` | Базовый интерфейс. Содержит метод `OnNotify(GameEventData)`, который вызывается при наступлении события |
| `ICompletableObserver` | Расширяет `IObserver`. Добавляет свойство `IsCompleted` для наблюдателей, которые могут завершить свою работу |
| `EventManager` | Центральный диспетчер событий. Управляет подписками и уведомляет наблюдателей о событиях |
| `GameEventType` | Перечисление типов событий: `MonsterKilled`, `ItemCollected`, `LevelCompleted` |
| `GameEventData` | DTO с данными о событии: тип, имя цели, значение (количество) |
| `Kill10MonstersAchievement` | Достижение "Истребитель" — активируется после убийства 10 монстров |
| `FirstBloodAchievement` | Достижение "Первая кровь" — активируется после первого убийства |
| `Collect3ItemsAchievement` | Достижение "Коллекционер" — активируется после сбора 3 предметов |
| `UIPopupObserver` | Простой наблюдатель, выводящий все события в консоль (имитация UI) |

---

## 🚀 Как это работает

### 1. Подписка на события

```csharp
EventManager manager = new EventManager();

var killAchievement = new Kill10MonstersAchievement();
var uiPopup = new UIPopupObserver();

manager.Subscribe(GameEventType.MonsterKilled, killAchievement);
manager.Subscribe(GameEventType.MonsterKilled, uiPopup);
```

### 2. Генерация событий

```csharp
manager.Notify(new GameEventData 
{ 
    Type = GameEventType.MonsterKilled, 
    TargetName = "Гоблин", 
    Value = 1 
});
```

### 3. Реакция наблюдателей

- `Kill10MonstersAchievement` увеличивает внутренний счетчик
- `UIPopupObserver` выводит уведомление в консоль

### 4. Автоматическая отписка

Когда достижение выполнено (`IsCompleted == true`), `EventManager` автоматически удаляет его из списка подписчиков. Это предотвращает:
- Лишние проверки в завершенных достижениях
- Утечки памяти (если бы наблюдатели хранились вечно)

---

## ➕ Расширение системы

### 1. Добавление нового типа события

```csharp
public enum GameEventType
{
    MonsterKilled,
    ItemCollected,
    LevelCompleted,
    BossDefeated  // Новое событие
}
```

### 2. Добавление нового достижения

```csharp
// 1. Создать класс, реализующий ICompletableObserver
public class KillBossAchievement : ICompletableObserver
{
    public bool IsCompleted { get; private set; }
    
    public void OnNotify(GameEventData eventData)
    {
        if (IsCompleted) return;
        if (eventData.Type != GameEventType.BossDefeated) return;
        
        IsCompleted = true;
        Console.WriteLine("Достижение получено: Победитель боссов");
    }
}

// 2. Подписать в Program.cs
manager.Subscribe(GameEventType.BossDefeated, new KillBossAchievement());
```

### Добавление наблюдателя без завершения (например, логгер)

```csharp
public class LoggerObserver : IObserver
{
    public void OnNotify(GameEventData eventData)
    {
        File.AppendAllText("game.log", $"{DateTime.Now}: {eventData.Type}\n");
    }
}
```

---

## 📐 Принципы проектирования

### SOLID в действии

| Принцип | Как применен |
|-------------------------|-----------------------------------------------------------|
| `Single Responsibility` | `EventManager` управляет подписками, каждое достижение отвечает только за свою логику|
| `Open/Closed` | Новые достижения добавляются без изменения существующего кода |
| `Liskov Substitution` | Любой `ICompletableObserver` может использоваться как `IObserver` |
| `Interface Segregation` | Разделены `IObserver` и `ICompletableObserver` — UI не реализует ненужные методы |
| `Dependency Inversion` | Наблюдатели зависят от абстракций (`IObserver`), а не от конкретных классов |

---

## ✅ Преимущества реализации
- Слабая связанность — издатель не знает о конкретных наблюдателях
- Расширяемость — новые достижения добавляются без изменения существующего кода
- Безопасная отписка — завершенные наблюдатели автоматически удаляются
- Типобезопасность — события типизированы через `GameEventType` и `GameEventData`

---

## 👨‍💻 Автор

Vladimir Vaize | [GitHub](https://github.com/VladimirVaize) | [Telegram Channel](https://t.me/rigelstudio_gamedev)

---

### ⭐ Если этот проект был полезен, поставьте звезду на GitHub!
