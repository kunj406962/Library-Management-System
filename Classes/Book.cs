using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPRG211FinalProject.Classes
{
    internal class Book
    {
        public string BookId { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public string Author { get; set; }
        public int Quantity { get; set; }

        public Book(string bookId, string title, string genre, string author, int quantity)
        {
            BookId = bookId;
            Title = title;
            Genre = genre;
            Author = author;
            Quantity = quantity;
        }
    }
}
