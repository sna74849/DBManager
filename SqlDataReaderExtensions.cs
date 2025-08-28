using System;
using Microsoft.Data.SqlClient;

namespace DBManager
{
    /// <summary>
    /// Extension methods for safely retrieving values from <see cref="SqlDataReader"/>.
    /// Provides null checking for <c>DBNull</c> and returns nullable types or default values.
    /// </summary>
    public static class SqlDataReaderExtensions
    {
        /// <summary>
        /// Determines whether the specified column exists in the <see cref="SqlDataReader"/>.
        /// </summary>
        /// <param name="reader">The <see cref="SqlDataReader"/> to inspect.</param>
        /// <param name="columnName">The name of the column to check.</param>
        /// <returns><c>true</c> if the column exists; otherwise, <c>false</c>.</returns>
        /// <example>
        /// if (reader.HasColumn("UserName")) { ... }
        /// </example>
        public static bool HasColumn(this SqlDataReader reader, string columnName)
        {
            for (int i = 0; i < reader.FieldCount; i++)
            {
                if (reader.GetName(i).Equals(columnName, StringComparison.InvariantCultureIgnoreCase))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Retrieves the value of the specified column as a nullable <see cref="string"/>.
        /// Returns <c>null</c> if the column contains <c>DBNull</c>.
        /// </summary>
        /// <param name="reader">The <see cref="SqlDataReader"/> to retrieve the value from.</param>
        /// <param name="columnName">The column name.</param>
        /// <returns>The string value, or <c>null</c> if the column contains <c>DBNull</c>.</returns>
        /// <exception cref="IndexOutOfRangeException">Thrown if the column does not exist.</exception>
        /// <example>
        /// string? name = reader.GetNullableString("UserName");
        /// </example>
        #nullable enable
        public static string? GetNullableString(this SqlDataReader reader, string columnName)
        {
            var ordinal = reader.GetOrdinal(columnName);
            return reader.IsDBNull(ordinal) ? null : reader.GetString(ordinal);
        }

        /// <summary>
        /// Retrieves the value of the specified column as a nullable <see cref="int"/>.
        /// Returns <c>null</c> if the column contains <c>DBNull</c>.
        /// </summary>
        /// <exception cref="IndexOutOfRangeException">Thrown if the column does not exist.</exception>
        public static int? GetNullableInt(this SqlDataReader reader, string columnName)
        {
            var ordinal = reader.GetOrdinal(columnName);
            return reader.IsDBNull(ordinal) ? null : reader.GetInt32(ordinal);
        }

        /// <summary>
        /// Retrieves the value of the specified column as a nullable <see cref="long"/>.
        /// Returns <c>null</c> if the column contains <c>DBNull</c>.
        /// </summary>
        public static long? GetNullableLong(this SqlDataReader reader, string columnName)
        {
            var ordinal = reader.GetOrdinal(columnName);
            return reader.IsDBNull(ordinal) ? null : reader.GetInt64(ordinal);
        }

        /// <summary>
        /// Retrieves the value of the specified column as a nullable <see cref="bool"/>.
        /// Returns <c>null</c> if the column contains <c>DBNull</c>.
        /// </summary>
        public static bool? GetNullableBool(this SqlDataReader reader, string columnName)
        {
            var ordinal = reader.GetOrdinal(columnName);
            return reader.IsDBNull(ordinal) ? null : reader.GetBoolean(ordinal);
        }

        /// <summary>
        /// Retrieves the value of the specified column as a nullable <see cref="DateTime"/>.
        /// Returns <c>null</c> if the column contains <c>DBNull</c>.
        /// </summary>
        public static DateTime? GetNullableDateTime(this SqlDataReader reader, string columnName)
        {
            var ordinal = reader.GetOrdinal(columnName);
            return reader.IsDBNull(ordinal) ? null : reader.GetDateTime(ordinal);
        }

        /// <summary>
        /// Retrieves the value of the specified column as a nullable <see cref="decimal"/>.
        /// Returns <c>null</c> if the column contains <c>DBNull</c>.
        /// </summary>
        public static decimal? GetNullableDecimal(this SqlDataReader reader, string columnName)
        {
            var ordinal = reader.GetOrdinal(columnName);
            return reader.IsDBNull(ordinal) ? null : reader.GetDecimal(ordinal);
        }

        /// <summary>
        /// Retrieves the value of the specified column as a <see cref="string"/>.
        /// Returns <see cref="string.Empty"/> if the column contains <c>DBNull</c>.
        /// </summary>
        /// <example>
        /// string name = reader.GetNonNullString("UserName");
        /// </example>
        public static string GetNonNullString(this SqlDataReader reader, string columnName)
        {
            var ordinal = reader.GetOrdinal(columnName);
            return reader.IsDBNull(ordinal) ? string.Empty : reader.GetString(ordinal);
        }

        /// <summary>
        /// Retrieves the value of the specified column as an <see cref="int"/>.
        /// Returns 0 if the column contains <c>DBNull</c>.
        /// </summary>
        public static int GetNonNullInt(this SqlDataReader reader, string columnName)
        {
            var ordinal = reader.GetOrdinal(columnName);
            return reader.IsDBNull(ordinal) ? 0 : reader.GetInt32(ordinal);
        }

        /// <summary>
        /// Retrieves the value of the specified column as a <see cref="long"/>.
        /// Returns 0L if the column contains <c>DBNull</c>.
        /// </summary>
        public static long GetNonNullLong(this SqlDataReader reader, string columnName)
        {
            var ordinal = reader.GetOrdinal(columnName);
            return reader.IsDBNull(ordinal) ? 0L : reader.GetInt64(ordinal);
        }

        /// <summary>
        /// Retrieves the value of the specified column as a <see cref="bool"/>.
        /// Returns false if the column contains <c>DBNull</c>.
        /// </summary>
        public static bool GetNonNullBool(this SqlDataReader reader, string columnName)
        {
            var ordinal = reader.GetOrdinal(columnName);
            return reader.IsDBNull(ordinal) ? false : reader.GetBoolean(ordinal);
        }

        /// <summary>
        /// Retrieves the value of the specified column as a <see cref="DateTime"/>.
        /// Returns <see cref="DateTime.Now"/> if the column contains <c>DBNull</c>.
        /// </summary>
        public static DateTime GetNonNullDateTime(this SqlDataReader reader, string columnName)
        {
            var ordinal = reader.GetOrdinal(columnName);
            return reader.IsDBNull(ordinal) ? DateTime.Now : reader.GetDateTime(ordinal);
        }

        /// <summary>
        /// Retrieves the value of the specified column as a <see cref="decimal"/>.
        /// Returns 0m if the column contains <c>DBNull</c>.
        /// </summary>
        public static decimal GetNonNullDecimal(this SqlDataReader reader, string columnName)
        {
            var ordinal = reader.GetOrdinal(columnName);
            return reader.IsDBNull(ordinal) ? 0m : reader.GetDecimal(ordinal);
        }
    }
}
