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
                        IReadableDao<CustomerEntity> dao = new Dao();
                        object[] pkeys = { "account1@emtech.com", "p" };
                        Console.WriteLine("DBManager has successed running.:" + dao.Find(pkeys).CustomerId);
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
                                email = @email 
                            AND 
                                password = @password";

            using var com = new SqlCommandBuilder()
                .WithCommandText(query)
                .AddParameter("@email", pkeys[0])
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
            throw new NotImplementedException();
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
