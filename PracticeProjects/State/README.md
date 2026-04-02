# Паттерн State. Конечный автомат (FSM) для врага в RPG

## 📝 Описание

Данный проект демонстрирует реализацию паттерна **`State`** (Состояние) на языке C#.  
В качестве примера реализован **конечный автомат (`Finite State Machine`)** для поведения врага в RPG-игре.

Враг может находиться в одном из трех состояний, автоматически переключаясь между ними в зависимости от расстояния до игрока.

## 🎮 Схема состояний
```
                           ┌─────────────────┐
                           │    IdleState    │
                           │     (Покой)     │
                           └────────┬────────┘
                                    │
                         расстояние < DetectionRange
                                    ▼
                           ┌─────────────────┐
                           │    ChaseState   │
                           │ (Преследование) │
                           └────────┬────────┘
                                    │
                   ┌────────────────┴────────────────┐
                   │                                 │
        расстояние < AttackRange          расстояние > DetectionRange
                   │                                 │
                   ▼                                 ▼
          ┌─────────────────┐                ┌─────────────────┐
          │   AttackState   │                │    IdleState    │
          │     (Атака)     │ ◄───────────── │     (Покой)     │
          └─────────────────┘   расстояние   └─────────────────┘
                                     =
                                AttackRange
```
## 🚀 Как это работает

### Состояния:

| Состояние | Описание | Условия перехода |
|-----------|----------|------------------|
| **`IdleState`** | Враг стоит и осматривается | Переход в `ChaseState`, если игрок в радиусе обнаружения |
| **`ChaseState`** | Враг преследует игрока | Переход в `AttackState` при достаточном сближении;<br>возврат в `IdleState`, если игрок убежал |
| **`AttackState`** | Враг атакует игрока | Переход в `ChaseState`, если игрок вышел из радиуса атаки |

### Логика переходов:

```csharp
// IdleState → ChaseState
if (distance < DetectionRange) → ChangeState(ChaseState)

// ChaseState → AttackState
if (distance < AttackRange) → ChangeState(AttackState)

// ChaseState → IdleState  
if (distance > DetectionRange) → ChangeState(IdleState)

// AttackState → ChaseState
if (distance >= AttackRange) → ChangeState(ChaseState)
```

## 📁 Структура проекта
```
State/
├── Program.cs              # Точка входа, игровой цикл
├── Enemy.cs                # Класс врага, управление состояниями
├── Player.cs               # Класс игрока
└── State/                  # Папка с состояниями
    ├── EnemyState.cs       # Абстрактный базовый класс
    ├── IdleState.cs        # Состояние покоя
    ├── ChaseState.cs       # Состояние преследования
    └── AttackState.cs      # Состояние атаки
```

## 📚 Используемые концепции C#

- Абстрактные классы и наследование
- Полиморфизм
- Инкапсуляция
- Паттерн проектирования `State`
- Конечный автомат (`Finite State Machine`)
- Работа с классами и объектами

---

## 👨‍💻 Автор

Vladimir Vaize | [GitHub](https://github.com/VladimirVaize) | [Telegram Channel](https://t.me/rigelstudio_gamedev)

---

### ⭐ Если этот проект был полезен, поставьте звезду на GitHub!
