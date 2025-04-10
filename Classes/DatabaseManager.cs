using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using MySqlConnector;

namespace CPRG211FinalProject.Classes
{
    internal static class DatabaseManager
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

        private static void CreateAndPopulate() 
        {
            string createTableBook =
                @"CREATE TABLE IF NOT EXISTS book (
                BookId VARCHAR(36) PRIMARY KEY,
                Title VARCHAR(255) NOT NULL,
                Genre VARCHAR(255),
                Author VARCHAR(255) NOT NULL,
                Quantity NUMBER);" +
                @"CREATE TABLE IF NOT EXISTS customer (
                CustomerId VARCHAR(36) PRIMARY KEY,
                FirstName VARCHAR(255) NOT NULL,
                LastName VARCHAR(255),
                Email VARCHAR(255),
                Phone NUMBER);" +
                @"CREATE TABLE IF NOT EXISTS borrow (
                BorrowId VARCHAR(36) PRIMARY KEY,
                CustomerId VARCHAR(36) NOT NULL,
                BookId VARCHAR(36) NOT NULL,
                Quantity INT,
                Returned VARCHAR(3),
                CONSTRAINT customerid_fk FOREIGN KEY (CustomerId) REFERENCES customer(CustomerId),
                CONSTRAINT bookid_fk FOREIGN KEY (BookId) REFERENCES book(BookId)
                );"; ;
            connection.Open();
            connection.Execute(createTableBook);
            connection.Close();
            

        }



    }
}
