using ShoppingCart.Models.Dao;
using System;
using System.Collections.Generic;

namespace DBManager
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("DBManager is running...");
            try
            {
                using var connectionManager = new ConnectionManager("shopping");
                {
                    try
                    {
                        IReadableDao<CustomerEntity> readableDao = new Dao();
                        object[] pkeys = { "account", "p" };
                        var customer = readableDao.Find(pkeys);
                        Console.WriteLine("DBManager has successed running.:" + customer);
                        using var transactionManager = connectionManager.BeginTransaction();
                        {
                            try
                            {
                                IWritableDao<CustomerEntity> writableDao = new Dao();
                                writableDao.Update(customer);
                                transactionManager.Commit();
                                Console.WriteLine($"Transaction successed and commited");
                            }
                            catch (Exception ex)
                            {
                                transactionManager.Rollback();
                                Console.WriteLine($"Transaction failed and rolled back: {ex.Message}");
                                new ExceptionLogger("logs").LogException(ex);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to connect to the database: {ex.Message}");
                new ExceptionLogger("logs").LogException(ex);
            }
            finally
            {
                Console.WriteLine("DBManager has finished running.");
            }
        }
    }

    internal class Dao : BaseEntityDao<CustomerEntity>
    {
        #nullable enable
        protected override CustomerEntity? Find(params object[] pkeys)
        {
            string query = @"
                            SELECT 
                                customer_id,
                                email 
                            FROM 
                                m_customer 
                            WHERE 
                                customer_id = @customerId 
                            AND 
                                password = @password";

            using var com = new SqlCommandBuilder()
                .WithCommandText(query)
                .AddParameter("@customerId", pkeys[0])
                .AddParameter("@password", pkeys[1])
                .Build();
            using var reader = com.ExecuteReader();
            {
                if (reader.Read())
                {
                    return new CustomerEntity
                    {
                        CustomerId = reader.GetNonNullString("customer_id"),
                        Email = reader.GetNonNullString("email")!,
                    };
                }
                else
                {
                    return null;
                }
            }
        }

        protected override List<CustomerEntity> Find()
        {
            throw new NotImplementedException();
        }

        protected override List<CustomerEntity> FindBy(params object[] pkeys)
        {
            throw new NotImplementedException();
        }

        protected override int Insert(CustomerEntity t)
        {
            throw new NotImplementedException();
        }

        protected override int Update(CustomerEntity t)
        {
            string query = @"
                            UPDATE
                                m_customer
                            SET
                                email = @email
                            WHERE 
                                customer_id = @customerId";

            using var com = new SqlCommandBuilder()
                .WithCommandText(query)
                .AddParameter("@customerId", t.CustomerId)
                .AddParameter("@email", t.Email)
                .Build();
            {
                return com.ExecuteNonQuery();
            }
        }
        protected override int Delete(CustomerEntity t)
        {
            throw new NotImplementedException();
        }
    }

    internal class CustomerEntity
    {
        public string CustomerId { get; set; } = default!;
        public string Password { get; set; } = default!;
        public string Email { get; set; } = default!;
        public DateTime CreatedAt { get; set; } = default!;
        public DateTime UpdatedAt { get; set; } = default!;
    }
}
