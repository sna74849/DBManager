using System;
using Microsoft.Data.SqlClient;
using ConfigurationManager = System.Configuration.ConfigurationManager;

namespace DBManager
{
    /// <summary>
    /// Provides simplified management of a single SQL Server connection and a single transaction.
    /// This class is intentionally designed for lightweight usage scenarios where
    /// only one active connection and one active transaction are required for the application.
    /// </summary>
    /// <remarks>
    /// <para>
    /// ⚠️ Design limitation:
    /// - Only <b>one database connection</b> is created and shared across the application.
    /// - Only <b>one transaction</b> can be active at any time.
    /// - Attempting to create multiple transactions simultaneously is not supported.
    /// </para>
    ///
    /// Typical usage:
    /// <code>
    /// using var manager = new ConnectionManager("MyConnectionStringName");
    /// using var transaction = manager.BeginTransaction();
    /// 
    /// try
    /// {
    ///     using var cmd = new SqlCommand("INSERT INTO ...", manager.GetConnection(), transaction.GetSqlTransaction());
    ///     cmd.ExecuteNonQuery();
    /// 
    ///     transaction.Commit();
    /// }
    /// catch
    /// {
    ///     transaction.Rollback();
    ///     throw;
    /// }
    /// </code>
    /// </remarks>
    public class ConnectionManager : IDisposable
    {
        /// <summary>
        /// The name of the connection string defined in <c>app.config</c> or <c>web.config</c>.
        /// </summary>
        private static string _connectionStringName;

        /// <summary>
        /// Thread-safe initialization of the <see cref="SqlConnection"/>.
        /// The connection will be opened automatically upon first access.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// Thrown when the connection string name is not set or is invalid.
        /// </exception>
        internal static readonly Lazy<SqlConnection> con = new(() =>
        {
            if (string.IsNullOrEmpty(_connectionStringName))
                throw new InvalidOperationException("Connection string name must be set before use.");

            var connectionString =
                ConfigurationManager.ConnectionStrings[_connectionStringName]?.ConnectionString
                ?? throw new InvalidOperationException("Invalid connection string name.");

            var conn = new SqlConnection(connectionString);
            conn.Open();
            return conn;
        });

        /// <summary>
        /// Thread-safe initialization of the transaction manager.
        /// A single <see cref="SqlTransaction"/> will be created and reused for the application lifetime.
        /// </summary>
        internal static ITransactionManager tmg;

        /// <summary>
        /// Initializes a new instance of <see cref="ConnectionManager"/> using the specified connection string name.
        /// This must be called before any operations that rely on the database connection or transaction manager.
        /// </summary>
        /// <param name="connectionStringName">
        /// The key of the connection string as defined in the application's configuration file.
        /// </param>
        public ConnectionManager(string connectionStringName)
        {
            _connectionStringName = connectionStringName;
        }
        /// <summary>
        /// Begins a new database transaction and returns a transaction manager instance.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Each call to this method starts a new <see cref="SqlTransaction"/> 
        /// and replaces the previously stored transaction manager in <see cref="tmg"/>.
        /// </para>
        /// <para>
        /// ⚠️ Limitation: Because the transaction manager is stored in a static field,
        /// only one transaction can be tracked at a time for the entire application.
        /// Concurrent transactions are not supported.
        /// </para>
        /// <para>
        /// The caller must explicitly commit or roll back the transaction.
        /// </para>
        /// </remarks>
        /// <returns>
        /// An instance of <see cref="ITransactionManager"/> that manages the active <see cref="SqlTransaction"/>.
        /// </returns>
        public ITransactionManager BeginTransaction() 
        { 
            tmg = new TransactionManager(con.Value.BeginTransaction());
            return tmg;
        }

        /// <summary>
        /// Returns the single active <see cref="SqlConnection"/> associated with this manager.
        /// </summary>
        /// <remarks>
        /// The connection is automatically opened on first access. 
        /// This connection is shared across all transactions and commands in the application.
        /// </remarks>
        /// <returns>The active <see cref="SqlConnection"/> instance.</returns>
        public SqlConnection GetConnection() => con.Value;

        /// <summary>
        /// Releases the database connection and transaction manager resources.
        /// </summary>
        /// <remarks>
        /// This method ensures that both the <see cref="SqlConnection"/> and <see cref="SqlTransaction"/> 
        /// (if created) are properly disposed. It should always be called when the connection manager
        /// is no longer needed, typically via a <c>using</c> statement.
        /// </remarks>
        public void Dispose()
        {
            tmg?.Dispose();

            if (con.IsValueCreated)
                con.Value.Dispose();
        }

        /// <summary>
        /// Provides internal management of the single SQL Server transaction.
        /// This class encapsulates <see cref="SqlTransaction"/> and ensures consistent disposal patterns.
        /// </summary>
        private class TransactionManager : ITransactionManager
        {
            private readonly SqlTransaction _trn;

            /// <summary>
            /// Initializes a new instance of the <see cref="TransactionManager"/> class.
            /// </summary>
            /// <param name="sqlTransaction">The SQL transaction to manage.</param>
            public TransactionManager(SqlTransaction sqlTransaction)
            {
                this._trn = sqlTransaction;
            }

            /// <summary>
            /// Commits the single active SQL transaction, making all changes permanent in the database.
            /// </summary>
            public void Commit() => this._trn.Commit();

            /// <summary>
            /// Rolls back the single active SQL transaction, undoing all changes made during the transaction.
            /// </summary>
            public void Rollback() => this._trn.Rollback();

            /// <summary>
            /// Releases all resources used by the transaction.
            /// </summary>
            public void Dispose() => this._trn.Dispose();

            /// <summary>
            /// Returns the underlying <see cref="SqlTransaction"/> instance.
            /// </summary>
            /// <returns>The <see cref="SqlTransaction"/> being managed.</returns>
            public SqlTransaction GetSqlTransaction() => this._trn;

            public static implicit operator Lazy<object>(TransactionManager v)
            {
                throw new NotImplementedException();
            }
        }
    }

    /// <summary>
    /// Defines the contract for transaction operations, including commit, rollback, and disposal.
    /// This interface is implemented by the internal transaction manager class.
    /// </summary>
    public interface ITransactionManager : IDisposable
    {
        /// <summary>
        /// Gets the underlying <see cref="SqlTransaction"/> object managed by this instance.
        /// </summary>
        /// <returns>The current <see cref="SqlTransaction"/>.</returns>
        SqlTransaction GetSqlTransaction();

        /// <summary>
        /// Commits the transaction, making all operations permanent.
        /// </summary>
        void Commit();

        /// <summary>
        /// Rolls back the transaction, undoing all changes since the transaction began.
        /// </summary>
        void Rollback();

        /// <summary>
        /// Releases all resources held by the transaction manager.
        /// </summary>
        new void Dispose();
    }
}
