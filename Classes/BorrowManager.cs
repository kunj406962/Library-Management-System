using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPRG211FinalProject.Classes
{
    public class BorrowManager : IManagercs<BorrowBooks>
    {
        public static List<BorrowBooks> BorrowBooks = DatabaseManager.GetAllBorrow();
        /// <summary>
        /// Checks the desired quantity, and returns a message
        /// according to the logic if it's possible or not.
        /// </summary>
        /// <param name="bookId">Id of book</param>
        /// <param name="quantity"> Quantitiy of Book</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static int CheckQuantity(string bookId, int quantity) 
        {
            Book book= DatabaseManager.GetBook(bookId);
            if (quantity > book.Quantity) 
            {
                throw new Exception("That quantity of books is unavailable");
            }
            else if (quantity <= 0)
            {
                throw new Exception("The quantity cannor be a negative or 0");
            }
            else 
            {
                return quantity;
            }
        }

        /// <summary>
        /// Creates a borrowed book data and automatically keeps returned as NO
        /// </summary>
        /// <param name="custId">Id of customer</param>
        /// <param name="bookId">Id of book</param>
        /// <param name="quantity">quantity that has been borrowed</param>
        public static void AddBorrow(string custId, string bookId, int quantity)
        {
            BorrowBooks = BorrowBooks.OrderBy(borrow => borrow.BorrowId.Length).ThenBy(borrow => borrow.BorrowId).ToList();
            BorrowBooks borrow = new BorrowBooks();
            Book book= DatabaseManager.GetBook(bookId);
            if (BorrowBooks.Count == 0)
            {
                borrow.BorrowId = "BR1";
                borrow.CustomerId = custId;
                borrow.BookId = bookId;
                borrow.Quantity = CheckQuantity(bookId, quantity);
                borrow.Returned = "NO";
                book.Quantity-=quantity;
                DatabaseManager.UpdateBook(book);
                DatabaseManager.AddBorrow(borrow);
                BorrowBooks.Add(borrow);
            }
            else
            {

                string lastCode = BorrowBooks.LastOrDefault()!.BorrowId;
                int lastCodeNum = Convert.ToInt32(lastCode.Substring(2));
                borrow.BorrowId = $"BR{lastCodeNum + 1}";
                borrow.CustomerId = custId;
                borrow.BookId = bookId;
                borrow.Quantity = CheckQuantity(bookId, quantity);
                borrow.Returned = "NO";
                book.Quantity -= quantity;
                DatabaseManager.UpdateBook(book);
                DatabaseManager.AddBorrow(borrow);
                BorrowBooks.Add(borrow) ;
            }
        }

        /// <summary>
        /// Returns only books that have not been returned
        /// </summary>
        /// <param name="custId"></param>
        /// <returns></returns>
        public static List<BorrowBooks> BooksNotReturned(string custId)
        {
            List<BorrowBooks> data= DatabaseManager.GetAllBorrowsBasedOnCustomerID(custId);
            List<BorrowBooks> onlyNotReturned= new List<BorrowBooks>();
            foreach (BorrowBooks book in data) 
            {
                if (book.Returned == "NO")
                {
                    onlyNotReturned.Add(book);
                }
            }
            return onlyNotReturned;
        }

        /// <summary>
        /// Updates a book object both in the list and the database
        /// </summary>
        /// <param name="book">Book object with same Id </param>
        public static void Update(BorrowBooks borrow)
        {
            foreach (BorrowBooks borrow1 in BorrowBooks)
            {
                if (borrow1.BorrowId == borrow.BorrowId)
                {
                    borrow1.BookId = borrow.BookId;
                    borrow1.CustomerId = borrow.CustomerId;
                    borrow1.Returned =borrow1.Returned;
                    borrow1.Quantity = borrow1.Quantity;
                    DatabaseManager.UpdateBorrow(borrow);
                    return;
                }
            }
        }

        /// <summary>
        /// Deletse book from both the list and the database
        /// </summary>
        /// <param name="book"></param>
        public static void Delete(BorrowBooks book)
        {
            BorrowBooks.Remove(book);
            DatabaseManager.DeleteBorrow(book.BookId);
        }
    }
}
