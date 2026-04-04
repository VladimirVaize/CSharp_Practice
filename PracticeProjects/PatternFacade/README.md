# 🎮 Pattern Facade — Упрощение запуска игровой сессии

[![C#](https://img.shields.io/badge/C%23-12.0-blue.svg)](https://docs.microsoft.com/en-us/dotnet/csharp/)
[![Pattern](https://img.shields.io/badge/Pattern-Facade-green.svg)](https://refactoring.guru/ru/design-patterns/facade)
[![Status](https://img.shields.io/badge/Status-Completed-brightgreen.svg)]()

---

## 📋 О проекте

Этот проект демонстрирует реализацию паттерна **Фасад (Facade)** на примере консольной игры. Фасад скрывает сложность запуска игровой сессии, объединяя 6 различных подсистем в один простой метод `StartGame()`.

### 🎯 Проблема, которую решает паттерн

Представьте, что при запуске игры нужно:

- Инициализировать логгер
- Проверить лицензию
- Загрузить конфигурацию
- Подключиться к серверу
- Загрузить сохранения
- Запустить таймер сессии

Без фасада клиентский код превращается в хаос из 30-40 строк с кучей проверок и обработки ошибок. Фасад упаковывает всю эту сложность в один вызов.

---

## 🏗️ Схема работы паттерна

```
┌─────────────────────────────────────────────────────────────────────────┐
│                           CLIENT (Program)                              │
│                                                                         │
│ GameFacade game = new GameFacade();                                     │
│ GameResult result = game.StartGame(); ←─── ОДИН ПРОСТОЙ ВЫЗОВ           │
│ game.Shutdown();                                                        │
└────────────────────────────────────┬────────────────────────────────────┘
                                     │
                                     ▼
┌─────────────────────────────────────────────────────────────────────────┐
│                              GAME FACADE                                │
│   ┌─────────────────────────────────────────────────────────────────┐   │
│   │                          StartGame()                            │   │
│   │   ┌─────────┐   ┌─────────┐   ┌─────────┐   ┌─────────┐         │   │
│   │   │ Logger  │ → │License  │ → │ Config  │ → │ Network │ ...     │   │
│   │   │ Init    │   │Validate │   │ Load    │   │Connect  │         │   │
│   │   └─────────┘   └─────────┘   └─────────┘   └─────────┘         │   │
│   └─────────────────────────────────────────────────────────────────┘   │
└─────────────────────────────────────────────────────────────────────────┘
                                      │
        ┌───────────────┬─────────────┼─────────────────┬──────────────┐
        ▼               ▼             ▼                 ▼              ▼
┌─────────────┐ ┌─────────────┐ ┌─────────────┐ ┌──────────────┐ ┌─────────────┐
│ GameLogger  │ │ License     │ │ ConfigLoader│ │NetworkService│ │ SaveSystem  │
│             │ │ Validator   │ │             │ │              │ │             │
│ • Initialize│ │ • Validate  │ │ • LoadConfig│ │ • Connect    │ │ • LoadSave  │
│ • Log       │ │ License     │ │ • GetValue  │ │ • Disconnect │ │             │
│ • Shutdown  │ │             │ │             │ │              │ │             │
└─────────────┘ └─────────────┘ └─────────────┘ └──────────────┘ └─────────────┘
┌─────────────┐
│ GameSession │
│ Timer       │
│ • StartTimer│
│ • StopTimer │
└─────────────┘
```

---

## 📁 Структура проекта

```
PatternFacade/
│
├── Program.cs # Точка входа (клиентский код)
│
├── Facade/ # Папка с фасадом
│ ├── GameFacade.cs # Главный фасад (упрощает запуск)
│ ├── GameResult.cs # Результат запуска (Success/Failure)
│ └── SaveData.cs # DTO для сохранений
│
└── Subsystems/ # Сложные подсистемы
  ├── GameLogger.cs # Логирование
  ├── LicenseValidator.cs # Проверка лицензии
  ├── ConfigLoader.cs # Загрузка конфигурации
  ├── NetworkService.cs # Сетевые подключения
  ├── SaveSystem.cs # Загрузка сохранений
  └── GameSessionTimer.cs # Таймер игровой сессии
```

---

## 🚀 Как это работает

### Клиентский код (до Фасада) — представьте этот ужас:

```csharp
// ❌ БЕЗ ФАСАДА — Кошмар поддержки
var logger = new GameLogger();
logger.Initialize();

var license = new LicenseValidator();
if (!license.ValidateLicense(out string error))
{
    Console.WriteLine(error);
    return;
}

var config = new ConfigLoader();
if (!config.LoadConfig(out error))
{
    logger.Log(error);
    return;
}

var network = new NetworkService();
if (!network.ConnectToServer(out error))
{
    logger.Log(error);
    return;
}

// ... и так далее 20+ строк ...
```

### Клиентский код (с Фасадом) — чистота и простота:

```csharp
// ✅ С ФАСАДОМ — Красота!
GameFacade game = new GameFacade();
GameResult result = game.StartGame();

if (result.IsSuccess)
{
    Console.WriteLine("Игра запущена!");
    Console.ReadLine();
    game.Shutdown();
}
else
{
    Console.WriteLine($"Ошибка: {result.ErrorMessage}");
}
```

### Диаграмма последовательности запуска:

```
Client     GameFacade    Logger    License    Config    Network    SaveSystem    Timer
  │           │           │         │          │          │           │           │
  ├StartGame─►│           │         │          │          │           │           │
  │           ├─Init─────►│         │          │          │           │           │
  │           │           ├─Log──────────────────────────────────────────────────►│
  │           ├─Validate───────────►│          │          │           │           │
  │           │           │         ├─OK───────┤          │           │           │
  │           ├─LoadConfig────────────────────►│          │           │           │
  │           │           │         │          ├─OK───────┤           │           │
  │           ├─Connect──────────────────────────────────►│           │           │
  │           │           │         │          │          ├─OK────────┤           │
  │           ├─LoadSave─────────────────────────────────────────────►│           │
  │           │           │         │          │          │           ├─OK────────┤
  │           ├─StartTimer───────────────────────────────────────────────────────►│
  │           │           │         │          │          │           │           │
  │◄─Success──┤           │         │          │          │           │           │
  │           │           │         │          │          │           │           │
  ├─Shutdown─►│           │         │          │          │           │           │
  │           ├─StopTimer────────────────────────────────────────────────────────►│
  │           ├─Disconnect───────────────────────────────►│           │           │
  │           ├─Shutdown─►│         │          │          │           │           │
  │           │           │         │          │          │           │           │
```

---

## 📦 Ключевые компоненты

### GameFacade — Сердце паттерна

```csharp
public class GameFacade
{
    private GameLogger _logger;
    private LicenseValidator _license;
    private ConfigLoader _config;
    private NetworkService _network;
    private SaveSystem _save;
    private GameSessionTimer _timer;

    public GameResult StartGame()
    {
        // 1. Инициализация логгера
        // 2. Проверка лицензии
        // 3. Загрузка конфигурации
        // 4. Подключение к сети
        // 5. Загрузка сохранений
        // 6. Запуск таймера
        
        // Если любой шаг упал — возвращаем Failure с ошибкой
    }
    
    public void Shutdown() { /* Корректное завершение */ }
}
```

### GameResult — Паттерн Result Object

```csharp
public class GameResult
{
    public bool IsSuccess { get; private set; }
    public string ErrorMessage { get; private set; }
    public SaveData LoadedSave { get; private set; }
    
    // Фабричные методы для удобного создания
    public static GameResult Success(SaveData save) { /* ... */ }
    public static GameResult Failure(string error) { /* ... */ }
}
```

---

## 🔧 Пример расширения (если нужно добавить новую подсистему)

Допустим, ты хочешь добавить AntiCheatSystem:

### Шаг 1: Создай подсистему

```csharp
// Subsystems/AntiCheatSystem.cs
public class AntiCheatSystem
{
    public bool ValidateIntegrity(out string errorMessage)
    {
        Console.WriteLine("[AntiCheat] Проверка целостности игры...");
        errorMessage = "";
        // Логика проверки
        return true;
    }
}
```

### Шаг 2: Добавь в Фасад

```csharp
// Facade/GameFacade.cs
public class GameFacade
{
    private AntiCheatSystem _antiCheat; // Новое поле
    
    public GameFacade()
    {
        // ...
        _antiCheat = new AntiCheatSystem(); // Инициализация
    }
    
    public GameResult StartGame()
    {
        // ... существующие проверки ...
        
        // Новая проверка (после лицензии, перед конфигом)
        if (!_antiCheat.ValidateIntegrity(out string antiCheatError))
        {
            _logger.Log(antiCheatError);
            return GameResult.Failure(antiCheatError);
        }
        
        // ... продолжение ...
    }
}
```

Вот и всё! Клиентский код не меняется — он по-прежнему вызывает StartGame(). В этом магия Фасада.

---

## 📊 Когда использовать Фасад

| ✅ Когда подходит | ❌ Когда не нужен |
|----------------------------------------|----------------------------------------|
| Сложная система с множеством классов | Система из 2-3 простых классов |
| Нужен простой интерфейс для клиента | Клиенту нужен полный контроль над подсистемами |
| Разные уровни абстракции в системе | Вы боитесь, что фасад станет "божественным объектом" |
| Хочется снизить связанность кода | Все подсистемы и так простые | 

---

## 🎯 Преимущества Фасада в этом проекте

1. Простота использования — клиент вызывает 2 метода вместо 15
2. Снижение связанности — клиент ничего не знает о 6 подсистемах
3. Централизованная обработка ошибок — все ошибки обрабатываются в одном месте
4. Легкость тестирования — можно замокать фасад или отдельные подсистемы
5. Упрощение рефакторинга — изменения внутри подсистем не затрагивают клиента

---

## 🖥️ Пример вывода программы

```
[Logger] Инициализация логгера...
[01:23:45] --- Игра запущена 04.03.2026 01:23:45 ---
[01:23:45] Начало запуска игры...
[License] Проверка лицензии...
[01:23:45] Лицензия валидна
[Config] Загрузка конфигурации...
[01:23:45] Конфигурация загружена (версия: 1.2.3)
[Network] Подключение к серверу...
[01:23:45] Подключение к серверу успешно
[SaveSystem] Загрузка последнего сохранения...
[01:23:45] Загружено сохранение: Герой (уровень 5)
[Timer] Сессия началась в 01:23:45
[01:23:45] Игра успешно запущена!

=== ИГРА ЗАПУЩЕНА ===
Загружен персонаж: Герой
Уровень: 5, Здоровье: 85,5

[ИГРА] Нажмите Enter для выхода...

[01:23:50] Завершение игры...
[Timer] Сессия длилась 5,2 секунд
[Network] Отключение от сервера...
[Logger] Завершение работы логгера...
```

---

## 📚 Связанные паттерны

| Паттерн | Связь с Фасадом |
|--------------|----------------------------------------|
| Mediator | Фасад упрощает интерфейс к подсистеме, Медиатор убирает прямые связи между компонентами |
| Singleton | Часто фасад делают синглтоном (но не обязательно) |
| Abstract Factory | Может использоваться вместе с фасадом для создания подсистем |

---

## 👨‍💻 Автор

Vladimir Vaize | [GitHub](https://github.com/VladimirVaize) | [Telegram Channel](https://t.me/rigelstudio_gamedev)

---

### ⭐ Если этот проект был полезен, поставьте звезду на GitHub!

