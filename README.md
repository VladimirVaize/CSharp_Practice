# CSharp Practice 
![C#](https://img.shields.io/badge/C%23-239120?logo=csharp&logoColor=white&style=flat)
![.NET](https://img.shields.io/badge/.NET-512BD4?logo=dotnet&logoColor=white&style=flat)
![GitHub](https://img.shields.io/badge/VladimirVaize-grey?logo=github&style=flat)

**Коллекция практических задач по C# с решениями, охватывающих базовые концепции языка.**

Этот репозиторий содержит консольные проекты, имитирующие игровые механики, для отработки ключевых тем C#: массивы, циклы, ссылочные типы и др.

**Технологии:** .NET, C#
***
## 📑 Содержание
- [Список проектов](#-список-проектов)
  - [ОСНОВА](#основа)
  - [РАБОТА С ДАННЫМИ](#работа-с-данными)
  - [АЛГОРИТМЫ СОРТИРОВКИ](#алгоритмы-сортировки)
  - [ПОРОЖДАЮЩИЕ ПАТТЕРНЫ (WIP)](#порождающие-паттерны-creational)
  - [ПОВЕДЕНЧЕСКИЕ ПАТТЕРНЫ (WIP)](#поведенческие-паттерны-behavioral--gamedev)
  - [СТРУКТУРНЫЕ ПАТТЕРНЫ (WIP)](#структурные-паттерны-structural)
  - [ПРОДВИНУТЫЕ ТЕМЫ (WIP)](#продвинутые-темы)
- [Структура репозитория](#-структура-репозитория)
- [Технологии](#-технологии)


алгоритмы-сортировки
## 📋 Список проектов

<table>
    <tr id="основа">
        <td colspan="4", align="center"><strong>ОСНОВА</strong></td>
    </tr>
    <tr>
        <td><em>№</em></td>
        <td><em>Проект / Тема</em></td>
        <td><em>Краткое описание задачи</em></td>
        <td><em>Ссылки</em></td>
    </tr>
    <tr>
        <td>1</td>
        <td><strong>Массив</strong></td>
        <td>Система дропа лута с монстров.</td>
        <td> <a href="Tasks/ArrayTask.txt">Условие</a> | <a href="PracticeProjects/Arrays/Program.cs">Решение</a> </td>
    </tr>
    <tr>
        <td>2</td>
        <td><strong>Двумерный массив</strong></td>
        <td>Головоломка "Пятнашки" (TileSwap).</td>
        <td> <a href="Tasks/MultidimensionalArrayTask.txt">Условие</a> | <a href="PracticeProjects/MultidimensionalArrays/Program.cs">Решение</a> </td>
    </tr>
    <tr>
        <td>3</td>
        <td><strong>Ссылочные типы</strong></td>
        <td>Система инвентаря <br>и модификации предметов.</td>
        <td> <a href="Tasks/ReferenceTypesTask.txt">Условие</a> | <a href="PracticeProjects/ReferenceTypes/Program.cs">Решение</a> </td>
    </tr>
    <tr>
        <td>4</td>
        <td><strong>Расширение массива</strong></td>
        <td>Динамический инвентарь героя.</td>
        <td> <a href="Tasks/ArrayExpansionTask.txt">Условие</a> | <a href="PracticeProjects/ArrayExpansion/Program.cs">Решение</a> </td>
    </tr>
    <tr>
        <td>5</td>
        <td><strong>Цикл Foreach</strong></td>
        <td>Система инвентаря <br>и применения предметов.</td>
        <td> <a href="Tasks/TheForeachCycleTask.txt">Условие</a> | <a href="PracticeProjects/TheForeachCycle/Program.cs">Решение</a> </td>
    </tr>
    <tr>
        <td>6</td>
        <td><strong>ref и out</strong></td>
        <td>Система модификации <br>статистики персонажа.</td>
        <td> <a href="Tasks/RefAndOutTask.txt">Условие</a> | <a href="PracticeProjects/RefAndOut/Program.cs">Решение</a> </td>
    </tr>
    <tr>
        <td>7</td>
        <td><strong>Перегрузка</strong></td>
        <td>Система расчета урона для RPG</td>
        <td> <a href="Tasks/OverloadTask.txt">Условие</a> | <a href="PracticeProjects/Overload/Program.cs">Решение</a> </td>
    </tr>
    <tr>
        <td>8</td>
        <td><strong>List</strong></td>
        <td>Система управления врагами <br>(Wave Manager)</td>
        <td> <a href="Tasks/ListTask.txt">Условие</a> | <a href="PracticeProjects/List/Program.cs">Решение</a> </td>
    </tr>
    <tr>
        <td>9</td>
        <td><strong>Queue - FIFO</strong></td>
        <td>Система спавна мобов <br>(Mobile Spawn Queue)</td>
        <td> <a href="Tasks/QueueTask.txt">Условие</a> | <a href="PracticeProjects/Queue/Program.cs">Решение</a> </td>
    </tr>
    <tr>
        <td>10</td>
        <td><strong>Stack - FILO</strong></td>
        <td>Система отката действий <br>(Memory Scrolls History)</td>
        <td> <a href="Tasks/StackTask.txt">Условие</a> | <a href="PracticeProjects/Stack/Program.cs">Решение</a> </td>
    </tr>
    <tr>
        <td>11</td>
        <td><strong>Dictionary</strong></td>
        <td>Инвентарь и экономика для RPG</td>
        <td> <a href="Tasks/DictionaryTask.txt">Условие</a> | <a href="PracticeProjects/Dictionary/Program.cs">Решение</a> </td>
    </tr>
    <tr>
        <td>12</td>
        <td><strong>Отношения в ООП</strong></td>
        <td>Рефакторинг и реализация <br>"Боевого пса"</td>
        <td> <a href="Tasks/RelationshipsAndTypingOOPTask.txt">Условие</a> | <a href="PracticeProjects/RelationshipsAndTypingOOP/Program.cs">Решение</a> </td>
    </tr>
    <tr>
        <td>13</td>
        <td><strong>Абстракция <br>(Полиморфизм)</strong></td>
        <td>Прототип системы квестов</td>
        <td> <a href="Tasks/PolymorphismTask.txt">Условие</a> | <a href="PracticeProjects/Polymorphism/Program.cs">Решение</a> </td>
    </tr>
    <tr>
        <td>14</td>
        <td><strong>Классы и объекты</strong></td>
        <td>Система прокачки оружия</td>
        <td> <a href="Tasks/ClassesAndObjectsTask.txt">Условие</a> | <a href="PracticeProjects/ClassesAndObjects/Program.cs">Решение</a> </td>
    </tr>
    <tr>
        <td>15</td>
        <td><strong>Поля и модификаторы <br>доступа</strong></td>
        <td>Разработка модуля "Броня Героя" <br>для RPG-игры</td>
        <td> <a href="Tasks/AccessFieldsAndModifiersTask.txt">Условие</a> | <a href="PracticeProjects/AccessFieldsAndModifiers/Program.cs">Решение</a> </td>
    </tr>
    <tr>
        <td>16</td>
        <td><strong>Интерфейсы</strong></td>
        <td>Система взаимодействия <br>с объектами в игре <br>"Космическая база"</td>
        <td> <a href="Tasks/InterfacesTask.txt">Условие</a> | <a href="PracticeProjects/Interfaces/Program.cs">Решение</a> </td>
    </tr>
    <tr>
        <td>17</td>
        <td><strong>Абстрактные классы</strong></td>
        <td>Система магических артефактов</td>
        <td> <a href="Tasks/AbstractClassesTask.txt">Условие</a> | <a href="PracticeProjects/AbstractClasses/Program.cs">Решение</a> </td>
    </tr>
    <tr>
        <td>18</td>
        <td><strong>Структуры</strong></td>
        <td>Звездный Курьер: <br>Оптимизация данных корабля</td>
        <td> <a href="Tasks/StructuresTask.txt">Условие</a> | <a href="PracticeProjects/Structures/Program.cs">Решение</a> </td>
    </tr>
    <tr>
        <td>19</td>
        <td><strong>Upcasting и <br>Downcasting</strong></td>
        <td>Система инвентаря для RPG</td>
        <td> <a href="Tasks/UpcastingAndDowncastingTask.txt">Условие</a> | <a href="PracticeProjects/UpcastingAndDowncasting/Program.cs">Решение</a> </td>
    </tr>
    <tr>
        <td>20</td>
        <td><strong>enum</strong></td>
        <td>Система состояний врага <br>(Enemy AI State Machine)</td>
        <td> <a href="Tasks/EnumTask.txt">Условие</a> | <a href="PracticeProjects/Enum/Program.cs">Решение</a> </td>
    </tr>
    <tr>
        <td>21</td>
        <td><strong>LINQ</strong></td>
        <td>Система поиска и фильтрации <br>предметов в инвентаре <br>(Inventory System)</td>
        <td> <a href="Tasks/LINQTask.txt">Условие</a> | <a href="PracticeProjects/LINQ/Program.cs">Решение</a> </td>
    </tr>
    <tr>
        <td>22</td>
        <td><strong>Обобщения <br>(Generics)</strong></td>
        <td>Универсальная система <br>пула объектов <br>(Object Pool) для игры</td>
        <td> <a href="Tasks/GenericsTask.txt">Условие</a> | <a href="PracticeProjects/Generics/Program.cs">Решение</a> </td>
    </tr>
    <tr>
        <td>23</td>
        <td><strong>Делегаты и события</strong></td>
        <td>Система достижений <br>(Achievement System)</td>
        <td> <a href="Tasks/DelegatesAndEventsTask.txt">Условие</a> | <a href="PracticeProjects/DelegatesAndEvents/Program.cs">Решение</a> </td>
    </tr>
    <tr>
        <td>24</td>
        <td><strong>Async/await</strong></td>
        <td>Асинхронный загрузчик уровней <br>с эмуляцией долгих операций <br>(Async Level Loader)</td>
        <td> <a href="Tasks/AsyncAndAwaitTask.txt">Условие</a> | <a href="PracticeProjects/AsyncAndAwait/Program.cs">Решение</a> </td>
    </tr>
    <tr>
        <td></td>
        <td><strong></strong></td>
        <td> Асинхронная кулинарная книга <br>(Easy) </td>
        <td> <a href="Tasks/AsyncAndAwaitEasyTask.txt">Условие</a> | <a href="PracticeProjects/AsyncAndAwaitEasy/Program.cs">Решение</a> </td>
    </tr>
    <tr>
        <td> 25 </td>
        <td><strong> Обработка ошибок <br>(Exceptions) </strong></td>
        <td> Система крафта с защитой <br>от ошибок игрока </td>
        <td> <a href="Tasks/ExceptionsTask.txt">Условие</a> | <a href="PracticeProjects/Exceptions/Program.cs">Решение</a> </td>
    </tr>
</table>

###

<table>
    <tr id="работа-с-данными">
        <td colspan="4", align="center"><strong>РАБОТА С ДАННЫМИ</strong></td>
    </tr>
    <tr>
        <td><em>№</em></td>
        <td><em>Проект / Тема</em></td>
        <td><em>Краткое описание задачи</em></td>
        <td><em>Ссылки</em></td>
    </tr>
    <tr>
        <td> 26 </td>
        <td><strong> Сериализация <br>(JSON, XML, бинарная) </strong></td>
        <td> Система сохранения и загрузки <br>прогресса в RPG игре <br>(Save/Load System) </td>
        <td> <a href="Tasks/SerializationTask.txt">Условие</a> | <a href="PracticeProjects/Serialization/Program.cs">Решение</a> </td>
    </tr>
    <tr>
        <td></td>
        <td colspan="2">
            <strong>
                Бинарная (Binary)<br>В .NET 5+ считается небезопасным и устаревшим
            </strong>
        </td>
        <td align="center"> <a href="PracticeProjects/Serialization/Binary/Program.cs">Решение</a> </td>
    </tr>
    <tr>
        <td></td>
        <td colspan="2">
            <strong>
                XML - Медленна, избыточна, неэффективна по памяти, 
                <br>легко читается игроками, что ведёт к читерству.
            </strong>
        </td>
        <td align="center"> <a href="PracticeProjects/Serialization/XML/Program.cs">Решение</a> </td>
    </tr>
    <tr>
        <td></td>
        <td colspan="2">
            <strong>
                Json
            </strong>
        </td>
        <td align="center"> <a href="PracticeProjects/Serialization/Json/Program.cs">Решение</a> </td>
    </tr>
    <tr>
        <td> 27 </td>
        <td><strong> Работа с файловой системой <br>(System.IO) </strong></td>
        <td> Система сохранения и <br>загрузки настроек игры <br>(Game Settings) </td>
        <td align="center"> <a href="Tasks/SystemIOTask.txt">Условие</a></td>
    </tr>
    <tr>
        <td></td>
        <td colspan="2">
            <strong>
                Program.cs
                <br>точка входа в программу. 
                <br>Инициализирует менеджер настроек, 
                <br>показывает выбор профиля и открывает меню настроек.
            </strong>
        </td>
        <td align="center"> <a href="PracticeProjects/SystemIO/Program.cs">Решение</a> </td>
    </tr>
    <tr>
        <td></td>
        <td colspan="2">
            <strong>
                SettingsManager.cs
                <br>статический класс
                <br>для управления профилями и настройками. 
                <br>Создает папки, сохраняет/загружает JSON-файлы, 
                <br>создает бэкапы, переключает профили 
                <br>и применяет настройки к консоли.
            </strong>
        </td>
        <td align="center"> <a href="PracticeProjects/SystemIO/Managers/SettingsManager.cs">Решение</a> </td>
    </tr>
    <tr>
        <td></td>
        <td colspan="2">
            <strong>
                DifficultyLevel.cs
                <br>перечисление (enum) с уровнями сложности: 
                <br>Easy, Normal, Hard, Nightmare.
            </strong>
        </td>
        <td align="center"> <a href="PracticeProjects/SystemIO/Models/DifficultyLevel.cs">Решение</a> </td>
    </tr>
    <tr>
        <td></td>
        <td colspan="2">
            <strong>
                GameSettings.cs
                <br>модель данных, хранящая настройки игрока: 
                <br>имя, громкость, цвет фона, 
                <br>сложность, список любимых героев.
            </strong>
        </td>
        <td align="center"> <a href="PracticeProjects/SystemIO/Models/GameSettings.cs">Решение</a> </td>
    </tr>
    <tr>
        <td></td>
        <td colspan="2">
            <strong>
                SettingsMenu.cs
                <br>пользовательский интерфейс для изменения настроек. 
                <br>Позволяет редактировать все параметры, 
                <br>вызывает сохранение и загрузку через 
                <br>SettingsManager.
            </strong>
        </td>
        <td align="center"> <a href="PracticeProjects/SystemIO/UI/SettingsMenu.cs">Решение</a> </td>
    </tr>
  <tr>
        <td> 28 </td>
        <td><strong> Управление ресурсами <br>(IDisposable, using, <br>финализаторы) </strong></td>
        <td> Система сохранения и <br>логирования игровых сессий <br>(Session Saver & Logger) </td>
        <td> <a href="Tasks/ResourceManagementTask.txt">Условие</a> | <a href="PracticeProjects/ResourceManagement/Program.cs">Решение</a> </td>
    </tr>
</table>

###

<table>
    <tr id="алгоритмы-сортировки">
        <td colspan="4", align="center"><strong>АЛГОРИТМЫ СОРТИРОВКИ (WIP)</strong></td>
    </tr>
    <tr>
        <td><em>№</em></td>
        <td><em>Проект / Тема</em></td>
        <td><em>Краткое описание задачи</em></td>
        <td><em>Ссылки</em></td>
    </tr>
</table>

###

<table>
    <tr id="порождающие-паттерны-creational">
        <td colspan="4", align="center"><strong>ПОРОЖДАЮЩИЕ ПАТТЕРНЫ (Creational) (WIP)</strong></td>
    </tr>
    <tr>
        <td><em>№</em></td>
        <td><em>Проект / Тема</em></td>
        <td><em>Краткое описание задачи</em></td>
        <td><em>Ссылки</em></td>
    </tr>
</table>

###

<table>
    <tr id="поведенческие-паттерны-behavioral--gamedev">
        <td colspan="4", align="center"><strong>ПОВЕДЕНЧЕСКИЕ ПАТТЕРНЫ (Behavioral) + GameDev (WIP)</strong></td>
    </tr>
    <tr>
        <td><em>№</em></td>
        <td><em>Проект / Тема</em></td>
        <td><em>Краткое описание задачи</em></td>
        <td><em>Ссылки</em></td>
    </tr>
</table>

###

<table>
    <tr id="структурные-паттерны-structural">
        <td colspan="4", align="center"><strong>СТРУКТУРНЫЕ ПАТТЕРНЫ (Structural) (WIP)</strong></td>
    </tr>
    <tr>
        <td><em>№</em></td>
        <td><em>Проект / Тема</em></td>
        <td><em>Краткое описание задачи</em></td>
        <td><em>Ссылки</em></td>
    </tr>
</table>

###

<table>
    <tr id="продвинутые-темы">
        <td colspan="4", align="center"><strong>ПРОДВИНУТЫЕ ТЕМЫ (WIP)</strong></td>
    </tr>
    <tr>
        <td><em>№</em></td>
        <td><em>Проект / Тема</em></td>
        <td><em>Краткое описание задачи</em></td>
        <td><em>Ссылки</em></td>
    </tr>
</table>


## 📁 Структура репозитория

```
CSharpPractice/
├── PracticeProjects/     # Решения задач
│   ├── Arrays/
│   │   └── Program.cs
│   ├── MultidimensionalArrays/
│   │   └── Program.cs
│   └── ...
└── Tasks/                # Условия задач
    ├── ArrayTask.txt
    ├── MultidimensionalArrayTask.txt
    └── ...
```

## 🛠 Технологии

- **Язык:** C#
- **Платформа:** .NET 6+
- **IDE:** Совместимо с Visual Studio, VS Code, Rider
