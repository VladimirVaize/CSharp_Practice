# 🎵 Pattern Service Locator

> Учебная реализация паттерна Service Locator на C# для консольной игры

---

## 📖 О проекте

Этот проект демонстрирует реализацию паттерна **Service Locator** в контексте разработки игр. 
**Service Locator** предоставляет глобальную точку доступа к сервисам (например, звуковому менеджеру) 
без необходимости передавать ссылки через конструкторы всех игровых объектов.

---

## 🎯 Проблема, которую решает паттерн


### БЕЗ Service Locator: звуковой менеджер нужно передавать ВЕЗДЕ

```csharp
public class Player
{
    private IAudioService _audio;
    public Player(IAudioService audio) => _audio = audio; // утомительно...
}

public class Enemy
{
    private IAudioService _audio;
    public Enemy(IAudioService audio) => _audio = audio; // дублирование...
}

// В Main приходится создавать и передавать один и тот же объект
var audio = new SoundManager();
var player = new Player(audio);
var enemy = new Enemy(audio); // утомительно при 20+ объектах
```

### С Service Locator: доступ к сервису из любого места

```csharp
public class Player
{
    public void Attack()
    {
        ServiceLocator.GetAudioService().PlaySound("sword_swing");
    }
}
```

---

## 🏗️ Архитектура проекта

### Схема работы

```
┌─────────────────────────────────────────────────────────────────┐
│                         PROGRAM.MAIN                            │
│  ┌─────────────────────────────────────────────────────────┐    │
│  │ ServiceLocator.RegisterAudioService(new SoundManager()) │    │
│  └─────────────────────────────────────────────────────────┘    │
└─────────────────────────────────────────────────────────────────┘
                              │
                              ▼
┌─────────────────────────────────────────────────────────────────┐
│                      SERVICE LOCATOR                            │
│  ┌─────────────────────────────────────────────────────────┐    │
│  │           private static IAudioService _audioService    │    │
│  ├─────────────────────────────────────────────────────────┤    │
│  │ + RegisterAudioService(service)  : void                 │    │
│  │ + GetAudioService()              : IAudioService        │    │
│  └─────────────────────────────────────────────────────────┘    │
└─────────────────────────────────────────────────────────────────┘
                              │
                              ▼
┌─────────────────────────────────────────────────────────────────┐
│                       IAudioService                             │
│  ┌─────────────────────────────────────────────────────────┐    │
│  │ + PlaySound(soundName)  : void                          │    │
│  │ + SetVolume(volume)     : void                          │    │
│  └─────────────────────────────────────────────────────────┘    │
└─────────────────────────────────────────────────────────────────┘
                              △
                              │
                              │ implements
                              │
┌─────────────────────────────────────────────────────────────────┐
│                       SoundManager                              │
│  ┌─────────────────────────────────────────────────────────┐    │
│  │ - _currentVolume : float                                │    │
│  ├─────────────────────────────────────────────────────────┤    │
│  │ + PlaySound(soundName)  : void                          │    │
│  │ + SetVolume(volume)     : void                          │    │
│  └─────────────────────────────────────────────────────────┘    │
└─────────────────────────────────────────────────────────────────┘
                              │
                              │ доступ через ServiceLocator
                              │
         ┌────────────────────┼────────────────────┐
         │                    │                    │
         ▼                    ▼                    ▼
   ┌──────────┐        ┌──────────┐        ┌──────────┐
   │  Player  │        │  Enemy   │        │   Main   │
   │ Attack() │        │TakeDamage│        │SetVolume │
   └──────────┘        └──────────┘        └──────────┘
```

### Диаграмма классов

```
┌─────────────────────┐         ┌─────────────────────────┐
│   «interface»       │         │   ServiceLocator        │
│   IAudioService     │         ├─────────────────────────┤
├─────────────────────┤         │ - _audioService         │
│ + PlaySound()       │         ├─────────────────────────┤
│ + SetVolume()       │         │ + RegisterAudioService()│
└──────────┬──────────┘         │ + GetAudioService()     │
           │                    └─────────────────────────┘
           │ implements
           │
┌──────────▼──────────┐
│    SoundManager     │
├─────────────────────┤
│ - _currentVolume    │
├─────────────────────┤
│ + PlaySound()       │
│ + SetVolume()       │
└─────────────────────┘
```

---

## 📁 Структура проекта

