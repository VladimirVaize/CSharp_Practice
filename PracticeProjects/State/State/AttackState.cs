using System;

namespace State.State
{
    public class AttackState : EnemyState
    {
        public AttackState(Enemy enemy) : base(enemy) { }

        public override void Enter()
        {
            Console.WriteLine($"{enemy.Name} входит в состояние Атаки!");
        }

        public override void Update(Player player)
        {
            float distance = enemy.GetDistanceToPlayer(player);

            if (distance < enemy.AttackRange)
            {
                Console.WriteLine($"{enemy.Name} наносит игроку {player.Name} - {enemy.Damage} урона.");
                enemy.AttackPlayer(player);
                if(player.Health <= 0)
                {
                    Console.WriteLine("Игрок побежден!");
                    enemy.ChangeState(enemy.IdleState);
                }
            }
            else
            {
                Console.WriteLine($"{enemy.Name}: игрок убежал!");
                enemy.ChangeState(enemy.ChaseState);
            }
        }

        public override void Exit()
        {
            Console.WriteLine($"{enemy.Name} выходит из состояния Атаки");
        }
    }
}
