using DBManager;
using System.Collections.Generic;

namespace ShoppingCart.Models.Dao
{
    /// <summary>
    /// DAO（Data Access Object）の基底抽象クラス。
    /// 特定のエンティティ型に対して <see cref="IReadableDao{T}"/> と <see cref="IWritableDao{T}"/> の明示的実装を提供します。
    /// 実装クラスは <c>Find</c> や <c>Insert</c> などの抽象メソッドを実装する必要があります。
    /// </summary>
    /// <typeparam name="TEntity">対象となるエンティティ型。</typeparam>
    public abstract class BaseEntityDao<TEntity> : IReadableDao<TEntity>, IWritableDao<TEntity>
        where TEntity : class
    {
        #region IRead<TEntity> 明示的実装

        /// <summary>
        /// 指定されたプライマリキーに一致するエンティティを取得します。
        /// このメソッドは <see cref="Find"/> を呼び出します。
        /// </summary>
        /// <param name="pkeys">プライマリキーの値。</param>
        /// <returns>一致するエンティティ。存在しない場合は <c>null</c>。</returns>
        #nullable enable
        TEntity? IReadableDao<TEntity>.Find(params object[] pkeys)
        {
            return Find(pkeys);
        }

        /// <summary>
        /// すべてのエンティティを取得します。
        /// このメソッドは <see cref="Find"/> を呼び出します。
        /// </summary>
        /// <returns>エンティティのリスト。</returns>
        List<TEntity> IReadableDao<TEntity>.Find()
        {
            return Find();
        }

        /// <summary>
        /// 指定されたプライマリキーに一致するエンティティをリストで取得します。
        /// このメソッドは <see cref="FindBy"/> を呼び出します。
        /// </summary>
        /// <param name="pkeys">プライマリキーの値。</param>
        /// <returns>一致するエンティティのリスト。</returns>
        List<TEntity> IReadableDao<TEntity>.FindBy(params object[] pkeys)
        {
            return FindBy(pkeys);
        }

        #endregion

        #region IWrite<TEntity> 明示的実装

        /// <summary>
        /// 指定されたエンティティを挿入します。
        /// このメソッドは <see cref="Insert"/> を呼び出します。
        /// </summary>
        /// <param name="t">挿入するエンティティ。</param>
        /// <returns>操作によって影響を受けた行数。</returns>
        int IWritableDao<TEntity>.Insert(TEntity t)
        {
            return Insert(t);
        }

        /// <summary>
        /// 指定されたエンティティを更新します。
        /// このメソッドは <see cref="Update"/> を呼び出します。
        /// </summary>
        /// <param name="t">更新するエンティティ。</param>
        /// <returns>操作によって影響を受けた行数。</returns>
        int IWritableDao<TEntity>.Update(TEntity t)
        {
            return Update(t);
        }

        /// <summary>
        /// 指定されたエンティティを削除します。
        /// このメソッドは <see cref="Delete"/> を呼び出します。
        /// </summary>
        /// <param name="t">削除するエンティティ。</param>
        /// <returns>操作によって影響を受けた行数。</returns>
        int IWritableDao<TEntity>.Delete(TEntity t)
        {
            return Delete(t);
        }

        #endregion

        #region Protected 抽象メソッド（派生クラスで実装）

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
