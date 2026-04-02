# 🌳 Behavior Tree Implementation in C# | Console AI Example

[![C# Version](https://img.shields.io/badge/C%23-12-blue.svg)](https://docs.microsoft.com/en-us/dotnet/csharp/)
[![Pattern](https://img.shields.io/badge/Pattern-Behavior%20Tree-orange.svg)]()
[![License](https://img.shields.io/badge/License-MIT-green.svg)](LICENSE)

## 📋 О проекте

Это учебная реализация паттерна **"Поведенческое дерево" (Behavior Tree)** на C#. Проект демонстрирует, как создавать гибкий, расширяемый и модульный ИИ для игр, используя композицию узлов.

В качестве примера реализован **ИИ охранника**, который:
- 🔄 Патрулирует между заданными точками
- 🚨 Переходит в режим тревоги при обнаружении игрока
- ⏱️ Ожидает 3 секунды после потери игрока
- 🎯 Возвращается к патрулированию

**Ключевая особенность:** Поведенческие деревья позволяют визуализировать логику ИИ, легко изменять приоритеты действий и переиспользовать узлы в разных контекстах.

---

## 🎮 Как это выглядит

```
=== СИСТЕМА ИИ ОХРАННИКА (BEHAVIOR TREE) ===

--- Тик 1 ---
Патрулирую: [1, 0] -> цель [3, 0]
Охранник: [1, 0]
Игрок на позиции: [5, 5]

--- Тик 2 ---
Патрулирую: [2, 0] -> цель [3, 0]
Охранник: [2, 0]

--- Тик 5 ---
!!! Охранник [3, 3] обнаружил игрока в [4, 3] !!!
Преследую игрока: [4, 3]
ОХРАННИК ВИДИТ ИГРОКА!

--- Тик 7 ---
Охранник [4, 3] потерял игрока из виду
Ожидаю... (1/3)
ОХРАННИК В ТРЕВОГЕ!
```

---

## 🧠 Архитектура паттерна

### Схема работы Behavior Tree

```
                       ┌─────────────────┐
                       │ SELECTOR        │ (главный узел)
                       │ (выполняет до   │
                       │ первого успеха) │
                       └────────┬────────┘
                                │
           ┌────────────────────┼────────────────────┐
           │                    │                    │
           ▼                    ▼                    ▼
   ┌───────────────┐    ┌───────────────┐    ┌───────────────┐
   │ SEQUENCE      │    │ SEQUENCE      │    │ ACTION        │
   │ (тревога)     │    │ (потерял)     │    │ (патруль)     │
   └───────┬───────┘    └───────┬───────┘    └───────────────┘
           │                    │
     ┌─────┴──────┐           ┌─┴──────────┐
     ▼            ▼           ▼            ▼
┌─────────┐ ┌────────────┐ ┌─────────┐ ┌───────┐
│CONDITION│ │ACTION      │ │CONDITION│ │ACTION │
│вижу?    │ │преследовать│ │потерял? │ │ждать  │
└─────────┘ └────────────┘ └─────────┘ └───────┘
```

### Типы узлов

| Узел | Тип | Описание | Возвращает |
|------|-----|----------|------------|
| **Sequence** | Композит | "И" - выполняет всех детей по порядку | Success (если все Success), Failure (если любой Failure) |
| **Selector** | Композит | "ИЛИ" - выполняет до первого успеха | Success (при первом Success), Failure (если все Failure) |
| **Condition** | Декоратор | Проверяет условие | Success (если true), Failure (если false) |
| **Action** | Лист | Выполняет действие | Success/Failure/Running |
| **Inverter** | Декоратор | Инвертирует результат | Success↔Failure, Running→Running |

### Жизненный цикл узла

```
               ┌─────────┐
               │  START  │
               └────┬────┘
                    │
                    ▼
               ┌─────────┐
               │Evaluate │◄────────┐
               └────┬────┘         │
                    │              │
          ┌─────────┴──────┐       │
          ▼                ▼       │
      ┌───────┐  ┌───────────────┐ │
      │Running│  │Success/Failure│ │
      └───┬───┘  └───────────────┘ │
          └────────────────────────┘ (на следующем тике)
```

---

## 🏗️ Структура проекта

```
PatternBehaviorTree/
├── Auxiliary/
│ └── Vector2.cs # 2D вектор для позиционирования
├── Tree/
│ ├── BTNode.cs # Базовый абстрактный узел
│ ├── Sequence.cs # Композит "И"
│ ├── Selector.cs # Композит "ИЛИ"
│ ├── Condition.cs # Условный узел
│ ├── ActionNode.cs # Узел-действие
│ └── Inverter.cs # Декоратор-инвертор
├── GuardAI.cs # ИИ охранника с построением дерева
├── PlayerController.cs # Управление игроком (WASD)
└── Program.cs # Точка входа, игровой цикл
```

---

## 🔧 Примеры расширения

### 1. Добавление нового действия (`Action`)

```csharp
// Пример: охранник стреляет при виде игрока
private BTNode.NodeState Shoot()
{
    if (!CanSeePlayer || Ammo <= 0)
        return BTNode.NodeState.Failure;
    
    Console.WriteLine($"Охранник стреляет в игрока! Осталось патронов: {--Ammo}");
    Player.TakeDamage(10);
    return BTNode.NodeState.Success;
}

// Добавление в дерево (в Sequence тревоги)
var alertSequence = new Sequence(new List<BTNode>
{
    checkPlayerInSight,
    new ActionNode(Shoot),        // Новое действие
    chaseAction
});
```

### 2. Создание кастомного декоратора (`RepeatUntilFail`)

```csharp
// Повторяет дочерний узел, пока тот не вернет Failure
public class RepeatUntilFail : BTNode
{
    private BTNode _child;
    
    public RepeatUntilFail(BTNode child)
    {
        _child = child;
    }
    
    public override NodeState Evaluate()
    {
        var state = _child.Evaluate();
        
        if (state == NodeState.Failure)
            return NodeState.Success;
        
        // Продолжаем повторять
        return NodeState.Running;
    }
}
```

### 3. Добавление приоритетов (`Weighted Selector`)

```csharp
// Selector с весами (приоритетами)
public class WeightedSelector : BTNode
{
    private List<(BTNode node, int weight)> _weightedChildren;
    private Random _random = new Random();
    
    public WeightedSelector(List<(BTNode, int)> weightedChildren)
    {
        _weightedChildren = weightedChildren;
    }
    
    public override NodeState Evaluate()
    {
        int totalWeight = _weightedChildren.Sum(w => w.weight);
        int roll = _random.Next(totalWeight);
        
        int cumulative = 0;
        foreach (var (node, weight) in _weightedChildren)
        {
            cumulative += weight;
            if (roll < cumulative)
                return node.Evaluate();
        }
        
        return NodeState.Failure;
    }
}
```

### 4. Система `Blackboard` (общая память для узлов)

```csharp
// Blackboard - общее хранилище данных
public class Blackboard
{
    private Dictionary<string, object> _data = new Dictionary<string, object>();
    
    public T Get<T>(string key) => (T)_data[key];
    public void Set(string key, object value) => _data[key] = value;
    public bool Has(string key) => _data.ContainsKey(key);
}

// Использование в узлах
public class CheckHealthCondition : Condition
{
    private Blackboard _blackboard;
    
    public CheckHealthCondition(Blackboard blackboard)
    {
        _blackboard = blackboard;
        _condition = () => _blackboard.Get<int>("Health") < 30;
    }
}
```

---

## 📚 Почему Behavior Tree?

| Критерий | `Finite State Machine` | `Behavior Tree` |
|-------------|-------------------|-------------------|
|Сложность логики|Растет экспоненциально|Растет линейно|
|Переиспользование|Низкое|Высокое (узлы)|
|Визуализация|Сложная|Интуитивная (дерево)|
|Приоритеты|Жесткие|Гибкие|
|Параллелизм|Сложно|Встроен (через Running)|

---

## 📖 Дополнительные ресурсы

- [Behavior Trees for Game AI (GDC 2015)](https://www.gdcvault.com/play/1021848/)
- [Understanding Behavior Trees (Chris Simpson)](https://www.gamedeveloper.com/programming/behavior-trees-for-ai-how-they-work)
- [Behavior Tree Implementation in C#](https://github.com/martin-klima/behavior-tree)

---

## 👨‍💻 Автор

Vladimir Vaize | [GitHub](https://github.com/VladimirVaize) | [Telegram Channel](https://t.me/rigelstudio_gamedev)

---

### ⭐ Если этот проект помог вам понять Behavior Tree, поставьте звезду на GitHub!
