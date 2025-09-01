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

**DAO Framework** is the framework library for data access object (readable or writable) and Data transport object or entity.  
**ExceptionLogger** is a logger for exception handling.  

---

## Usage
### Connection configuration
Please modify `app.config`
```
<?xml version="1.0" encoding="utf-8" ?>
<configuration>
    <startup> 
        <supportedRuntime version="v4.0" sku=".NETFramework,Version=v4.8" />
    </startup>
	<connectionStrings>
		<add name="shopping"
			 providerName="System.Data.SqlClient"
			 connectionString="Data Source=127.0.0.1; Initial Catalog=DatabaseName; User ID=UserId; Password=Password;
           Connect Timeout=10; Persist Security Info=True;TrustServerCertificate=True" />
	</connectionStrings>
</configuration>
```

### Connection and Transaction Management sample code
Please check `Program.cs` code.

---

## Dependencies

This library depends on the following NuGet packages:

- [System.Configuration.ConfigurationManager](https://www.nuget.org/packages/System.Configuration.ConfigurationManager/)  
- [Microsoft.Data.SqlClient](https://www.nuget.org/packages/Microsoft.Data.SqlClient/)  

---

## License

This project is licensed under the **MIT License**.  

