using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Xemio.GameLight.Entities
{
    public class EntityEnvironment
    {
        private readonly IList<Entity> _entities;

        public IReadOnlyList<Entity> Entities => new ReadOnlyCollection<Entity>(this._entities);

        public event EventHandler<EntityAddedEventArgs> EntityAdded;
        public event EventHandler<EntityRemovedEventArgs> EntityRemoved;

        public EntityEnvironment()
        {
            this._entities = new List<Entity>();
        }

        public void Add(Entity entity)
        {
            entity.Environment = this;
            this._entities.Add(entity);

            this.EntityAdded?.Invoke(this, new EntityAddedEventArgs(entity));
        }

        public bool Remove(Entity entity)
        {
            var result = this._entities.Remove(entity);

            if (result)
            {
                entity.Environment = null;
                this.EntityRemoved?.Invoke(this, new EntityRemovedEventArgs(entity));
            }

            return result;
        }

        public void Tick(double elapsed)
        {
            foreach (var entity in new List<Entity>(this.Entities))
            {
                entity.Tick(elapsed);
            }
        }

        public void Render()
        {
            foreach (var entity in new List<Entity>(this.Entities))
            {
                entity.Render();
            }
        }
    }

    public class EntityAddedEventArgs : EventArgs
    {
        public EntityAddedEventArgs(Entity entity)
        {
            this.Entity = entity;
        }

        public Entity Entity { get; }
    }

    public class EntityRemovedEventArgs : EventArgs
    {
        public EntityRemovedEventArgs(Entity entity)
        {
            this.Entity = entity;
        }

        public Entity Entity { get; }
    }
}