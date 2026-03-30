using State.State;
using System;

namespace State
{
    public class Enemy
    {
        public string Name { get; private set; }
        public float Health { get; private set; }
        public float DetectionRange { get; private set; }
        public float AttackRange { get; private set; }
        public int Damage { get; private set; }
        public float MoveSpeed { get; private set; }
        public float X { get; private set; } = 0;
        public float Y { get; private set; } = 0;

        public IdleState IdleState { get; private set; }
        public ChaseState ChaseState { get; private set; }
        public AttackState AttackState { get; private set; }

        private EnemyState currentState;

        public Enemy(string name, float health, float detectionRange, float attackRange,int damage, float moveSpeed)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Health = health;
            DetectionRange = detectionRange;
            AttackRange = attackRange;
            Damage = damage;
            MoveSpeed = moveSpeed;

            IdleState = new IdleState(this);
            ChaseState = new ChaseState(this);
            AttackState = new AttackState(this);

            currentState = IdleState;
            currentState.Enter();
        }

        public void ChangeState(EnemyState newState)
        {
            currentState.Exit();
            currentState = newState;
            currentState.Enter();
        }

        public void Update(Player player)
        {
            currentState.Update(player);
        }

        public void MoveTowardsPlayer(Player player)
        {
            float deltaX = player.X - X;
            float deltaY = player.Y - Y;
            float distance = (float)Math.Sqrt(deltaX * deltaX + deltaY * deltaY);

            if (distance > 0.01f)
            {
                float step = Math.Min(MoveSpeed / 2, distance);
                X += (deltaX / distance) * step;
                Y += (deltaY / distance) * step;
            }
        }

        public void AttackPlayer(Player player)
        {
            player.TakeDamage(Damage);
        }

        public float GetDistanceToPlayer(Player player)
        {
            double deltaX = player.X - X;
            double deltaY = player.Y - Y;

            return (float)Math.Sqrt(deltaX * deltaX + deltaY * deltaY);
        }

        public string CurrentStateName()
        {
            switch (currentState)
            {
                case IdleState IdleState:
                    return "Покой";
                case ChaseState ChaseState:
                    return "Преследование";
                case AttackState AttackState:
                    return "Атака";
                default:
                    return "Unknown";
            }
        }
    }
}
