using System;

namespace RelationshipsAndTypingOOP
{
    internal class Program
    {
        static void Main(string[] args)
        {
            GameEngine engine = new GameEngine();
            Hero player = new Hero("Артур", 100);
            WarDog dog = new WarDog("Пёсель", 40, player);
            Mob monster = new Mob("Гоблин", 50);

            Console.WriteLine("Задание: 1");
            Console.WriteLine("=======================================================\n");

            engine.Heal(monster, 10);
            engine.Heal(player, 10);

            engine.HeroAttackEntity(player, player, 10);
            engine.HeroAttackEntity(player, monster, 10);

            Console.WriteLine();

            player.ShowInfo();
            monster.ShowInfo();

            Console.WriteLine("\nЗадание: 2");
            Console.WriteLine("=======================================================\n");

            dog.Bite(monster);
            player.TakeDamage(200);
            dog.Bite(monster);

            Console.WriteLine();

            player.ShowInfo();
            monster.ShowInfo();
            dog.ShowInfo();

        }
    }

    public abstract class Entity
    {
        public string Name { get; private set; }
        public int Health { get; private set; }
        public int MaxHealth { get; private set; }

        public bool isDead => Health <= 0;

        protected Entity (string name, int health)
        {
            Name = name;
            Health = health;
            MaxHealth = health;
        }

        public void SetHealth(int health)
        {
            if(health < 0)
                health = 0;

            Health = Math.Min(health, MaxHealth);
        }

        public virtual void TakeDamage(int damage)
        {
            Health = Math.Max(0, Health - damage);
            Console.WriteLine($"{Name} получил {damage} урона. Осталось здоровья: {Health}/{MaxHealth}");

            if(isDead)
                Console.WriteLine($"{Name} погиб! Причина: получен смертельный урон.");
        }

        public void ShowInfo()
        {
            Console.WriteLine($"{Name}: {Health}/{MaxHealth}");
        }
    }

    public class Hero : Entity
    {
        public Hero(string name, int health) : base(name, health) { }
    }

    public class Mob : Entity
    {
        public Mob(string name, int health) : base(name, health) { }
    }

    public class WarDog : Hero
    {
        private Hero _owner;

        private const int BaseDamage = 15;
        private const int RageDamageBonus = 5;

        public WarDog(string name, int health, Hero owner) : base(name, health)
        {
            _owner = owner ?? throw new ArgumentNullException(nameof(owner), "У пса должен быть хозяин!");
        }

        public void Bite(Mob target)
        {
            if (target == null)
            {
                Console.WriteLine("Нет цели для атаки!");
                return;
            }

            int damage = BaseDamage;
            if (_owner?.Health <= 0)
                damage += RageDamageBonus;

            Console.WriteLine($"{Name} кусает {target.Name}");
            target.TakeDamage(damage);
        }

        public override void TakeDamage(int damage)
        {
            base.TakeDamage(damage);
            Console.WriteLine($"{Name} жалобно скулит.");
        }
    }

    public class GameEngine
    {
        public void Heal (Entity target, int amount)
        {
            if (target is Hero)
            {
                if(amount < 0)
                {
                    Console.WriteLine("Нельзя вылечить отрицательным значением!");
                    return;
                }
                target.SetHealth(target.Health + amount);
                Console.WriteLine($"{target.Name} вылечен на {amount} здоровья. Текущее здоровье {target.Health}/{target.MaxHealth}");
            }
            else
            {
                Console.WriteLine("Нельзя вылечить монстра!");
            }
        }

        public void HeroAttackEntity(Hero attacker, Entity target, int damage)
        {
            if (target == attacker)
            {
                Console.WriteLine("Нельзя атаковать себя");
                return;
            }

            target.TakeDamage(damage);
        }
    }
}
