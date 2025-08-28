using System;
using Microsoft.Data.SqlClient;

namespace DBManager
{
    /// <summary>
    /// Builder class for constructing <see cref="SqlCommand"/> instances.
    /// Enables fluent-style configuration of SQL statements and parameters.
    /// </summary>
    /// <remarks>
    ///
    /// Design principle:
    /// - One connection, one transaction (simplified connection handling).
    /// - Builder pattern ensures safe and readable command creation.
    ///
    /// <code>
    /// using var command = new SqlCommandBuilder()
    ///     .WithCommandText("SELECT * FROM table_name WHERE column_name = @parameter")
    ///     .AddParameter("@parameter", "parameter")
    ///     .Build();
    /// </code>
    /// </remarks>
    /// <summary>
    /// Internal <see cref="SqlCommand"/> instance representing the SQL command.
    /// </summary>
    public class SqlCommandBuilder
    {

        private readonly SqlCommand _com = new();

        /// <summary>
        /// Initializes a new instance of <see cref="SqlCommandBuilder"/>.
        /// The connection and transaction are automatically associated with
        /// <see cref="ConnectionManager.con"/> and <see cref="ConnectionManager.tmg"/>.
        /// </summary>
        public SqlCommandBuilder()
        {
            // Database connection
            _com.Connection = ConnectionManager.con.Value;
            // Transaction manager
            _com.Transaction = ConnectionManager.tmg?.GetSqlTransaction();
        }

        /// <summary>
        /// Sets the SQL command text to be executed.
        /// </summary>
        /// <param name="commandText">The SQL command string.</param>
        /// <returns>The current <see cref="SqlCommandBuilder"/> instance for method chaining.</returns>
        public SqlCommandBuilder WithCommandText(string commandText)
        {
            _com.CommandText = commandText;
            return this;
        }

        /// <summary>
        /// Adds a parameter to the SQL command.
        /// </summary>
        /// <param name="name">The parameter name (must match a placeholder in the SQL statement).</param>
        /// <param name="value">The parameter value. If <c>null</c>, <see cref="DBNull.Value"/> is used.</param>
        /// <returns>The current <see cref="SqlCommandBuilder"/> instance for method chaining.</returns>
        public SqlCommandBuilder AddParameter(string name, object? value)
        {
            _com.Parameters.AddWithValue(name, value ?? DBNull.Value);
            return this;
        }

        /// <summary>
        /// Builds and returns the constructed <see cref="SqlCommand"/>.
        /// </summary>
        /// <remarks>
        /// Throws an exception if the connection or command text has not been set.
        /// </remarks>
        /// <returns>The fully constructed <see cref="SqlCommand"/>.</returns>
        /// <exception cref="InvalidOperationException">
        /// Thrown when the connection is <c>null</c> or the command text is not set.
        /// </exception>
        public SqlCommand Build()
        {
            if (_com.Connection == null)
                throw new InvalidOperationException("Connection must be set.");
            if (string.IsNullOrWhiteSpace(_com.CommandText))
                throw new InvalidOperationException("CommandText must be set.");
            return _com;
        }

        /// <summary>
        /// Disposes of the internal <see cref="SqlCommand"/> instance.
        /// </summary>
        public void Dispose()
        {
            _com.Dispose();
        }
    }
}
