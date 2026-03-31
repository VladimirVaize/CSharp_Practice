using System;

namespace Command.Commands
{
    public class AttackCommand : ICommand
    {
        private Enemy _enemy;
        private int _damage;
        private int _previousHealth;

        public AttackCommand(Enemy enemy, int damage)
        {
            _enemy = enemy ?? throw new ArgumentNullException(nameof(enemy));
            _damage = Math.Max(0, damage);
        }

        public void Execute()
        {
            _previousHealth = _enemy.Health;
            _enemy.TakeDamage(_damage);
            Console.WriteLine($"\nИгрок атакует врага. Нанесено {_damage} урона. Здоровье врага: {_previousHealth} -> {_enemy.Health}");
        }

        public void Undo()
        {
            if (_enemy.Health == _previousHealth)
            {
                Console.WriteLine("Невозможно отменить: состояние не изменилось");
                return;
            }

            _enemy.Health = _previousHealth;
            Console.WriteLine($"Отменено: атака. Враг восстановил {_damage} здоровья. Здоровье врага: {_enemy.Health}");
        }
    }
}
