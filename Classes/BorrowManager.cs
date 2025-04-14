using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPRG211FinalProject.Classes
{
    public static class BorrowManager
    {
        public static List<BorrowBooks> BorrowBooks = DatabaseManager.GetAllBorrow();

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
            }
        }
    }
}
