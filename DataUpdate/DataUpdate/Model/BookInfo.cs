using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataUpdate.Model
{
    public class BookInfo
    {
        //private int bookInfoId;
        //public int BookInfoId
        //{
        //    get { return bookInfoId; }
        //    set { bookInfoId = value; }

        //    // get { return _unit; }
        //    //set { _unit = value != null ? value.Trim() : null; }
        //}

        private string isbn;
        public string Isbn
        {
            get { return isbn; }
            set { isbn = value != null ? value.Trim() : ""; }
        }

        private string title;
        public string Title
        {
            get { return title; }
            set { title = value != null ? value.Trim() : ""; }
        }

        private string publicDate;
        public string PublicDate
        {
            get { return publicDate; }
            set { publicDate = value != null ? value.Trim() : ""; }
        }

        private string author;
        public string Author
        {
            get { return author; }
            set { author = value != null ? value.Trim() : ""; }
        }

        private string category;
        public string Category
        {
            get { return category; }
            set { category = value != null ? value.Trim() : ""; }
        }

        private int available;
        public int Available
        {
            get { return available; }
            set { available = value; }
        }

        private int count;
        public int Count
        {
            get { return count; }
            set { count = value; }
        }
    }
}
