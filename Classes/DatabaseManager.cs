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
    public static class DatabaseManager
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
        /// Creates all Database Tables if this is the first time the program is run
        /// </summary>
        private static void CreateTabless() 
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
                Phone VARCHAR(12);
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
        /// Gets all books
        /// </summary>
        /// <returns></returns>
        public static List<Book> GetAllBooks() 
        {
            List<Book> list = new List<Book>();
            string query = "SELECT * FROM book;";
            MySqlCommand cmd = new MySqlCommand(query,connection);
            connection.Open();
            using (MySqlDataReader reader = cmd.ExecuteReader()) 
            {
                while (reader.Read()) 
                {
                    Book book = new Book();
                    book.BookId = reader.GetString(0);
                    book.Title = reader.GetString(1);
                    book.Genre = reader.GetString(2);
                    book.Author = reader.GetString(3);
                    book.Quantity = reader.GetInt32(4);
                    list.Add(book);
                }
            }
            connection.Close() ;
            return list;
        }

        /// <summary>
        /// Implement the AddBook method to add a book to the database
        /// </summary>
        /// <param name="book"></param>
        public static void AddBook(Book book)
        {
            connection.Open();
            string insertSql = "INSERT INTO book (BookId, Title, Author, Genre, Quantity) VALUES" +
                              $"('{book.BookId}','{book.Title}','{book.Author}','{book.Genre}',{book.Quantity});";
            MySqlCommand cmd = new MySqlCommand(insertSql, connection);
            int execute = cmd.ExecuteNonQuery();
            connection.Close();

        }

        /// <summary>
        /// Implement the UpdateBook method to update a book in the database
        /// </summary>
        /// <param name="book"></param>
        public static void UpdateBook(Book book)
        {
            connection.Open();
            string sqlQuery = $"UPDATE books SET Title='{book.Title}', Author= '{book.Author}',  Genre='{book.Genre}', Quantity={book.Quantity}, WHERE BookId='{book.BookId}'";
            MySqlCommand command = new MySqlCommand(sqlQuery, connection);
            int execute = command.ExecuteNonQuery();
            connection.Close();
        }

        /// <summary>
        /// Implement the GetBook method to get a book from the database
        /// </summary>
        /// <param name="bookId"></param>
        /// <returns></returns>
        public static Book GetBook(string bookId)
        {
            string sqlQuery = $"SELECT * FROM books WHERE BOOKID='{bookId}'";
            Book book = new Book();
            MySqlCommand read = new MySqlCommand(sqlQuery, connection);
            connection.Open();
            using (MySqlDataReader reader = read.ExecuteReader())
            {
                while (reader.Read())
                {
                    book.BookId = reader.GetString(0);
                    book.Title = reader.GetString(1);
                    book.Genre = reader.GetString(2);
                    book.Author = reader.GetString(3);
                    book.Quantity = reader.GetInt32(4);
                }
            }
            connection.Close();
            return book;

        }


        /// <summary>
        /// Implement the DeleteBook method to delete a book from the database
        /// </summary>
        /// <param name="bookId"></param>
        public static void DeleteBook(string bookId)
        {
            connection.Open();
            string sqlQuery = $"DELETE FROM books WHERE BookId='{bookId}'";
            MySqlCommand command = new MySqlCommand(sqlQuery, connection);
            int execute = command.ExecuteNonQuery();
            connection.Close();
        }

        public static List<Customer> GetAllCustomers()
        {
            connection.Open();
            List<Customer> customers = new List<Customer>();
            string sql = "select * from customer;";

            MySqlCommand command = new MySqlCommand(sql,connection);
            using(MySqlDataReader reader = command.ExecuteReader())
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

        public static Customer GetCustomer(string id)
        {
            connection.Open();
            Customer customer = new Customer();
            string sql = $"select * from customer where customer_id = '{id}'; ";
            MySqlCommand command = new MySqlCommand(sql,connection);
            using( MySqlDataReader reader = command.ExecuteReader())
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

        public static void AddCustomer(Customer customer)
        {
            connection.Open();
            string sql = $"insert into customer values ( '{customer.CustomerID}', '{customer.FirstName}', '{customer.LastName}', '{customer.Email}', '{customer.Phone}' ); ";
            MySqlCommand command = new MySqlCommand( sql,connection);
            command.ExecuteNonQuery();
            connection.Close();
        }

        public static void DeleteCustomer(string id)
        {
            connection.Open();
            string sql = $"delete from customer where customerId = '{id}' ;";
            MySqlCommand command = new MySqlCommand(sql, connection);
            command.ExecuteNonQuery();
            connection.Close();
        }
    }
}
