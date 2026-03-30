namespace State.State
{
    public abstract class EnemyState
    {
        protected Enemy enemy;

        public EnemyState(Enemy enemy)
        {
            this.enemy = enemy;
        }

        public abstract void Enter();
        public abstract void Update(Player player);
        public abstract void Exit();
    }
}
