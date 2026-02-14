using System;
using System.Collections.Generic;

namespace Overload
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DamageCalculator.Test();
        }
    }

    static class DamageCalculator
    {
        static Random rand = new Random();
        public static float CalculateDamage(int baseDamage, float attackerAttackStat, float defenderDefenseStat)
        {
            if (defenderDefenseStat <= 0)
                return baseDamage * 2;

            return baseDamage * (attackerAttackStat / defenderDefenseStat);
        }

        public static float CalculateDamage(int baseDamage, float attackerAttackStat, float defenderDefenseStat, float criticalMultiplier, float criticalChance)
        {
            if (defenderDefenseStat <= 0)
                return baseDamage * 2;

            if (criticalChance < 0 || criticalChance > 1)
                throw new ArgumentOutOfRangeException(nameof(criticalChance), "Шанс крита должен быть от 0 до 100");

            float damage = CalculateDamage(baseDamage, attackerAttackStat, defenderDefenseStat);

            if (rand.NextDouble() <= criticalChance)
                damage *= criticalMultiplier;

            if (damage < 0)
                damage = 0;

            return damage;
        }

        public static float CalculateDamage(int baseDamage, float attackerAttackStat, float defenderDefenseStat, string damageType, Dictionary<string, float> defenderResistances)
        {
            if(defenderResistances == null)
                throw new ArgumentNullException(nameof(defenderResistances));

            if (defenderDefenseStat <= 0)
                return baseDamage * 2;

            float damage = CalculateDamage(baseDamage, attackerAttackStat, defenderDefenseStat);

            if (defenderResistances.ContainsKey(damageType))
                damage *= (1 - defenderResistances[damageType]);

            if (damage < 0)
                damage = 0;

            return damage;
        }

        public static float CalculateDamage(int baseDamage, float attackerAttackStat, float defenderDefenseStat, double distanceFromEpicenter, double maxRadius)
        {
            if (defenderDefenseStat <= 0)
                return baseDamage * 2;

            if (maxRadius <= 0)
                throw new ArgumentOutOfRangeException(nameof(maxRadius), "Радиус должен быть положительным.");

            if (distanceFromEpicenter > maxRadius)
                return 0;

            double damage = CalculateDamage(baseDamage, attackerAttackStat, defenderDefenseStat);

            damage *= 1 - distanceFromEpicenter / maxRadius;

            if (damage < 0)
                damage = 0;

            return (float)damage;
        }

        public static void Test()
        {
            int baseDamage = 50;
            float atk = 35f;
            float def = 25f;

            Dictionary<string, float> resistances = new Dictionary<string, float>
            { {"Fire", 0.5f}, {"Frost", 0.2f}};
            Console.WriteLine("=== Тест системы урона ===");

            float dmg1 = CalculateDamage(baseDamage, atk, def);
            Console.WriteLine($"1. Мечом по голему: {dmg1:F2} урона.");

            float dmg2 = CalculateDamage(baseDamage, atk, def, 1.5f, 0.75f);
            string critMessage = dmg2 > CalculateDamage(baseDamage, atk, def) ? "КРИТИЧЕСКИЙ УДАР!" : "Обычный удар.";
            Console.WriteLine($"2. Выстрел с шансом крита (75%)... {critMessage} {dmg2:F2} урона!");

            float dmg3 = CalculateDamage(baseDamage, atk, def, "Fire", resistances);
            Console.WriteLine($"3. Огненный шар по ледяному элементалю (сопр. 50%): {dmg3:F2} урона.");

            float dmg4 = CalculateDamage(baseDamage, atk, def, 2.0, 5.0);
            Console.WriteLine($"4. Взрыв гранаты (цель в 2м из 5м): {dmg4:F2} урона.");
        }
    }
}
