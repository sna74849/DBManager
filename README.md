# DBManager

**DBManager** is a lightweight utility library for simplified management of SQL Server connections, transactions, and commands in .NET applications.

## Features

- **One Connection, One Transaction** model
  - A single `SqlConnection` is shared across the entire application.
  - A single `SqlTransaction` can be active at a time.
- **Builder pattern** for safe and readable `SqlCommand` creation.
- **Extension methods** for safe value retrieval from `SqlDataReader`.
- **Explicit design limitations**:
  - Multiple concurrent transactions are not supported.
  - Intended for lightweight usage, not for high-concurrency scenarios.

---

## Usage

### Connection and Transaction Management
