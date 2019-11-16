using System;


namespace LampStore.AppCore.Core.Entities
{
    /// <summary>
    /// Base domain entities' class - abstract entity.
    /// This class encapsulate "DB filed" Id which basically does not belong to domain.
    /// </summary>
    public abstract class Entity<TKey>
        where TKey: struct
    {
        /// <summary>
        /// Entity's unique Id.
        /// </summary>
        public TKey Id { get; private set; }


        protected Entity(TKey id)
        {
            Id = id;
        }


        /// <summary>
        /// Change entity Id. Only if current Id wasn't set before.
        /// </summary>
        /// <exception cref="InvalidOperationException">If was set already.</exception>
        public void SetId(TKey id)
        {
            if(IsIdSet())
                throw new InvalidOperationException("Id was set already.");

            Id = id;
        }

        /// <summary>
        /// Is entity's id set already.
        /// </summary>
        public virtual bool IsIdSet()
        {
            return !Equals(Id, default(TKey));
        }
    }
}
