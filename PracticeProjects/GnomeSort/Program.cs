using System;
using System.Collections.Generic;
using System.Linq;

namespace GnomeSort
{
    internal class Program
    {
        private static readonly Random _random = new Random();
        static void Main(string[] args)
        {
            var turnOrder = new TurnOrder();
            try
            {
                turnOrder.AddCombatant(new Combatant("Паладин", 120, false, 100)); turnOrder.AddCombatant(new Combatant("Маг", 115, false, 80));
                turnOrder.AddCombatant(new Combatant("Лучник", 100, false, 85));
                turnOrder.AddCombatant(new Combatant("Гоблин", 80, false, 50));
                turnOrder.AddCombatant(new Combatant("Скелет", 75, false, 45));
                turnOrder.AddCombatant(new Combatant("Эльф", 110, false, 70));
                turnOrder.AddCombatant(new Combatant("Тролль", 60, false, 120));

                Console.WriteLine("=== НАЧАЛО БОЯ ===\n");
                turnOrder.DisplayOrder();

                int stunnedCounter = 0;
                Combatant stunnedCombatant = null;

                for (int turn = 1; turn <= 15; turn++)
                {
                    Console.WriteLine($"\n--- ХОД {turn} ---");

                    if (stunnedCombatant != null && stunnedCounter >= 2)
                    {
                        stunnedCombatant.IsStunned = false;
                        Console.WriteLine($"{stunnedCombatant.Name} пришел в себя после оглушения!");
                        stunnedCombatant = null;
                        stunnedCounter = 0;
                    }
                    else if (stunnedCombatant != null)
                        stunnedCounter++;

                    var currentCombatant = turnOrder.GetNextTurn();

                    if (currentCombatant == null)
                    {
                        Console.WriteLine("Нет доступных для хода участников!");
                        break;
                    }

                    Console.WriteLine($"Ходит: {currentCombatant.Name} (инициатива {currentCombatant.Initiative})");

                    int effectRoll = _random.Next(100);
                    EffectType effect;
                    int value = _random.Next(10, 31);

                    if (effectRoll < 20)
                    {
                        effect = EffectType.Stun;
                        value = 0;
                    }
                    else if (effectRoll < 60)
                        effect = EffectType.Haste;
                    else
                        effect = EffectType.Slow;

                    Combatant target;
                    if (_random.Next(2) == 0)
                    {
                        target = currentCombatant;
                        Console.Write($"Применен эффект: {effect} на самого себя");
                    }
                    else
                    {
                        var otherCombatants = turnOrder.GetCombatants().Where(c => c != currentCombatant).ToList();
                        if (otherCombatants.Any())
                        {
                            target = otherCombatants[_random.Next(otherCombatants.Count)];
                            Console.Write($"Применен эффект: {effect} на {target.Name}");
                        }
                        else
                        {
                            target = currentCombatant;
                            Console.Write($"Применен эффект: {effect} на самого себя");
                        }
                    }

                    try
                    {
                        int oldInitiative = target.Initiative;
                        turnOrder.UpdateInitiative(target, effect, value);

                        if (effect == EffectType.Haste)
                        {
                            Console.WriteLine($" (+{value} инициативы)");
                            Console.WriteLine($"Инициатива {target.Name}: {oldInitiative} -> {target.Initiative}");
                        }
                        else if (effect == EffectType.Slow)
                        {
                            Console.WriteLine($" (-{value} инициативы)");
                            Console.WriteLine($"Инициатива {target.Name}: {oldInitiative} -> {target.Initiative}");
                        }
                        else if (effect == EffectType.Stun)
                        {
                            Console.WriteLine();
                            Console.WriteLine($"{target.Name} оглушен на 2 хода!");
                            stunnedCombatant = target;
                            stunnedCounter = 0;
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"\nОшибка при применении эффекта: {ex.Message}");
                    }
                }
                Console.WriteLine("\n=== БОЙ ЗАВЕРШЕН ===");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }

    public enum EffectType
    {
        Haste,
        Slow,
        Stun
    }

    public class Combatant
    {
        public string Name { get; private set; }
        public int Initiative { get; private set; }
        public bool IsStunned { get; set; }
        public int Health { get; private set; }

        public Combatant(string name, int initiative, bool isStunned, int health)
        {
            Name = name;
            Initiative = initiative;
            IsStunned = isStunned;
            Health = health;
        }

        public void ApplyEffect(EffectType effect, int value)
        {
            if (value < 0)
                throw new ArgumentOutOfRangeException(nameof(value), "Значение не может быть отрицательным");

            switch (effect)
            {
                case EffectType.Haste:
                    Initiative += value;
                    break;
                case EffectType.Slow:
                    Initiative -= value;
                    break;
                case EffectType.Stun:
                    IsStunned = true;
                    break;
                default:
                    throw new ArgumentException($"Неизвестный тип эффекта: {effect}");
            }
        }

        public override string ToString()
        {
            return $"{Name} (инициатива {Initiative}){(IsStunned ? " [ОГЛУШЕН]" : "")}";
        }
    }

    public class TurnOrder
    {
        private List<Combatant> _combatants;

        public TurnOrder() { _combatants = new List<Combatant>(); }

        public void AddCombatant(Combatant combatant)
        {
            _combatants.Add(combatant);
            SortByInitiative();
        }

        public List<Combatant> GetCombatants() { return _combatants.ToList(); }

        public void SortByInitiative()
        {
            int index = 1;

            while (index < _combatants.Count)
            {
                if (index == 0 || _combatants[index].Initiative <= _combatants[index - 1].Initiative)
                    index++;
                else
                {
                    (_combatants[index], _combatants[index - 1]) = (_combatants[index - 1], _combatants[index]);
                    index--;
                }
            }
        }

        public void UpdateInitiative(Combatant target, EffectType effect, int value)
        {
            var combatant = _combatants.FirstOrDefault(c => c.Name == target.Name);

            if (combatant == null)
                throw new InvalidOperationException($"Участник {target.Name} не найден в списке боя");

            combatant.ApplyEffect(effect, value);

            SortByInitiative();
        }

        public Combatant GetNextTurn() { return _combatants.FirstOrDefault(c => !c.IsStunned); }

        public void DisplayOrder()
        {
            if (_combatants.Count == 0)
            {
                Console.WriteLine("Список участников пуст");
                return;
            }

            for (int i = 0; i < _combatants.Count; i++)
                Console.WriteLine($"{i + 1}. {_combatants[i]}");
        }
    }
}
