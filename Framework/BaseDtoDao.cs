using System.Collections.Generic;

namespace DBManager.Framework
{
    /// <summary>
    /// Abstract base class for DAOs (Data Access Objects) that operate on DTOs.  
    /// Provides an explicit implementation of <see cref="IReadableDao{TDto}"/> 
    /// for a specific DTO type.  
    /// Derived classes must implement the protected abstract methods 
    /// such as <c>Fetch</c> and <c>Find</c>.
    /// </summary>
    /// <typeparam name="TDto">The type of the target DTO.</typeparam>
    public abstract class BaseDtoDao<TDto> : IReadableDao<TDto> where TDto : class
    {
        #region IRead<TDto> explicit implementation

        /// <summary>
        /// Retrieves the DTO that matches the specified primary key(s).  
        /// This method calls <see cref="Fetch"/>.
        /// </summary>
        /// <param name="pkeys">The values of the primary key(s).</param>
        /// <returns>
        /// The matching DTO, or <c>null</c> if no match is found.
        /// </returns>
#nullable enable
        TDto? IReadableDao<TDto>.Fetch(params object[] pkeys)
        {
            return Fetch(pkeys);
        }

        /// <summary>
        /// Retrieves all DTOs.  
        /// This method calls <see cref="Find()"/>.
        /// </summary>
        /// <returns>A list containing all DTOs.</returns>
        List<TDto> IReadableDao<TDto>.Find()
        {
            return Find();
        }

        /// <summary>
        /// Retrieves a list of DTOs that match the specified primary key(s).  
        /// This method calls <see cref="Find(object[])"/>.
        /// </summary>
        /// <param name="pkeys">The values of the primary key(s).</param>
        /// <returns>A list of matching DTOs.</returns>
        List<TDto> IReadableDao<TDto>.Find(params object[] pkeys)
        {
            return Find(pkeys);
        }

        #endregion

        #region Protected abstract methods (to be implemented in derived classes)

        /// <summary>
        /// Retrieves the DTO that matches the specified primary key(s).
        /// </summary>
        /// <param name="pkeys">The values of the primary key(s).</param>
        /// <returns>
        /// The matching DTO, or <c>null</c> if no match is found.
        /// </returns>
        protected abstract TDto? Fetch(params object[] pkeys);

        /// <summary>
        /// Retrieves all DTOs.
        /// </summary>
        /// <returns>A list containing all DTOs.</returns>
        protected abstract List<TDto> Find();

        /// <summary>
        /// Retrieves a list of DTOs that match the specified primary key(s).
        /// </summary>
        /// <param name="pkeys">The values of the primary key(s).</param>
        /// <returns>A list of matching DTOs.</returns>
        protected abstract List<TDto> Find(params object[] pkeys);

        #endregion
    }
}
