using System.Collections.Generic;

namespace DBManager.Framework
{
    /// <summary>
    /// Defines read operations (retrieval) for an entity or DTO.
    /// </summary>
    /// <typeparam name="T">
    /// The type of entity or DTO to operate on. Must be a reference type.
    /// </typeparam>
    public interface IReadableDao<T> where T : class
    {
        /// <summary>
        /// Searches for an entity/DTO using the specified primary key.
        /// </summary>
        /// <param name="pkeys">The values of the primary key.</param>
        /// <returns>
        /// The matching entity/DTO, or <c>null</c> if no match is found.
        /// </returns>
#nullable enable
        T? Fetch(params object[] pkeys);

        /// <summary>
        /// Retrieves a list of entities/DTOs that match the specified conditions.
        /// </summary>
        /// <returns>
        /// A list of matching entities/DTOs. Returns an empty list if none are found.
        /// </returns>
        List<T> Find();

        /// <summary>
        /// Searches for entities/DTOs using the specified primary key.
        /// </summary>
        /// <param name="pkeys">The values of the primary key.</param>
        /// <returns>
        /// A list of matching entities/DTOs. Returns an empty list if none are found.
        /// </returns>
        List<T> Find(params object[] pkeys);
    }
}
