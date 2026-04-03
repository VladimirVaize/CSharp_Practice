# 🎮 Pattern Decorator — Система баффов для RPG

---

## 📝 Описание проекта

Демонстрация паттерна Декоратор (Decorator) на примере системы временных баффов для RPG-персонажа.

### Суть задачи:

Персонаж имеет базовые характеристики (урон, скорость атаки, сопротивление магии). <br>
На него можно накладывать различные эффекты (баффы/дебаффы), которые модифицируют эти характеристики. <br>
Баффы могут:
- Комбинироваться между собой
- Влиять последовательно
- Иметь условную логику

### Почему Декоратор?

Каждый бафф "оборачивает" персонажа, добавляя свою модификацию, при этом сохраняя единый интерфейс. <br>
Это позволяет накладывать бесконечное количество эффектов без изменения существующих классов.

---

## 🏗️ Схема работы паттерна

```
      ┌─────────────────────────────────────────┐
      │                <<interface>>            │
      │               ICharacterStats           │
      │            ┌───────────────────┐        │
      │            │ + GetDamage()     │        │
      │            │ + GetAttackSpeed()│        │
      │            │ + GetMagicResist()│        │
      │            └───────────────────┘        │
      └──────┬─────────────────────────────┬────┘
             │                             │
             ▼                             ▼
┌────────────────────────┐      ┌─────────────────────────────────┐
│    BaseCharacter       │      │      BuffDecorator              │
│    (ConcreteComponent) │      │     (AbstractDecorator)         │
│  ┌───────────────────┐ │      │  ┌───────────────────────────┐  │
│  │ - _damage         │ │      │  │ # _stats: ICharacterStats │  │
│  │ - _attackSpeed    │ │      │  ├───────────────────────────┤  │
│  │ - _magicResist    │ │      │  │ + BuffDecorator(stats)    │  │
│  └───────────────────┘ │      │  │ + virtual GetDamage()     │  │
│  + GetDamage() → base  │      │  │ + virtual GetAttackSpeed()│  │
│  + GetAttackSpeed()    │      │  │ + virtual GetMagicResist()│  │
│  + GetMagicResist()    │      │  └───────────────────────────┘  │
└────────────────────────┘      └────────────────┬────────────────┘
                                                 │
                    ┌────────────────────────────┼────────────────────────────┐
                    │                            │                            │
                    ▼                            ▼                            ▼
         ┌────────────────────┐       ┌────────────────────┐       ┌────────────────────┐
         │   BerserkBuff      │       │    HasteBuff       │       │  MagicShieldBuff   │
         │ (ConcreteDecorator)|       │ (ConcreteDecorator)|       │ (ConcreteDecorator)|
         ├────────────────────┤       ├────────────────────┤       ├────────────────────┤
         │ Damage    × 1.5    │       │ AttackSpeed ×1.3   │       │ MagicResist + 20   │
         │ MagicResist ×0.5   │       └────────────────────┘       └────────────────────┘
         └────────────────────┘
                   │
                   ▼
         ┌──────────────────┐
         │ DoubleDamageBuff │
         │ (Bonus Decorator)│
         ├──────────────────┤
         │ If Damage < 30   │
         │   then Damage ×2 │
         └──────────────────┘
```

---

## 🔄 Динамика работы

```
Базовый персонаж (10 урона, 1.0 скорости, 5 сопротивления)
        │
        ▼
   [BerserkBuff]          → Урон ×1.5, Сопротивление ×0.5
        │
        ▼
    [HasteBuff]           → Скорость ×1.3
        │
        ▼
 [MagicShieldBuff]        → Сопротивление +20
        │
        ▼
 [DoubleDamageBuff]       → Если урон < 30, то ×2
        │
        ▼
Финальный результат
```

### Результат вычислений:
```
Урон:           10 → 15 → 15 → 15 → 30 (удвоение сработало, т.к. 15 < 30)
Скорость:       1.0 → 1.0 → 1.3 → 1.3 → 1.3
Сопротивление:  5 → 2.5 → 2.5 → 22.5 → 22.5
```

---

## 📁 Структура проекта

```
PatternDecorator/
│
├── Character/
│   ├── Core/
│   │   └── ICharacterStats.cs          # Общий интерфейс
│   │
│   ├── BaseCharacter.cs                # Базовый персонаж (без баффов)
│   ├── BuffDecorator.cs                # Абстрактный декоратор
│   │
│   ├── BerserkBuff.cs                  # Бафф: урон +50%, сопротивление -50%
│   ├── HasteBuff.cs                    # Бафф: скорость атаки +30%
│   ├── MagicShieldBuff.cs              # Бафф: сопротивление +20
│   └── DoubleDamageBuff.cs             # Бонус: удвоение урона (при условии)
│
└── Program.cs                          # Точка входа и демонстрация
```

