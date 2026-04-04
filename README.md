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
  - [ПОРОЖДАЮЩИЕ ПАТТЕРНЫ](#порождающие-паттерны-creational)
  - [ПОВЕДЕНЧЕСКИЕ ПАТТЕРНЫ](#поведенческие-паттерны-behavioral--gamedev)
  - [СТРУКТУРНЫЕ ПАТТЕРНЫ](#структурные-паттерны-structural)
  - [ПРОДВИНУТЫЕ ТЕМЫ (WIP)](#продвинутые-темы)
- [Структура репозитория](#-структура-репозитория)
- [Технологии](#-технологии)

## 📋 Список проектов

<table>
    <tr id="основа">
        <td colspan="4", align="center"><h3><strong>ОСНОВА</strong></h3></td>
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
        <td><strong>Цикл <code>foreach</code></strong></td>
        <td>Система инвентаря <br>и применения предметов.</td>
        <td> <a href="Tasks/TheForeachCycleTask.txt">Условие</a> | <a href="PracticeProjects/TheForeachCycle/Program.cs">Решение</a> </td>
    </tr>
    <tr>
        <td>6</td>
        <td><strong><code>ref</code> и <code>out</code></strong></td>
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
        <td><strong><code>List<></code></strong></td>
        <td>Система управления врагами <br>(Wave Manager)</td>
        <td> <a href="Tasks/ListTask.txt">Условие</a> | <a href="PracticeProjects/List/Program.cs">Решение</a> </td>
    </tr>
    <tr>
        <td>9</td>
        <td><strong><code>Queue</code> - FIFO</strong></td>
        <td>Система спавна мобов <br>(Mobile Spawn Queue)</td>
        <td> <a href="Tasks/QueueTask.txt">Условие</a> | <a href="PracticeProjects/Queue/Program.cs">Решение</a> </td>
    </tr>
    <tr>
        <td>10</td>
        <td><strong><code>Stack</code> - FILO (LIFO)</strong></td>
        <td>Система отката действий <br>(Memory Scrolls History)</td>
        <td> <a href="Tasks/StackTask.txt">Условие</a> | <a href="PracticeProjects/Stack/Program.cs">Решение</a> </td>
    </tr>
    <tr>
        <td>11</td>
        <td><strong><code>Dictionary</code></strong></td>
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
        <td><strong><code>enum</code></strong></td>
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
        <td><strong><code>Async</code>/<code>await</code></strong></td>
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
##
###

<table>
    <tr id="работа-с-данными">
        <td colspan="4", align="center"><h3><strong>РАБОТА С ДАННЫМИ</strong></h3></td>
    </tr>
    <tr>
        <td><em>№</em></td>
        <td><em>Проект / Тема</em></td>
        <td><em>Краткое описание задачи</em></td>
        <td><em>Ссылки</em></td>
    </tr>
    <tr>
        <td> 26 </td>
        <td><strong> Сериализация <br>(<code>JSON</code>, <code>XML</code>, бинарная) </strong></td>
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
        <td><strong> Работа с файловой системой <br>(<code>System.IO</code>) </strong></td>
        <td> Система сохранения и <br>загрузки настроек игры <br>(Game Settings) </td>
        <td align="center"> <a href="Tasks/SystemIOTask.txt">Условие</a></td>
    </tr>
    <tr>
        <td></td>
        <td colspan="2">
            <strong>
                <code>Program.cs</code>
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
                <code>SettingsManager.cs</code>
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
                <code>DifficultyLevel.cs</code>
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
                <code>GameSettings.cs</code>
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
                <code>SettingsMenu.cs</code>
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
        <td><strong> Управление ресурсами <br>(<code>IDisposable</code>, <code>using</code>, <br>финализаторы) </strong></td>
        <td> Система сохранения и <br>логирования игровых сессий <br>(Session Saver & Logger) </td>
        <td> <a href="Tasks/ResourceManagementTask.txt">Условие</a> | <a href="PracticeProjects/ResourceManagement/Program.cs">Решение</a> </td>
    </tr>
</table>

###
##
###

<table>
    <tr id="алгоритмы-сортировки">
        <td colspan="4", align="center"><h3><strong>АЛГОРИТМЫ СОРТИРОВКИ</strong></h3></td>
    </tr>
    <tr>
        <td><em>№</em></td>
        <td><em>Проект / Тема</em></td>
        <td><em>Краткое описание задачи</em></td>
        <td><em>Ссылки</em></td>
    </tr>
    <tr>
        <td>29</td>
        <td><strong>Сортировка пузырьком <br>(<code>Bubble Sort</code>)</strong></td>
        <td>Система лидерборда <br>(таблица рекордов) <br>для инди-игры</td>
        <td> <a href="Tasks/BubbleSortTask.txt">Условие</a> | <a href="PracticeProjects/BubbleSort/Program.cs">Решение</a> </td>
    </tr>
    <tr>
        <td>30</td>
        <td><strong>Шейкерная сортировка <br>(<code>Cocktail Sort</code>)</strong></td>
        <td>Система слоев объектов в 2D игре</td>
        <td> <a href="Tasks/CocktailSortTask.txt">Условие</a> | <a href="PracticeProjects/CocktailSort/Program.cs">Решение</a> </td>
    </tr>
    <tr>
        <td>31</td>
        <td><strong>Сортировка вставками <br>(<code>Insertion Sort</code>)</strong></td>
        <td>Система рейтинга игроков <br>(Leaderboard)</td>
        <td> <a href="Tasks/InsertionSortTask.txt">Условие</a> | <a href="PracticeProjects/InsertionSort/Program.cs">Решение</a> </td>
    </tr>
    <tr>
        <td>32</td>
        <td><strong>Сортировка Шелла <br>(<code>Shell Sort</code>)</strong></td>
        <td>Система приоритетов обработки <br>событий в игровом движке</td>
        <td> <a href="Tasks/ShellSortTask.txt">Условие</a> | <a href="PracticeProjects/ShellSort/Program.cs">Решение</a> </td>
    </tr>
    <tr>
        <td>33</td>
        <td><strong>Сортировка деревом <br>(<code>Tree Sort</code>)</strong></td>
        <td>Система квестов с приоритетами</td>
        <td> <a href="Tasks/TreeSortTask.txt">Условие</a> | <a href="PracticeProjects/TreeSort/Program.cs">Решение</a> </td>
    </tr>
    <tr>
        <td>34</td>
        <td><strong>Пирамидальная сортировка <br>(Кучей) (<code>Heap Sort</code>)</strong></td>
        <td></td>
        <td align="center">^^^^^ | <a href="PracticeProjects/HeapSort/Program.cs">Решение</a></td>
    </tr>
    <tr>
        <td>35</td>
        <td><strong>Сортировка выбором <br>(<code>Selection Sort</code>)</strong></td>
        <td>Система сортировки заказов <br>в таверне</td>
        <td> <a href="Tasks/SelectionSortTask.txt">Условие</a> | <a href="PracticeProjects/SelectionSort/Program.cs">Решение</a> </td>
    </tr>
    <tr>
        <td>36</td>
        <td><strong>Гномья сортировка <br>(<code>Gnome Sort</code>)</strong></td>
        <td>Система порядка хода <br>в пошаговой RPG</td>
        <td><a href="Tasks/GnomeSortTask.txt">Условие</a> | <a href="PracticeProjects/GnomeSort/Program.cs">Решение</a></td>
    </tr>
    <tr>
        <td>37</td>
        <td><strong>Поразрядная сортировка <br>(<code>Radix Sort</code>)</strong></td>
        <td>Система генерации и <br>сортировки подземелья</td>
        <td><a href="Tasks/RadixSortTask.txt">Условие</a> | <a href="PracticeProjects/RadixSort/Program.cs">Решение</a></td>
    </tr>
    <tr>
        <td>38</td>
        <td><strong>Сортировка слиянием <br>(<code>Merge Sort</code>)</strong></td>
        <td>Система сортировки реплеев</td>
        <td><a href="Tasks/MergeSortTask.txt">Условие</a> | <a href="PracticeProjects/MergeSort/Program.cs">Решение</a></td>
    </tr>
    <tr>
        <td>39</td>
        <td><strong>Быстрая сортировка Хоара <br>(<code>Quick Sort</code>)</strong></td>
        <td>Система рендеринга <br>2D-объектов по глубине <br>(Painter's Algorithm)</td>
        <td><a href="Tasks/QuickSortTask.txt">Условие</a> | <a href="PracticeProjects/QuickSort/Program.cs">Решение</a></td>
    </tr>
</table>

###
##
###

<table>
    <tr id="порождающие-паттерны-creational">
        <td colspan="4", align="center"><h3><strong>ПОРОЖДАЮЩИЕ ПАТТЕРНЫ (Creational)</strong></h3></td>
    </tr>
    <tr>
        <td><em>№</em></td>
        <td><em>Проект / Тема</em></td>
        <td><em>Краткое описание задачи</em></td>
        <td><em>Ссылки</em></td>
    </tr>
    <tr>
        <td>40</td>
        <td><strong>Одиночка (<code>Singleton</code>)</strong></td>
        <td>Система логов и отладки <br>(<code>Game Logger</code>)</td>
        <td><a href="Tasks/SingletonTask.txt">Условие</a> | <a href="PracticeProjects/Singleton">Решение</a></td>
    </tr>
    <tr>
        <td>41</td>
        <td><strong>Пул объектов (<code>Object Pool</code>)</strong></td>
        <td>Система частиц (<code>Particle System</code>) <br>для консольной игры</td>
        <td><a href="Tasks/ObjectPoolTask.txt">Условие</a> | <a href="PracticeProjects/ObjectPool">Решение</a></td>
    </tr>
    <tr>
        <td>42</td>
        <td><strong>Абстрактная фабрика <br>(<code>Abstract Factory</code>)</strong></td>
        <td>Система экипировки героя <br>для разных фракций</td>
        <td><a href="Tasks/AbstractFactoryTask.txt">Условие</a> | <a href="PracticeProjects/AbstractFactory">Решение</a></td>
    </tr>
</table>

###
##
###

<table>
    <tr id="поведенческие-паттерны-behavioral--gamedev">
        <td colspan="4", align="center"><h3><strong>ПОВЕДЕНЧЕСКИЕ ПАТТЕРНЫ (Behavioral) + GameDev</strong></h3></td>
    </tr>
    <tr>
        <td><em>№</em></td>
        <td><em>Проект / Тема</em></td>
        <td><em>Краткое описание задачи</em></td>
        <td><em>Ссылки</em></td>
    </tr>
    <tr>
        <td>43</td>
        <td><strong>Состояние (<code>State</code>), <br>(<code>State Machine</code> / <code>FSM</code>)</strong></td>
        <td>Система поведения врага <br>с конечным автоматом</td>
        <td><a href="Tasks/StateTask.txt">Условие</a> | <a href="PracticeProjects/State">Решение</a></td>
    </tr>
    <tr>
        <td>44</td>
        <td><strong>Команда (<code>Command</code>)</strong></td>
        <td>Система управления <br>с отменой действий (<code>Undo System</code>)</td>
        <td><a href="Tasks/CommandTask.txt">Условие</a> | <a href="PracticeProjects/Command">Решение</a></td>
    </tr>
    <tr>
        <td>45</td>
        <td><strong>Стратегия (<code>Strategy</code>)</strong></td>
        <td>Система расчета урона</td>
        <td><a href="Tasks/PatternStrategyTask.txt">Условие</a> | <a href="PracticeProjects/PatternStrategy">Решение</a></td>
    </tr>
    <tr>
        <td>46</td>
        <td><strong>Наблюдатель (<code>Observer</code>)</strong></td>
        <td>Система Достижений (<code>Achievements</code>)</td>
        <td><a href="Tasks/PatternObserverTask.txt">Условие</a> | <a href="PracticeProjects/PatternObserver">Решение</a></td>
    </tr>
    <tr>
        <td>47</td>
        <td><strong>Поведенческое дерево <br>(<code>Behavior Tree</code>)</strong></td>
        <td>Система ИИ для <br>патрулирующего охранника</td>
        <td><a href="Tasks/PatternBehaviorTreeTask.txt">Условие</a> | <a href="PracticeProjects/PatternBehaviorTree">Решение</a></td>
    </tr>
</table>

###
##
###

<table>
    <tr id="структурные-паттерны-structural">
        <td colspan="4", align="center"><h3><strong>СТРУКТУРНЫЕ ПАТТЕРНЫ (Structural)</strong></h3></td>
    </tr>
    <tr>
        <td><em>№</em></td>
        <td><em>Проект / Тема</em></td>
        <td><em>Краткое описание задачи</em></td>
        <td><em>Ссылки</em></td>
    </tr>
    <tr>
        <td>48</td>
        <td><strong>Компоновщик (<code>Composite</code>)</strong></td>
        <td>Система отрядов и боевых групп</td>
        <td><a href="Tasks/PatternCompositeTask.txt">Условие</a> | <a href="PracticeProjects/PatternComposite">Решение</a></td>
    </tr>
    <tr>
        <td>49</td>
        <td><strong>Декоратор (<code>Decorator</code>)</strong></td>
        <td>Система временных баффов <br>и усилений</td>
        <td><a href="Tasks/PatternDecoratorTask.txt">Условие</a> | <a href="PracticeProjects/PatternDecorator">Решение</a></td>
    </tr>
    <tr>
        <td>50</td>
        <td><strong>Фасад (<code>Facade</code>)</strong></td>
        <td>Система запуска игры <br>(<code>Game Startup Facade</code>)</td>
        <td><a href="Tasks/PatternFacadeTask.txt">Условие</a> | <a href="PracticeProjects/PatternFacade">Решение</a></td>
    </tr>
    <tr>
        <td>51</td>
        <td><strong>Посетитель (<code>Visitor</code>)</strong></td>
        <td>Система логирования <br>боевых событий</td>
        <td><a href="Tasks/PatternVisitorTask.txt">Условие</a> | <a href="PracticeProjects/PatternVisitor">Решение</a></td>
    </tr>
</table>

###
##
###

<table>
    <tr id="продвинутые-темы">
        <td colspan="4", align="center"><h3><strong>ПРОДВИНУТЫЕ ТЕМЫ (WIP)</strong></h3></td>
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
