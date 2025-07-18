﻿using System;
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
            string sqlQuery = $"UPDATE book SET Title='{book.Title}',Author='{book.Author}', Genre='{book.Genre}', Quantity='{book.Quantity}' WHERE BookId='{book.BookId}'";
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
            string sqlQuery = $"SELECT * FROM book WHERE BOOKID='{bookId}'";
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
            string sqlQuery1= $"DELETE FROM borrow WHERE BookId='{bookId}'";
            MySqlCommand command1 = new MySqlCommand(sqlQuery1, connection);
            int execute1 = command1.ExecuteNonQuery();
            string sqlQuery2 = $"DELETE FROM book WHERE BookId='{bookId}'";
            MySqlCommand command2 = new MySqlCommand(sqlQuery2, connection);
            int execute2 = command2.ExecuteNonQuery();
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

        /// <summary>
        /// Adds a new customer to the database.
        /// </summary>
        /// <param name="customer">The Customer object to add</param>
        public static void AddCustomer(Customer customer)
        {
            connection.Open();
            string sql = $"insert into customer values ( '{customer.CustomerID}', '{customer.FirstName}', '{customer.LastName}', '{customer.Email}', '{customer.Phone}' ); ";
            MySqlCommand command = new MySqlCommand( sql,connection);
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

        /// <summary>
        /// Method that gets all borrow data and return a list of borrow books
        /// </summary>
        /// <returns></returns>
        public static List<BorrowBooks> GetAllBorrow()
        {
            List<BorrowBooks> list = new List<BorrowBooks>();
            string query = "SELECT * FROM borrow;";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            connection.Open();
            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    BorrowBooks borrow = new BorrowBooks();
                    borrow.BorrowId = reader.GetString(0);
                    borrow.CustomerId = reader.GetString(1);
                    borrow.BookId = reader.GetString(2);
                    borrow.Quantity = reader.GetInt32(3);
                    borrow.Returned = reader.GetString(4);
                    list.Add(borrow);
                }
            }
            connection.Close();
            return list;
        }

        /// <summary>
        /// This retrieves an id and return a borrowbooks object from the database
        /// </summary>
        /// <param name="id">id of the borrowbook</param>
        /// <returns></returns>
        public static BorrowBooks GetBorrow(string id)
        {
            BorrowBooks borrow = new BorrowBooks();
            string query = $"SELECT * FROM borrow where borrowid = '{id}';";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            connection.Open();
            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    borrow.BorrowId = reader.GetString(0);
                    borrow.CustomerId = reader.GetString(1);
                    borrow.BookId = reader.GetString(2);
                    borrow.Quantity = reader.GetInt32(3);
                    borrow.Returned = reader.GetString(4);
                }
            }
            connection.Close();
            return borrow;
        }

        /// <summary>
        /// Add  borrow data to the databse
        /// </summary>
        /// <param name="borrow">Borrow Bok to add</param>
        public static void AddBorrow(BorrowBooks borrow)
        {
            connection.Open();
            string sql = $"INSERT INTO borrow (BorrowId, CustomerId, BookId, Quantity, Returned) VALUES ('{borrow.BorrowId}', '{borrow.CustomerId}', '{borrow.BookId}', {borrow.Quantity}, '{borrow.Returned}');";
            MySqlCommand command = new MySqlCommand(sql, connection);
            command.ExecuteNonQuery();
            connection.Close();
        }

        /// <summary>
        /// To dlete a borrowed book from the database
        /// </summary>
        /// <param name="id">The id of the book to delete</param>
        public static void DeleteBorrow(string id) 
        {
            connection.Open();
            string sqlQuery1 = $"DELETE FROM borrow WHERE BookId='{id}'";
            MySqlCommand command1 = new MySqlCommand(sqlQuery1, connection);
            int execute1 = command1.ExecuteNonQuery();
            connection.Close();
        }

        /// <summary>
        /// Update a borrow statement
        /// </summary>
        /// <param name="borrow">The borrowed data</param>
        public static void UpdateBorrow(BorrowBooks borrow)
        {
            connection.Open();
            string sqlQuery = $"UPDATE borrow SET CustomerId='{borrow.CustomerId}',BookId='{borrow.BookId}', Quantity={borrow.Quantity}, Returned = '{borrow.Returned}' WHERE BorrowId='{borrow.BorrowId}'";
            MySqlCommand command = new MySqlCommand(sqlQuery, connection);
            int execute = command.ExecuteNonQuery();
            connection.Close();

        }

        public static List<BorrowBooks> GetAllBorrowsBasedOnCustomerID(string id)
        {
            connection.Open();
            List < BorrowBooks > borrowBooks = new List<BorrowBooks>();
            string sql = $"SELECT * from borrow where customerID = '{id.ToUpper()}';";
            MySqlCommand command = new MySqlCommand(sql, connection);
            using(MySqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    BorrowBooks book = new BorrowBooks(
                        reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetInt32(3), reader.GetString(4)
                        );
                    borrowBooks.Add(book);
                }
            }
            connection.Close();
            return borrowBooks;
        }
    }
}
