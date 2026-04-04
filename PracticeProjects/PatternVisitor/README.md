# 🎮 Pattern Visitor — Система логирования боевых событий

Данный проект демонстрирует реализацию паттерна Visitor на C# в контексте игровой разработки. 
Проект показывает, как добавить новую функциональность (логирование, расчет урона) к существующим классам боевых объектов, не изменяя их код.

---

## 📋 Содержание
- О паттерне Visitor
- Схема работы
- Структура проекта
- Пример использования
- Как расширить проект

---

## 🧠 О паттерне Visitor

Visitor — поведенческий паттерн проектирования, который позволяет добавлять новые операции к существующим классам, не изменяя их структуру.

### Когда использовать:

- Когда нужно выполнить операцию над всеми элементами сложной структуры (например, дерево объектов)
- Когда классы редко меняются, но новые операции над ними добавляются часто
- Когда логика операции не должна загрязнять классы элементов

### В игровой разработке Visitor применяется для:

- Систем логов и отладки
- Рендеринга объектов на сцене
- Сохранения/загрузки состояния
- Расчета урона или эффектов
- Валидации данных

---

## 📐 Схема работы

```
┌─────────────────────────────────────────────────────────────────────────────┐
│                              ПАТТЕРН VISITOR                                │
├─────────────────────────────────────────────────────────────────────────────┤
│                                                                             │
│  ┌───────────────┐         ┌────────────────────┐                           │
│  │ IBattleElement│         │  IBattleVisitor    │                           │
│  ├───────────────┤         ├────────────────────┤                           │
│  │ +Accept(v)    │◄────────│ +Visit(Player)     │                           │
│  └───────────────┘         │ +Visit(Enemy)      │                           │
│           ▲                │ +Visit(Trap)       │                           │
│           │                │ +Visit(Chest)      │                           │
│           │                └────────────────────┘                           │
│           │                         ▲                                       │
│           │                         │                                       │
│  ┌────────┴────────┐       ┌────────┴────────┐       ┌─────────────────┐    │
│  │     Player      │       │  BattleLogger   │       │ DamageCalculator│    │
│  ├─────────────────┤       ├─────────────────┤       ├─────────────────┤    │
│  │ -Name           │       │ +Visit(Player)  │       │ +Visit(Player)  │    │
│  │ -Health         │       │ +Visit(Enemy)   │       │ +Visit(Enemy)   │    │
│  │ -Level          │       │ +Visit(Trap)    │       │ +Visit(Trap)    │    │
│  │ +Accept(v)      │       │ +Visit(Chest)   │       │ +Visit(Chest)   │    │
│  └─────────────────┘       └─────────────────┘       └─────────────────┘    │
│                                                                             │
│  ┌──────────────┐         ┌──────────────┐         ┌──────────────┐         │
│  │    Enemy     │         │     Trap     │         │    Chest     │         │
│  ├──────────────┤         ├──────────────┤         ├──────────────┤         │
│  │ -Type        │         │ -TrapName    │         │ -Contents    │         │
│  │ -Health      │         │ -Damage      │         │ -GoldAmount  │         │
│  │ -Damage      │         │ -IsTriggered │         │ -IsOpened    │         │
│  │ +Accept(v)   │         │ +Accept(v)   │         │ +Accept(v)   │         │
│  └──────────────┘         └──────────────┘         └──────────────┘         │
│                                                                             │
└─────────────────────────────────────────────────────────────────────────────┘

                              ПОТОК ВЫЗОВОВ
┌─────────────────────────────────────────────────────────────────────────────┐
│                                                                             │
│   foreach (element in elements)                                             │
│   {                                                                         │
│       ┌──────────┐     ┌─────────────────┐     ┌────────────────────┐       │
│       │ element  │────►│ element.Accept  │────►│ visitor.Visit      │       │
│       │          │     │ (visitor)       │     │ (element)          │       │
│       └──────────┘     └─────────────────┘     └────────────────────┘       │
│                                                            │                │
│                                                            ▼                │
│                                              ┌─────────────────────────┐    │
│                                              │ Специфическая логика    │    │
│                                              │ для данного типа объекта│    │
│                                              └─────────────────────────┘    │
│   }                                                                         │
│                                                                             │
└─────────────────────────────────────────────────────────────────────────────┘
```

### Ключевой механизм — Double Dispatch (двойная диспетчеризация)

