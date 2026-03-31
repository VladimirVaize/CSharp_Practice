using Command.Helpers;
using System;

namespace Command.Commands
{
    public class MoveCommand : ICommand
    {
        private Player _player;
        private int _dx, _dy;
        private int _previousX, _previousY;

        public MoveCommand(Player player, int dx, int dy)
        {
            _player = player ?? throw new ArgumentNullException(nameof(player));
            _dx = dx;
            _dy = dy;
        }

        public void Execute()
        {
            _previousX = _player.X;
            _previousY = _player.Y;
            _player.Move(_dx, _dy);
            Console.WriteLine($"\nИгрок перемещается на {DirectionHelper.GetDirectionName(_dx, _dy)}");
            Console.WriteLine($"Позиция: X=[{_previousX} -> {_player.X}], Y=[{_previousY} -> {_player.Y}]");
        }

        public void Undo()
        {
            if (_player.X == _previousX && _player.Y == _previousY)
            {
                Console.WriteLine("Невозможно отменить: состояние не изменилось");
                return;
            }

            _player.X = _previousX;
            _player.Y = _previousY;
            Console.WriteLine($"Отменено: перемещение на {DirectionHelper.GetDirectionName(_dx, _dy)}");
            Console.WriteLine($"Позиция: X={_player.X}, Y={_player.Y}");
        }
    }
}
