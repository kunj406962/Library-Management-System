using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using CoreData;
using Dapper;
using MySqlConnector;

namespace CPRG211FinalProject.Classes
{
    public class DatabaseManager
    {
        private static MySqlConnectionStringBuilder builder =
        new MySqlConnectionStringBuilder
        {
            // Set the server address to "localhost"
            Server = "localhost",
            // Set the user ID to "root"
            UserID = "root",
            // Set the password to "password"
            Password = "password",
            // Set the database name to "librarydatabase"
            Database = "librarydatabase",   //Create this in HeidiSQL
        };
        
        //Build Connection
        private static MySqlConnection connection= new MySqlConnection(builder.ConnectionString);

        /// <summary>
        /// Instantiate this on the home page so it runs this
        /// </summary>
        public DatabaseManager() 
        {
            string createTableBook =
                @"CREATE TABLE IF NOT EXISTS book (
                BookId VARCHAR(36) PRIMARY KEY,
                Title VARCHAR(255) NOT NULL,
                Genre VARCHAR(255),
                Author VARCHAR(255) NOT NULL,
                Quantity INT);

                CREATE TABLE IF NOT EXISTS customer (
                CustomerId VARCHAR(36) PRIMARY KEY,
                FirstName VARCHAR(255) NOT NULL,
                LastName VARCHAR(255),
                Email VARCHAR(255),
                Phone VARCHAR(12));
                CREATE TABLE IF NOT EXISTS borrow (
                BorrowId VARCHAR(36) PRIMARY KEY,
                CustomerId VARCHAR(36) NOT NULL,
                BookId VARCHAR(36) NOT NULL,
                Quantity INT,
                Returned VARCHAR(3),
                CONSTRAINT customerid_fk FOREIGN KEY (CustomerId) REFERENCES customer(CustomerId),
                CONSTRAINT bookid_fk FOREIGN KEY (BookId) REFERENCES book(BookId));";
            connection.Open();
            connection.Execute(createTableBook);
            connection.Close();


        
        }

        /// <summary>
        /// Gets all customers from the database.
        /// </summary>
        /// <returns>List of Customer objects</returns>
        public static List<Customer> GetAllCustomers()
        {
            connection.Open();
            List<Customer> customers = new List<Customer>();
            string sql = "select * from customer;";

            MySqlCommand command = new MySqlCommand(sql, connection);
            using (MySqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Customer customer = new Customer
                    {
                        CustomerID = reader.GetString(0),
                        FirstName = reader.GetString(1),
                        LastName = reader.GetString(2),
                        Email = reader.GetString(3),
                        Phone = reader.GetString(4)
                    };
                    customers.Add(customer);
                }
            }
            connection.Close();
            return customers;
        }

        /// <summary>
        /// Retrieves a customer from the database using the specified ID.
        /// </summary>
        /// <param name="id">The ID of the customer</param>
        /// <returns>A Customer object</returns>
        public static Customer GetCustomer(string id)
        {
            connection.Open();
            Customer customer = new Customer();
            string sql = $"select * from customer where customerid = '{id}'; ";
            MySqlCommand command = new MySqlCommand(sql, connection);
            using (MySqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    customer = new Customer
                    {
                        CustomerID = reader.GetString(0),
                        FirstName = reader.GetString(1),
                        LastName = reader.GetString(2),
                        Email = reader.GetString(3),
                        Phone = reader.GetString(4)
                    };
                }
            }
            connection.Close();
            return customer;
        }

        /// <summary>
        /// Adds a new customer to the database.
        /// </summary>
        /// <param name="customer">The Customer object to add</param>
        public static void AddCustomer(Customer customer)
        {
            connection.Open();
            string sql = $"insert into customer values ( '{customer.CustomerID}', '{customer.FirstName}', '{customer.LastName}', '{customer.Email}', '{customer.Phone}' ); ";
            MySqlCommand command = new MySqlCommand(sql, connection);
            command.ExecuteNonQuery();
            connection.Close();
        }

        /// <summary>
        /// Deletes a customer and their associated borrow records from the database.
        /// </summary>
        /// <param name="id">The ID of the customer to delete</param>
        public static void DeleteCustomer(string id)
        {
            connection.Open();
            string sql = $"DELETE from borrow where customerID = '{id}'; " +
                               $"DELETE from customer WHERE customerId = '{id}'";
            MySqlCommand command = new MySqlCommand(sql, connection);
            int execute = command.ExecuteNonQuery();
            connection.Close();
        }

        /// <summary>
        /// Updates a customer's information in the database.
        /// </summary>
        /// <param name="customer">The Customer object with updated information</param>
        public static void UpdateCustomer(Customer customer)
        {
            connection.Open();
            string sql = $"UPDATE customer SET FirstName = '{customer.FirstName}', LastName = '{customer.LastName}', Email = '{customer.Email}', Phone = '{customer.Phone}' WHERE CustomerId = '{customer.CustomerID}';";
            MySqlCommand command = new MySqlCommand(sql, connection);
            int execute = command.ExecuteNonQuery();
            connection.Close();
        }
    }
