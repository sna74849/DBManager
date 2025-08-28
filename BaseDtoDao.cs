using DBManager;
using System.Collections.Generic;

namespace ShoppingCart.Models.Dao
{
    /// <summary>
    /// Abstract base class for DAOs (Data Access Objects).
    /// Provides an explicit implementation of <see cref="IReadableDao{T}"/> 
    /// for a specific DTO type.
    /// Derived classes must implement abstract methods such as <c>Find</c>.
    /// </summary>
    /// <typeparam name="TDto">The type of the target DTO.</typeparam>
    public abstract class BaseDtoDao<TDto> : IReadableDao<TDto> where TDto : class
    {
        #region IRead<TDto> explicit implementation

        /// <summary>
        /// Retrieves the DTO that matches the specified primary key.
        /// This method calls <see cref="Find"/>.
        /// </summary>
        /// <param name="pkeys">The values of the primary key.</param>
        /// <returns>
        /// The matching DTO, or <c>null</c> if no match is found.
        /// </returns>
#nullable enable
        TDto? IReadableDao<TDto>.Find(params object[] pkeys)
        {
            return Find(pkeys);
        }

        /// <summary>
        /// Retrieves all DTOs.
        /// This method calls <see cref="Find"/>.
        /// </summary>
        /// <returns>A list of DTOs.</returns>
        List<TDto> IReadableDao<TDto>.Find()
        {
            return Find();
        }

        /// <summary>
        /// Retrieves a list of DTOs that match the specified primary key.
        /// This method calls <see cref="FindBy"/>.
        /// </summary>
        /// <param name="pkeys">The values of the primary key.</param>
        /// <returns>A list of matching DTOs.</returns>
        List<TDto> IReadableDao<TDto>.FindBy(params object[] pkeys)
        {
            return FindBy(pkeys);
        }

        #endregion

        #region Protected abstract methods (to be implemented in derived classes)

        /// <inheritdoc/>
        protected abstract TDto? Find(params object[] pkeys);

        /// <inheritdoc/>
        protected abstract List<TDto> Find();

        /// <inheritdoc/>
        protected abstract List<TDto> FindBy(params object[] pkeys);

        #endregion
    }
}
