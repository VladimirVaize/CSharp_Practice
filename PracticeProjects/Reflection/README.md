# 🔍 GameDebugger — Reflection в действии

[![C#](https://img.shields.io/badge/C%23-12-blue.svg)](https://docs.microsoft.com/en-us/dotnet/csharp/)
[![.NET](https://img.shields.io/badge/.NET-8.0-purple.svg)](https://dotnet.microsoft.com/)

**GameDebugger** — это утилита для отладки игр, построенная на Reflection. Она позволяет инспектировать и управлять любыми объектами во время выполнения программы, даже если их поля и методы приватные.

> 🎯 **Реальный сценарий:** Представь, что тестировщик говорит: *"У врага здоровье упало ниже нуля"*. С GameDebugger ты можешь сделать дамп объекта врага и увидеть все его поля — включая приватные `_health`, `_maxHealth`, `_isAlive` — без остановки игры и без изменения кода.

---

## 📐 Схема работы

```
┌─────────────────────────────────────────────────────────────────────────────┐
│                     GAMEDEBUGGER (Reflection API)                           │
├─────────────────────────────────────────────────────────────────────────────┤
│                                                                             │
│ ┌──────────────┐       ┌──────────────────────────────────────────────────┐ │
│ │ Любой        │       │ DumpObject                                       │ │
│ │ объект       │ ───▶ │ ┌────────────────────────────────────────────┐   │ │
│ │ (Player,     │       │ │ 1. obj.GetType() → получаем Type           │   │ │
│ │ Quest,       │       │ │ 2. GetFields(BindingFlags...) → все поля   │   │ │
│ │ Enemy...)    │       │ │ 3. GetProperties(...) → все свойства       │   │ │
│ └──────────────┘       │ │ 4. Фильтрация backing-полей авто-свойств   │   │ │
│                        │ │ 5. Вывод в консоль: имя, тип, значение     │   │ │
│                        │ └────────────────────────────────────────────┘   │ │
│                        │                                                  │ │
│                        │ Вывод:                                           │ │
│                        │ ═══ Дамп: Игрок ═══                              │ │
│                        │ Тип: Player                                      │ │
│                        │ Поля:                                            │ │
│                        │ - (private) _health : Int32 = 75                 │ │
│                        │ - (private) _maxHealth : Int32 = 100             │ │
│                        │ Свойства:                                        │ │
│                        │ - Name : String = "Геральт" (readonly)           │ │
│                        │ - Level : Int32 = 5                              │ │
├────────────────────────┼──────────────────────────────────────────────────┤ │
│                        │                                                  │ │
│ ┌──────────────┐       │ InvokeMethod                                     │ │
│ │ Объект +     │       │ ┌────────────────────────────────────────────┐   │ │
│ │ имя метода   │ ───▶ │ │ 1. GetMethods().Where(m => m.Name == ...)  │   │ │
│ │ + параметры  │       │ │ 2. Проверка количества параметров          │   │ │
│ └──────────────┘       │ │ 3. method.Invoke(obj, parameters)          │   │ │
│                        │ │ 4. Обработка возвращаемого значения        │   │ │
│                        │ │ 5. Перехват TargetInvocationException      │   │ │
│                        │ └────────────────────────────────────────────┘   │ │
│                        │                                                  │ │
│                        │ Вывод:                                           │ │
│                        │ Метод 'TakeDamage' успешно выполнен              │ │
│                        │ Геральт получил 30 урона! HP: 70/100             │ │
│                        │                                                  │ │
├────────────────────────┴──────────────────────────────────────────────────┘ │
│                                                                             |
|             ◀── Работает с ПУБЛИЧНЫМИ и ПРИВАТНЫМИ членами ──▶             │
│                                                                             |
└─────────────────────────────────────────────────────────────────────────────┘
```

---

## 📖 API Справка

**`GameDebugger.DumpObject(object obj, string objectName)`**

Выводит в консоль все поля и свойства объекта.

| Параметр | Тип | Описание |
|---------|------|-------------------------|
| `obj` | `object` | Исследуемый объект (может быть `null`) |
| `objectName` | `string` | Отображаемое имя объекта в дампе |

Особенности:

- Показывает приватные поля (отмечены `(private)`)
- Фильтрует технические backing-поля авто-свойств
- Обрабатывает ошибки доступа

**`GameDebugger.InvokeMethod(object obj, string methodName, params object[] parameters)`**

Находит и вызывает метод по имени (включая приватные).

| Параметр | Тип | Описание |
|---------|------|-------------------------|
| `obj` | `object` | Объект, у которого вызывается метод |
| `methodName` | `string` | Имя метода (регистр важен) |
| `parameters` | `params object[]` | Аргументы метода |

Возвращает: `true` — метод найден и выполнен, `false` — ошибка.

---

## 🧩 Примеры расширения

GameDebugger спроектирован так, чтобы его легко было расширять под свои нужды.

### 1. Сохранение дампа в файл (логгирование)

```csharp
public static void DumpObjectToFile(object obj, string objectName, string filePath)
{
    using var writer = new StreamWriter(filePath, append: true);
    var originalOut = Console.Out;
    
    try
    {
        using var stringWriter = new StringWriter();
        Console.SetOut(stringWriter);
        
        DumpObject(obj, objectName);
        
        writer.WriteLine(stringWriter.ToString());
    }
    finally
    {
        Console.SetOut(originalOut);
    }
}
```

### 2. Поиск всех полей определенного типа (например, всех врагов на сцене)

```csharp
public static List<FieldInfo> FindFieldsOfType<T>(object obj)
{
    return obj.GetType()
        .GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance)
        .Where(field => field.FieldType == typeof(T) || field.FieldType.IsSubclassOf(typeof(T)))
        .ToList();
}
```

### 3. Вызов метода с автоматическим подбором перегрузки

```csharp
public static bool InvokeMethodBestMatch(object obj, string methodName, params object[] parameters)
{
    var methods = obj.GetType().GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
        .Where(m => m.Name == methodName)
        .OrderBy(m => m.GetParameters().Length)
        .ToList();
    
    foreach (var method in methods)
    {
        var paramTypes = method.GetParameters().Select(p => p.ParameterType).ToArray();
        if (paramTypes.Length != parameters.Length) continue;
        
        bool match = true;
        for (int i = 0; i < paramTypes.Length; i++)
        {
            if (parameters[i] != null && !paramTypes[i].IsAssignableFrom(parameters[i].GetType()))
            {
                match = false;
                break;
            }
        }
        
        if (match)
        {
            method.Invoke(obj, parameters);
            return true;
        }
    }
    return false;
}
```

### 4. Добавление цветового вывода в консоль

```csharp
// Внутри DumpObject:
Console.ForegroundColor = ConsoleColor.Cyan;
Console.WriteLine($"  - {field.Name} : {field.FieldType.Name}");
Console.ForegroundColor = ConsoleColor.Yellow;
Console.Write(" = ");
Console.ForegroundColor = ConsoleColor.Green;
Console.WriteLine(valueStr);
Console.ResetColor();
```

---

## 🧠 Что такое Reflection (для тех, кто только знакомится)

Reflection — это возможность программы исследовать и изменять свою собственную структуру во время выполнения.

| Возможность | Пример из GameDebugger |
|----------------------|------------------------------------------|
| Узнать тип объекта | `obj.GetType()` |
| Получить список всех полей | `GetFields(BindingFlags.NonPublic \| BindingFlags.Instance)` |
| Прочитать приватное поле | `field.GetValue(obj)` |
| Найти метод по имени | `GetMethods().Where(m => m.Name == "TakeDamage")` |
| Вызвать приватный метод | `method.Invoke(obj, 30)` |

### Когда использовать Reflection в играх:

- 🎮 Системы модов — поиск и загрузка пользовательских классов
- 🛠️ Редакторы уровней — отображение всех полей объекта для редактирования
- 📜 Системы диалогов/квестов — вызов методов по имени из JSON
- 🐛 Отладка — инспекция состояния игры без остановки выполнения
- 💾 Сериализация — сохранение/загрузка любых объектов без дублирования кода

### Когда НЕ стоит использовать Reflection:

- ⚡ В хот-пате (Update-методе) — вызов GetFields() каждый кадр убьет производительность
- 🔒 В коде, где важна безопасность — Reflection может обойти модификаторы доступа
- 📦 В кроссплатформенных AOT-сборках — некоторые платформы (iOS) ограничивают Reflection

---

## 📁 Структура проекта

```
Reflection/
├── GameDebugger.cs          # Основной класс с методами DumpObject и InvokeMethod
├── Player.cs                # Пример класса игрока
├── Quest.cs                 # Пример класса квеста
├── Program.cs               # Демонстрация работы
└── README.md                # Этот файл
```

---

## 🏆 Что демонстрирует этот проект

| Навык | Реализация |
|--------------|---------------------------------|
| ✅ Reflection API | `Type`, `FieldInfo`, `MethodInfo`, `PropertyInfo` |
| ✅ BindingFlags | Комбинирование флагов для доступа к непубличным членам | 
| ✅ Обработка исключений | `TargetInvocationException` и проброс InnerException |
| ✅ Фильтрация данных | Отсеивание backing-полей авто-свойств | 
| ✅ Работа с параметрами | Проверка количества аргументов перед вызовом |
| ✅ Чистый код | Комментарии, осмысленные имена, утилитарные методы |

---

## 🔗 Полезные ссылки
[Microsoft Docs — Reflection in .NET](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/reflection)
[BindingFlags Enumeration](https://docs.microsoft.com/en-us/dotnet/api/system.reflection.bindingflags)

---

## 👨‍💻 Автор

Vladimir Vaize | [GitHub](https://github.com/VladimirVaize) | [Telegram Channel](https://t.me/rigelstudio_gamedev)

---

### ⭐ Если этот проект был полезен, поставьте звезду на GitHub!
