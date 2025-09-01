using System.Collections.Generic;

namespace DBManager.Framework
{
    /// <summary>
    /// Abstract base class for DAOs (Data Access Objects).
    /// Provides explicit implementations of <see cref="IReadableDao{TEntity}"/> 
    /// and <see cref="IWritableDao{TEntity}"/> for a specific entity type.
    /// Derived classes must implement the protected abstract methods 
    /// such as <c>Find</c>, <c>Fetch</c>, <c>Insert</c>, and <c>Update</c>.
    /// </summary>
    /// <typeparam name="TEntity">The target entity type.</typeparam>
    public abstract class BaseEntityDao<TEntity> : IReadableDao<TEntity>, IWritableDao<TEntity>
        where TEntity : class
    {
        #region IRead<TEntity> explicit implementation

        /// <summary>
        /// Retrieves the entity that matches the specified primary key(s).  
        /// This method calls <see cref="Fetch"/>.
        /// </summary>
        /// <param name="pkeys">The values of the primary key(s).</param>
        /// <returns>
        /// The matching entity, or <c>null</c> if no match is found.
        /// </returns>
#nullable enable
        TEntity? IReadableDao<TEntity>.Fetch(params object[] pkeys)
        {
            return Fetch(pkeys);
        }

        /// <summary>
        /// Retrieves all entities.  
        /// This method calls <see cref="Find()"/>.
        /// </summary>
        /// <returns>A list containing all entities.</returns>
        List<TEntity> IReadableDao<TEntity>.Find()
        {
            return Find();
        }

        /// <summary>
        /// Retrieves a list of entities that match the specified primary key(s).  
        /// This method calls <see cref="Find(object[])"/>.
        /// </summary>
        /// <param name="pkeys">The values of the primary key(s).</param>
        /// <returns>A list of matching entities.</returns>
        List<TEntity> IReadableDao<TEntity>.Find(params object[] pkeys)
        {
            return Find(pkeys);
        }

        #endregion

        #region IWrite<TEntity> explicit implementation

        /// <summary>
        /// Inserts the specified entity.  
        /// This method calls <see cref="Insert(TEntity)"/>.
        /// </summary>
        /// <param name="t">The entity to insert.</param>
        /// <returns>The number of rows affected by the operation.</returns>
        int IWritableDao<TEntity>.Insert(TEntity t)
        {
            return Insert(t);
        }

        /// <summary>
        /// Updates the specified entity.  
        /// This method calls <see cref="Update(TEntity)"/>.
        /// </summary>
        /// <param name="t">The entity to update.</param>
        /// <returns>The number of rows affected by the operation.</returns>
        int IWritableDao<TEntity>.Update(TEntity t)
        {
            return Update(t);
        }

        /// <summary>
        /// Partially updates the record that matches the specified primary key(s).  
        /// This method calls <see cref="Patch(object, object[])"/>.
        /// </summary>
        /// <param name="value">The value or set of values to update.</param>
        /// <param name="pkeys">The values of the primary key(s).</param>
        /// <returns>The number of rows affected by the operation.</returns>
        int IWritableDao<TEntity>.Patch(object value, params object[] pkeys)
        {
            return Patch(value, pkeys);
        }

        /// <summary>
        /// Deletes the record that matches the specified primary key(s).  
        /// This method calls <see cref="Delete(object[])"/>.
        /// </summary>
        /// <param name="pkeys">The values of the primary key(s).</param>
        /// <returns>The number of rows affected by the operation.</returns>
        int IWritableDao<TEntity>.Delete(params object[] pkeys)
        {
            return Delete(pkeys);
        }

        #endregion

        #region Protected abstract methods (to be implemented in derived classes)

        /// <summary>
        /// Retrieves the entity that matches the specified primary key(s).
        /// </summary>
        /// <param name="pkeys">The values of the primary key(s).</param>
        /// <returns>
        /// The matching entity, or <c>null</c> if no match is found.
        /// </returns>
        protected abstract TEntity? Fetch(params object[] pkeys);

        /// <summary>
        /// Retrieves all entities.
        /// </summary>
        /// <returns>A list containing all entities.</returns>
        protected abstract List<TEntity> Find();

        /// <summary>
        /// Retrieves a list of entities that match the specified primary key(s).
        /// </summary>
        /// <param name="pkeys">The values of the primary key(s).</param>
        /// <returns>A list of matching entities.</returns>
        protected abstract List<TEntity> Find(params object[] pkeys);

        /// <summary>
        /// Inserts the specified entity.
        /// </summary>
        /// <param name="t">The entity to insert.</param>
        /// <returns>The number of rows affected by the operation.</returns>
        protected abstract int Insert(TEntity t);

        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <param name="t">The entity to update.</param>
        /// <returns>The number of rows affected by the operation.</returns>
        protected abstract int Update(TEntity t);

        /// <summary>
        /// Deletes the record that matches the specified primary key(s).
        /// </summary>
        /// <param name="pkeys">The values of the primary key(s).</param>
        /// <returns>The number of rows affected by the operation.</returns>
        protected abstract int Delete(object[] pkeys);

        /// <summary>
        /// Partially updates the record that matches the specified primary key(s).
        /// </summary>
        /// <param name="value">The value or set of values to update.</param>
        /// <param name="pkeys">The values of the primary key(s).</param>
        /// <returns>The number of rows affected by the operation.</returns>
        protected abstract int Patch(object value, params object[] pkeys);

        #endregion
    }
}
