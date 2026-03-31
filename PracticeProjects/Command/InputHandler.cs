using Command.Commands;
using System;
using System.Collections.Generic;

namespace Command
{
    public class InputHandler
    {
        private Stack<ICommand> _commandHistory = new Stack<ICommand>();

        public void ExecuteCommand(ICommand command)
        {
            if (command == null)
            {
                Console.WriteLine("Ошибка: команда не может быть null");
                return;
            }

            command.Execute();
            _commandHistory.Push(command);
        }

        public void UndoLastCommand()
        {
            if (_commandHistory.Count == 0)
            {
                Console.WriteLine("\n> НЕЧЕГО ОТМЕНЯТЬ");
                return;
            }

            Console.WriteLine("\n> ОТМЕНА последнего действия");
            var command = _commandHistory.Pop();
            command.Undo();
        }
    }
}
