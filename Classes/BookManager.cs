using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPRG211FinalProject.Classes
{
    public  static class BookManager
    {
        public static List<Book> Books= DatabaseManager.GetAllBooks();

        /// <summary>
        /// Creates a book object and adds it to the list as well as the database
        /// </summary>
        /// <param name="title">Title of new book</param>
        /// <param name="author">Author of new book</param>
        /// <param name="genre">Genre of new book</param>
        /// <param name="quatity">Quantity of new book</param>
        public static void CreateBook(string title, string author, string genre, int quatity)
        {
            Books = Books.OrderBy(book => book.BookId.Length).ThenBy(book => book.BookId).ToList();
            Book book = new Book();
            if (Books.Count == 0)
            {
                book.BookId = "B1";
                book.Title = title;
                book.Author = author;
                book.Genre = genre;
                book.Quantity = quatity;
                Books.Add(book);
                DatabaseManager.AddBook(book);
            }
            else
            {

                string lastCode = Books.LastOrDefault()!.BookId;
                int lastCodeNum = Convert.ToInt32(lastCode.Substring(1));
                string newCode = $"B{lastCodeNum + 1}";
                book.BookId = newCode;
                book.Title = title;
                book.Author = author;
                book.Genre = genre;
                book.Quantity = quatity;
                Books.Add(book);
                DatabaseManager.AddBook(book);
            }
        }

        /// <summary>
        /// Updates a book object both in the list and the database
        /// </summary>
        /// <param name="book">Book object with same Id </param>
        public static void UpdateBook(Book book) 
        {
           foreach(Book book1 in Books) 
           {
                if (book1.BookId == book.BookId)
                {
                    book1.Title = book.Title;
                    book1.Author = book.Author;
                    book1.Genre = book.Genre;
                    book1.Quantity = book.Quantity;
                    DatabaseManager.UpdateBook(book);
                    return;
                }
           }
        }

        /// <summary>
        /// Deletse book from both the list and the database
        /// </summary>
        /// <param name="book"></param>
        public static void DeleteBook(Book book) 
        {
            Books.Remove(book);
            DatabaseManager.DeleteBook(book.BookId);
        }

       
    }
}
