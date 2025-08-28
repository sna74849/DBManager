using DBManager;
using System.Collections.Generic;

namespace ShoppingCart.Models.Dao
{
    /// <summary>
    /// Abstract base class for DAOs (Data Access Objects).
    /// Provides explicit implementations of <see cref="IReadableDao{T}"/> 
    /// and <see cref="IWritableDao{T}"/> for a specific entity type.
    /// Derived classes must implement abstract methods such as <c>Find</c> and <c>Insert</c>.
    /// </summary>
    /// <typeparam name="TEntity">The target entity type.</typeparam>
    public abstract class BaseEntityDao<TEntity> : IReadableDao<TEntity>, IWritableDao<TEntity>
        where TEntity : class
    {
        #region IRead<TEntity> explicit implementation

        /// <summary>
        /// Retrieves the entity that matches the specified primary key.
        /// This method calls <see cref="Find"/>.
        /// </summary>
        /// <param name="pkeys">The values of the primary key.</param>
        /// <returns>
        /// The matching entity, or <c>null</c> if no match is found.
        /// </returns>
#nullable enable
        TEntity? IReadableDao<TEntity>.Find(params object[] pkeys)
        {
            return Find(pkeys);
        }

        /// <summary>
        /// Retrieves all entities.
        /// This method calls <see cref="Find"/>.
        /// </summary>
        /// <returns>A list of entities.</returns>
        List<TEntity> IReadableDao<TEntity>.Find()
        {
            return Find();
        }

        /// <summary>
        /// Retrieves a list of entities that match the specified primary key.
        /// This method calls <see cref="FindBy"/>.
        /// </summary>
        /// <param name="pkeys">The values of the primary key.</param>
        /// <returns>A list of matching entities.</returns>
        List<TEntity> IReadableDao<TEntity>.FindBy(params object[] pkeys)
        {
            return FindBy(pkeys);
        }

        #endregion

        #region IWrite<TEntity> explicit implementation

        /// <summary>
        /// Inserts the specified entity.
        /// This method calls <see cref="Insert"/>.
        /// </summary>
        /// <param name="t">The entity to insert.</param>
        /// <returns>The number of rows affected by the operation.</returns>
        int IWritableDao<TEntity>.Insert(TEntity t)
        {
            return Insert(t);
        }

        /// <summary>
        /// Updates the specified entity.
        /// This method calls <see cref="Update"/>.
        /// </summary>
        /// <param name="t">The entity to update.</param>
        /// <returns>The number of rows affected by the operation.</returns>
        int IWritableDao<TEntity>.Update(TEntity t)
        {
            return Update(t);
        }

        /// <summary>
        /// Deletes the specified entity.
        /// This method calls <see cref="Delete"/>.
        /// </summary>
        /// <param name="t">The entity to delete.</param>
        /// <returns>The number of rows affected by the operation.</returns>
        int IWritableDao<TEntity>.Delete(TEntity t)
        {
            return Delete(t);
        }

        #endregion

        #region Protected abstract methods (to be implemented in derived classes)

        /// <inheritdoc/>
        protected abstract TEntity? Find(params object[] pkeys);

        /// <inheritdoc/>
        protected abstract List<TEntity> Find();

        /// <inheritdoc/>
        protected abstract List<TEntity> FindBy(params object[] pkeys);

        /// <inheritdoc/>
        protected abstract int Insert(TEntity t);

        /// <inheritdoc/>
        protected abstract int Update(TEntity t);

        /// <inheritdoc/>
        protected abstract int Delete(TEntity t);

        #endregion
    }
}
