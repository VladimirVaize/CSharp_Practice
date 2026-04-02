# Pattern Composite - Army System

[![C#](https://img.shields.io/badge/C%23-12-blue.svg)](https://docs.microsoft.com/en-us/dotnet/csharp/)
[![.NET](https://img.shields.io/badge/.NET-8.0-purple.svg)](https://dotnet.microsoft.com/)

Демонстрация паттерна **Composite (Компоновщик)** на примере тактической RPG системы отрядов и армий.

---

## 🎯 Описание паттерна

**Composite** — структурный паттерн проектирования, который позволяет сгруппировать объекты в древовидную структуру, а затем работать с ними так, как будто это единичный объект.

### Когда использовать:

| Ситуация | Пример в играх |
|----------|----------------|
| Объекты могут содержать другие объекты | Отряд → юниты |
| Клиент не должен различать простые и составные объекты | Приказ атаки для одного юнита и для целой армии |
| Нужна иерархическая структура | Дерево навыков, система папок файлов, UI компоненты |

### Плюсы и минусы

| ✅ Плюсы | ❌ Минусы |
|----------|-----------|
| Упрощает клиентский код (не нужно проверять типы) | Может сделать дизайн слишком общим |
| Легко добавлять новые типы компонентов | Усложняет ограничение типов в контейнере |
| Поддерживает принцип открытости/закрытости (OCP) | |

---

## 📐 Схема работы

```
┌─────────────────────────────────────────────────────────────────┐
│                       Клиентский код                            │
│              Program.cs / любой игровой менеджер                │
└───────────────────────────────┬─────────────────────────────────┘
                                │
                                ▼
                    ┌───────────────────────┐
                    │      «interface»      │
                    │      IGameEntity      │
                    ├───────────────────────┤
                    │ + TakeDamage(int)     │
                    │ + GetTotalHealth()    │
                    └───────────┬───────────┘
                                │
              ┌─────────────────┼─────────────────┐
              │                                   │
              ▼                                   ▼
 ┌─────────────────────────┐       ┌─────────────────────────────┐
 │          Unit           │       │            Group            │
 │      (Лист / Leaf)      │       │   (Контейнер / Composite)   │
 ├─────────────────────────┤       ├─────────────────────────────┤
 │     - Name : string     │       │  - _entities : List<IGame>  │
 │     - Health : int      │       ├─────────────────────────────┤
 ├─────────────────────────┤       │  + Add(IGameEntity)         │
 │   + TakeDamage(int)     │       │  + Remove(IGameEntity)      │
 │   + GetTotalHealth()    │       │  + TakeDamage(int)          │
 └─────────────────────────┘       │  + GetTotalHealth()         │
                                   └─────────────────────────────┘
                                                  │
                                                  │ содержит
                                                  ▼
                                         ┌─────────────────┐
                                         │ IGameEntity     │
                                         │ (рекурсия)      │
                                         └─────────────────┘
```

### Древовидная структура примера:

```
Армия (Group)
├── Отряд "Мечники" (Group)
│ ├── Рыцарь (Unit, 100 HP)
│ ├── Солдат (Unit, 80 HP)
│ └── Солдат (Unit, 80 HP)
├── Отряд "Лучники" (Group)
│ ├── Лучник (Unit, 60 HP)
│ ├── Лучник (Unit, 60 HP)
│ └── Маг (Unit, 50 HP)
└── Герой "Паладин" (Unit, 150 HP)
```

---

## 📁 Структура проекта

```
PatternComposite/
│
├── Composite/
│ ├── IGameEntity.cs # Интерфейс для всех игровых сущностей
│ ├── Unit.cs # Листовой элемент (один юнит)
│ └── Group.cs # Контейнер (группа/отряд/армия)
├── Program.cs # Точка входа и демонстрация
└── README.md # Этот файл
```

### Описание классов

| Класс/Интерфейс | Роль в паттерне | Описание |
|-----------------|-----------------|----------|
| `IGameEntity` | **Component** | Определяет общий интерфейс для всех элементов |
| `Unit` | **Leaf** | Листовой элемент, не может содержать других элементов |
| `Group` | **Composite** | Контейнер, может содержать другие `IGameEntity` |
| `Program` | **Client** | Работает с объектами через общий интерфейс |

---

## 💻 Пример использования

```csharp
// Создаем отряды
Group swordsmen = new Group();
swordsmen.Add(new Unit("Рыцарь", 100));
swordsmen.Add(new Unit("Солдат", 80));

Group archers = new Group();
archers.Add(new Unit("Лучник", 60));
archers.Add(new Unit("Маг", 50));

// Создаем армию и добавляем в нее отряды
Group army = new Group();
army.Add(swordsmen);
army.Add(archers);

// Наносим урон по всей армии (рекурсивно)
army.TakeDamage(30);

// Получаем общее здоровье
int totalHealth = army.GetTotalHealth();
```

### Пример вывода

```
Общее здоровье армии: 370

Наносим 30 урона по всей армии:

Юнит Рыцарь получил 30 урона. Осталось здоровья: 70
Юнит Солдат получил 30 урона. Осталось здоровья: 50
Юнит Лучник получил 30 урона. Осталось здоровья: 30
Юнит Маг получил 30 урона. Осталось здоровья: 20

Общее здоровье армии: 170
```

---

## 🚀 Расширение паттерна

### 1. Добавление новых типов урона (Strategy + Composite)

```csharp
public enum DamageType
{
    Physical,
    Fire,
    Ice,
    Poison
}

public interface IGameEntity
{
    void TakeDamage(int damage, DamageType type = DamageType.Physical);
    int GetTotalHealth();
}

// Unit.cs
public void TakeDamage(int damage, DamageType type)
{
    int actualDamage = type switch
    {
        DamageType.Fire => damage * 2,   // Уязвимость к огню
        DamageType.Ice => damage / 2,     // Сопротивление холоду
        _ => damage
    };
    Health = Math.Max(0, Health - actualDamage);
}
```

### 2. Добавление баффов/дебаффов (Decorator + Composite)

```csharp
public class BuffedUnit : IGameEntity
{
    private readonly IGameEntity _entity;
    private readonly int _damageReduction;
    
    public BuffedUnit(IGameEntity entity, int damageReduction)
    {
        _entity = entity;
        _damageReduction = damageReduction;
    }
    
    public void TakeDamage(int damage)
    {
        int reducedDamage = Math.Max(1, damage - _damageReduction);
        _entity.TakeDamage(reducedDamage);
    }
    
    public int GetTotalHealth() => _entity.GetTotalHealth();
}
```

### 3. Поиск юнитов в группе (Visitor + Composite)

```csharp
public interface IGameEntityVisitor
{
    void Visit(Unit unit);
    void Visit(Group group);
}

public class HealVisitor : IGameEntityVisitor
{
    private readonly int _healAmount;
    
    public HealVisitor(int healAmount) => _healAmount = healAmount;
    
    public void Visit(Unit unit) => unit.Heal(_healAmount);
    public void Visit(Group group) { } // Группа обрабатывается отдельно
}

// В классе Group
public void Accept(IGameEntityVisitor visitor)
{
    visitor.Visit(this);
    foreach (var entity in _entities)
    {
        if (entity is Unit unit)
            visitor.Visit(unit);
        else if (entity is Group group)
            group.Accept(visitor);
    }
}
```

### 4. Ленивая инициализация (для больших групп)

```csharp
public class LazyGroup : IGameEntity
{
    private List<IGameEntity> _entities;
    private readonly Func<List<IGameEntity>> _loader;
    
    public LazyGroup(Func<List<IGameEntity>> loader)
    {
        _loader = loader;
    }
    
    private void EnsureLoaded()
    {
        if (_entities == null)
            _entities = _loader();
    }
    
    public void TakeDamage(int damage)
    {
        EnsureLoaded();
        foreach (var entity in _entities)
            entity.TakeDamage(damage);
    }
    
    public int GetTotalHealth()
    {
        EnsureLoaded();
        return _entities.Sum(e => e.GetTotalHealth());
    }
}
```

---

## 📚 Связанные паттерны

| Паттерн | Связь с Composite |
|------------|---------------------------------------------|
| `Decorator` | Часто используется вместе с Composite для добавления поведения |
| `Visitor` | Позволяет выполнять операции над сложной структурой Composite |
| `Iterator` | Используется для обхода дерева Composite |
| `Flyweight` | Позволяет разделять общее состояние между листьями |

---

## 👨‍💻 Автор

Vladimir Vaize | [GitHub](https://github.com/VladimirVaize) | [Telegram Channel](https://t.me/rigelstudio_gamedev)

---

### ⭐ Если этот проект был полезен, поставьте звезду на GitHub!
