using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibrarySystem.Models
{
    public class EmailCodeVM
    {
        public virtual int EmailCodeId { get; set; }

        public virtual string Code { get; set; }


        public virtual string Email { get; set; }

        public virtual int Type { get; set; }

        public virtual DateTime CreateTime { get; set; }
    }
}