Вместо того чтобы использовать if (obj is Player) или switch, паттерн использует виртуальные методы для выбора правильной перегрузки:

1. Первый dispatch: element.Accept(visitor) — выбирается по типу элемента
2. Второй dispatch: visitor.Visit(this) — выбирается по типу посетителя и элемента

---

## 📁 Структура проекта

```
PatternVisitor/
│
├── Program.cs                 # Точка входа, создание объектов и обход
│
├── CombatObjects/             # "Замороженные" классы игровых объектов
│   ├── Player.cs              # Игрок (имя, здоровье, уровень)
│   ├── Enemy.cs               # Враг (тип, здоровье, урон)
│   ├── Trap.cs                # Ловушка (название, урон, активирована)
│   └── Chest.cs               # Сундук (содержимое, золото, открыт)
│
└── Visitor/                   # Посетители (добавляемая функциональность)
    ├── IBattleElement.cs      # Интерфейс элементов
    ├── IBattleVisitor.cs      # Интерфейс посетителя
    ├── BattleLogger.cs        # Логирование состояния объектов
    └── DamageCalculator.cs    # Подсчет общего потенциального урона
```

---

## 💻 Пример использования

```csharp
// Создание списка боевых элементов
List<IBattleElement> elements = new List<IBattleElement>
{
    new Player("Gandalf", 120, 5),
    new Enemy("Dragon", 300, 45),
    new Trap("Огненная яма", 50, true),
    new Chest("Легендарный меч", 150, false)
};

// Создание посетителей
BattleLogger logger = new BattleLogger();
DamageCalculator damageCalc = new DamageCalculator();

// Применение посетителей ко всем элементам
foreach (var element in elements)
{
    element.Accept(logger);      // Логирование
    element.Accept(damageCalc);  // Подсчет урона
}

Console.WriteLine($"Общий урон: {damageCalc.TotalDamage}");
```

### Вывод в консоль:

```
[LOG] Игрок Gandalf (Уровень 5) имеет 120 HP.
[LOG] Враг Dragon (Урон 45) имеет 300 HP.
[LOG] Ловушка "Огненная яма" нанесла 50 урона. Активирована: True
[LOG] Сундук содержит "Легендарный меч" и 150 золота. Открыт: False
Общий урон: 145
```

---

## 🚀 Как расширить проект

### Добавление нового типа объекта

1. Создать новый класс, реализующий IBattleElement:

```csharp
public class NPC : IBattleElement
{
    public string Name { get; set; }
    public string Dialogue { get; set; }
    
    public void Accept(IBattleVisitor visitor) => visitor.Visit(this);
}
```

2. Добавить метод в интерфейс IBattleVisitor:

```csharp
void Visit(NPC npc);
```

3. Реализовать метод во всех существующих посетителях:

```csharp
// В BattleLogger
public void Visit(NPC npc) => Console.WriteLine($"[LOG] NPC {npc.Name} говорит: {npc.Dialogue}");

// В DamageCalculator
public void Visit(NPC npc) { } // NPC не наносит урон
```

### Добавление нового посетителя (без изменения существующих классов)

Просто создай новый класс, реализующий IBattleVisitor:

```csharp
// Сохранение в JSON (пример)
public class SaveVisitor : IBattleVisitor
{
    private StringBuilder _sb = new StringBuilder();
    
    public void Visit(Player player) 
        => _sb.AppendLine($"{{\"Type\":\"Player\",\"Name\":\"{player.Name}\",\"Health\":{player.Health}}}");
    
    public void Visit(Enemy enemy) 
        => _sb.AppendLine($"{{\"Type\":\"Enemy\",\"Type\":\"{enemy.Type}\",\"Health\":{enemy.Health}}}");
    
    // ... остальные Visit методы
    
    public string GetJson() => $"[{_sb.ToString().TrimEnd()}]";
}
```

---

## 📚 Связанные паттерны

| Паттерн | Связь с Visitor |
|------------|------------------------------------------|
| Composite | Visitor часто используется для обхода деревьев Composite |
| Iterator | Visitor + Iterator позволяют обходить структуры разными способами |
| Command | Visitor — это как Command, но для семейства объектов |

---

## 👨‍💻 Автор

Vladimir Vaize | [GitHub](https://github.com/VladimirVaize) | [Telegram Channel](https://t.me/rigelstudio_gamedev)

---

### ⭐ Если этот проект был полезен, поставьте звезду на GitHub!

