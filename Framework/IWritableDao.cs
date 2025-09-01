namespace DBManager.Framework
{
    /// <summary>
    /// Provides write operations (insert, update, partial update, delete)  
    /// for a given entity or DTO type.
    /// </summary>
    /// <typeparam name="T">
    /// The entity or DTO type to operate on. Must be a reference type.
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
        /// Partially updates the record that matches the specified primary key(s).
        /// </summary>
        /// <param name="value">
        /// The value or set of values to update.  
        /// Can be an anonymous object containing only the properties to change.
        /// </param>
        /// <param name="pkeys">
        /// The primary key(s) used to identify the target record.  
        /// For composite keys, specify the values in order.
        /// </param>
        /// <returns>The number of rows affected.</returns>
        int Patch(object value, params object[] pkeys);

        /// <summary>
        /// Deletes the record that matches the specified primary key(s).
        /// </summary>
        /// <param name="pkeys">
        /// The primary key(s) used to identify the target record.  
        /// For composite keys, specify the values in order.
        /// </param>
        /// <returns>The number of rows affected.</returns>
        int Delete(params object[] pkeys);
    }
}
