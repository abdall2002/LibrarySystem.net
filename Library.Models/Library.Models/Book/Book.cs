using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Models
{
    public class Book
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public int AuthorID { get; set; }
        public string Description { get; set; }
        public virtual Author Author { get; set; }
        public virtual ICollection<BookImage> BookImages { get; set;}
    }
}
