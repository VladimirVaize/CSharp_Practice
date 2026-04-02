# Abstract Factory Pattern — RPG Equipment System

## 📌 Описание

Этот проект демонстрирует реализацию порождающего паттерна проектирования **Абстрактная фабрика (Abstract Factory)** на языке C#.

Контекст: RPG-игра, в которой игрок выбирает фракцию (Люди, Эльфы, Орки, Нежить). Каждая фракция предоставляет свой уникальный набор стартовой экипировки:
- Оружие
- Броня
- Расходуемый предмет (зелье/эликсир)

Паттерн позволяет создавать семейства связанных объектов без привязки к конкретным классам. Добавление новой фракции не требует изменения существующего кода — достаточно создать новые классы продуктов и фабрику.

---

## 🧩 Структура проекта

```
AbstractFactory/
│
├── AbstractFactory/ # Интерфейсы паттерна
│ ├── IWeapon.cs # Интерфейс оружия
│ ├── IArmor.cs # Интерфейс брони
│ ├── IConsumable.cs # Интерфейс расходуемых предметов
│ └── IEquipmentFactory.cs # Абстрактная фабрика
│
├── Human/ # Фракция "Люди"
│ ├── HumanEquipmentFactory.cs
│ ├── HumanWeapon.cs
│ ├── HumanArmor.cs
│ └── HumanConsumable.cs
│
├── Elf/ # Фракция "Эльфы"
│ ├── ElfEquipmentFactory.cs
│ ├── ElfWeapon.cs
│ ├── ElfArmor.cs
│ └── ElfConsumable.cs
│
├── Orc/ # Фракция "Орки"
│ ├── OrcEquipmentFactory.cs
│ ├── OrcWeapon.cs
│ ├── OrcArmor.cs
│ └── OrcConsumable.cs
│
├── Undead/ # Фракция "Нежить" (бонус)
│ ├── UndeadEquipmentFactory.cs
│ ├── UndeadWeapon.cs
│ ├── UndeadArmor.cs
│ └── UndeadConsumable.cs
│
├── Hero.cs # Класс героя, использующий фабрику
└── Program.cs # Точка входа, демонстрация работы
```

---

## 🎮 Как это работает

### 1. Интерфейсы продуктов

Каждый тип экипировки имеет свой интерфейс:

```csharp
public interface IWeapon
{
    string Name { get; }
    int Damage { get; }
    void ShowInfo();
    void Attack();
}

public interface IArmor
{
    string Name { get; }
    int Protection { get; }
    void ShowInfo();
    void Defend();
}

public interface IConsumable
{
    string Name { get; }
    int Power { get; }
    void ShowInfo();
    void Use();
}
```

### 2. Абстрактная фабрика
Фабрика определяет методы для создания всех продуктов:

```csharp
public interface IEquipmentFactory
{
    IWeapon CreateWeapon();
    IArmor CreateArmor();
    IConsumable CreateConsumable();
}
```

### 3. Конкретные фабрики
Каждая фракция реализует свою фабрику, создавая соответствующие предметы:

```csharp
public class HumanEquipmentFactory : IEquipmentFactory
{
    public IWeapon CreateWeapon() => new HumanWeapon();
    public IArmor CreateArmor() => new HumanArmor();
    public IConsumable CreateConsumable() => new HumanConsumable();
}
```

### 4. Класс Hero
Герой получает фабрику в конструкторе и использует её для создания экипировки:

```csharp
public class Hero
{
    public IWeapon Weapon { get; private set; }
    public IArmor Armor { get; private set; }
    public IConsumable Consumable { get; private set; }

    public Hero(IEquipmentFactory factory)
    {
        Weapon = factory.CreateWeapon();
        Armor = factory.CreateArmor();
        Consumable = factory.CreateConsumable();
    }

    public void ShowEquipment()
    {
        Console.WriteLine("Экипировка:");
        Weapon.ShowInfo();
        Armor.ShowInfo();
        Consumable.ShowInfo();
    }

    public void UseConsumable()
    {
        Console.WriteLine("Использование предмета:");
        Consumable.Use();
    }
}
```

## 🚀 Пример использования

```csharp
Hero human = new Hero(new HumanEquipmentFactory());
Hero elf = new Hero(new ElfEquipmentFactory());
Hero orc = new Hero(new OrcEquipmentFactory());
Hero undead = new Hero(new UndeadEquipmentFactory());

human.ShowEquipment();
human.UseConsumable();
```

### Вывод:

```
=== Герой фракции Люди ===
Экипировка:
- Меч: наносит рубящий удар! Урон: 15
- Кожаная броня: защищает от легких ударов. Защита: 10
- Целебное зелье: восстанавливает 50 HP.
Использование предмета:
Восстанавливает 50 HP.

=== Герой фракции Эльфы ===
Экипировка:
- Лук: выпускает стрелу с расстояния! Урон: 7
- Легкая броня: позволяет уклоняться от атак. Защита: 5
- Эликсир маны: восстанавливает 30 MP.
Использование предмета:
Восстанавливает 30 MP.

=== Герой фракции Орки ===
Экипировка:
- Топор: наносит мощный рубящий удар! Урон: 25
- Тяжелая броня: поглощает большую часть урона. Защита: 45
- Зелье ярости: увеличивает урон на 50% на 10 секунд
Использование предмета:
Увеличивает урон на 50% на 10 секунд.

=== Бонус-задание: Герой фракции Нежить ===
Экипировка:
- Коса: наносит проклятый удар! Урон: 12
- Гнилая броня: отпугивает врагов своим видом. Защита: 4
- Зелье распада: наносит 10 урона врагу в течении 5 сек.
Использование предмета:
Наносит 10 урона врагу в течении 5 сек.
```

## ✨ Ключевые преимущества паттерна
- Гибкость - легко добавить новую фракцию, не меняя существующий код
- Инкапсуляция - клиент (Hero) не знает о конкретных классах продуктов
- Согласованность - гарантирует, что предметы одной фракции сочетаются друг с другом
- Тестируемость - можно подставить мок-фабрику для юнит-тестов

## 📚 Изучаемые темы
- Порождающие паттерны проектирования
- Абстрактная фабрика (Abstract Factory)
- Принцип открытости/закрытости (Open/Closed Principle)
- Инкапсуляция и полиморфизм в C#

---

## 👨‍💻 Автор

Vladimir Vaize | [GitHub](https://github.com/VladimirVaize) | [Telegram Channel](https://t.me/rigelstudio_gamedev)

---

### ⭐ Если этот проект был полезен, поставьте звезду на GitHub!
