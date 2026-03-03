using System;
using System.Collections.Generic;
using System.Linq;

namespace AbstractClasses
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Player player = new Player("John", 100);
            Enemy goblin = new Enemy("Гоблин", 50, 10);

            Console.WriteLine(new string('-', 25));

            player.Attack(goblin);
            goblin.Attack(player);
            goblin.Attack(player);

            Console.WriteLine(new string('-', 25));

            PickUpArtifact(player, new HealthAmulet());
            PickUpArtifact(player, new BurningBlade());
            PickUpArtifact(player, new LuckStone());

            Console.WriteLine(new string('-', 25));

            player.Attack(goblin);
            goblin.Attack(player);
            goblin.Attack(player);
        }

        static void PickUpArtifact(Player player, BaseArtifact artifact)
        {
            player.AddArtifact(artifact);
            artifact.ApplyEffect(player);
        }
    }

    public class Player
    {
        private Random _random = new Random();

        private string _name;
        private int _health;
        private int _maxHealth;
        private int _baseDamage = 10;
        private List<BaseArtifact> _artifacts = new List<BaseArtifact>();

        public string Name => _name;
        public int Health => _health;
        public int BaseDamage => _baseDamage;
        public List<BaseArtifact> Artifacts => _artifacts;

        public Player(string name, int maxHealth)
        {
            _name = name;
            _maxHealth = Math.Max(maxHealth, 1);
            _health = _maxHealth;
        }

        public void Heal(int healPower)
        {
            _health = Math.Max(_health + healPower, _health);
            _health = Math.Min(_health, _maxHealth);
        }

        public void AddArtifact(BaseArtifact artifact)
        {
            if (artifact != null)
                _artifacts.Add(artifact);
        }

        public void Attack(Enemy enemy)
        {
            Console.WriteLine($"{Name} атакует {enemy.Name} и наносит {BaseDamage} урона.");

            int totalDamage = _baseDamage;

            var blade = _artifacts.OfType<BurningBlade>().FirstOrDefault(); // Так не надо делать, но чтобы упростить, пусть будет так)
            if (blade != null)
            {
                totalDamage += blade.ExtraFireDamage;
                Console.WriteLine($"[{blade.Name}] поджигает врага! +{blade.ExtraFireDamage} урона.");
            }

            enemy.TakeDamage(totalDamage);
        }

        public void TakeDamage(int damage)
        {
            var stone = Artifacts.OfType<LuckStone>().FirstOrDefault(); // Так не надо делать, но чтобы упростить, пусть будет так)
            if (stone != null)
            {
                if (_random.NextDouble() <= stone.DodgeChance)
                {
                    Console.WriteLine($"{_name} увернулся от атаки.");
                    return;
                }
            }
            int newHealth = _health - damage;
            _health = Math.Min(newHealth < 0 ? 0 : newHealth, _health);
            Console.WriteLine($"{Name} получает {damage} урона. Осталось здоровья: {_health}");
        }
    }

    public class Enemy
    {
        private string _name;
        private int _health;
        private int _maxHealth;
        private int _damage;

        public Enemy(string name, int maxHealth, int damage)
        {
            _name = name;
            _maxHealth = Math.Max(maxHealth, 1);
            _health = _maxHealth;
            _damage = damage;
        }

        public string Name => _name;
        public int Health => _health;
        public int Damage => _damage;

        public void Attack(Player player)
        {
            Console.WriteLine($"{Name} атакует {player.Name} и наносит {_damage} урона.");

            player.TakeDamage(_damage);
        }

        public void TakeDamage(int damage)
        {
            int newHealth = _health - damage;
            _health = Math.Min(newHealth < 0 ? 0 : newHealth, _health);

            Console.WriteLine($"{_name} получил {damage} урона. Здоровье: {_health}");
        }
    }

    public abstract class BaseArtifact
    {
        protected string _name;
        protected string _description;

        public string Name => _name;
        public string Description => _description;

        public abstract void ApplyEffect(Player player);
    }

    public class HealthAmulet : BaseArtifact
    {
        private const int _healPower = 25;

        public HealthAmulet()
        {
            _name = "Амулет лечения";
            _description = $"Восстанавливает {_healPower} здоровья";
        }
        public override void ApplyEffect(Player player)
        {
            player.Heal(_healPower);
            Console.WriteLine($"{player.Name} востановил {_healPower} здоровья благодаря {Name}.");
        }
    }

    public class BurningBlade : BaseArtifact
    {
        private const int _extraFireDamage = 15;

        public int ExtraFireDamage => _extraFireDamage;

        public BurningBlade()
        {
            _name = "Пылающий клинок";
            _description = "Добавляет 5 единиц урона огнем к каждой атаке.";
        }

        public override void ApplyEffect(Player player)
        {
            Console.WriteLine($"{player.Name} теперь владеет {Name}. Атаки наносят дополнительный урон огнем.");
        }
    }

    public class LuckStone : BaseArtifact
    {
        private const float _dodgeChance = 0.25f;

        public float DodgeChance => _dodgeChance;

        public LuckStone()
        {
            _name = "Камень удачи";
            _description = "Дает 25% шанс избежать урона.";
        }

        public override void ApplyEffect(Player player)
        {
            Console.WriteLine($"{player.Name} чувствует прилив удачи. Камень дает шанс уклониться.");
        }
    }
}
