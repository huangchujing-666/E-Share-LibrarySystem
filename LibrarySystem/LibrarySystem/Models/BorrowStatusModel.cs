using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibrarySystem.Admin.Models
{
    public class BorrowStatusModel
    {
        public int BookOrderId { get; set; }

        public int BorrowStatus { get; set; }
    }
}