using System;

namespace Command.Commands
{
    public class HealCommand : ICommand
    {
        private Player _player;
        private int _healPower;
        private int _previousHealth;

        public HealCommand(Player player, int healPower)
        {
            _player = player ?? throw new ArgumentNullException(nameof(player));
            _healPower = Math.Max(0, healPower);
        }

        public void Execute()
        {
            _previousHealth = _player.Health;
            _player.Heal(_healPower);
            Console.WriteLine($"\nИгрок восстановил  здоровье. Восстановлено {_healPower} HP. Здоровье игрока: {_previousHealth} -> {_player.Health}");
        }

        public void Undo()
        {
            if (_player.Health == _previousHealth)
            {
                Console.WriteLine("Невозможно отменить: состояние не изменилось");
                return;
            }

            _player.Health = _previousHealth;
            Console.WriteLine($"Отменено: лечение. Игрок вернулся к {_player.Health} HP (потеряно {_healPower} HP)");
        }
    }
}
