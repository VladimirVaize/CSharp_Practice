# Pattern Strategy

## 📌 Описание

Демонстрационный проект, показывающий реализацию паттерна **Strategy** на C#.

**Контекст:** Система расчета урона в пошаговой RPG. Боец (Fighter) может динамически менять формулу расчета урона во время боя без изменения своего кода.

## 🎯 Цель паттерна Strategy

> Определяет семейство алгоритмов, инкапсулирует каждый из них и делает их взаимозаменяемыми. Strategy позволяет изменять алгоритм независимо от клиентов, которые его используют.

— *GoF (Gang of Four)*

## 📐 Схема работы

```
┌─────────────────────────────────────────────────────────────────────────┐
│                            CLIENT (Program)                             │
│                                                                         │
│   fighter = new Fighter("Григорий", 10, new NormalDamageStrategy());    │
│   fighter.Attack(); // Обычный урон                                     │
│   fighter.SetStrategy(new CriticalDamageStrategy());                    │
│   fighter.Attack(); // КРИТ! урон x2                                    │
└───────────────────────────────────┬─────────────────────────────────────┘
                                    │
                                    ▼
                    ┌─────────────────────────────┐      ┌─────────────────────────────────────┐
                    │            FIGHTER          │      │ <<interface>>                       │
                    ├─────────────────────────────┤      │ IDamageStrategy                     │
                    │ - Name: string              │      ├─────────────────────────────────────┤
                    │ - BaseDamage: int           │────▶│ + CalculateDamage(int): int         │
                    │ - DamageStrategy: IDamage...│      └─────────────────────────────────────┘
                    ├─────────────────────────────┤                      △
                    │ + Attack(): void            │       ┌──────────────┼──────────────┬────────┬─ Другие стратегии
                    │ + SetStrategy(...): void    │       │              │              │        │
                    └─────────────────────────────┘       │              │              │        │
                                                          │              │              │
                                                ┌─────────┴────────┐┌────┴────┐ ┌───────┴──────┐
                                                │   NormalDamage   ││Critical │ │   Glancing   │
                                                │     Strategy     ││Damage   │ │    Damage    │
                                                ├──────────────────┤│Strategy │ │   Strategy   │
                                                │ CalculateDamage  │├─────────┤ ├──────────────┤
                                                │ return base      ││  return │ │return base/2 │
                                                └──────────────────┘│  base*2 │ └──────────────┘
                                                                    └─────────┘ 
```

## 🧩 Структура проекта

```
PatternStrategy/
├── Program.cs # Точка входа, демонстрация работы
├── Fighter.cs # Класс бойца (контекст)
├── Strategies/ # Папка со стратегиями
│ ├── IDamageStrategy.cs # Интерфейс стратегии
│ ├── NormalDamageStrategy.cs # Обычный урон
│ ├── CriticalDamageStrategy.cs # Критический урон (x2)
│ ├── GlancingDamageStrategy.cs # Скользящий урон (/2)
│ └── FireDamageStrategy.cs # Огненный урон (+20%)
└── README.md
```

## 🚀 Пример использования

```csharp
// Создание бойца с обычной стратегией
Fighter fighter = new Fighter("Григорий", 10, new NormalDamageStrategy());

fighter.Attack();  // Атака! Григорий наносит 10 урона!

// Смена стратегии на критический удар
fighter.SetStrategy(new CriticalDamageStrategy());
fighter.Attack();  // Атака! Григорий наносит 20 урона! (КРИТ!)

// Смена стратегии на скользящий удар
fighter.SetStrategy(new GlancingDamageStrategy());
fighter.Attack();  // Атака! Григорий наносит 5 урона!
```

## 📦 Доступные стратегии

| Стратегия | Формула | Описание |
|---------------|-------------|----------------------------|
| `NormalDamageStrategy` | `damage = base` | Обычный урон без изменений |
| `CriticalDamageStrategy` | `damage = base * 2` | Двойной урон (критический удар) |
| `GlancingDamageStrategy` | `damage = base / 2` | Половина урона (скользящий удар) |
| `FireDamageStrategy` | `damage = base * 1.2` | ОДополнительные 20% огненного урона |

## 🔧 Как добавить новую стратегию

1. Создать новый класс в папке `Strategies/`
2. Реализовать интерфейс `IDamageStrategy`
3. Определить логику в методе `CalculateDamage()`

```csharp
public class PoisonDamageStrategy : IDamageStrategy
{
    public int CalculateDamage(int baseDamage)
    {
        return baseDamage;      // + эффект отравления
    }
}
```

## ✅ Преимущества использования Strategy в данном проекте
- Open/Closed Principle - новые стратегии добавляются без изменения класса `Fighter`
- Инкапсуляция алгоритмов - каждая формула урона изолирована
- Гибкость во время выполнения - стратегию можно менять динамически (`SetStrategy`)
- Упрощение тестирования - каждую стратегию можно тестировать отдельно

---

## 👨‍💻 Автор

Vladimir Vaize | [GitHub](https://github.com/VladimirVaize) | [Telegram Channel](https://t.me/rigelstudio_gamedev)

---

### ⭐ Если этот проект был полезен, поставьте звезду на GitHub!
