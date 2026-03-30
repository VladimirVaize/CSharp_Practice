using System;

namespace State.State
{
    public class IdleState : EnemyState
    {
        public IdleState(Enemy enemy) : base(enemy) { }

        public override void Enter()
        {
            Console.WriteLine($"{enemy.Name} входит в состояние Покоя");
        }

        public override void Update(Player player)
        {
            float distance = enemy.GetDistanceToPlayer(player);

            Console.WriteLine($"{enemy.Name} стоит и осматривается...");

            if (player.Health > 0 && distance < enemy.DetectionRange)
            {
                Console.WriteLine($"{enemy.Name} заметил игрока!");
                enemy.ChangeState(enemy.ChaseState);
            }
        }

        public override void Exit()
        {
            Console.WriteLine($"{enemy.Name} выходит из состояния Покоя");
        }
    }
}
