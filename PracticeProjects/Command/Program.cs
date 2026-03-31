using Command.Commands;

namespace Command
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Player player = new Player(5, 5, 50);
            Enemy goblin = new Enemy(100);

            InputHandler inputHandler = new InputHandler();

            inputHandler.ExecuteCommand(new MoveCommand(player, 1, 1));
            inputHandler.ExecuteCommand(new MoveCommand(player, -1, 1));
            inputHandler.ExecuteCommand(new MoveCommand(player, -1, 0));
            inputHandler.ExecuteCommand(new MoveCommand(player, 0, 0));

            inputHandler.ExecuteCommand(new AttackCommand(goblin, 10));
            inputHandler.ExecuteCommand(new AttackCommand(goblin, 5));

            inputHandler.ExecuteCommand(new HealCommand(player, 25));

            inputHandler.UndoLastCommand();
            inputHandler.UndoLastCommand();
            inputHandler.UndoLastCommand();
            inputHandler.UndoLastCommand();
            inputHandler.UndoLastCommand();
            inputHandler.UndoLastCommand();
            inputHandler.UndoLastCommand();
            inputHandler.UndoLastCommand();
        }
    }
}
