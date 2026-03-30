using System;

namespace State.State
{
    public class ChaseState : EnemyState
    {
        public ChaseState(Enemy enemy) : base(enemy) { }

        public override void Enter()
        {
            Console.WriteLine($"{enemy.Name} начинает преследование!");
        }

        public override void Update(Player player)
        {
            float distance = enemy.GetDistanceToPlayer(player);
            Console.WriteLine($"{enemy.Name} преследует игрока. Расстояние: {distance:F2}");

            enemy.MoveTowardsPlayer(player);

            if (player.Health > 0 && distance < enemy.AttackRange)
            {
                Console.WriteLine($"{enemy.Name} достиг игрока!");
                enemy.ChangeState(enemy.AttackState);
            }
            else if (distance > enemy.DetectionRange)
            {
                Console.WriteLine($"Игрок {player.Name} убежал.");
                enemy.ChangeState(enemy.IdleState);
            }
        }

        public override void Exit()
        {
            Console.WriteLine($"{enemy.Name} прекращает преследование");
        }
    }
}