```
PatternServiceLocator/
│
├── Audio/
│   ├── IAudioService.cs          # Интерфейс сервиса
│   ├── ServiceLocator.cs         # Сервис-локатор (точка доступа)
│   └── SoundManager.cs           # Реализация звукового сервиса
│
├── Player.cs                     # Игрок (использует звуки)
├── Enemy.cs                      # Враг (использует звуки)
└── Program.cs                    # Точка входа (регистрация + демо)
```

---

## 🚀 Пример использования

### 1. Регистрация сервиса при запуске игры

```csharp
ServiceLocator.RegisterAudioService(new SoundManager());
```

### 2. Использование в любом месте кода

```csharp
var player = new Player();
player.Attack();  // Выведет: "Игрок атакует!"
                  //          "Воспроизведение звука: sword_swing"
```

### 3. Управление сервисом из любого места

```csharp
ServiceLocator.GetAudioService().SetVolume(0.5f);
```

### Вывод в консоли

```
Игрок атакует!
Воспроизведение звука: sword_swing
Враг получил урон!
Воспроизведение звука: enemy_hurt
Громкость установлена на 50%
Игрок атакует!
Воспроизведение звука: sword_swing
```

---

## 🔧 Расширение возможностей

### ➕ Добавление нового сервиса (например, логирования)

```csharp
// 1. Создай интерфейс
public interface ILoggingService
{
    void Log(string message);
    void LogWarning(string message);
    void LogError(string message);
}

// 2. Реализуй сервис
public class ConsoleLogger : ILoggingService
{
    public void Log(string message) => Console.WriteLine($"[INFO] {message}");
    public void LogWarning(string message) => Console.WriteLine($"[WARN] {message}");
    public void LogError(string message) => Console.WriteLine($"[ERROR] {message}");
}

// 3. Добавь в ServiceLocator
public static class ServiceLocator
{
    private static IAudioService _audioService;
    private static ILoggingService _loggingService; // новый сервис
    
    public static void RegisterLoggingService(ILoggingService service) =>
        _loggingService = service ?? throw new ArgumentNullException(nameof(service));
    
    public static ILoggingService GetLoggingService() =>
        _loggingService ?? throw new InvalidOperationException("Logging service not registered");
}
```

### 🔄 Замена реализации во время выполнения

```csharp
// Обычный звуковой менеджер
ServiceLocator.RegisterAudioService(new SoundManager());

// Игровой процесс...

// Переключение на "премиум" версию с крутыми эффектами
ServiceLocator.RegisterAudioService(new PremiumSoundManager());

// Все последующие звуки будут через PremiumSoundManager
player.Attack(); // Теперь со спецэффектами!
```

### 🎮 Расширение SoundManager новыми методами

```csharp
public interface IAudioService
{
    void PlaySound(string soundName);
    void SetVolume(float volume);
    void StopAllSounds();      // новый метод
    void PlayLooping(string soundName); // новый метод
}

public class SoundManager : IAudioService
{
    // ... существующий код ...
    
    public void StopAllSounds()
    {
        Console.WriteLine("Все звуки остановлены");
    }
    
    public void PlayLooping(string soundName)
    {
        Console.WriteLine($"Запущен зацикленный звук: {soundName}");
    }
}
```

---

## ⚠️ Важные замечания

### Когда Service Locator полезен:

- ✅ Прототипирование и быстрая разработка
- ✅ Инструменты разработчика (дебаг-меню, консольные команды)
- ✅ Сервисы, которые гарантированно существуют всё время игры

### Когда лучше избегать Service Locator:

- ❌ В крупных проектах с командой разработчиков (скрывает зависимости)
- ❌ При Unit-тестировании (сложно подменять зависимости)
- ❌ Когда сервис может отсутствовать в некоторых сценах/режимах

---

## 🔗 Связанные паттерны

| Паттерн | Отличие |
|------------|-----------------------------------|
| Singleton | Гарантирует один экземпляр, но не абстрагируется через интерфейс |
| Dependency Injection | Зависимости передаются явно (через конструктор), а не скрыто |
| Factory | Создает объекты, но не управляет их глобальным доступом |
| Service Locator | Предоставляет глобальный доступ к сервисам (этот паттерн) |

---

## 👨‍💻 Автор

Vladimir Vaize | [GitHub](https://github.com/VladimirVaize) | [Telegram Channel](https://t.me/rigelstudio_gamedev)

---

### ⭐ Если этот проект был полезен, поставьте звезду на GitHub!