---

## 🎯 Как это работает (пример кода)

```csharp
// 1. Создаем базового персонажа
ICharacterStats character = new BaseCharacter(10, 1.0f, 5);

// 2. Накладываем баффы последовательно (оборачиваем)
character = new BerserkBuff(character);
character = new HasteBuff(character);
character = new MagicShieldBuff(character);
character = new DoubleDamageBuff(character);

// 3. Получаем итоговые характеристики
float finalDamage = character.GetDamage();        // 30
float finalSpeed = character.GetAttackSpeed();   // 1.3
float finalResist = character.GetMagicResist();  // 22.5
```

---

## 🚀 Примеры расширения

### 1. Добавление нового баффа (например, "Огненное оружие")

```csharp
public class FireWeaponBuff : BuffDecorator
{
    private const float FIRE_DAMAGE_BONUS = 15f;
    
    public FireWeaponBuff(ICharacterStats stats) : base(stats) { }
    
    public override float GetDamage() => _stats.GetDamage() + FIRE_DAMAGE_BONUS;
    
    // Остальные характеристики не меняются
}
```

### 2. Бафф с ограничением по времени (нужна внешняя система)

```csharp
public class TimedBuff : BuffDecorator
{
    private float _duration;
    private DateTime _startTime;
    
    public TimedBuff(ICharacterStats stats, float duration) : base(stats)
    {
        _duration = duration;
        _startTime = DateTime.Now;
    }
    
    private bool IsExpired => (DateTime.Now - _startTime).TotalSeconds > _duration;
    
    public override float GetDamage() => 
        IsExpired ? _stats.GetDamage() : base.GetDamage() * 2f;
}
```

### 3. Дебафф (противоположность баффу)

```csharp
public class SlowDebuff : BuffDecorator
{
    public SlowDebuff(ICharacterStats stats) : base(stats) { }
    
    public override float GetAttackSpeed() => _stats.GetAttackSpeed() * 0.5f;
}
```

### 4. Комбинированный эффект (зависит от других характеристик)

```csharp
public class RageBuff : BuffDecorator
{
    public RageBuff(ICharacterStats stats) : base(stats) { }
    
    public override float GetDamage()
    {
        // Чем ниже здоровье, тем выше урон
        float healthPercent = GetCurrentHealthPercent(); // внешний метод
        float bonus = 1f + (1f - healthPercent);
        return _stats.GetDamage() * bonus;
    }
}
```

---

## 📊 Сравнение с альтернативными подходами

| Подход | Сложность | Гибкость | Условная логика	 | Переиспользование |
|-----------------|-----------|-----------|-----------|-----------|
| Декоратор | Средняя | ★★★★★ | ✅ Легко | ★★★★★ |
| Наследование | Низкая | ★☆☆☆☆ | ❌ Сложно | ★☆☆☆☆ |
| Композиция (список баффов) | Высокая | ★★★★☆ | ❌ Сложно | ★★★☆☆ |
| Компонентная система (ECS) | Очень высокая | ★★★★★ | ✅ Легко | ★★★★★ |

---

## 🧠 Ключевые выводы

1. Декоратор позволяет добавлять функциональность динамически, не изменяя существующие классы.
2. Композиция вместо наследования — декораторы хранят ссылку на обернутый объект, а не наследуют его поведение.
3. Порядок декораторов важен — цепочка вычислений идет от внешнего к внутреннему.
4. Декоратор и Спецификация — два паттерна, которые часто работают вместе (как в `DoubleDamageBuff`).
5. Когда использовать:
   - ✅ Нужно добавлять поведение динамически
   - ✅ Комбинации эффектов заранее неизвестны
   - ✅ Нельзя изменять исходный код класса
   - ❌ Не использовать при малом количестве вариаций (проще добавить флаги)
  
---

## 🏆 Итог

#### Данный проект является эталонным примером реализации паттерна Декоратор для игровой механики. Он демонстрирует:

- Правильную архитектуру с разделением ответственности
- Реальную бизнес-логику (баффы в RPG)
- Возможность легкого расширения без модификации существующего кода
- Принцип открытости/закрытости (Open/Closed Principle)

---

## 👨‍💻 Автор

Vladimir Vaize | [GitHub](https://github.com/VladimirVaize) | [Telegram Channel](https://t.me/rigelstudio_gamedev)

---

### ⭐ Если этот проект помог вам понять паттерн Декоратор, поставьте звезду!
