namespace DBManager.Framework
{
    /// <summary>
    /// Defines write operations (insert, update, delete) for an entity or DTO.
    /// </summary>
    /// <typeparam name="T">
    /// The type of entity or DTO to operate on. Must be a reference type.
    /// </typeparam>
    public interface IWritableDao<T> where T : class
    {
        /// <summary>
        /// Inserts the specified entity or DTO into the data store.
        /// </summary>
        /// <param name="t">The entity or DTO to insert.</param>
        /// <returns>The number of rows affected.</returns>
        int Insert(T t);

        /// <summary>
        /// Updates the contents of the specified entity or DTO in the data store.
        /// </summary>
        /// <param name="t">The entity or DTO to update.</param>
        /// <returns>The number of rows affected.</returns>
        int Update(T t);

        /// <summary>
        /// Deletes the entity or DTO that matches the specified primary key from the data store.
        /// </summary>
        /// <param name="t">
        /// The entity or DTO to delete.  
        /// For composite keys, specify multiple values in order.
        /// </param>
        /// <returns>The number of rows affected.</returns>
        int Delete(T t);
    }
}
