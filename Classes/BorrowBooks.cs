using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPRG211FinalProject.Classes
{
    public class BorrowBooks
    {
        public string BorrowId {  get; set; }
        public string CustomerId {  get; set; }
        public string BookId {  get; set; }
        public string Returned {  get; set; }
        public int Quantity {  get; set; }
        
        public BorrowBooks() { }    
        public BorrowBooks(string borrrowId, string customerId, string bookId, int quantity string returned) 
        {
            BorrowId = borrrowId;
            CustomerId = customerId;
            BookId = bookId;
            Quantity = quantity;
            Returned = returned;
        }
    }
}
