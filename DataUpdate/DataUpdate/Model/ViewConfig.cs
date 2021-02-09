using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataUpdate.Model
{
    public class ViewConfig
    {
        private int viewConfigId;
        public int ViewConfigId
        {
            get { return this.viewConfigId; }
            set { this.viewConfigId = value; }
        }

        private string isbn;
        public string Isbn
        {
            get { return this.isbn; }
            set { this.isbn = value; }
        }
        private string title;
        public string Title
        {
            get { return this.title; }
            set { this.title = value; }
        }
        private string publicDate;
        public string PublicDate
        {
            get { return this.publicDate; }
            set { this.publicDate = value; }
        }

        private string author;
        public string Author
        {
            get { return this.author; }
            set { this.author = value; }
        }
        private string category;
        public string Category
        {
            get { return this.category; }
            set { this.category = value; }
        }
        private string count;
        public string Count
        {
            get { return this.count; }
            set { this.count = value; }
        }
        private string available;
        public string Available
        {
            get { return this.available; }
            set { this.available = value; }
        }
        private string viewName;
        public string ViewName
        {
            get { return this.viewName; }
            set { this.viewName = value; }
        }
    }
}
