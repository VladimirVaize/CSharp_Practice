using System;
using System.Collections.Generic;

namespace PatternComposite.Composite
{
    public class Group : IGameEntity
    {
        private List<IGameEntity> _entities;

        public Group()
        {
            _entities = new List<IGameEntity>();
        }

        public void Add(IGameEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            _entities.Add(entity);
        }

        public void Remove(IGameEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            _entities.Remove(entity);
        }

        public void TakeDamage(int damage)
        {
            if (damage <= 0)
                return;

            foreach (var entity in _entities)
            {
                entity.TakeDamage(damage);
            }
        }

        public int GetTotalHealth()
        {
            int totalHealth = 0;

            foreach (var entity in _entities)
            {
                totalHealth += entity.GetTotalHealth();
            }

            return totalHealth;
        }
    }
}
