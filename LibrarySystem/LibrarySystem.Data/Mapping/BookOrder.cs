using LibrarySystem.Domain.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Data.Mapping
{
    public class BookOrderMap : EntityTypeConfiguration<BookOrder>
    {
        public BookOrderMap()
        {
            this.ToTable("BookOrder");
            this.HasKey(m => m.BookOrderId);
            this.Property(m => m.BookOrderId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(m => m.BookInfoId);
            this.Property(m => m.BorrowStatus);
            this.Property(m => m.Count);
            this.Property(m => m.BackTime);
            this.Property(m => m.TrafficType);
            this.Property(m => m.ExpressId);
            this.Property(m => m.UniversityId);
            this.Property(m => m.CreateTime);
            this.Property(m => m.EditPersonId);
            this.Property(m => m.EditTime);
            this.Property(m => m.IsDelete);
            this.Property(m => m.IsReceived);
            this.Property(m => m.Status);
            this.Property(m => m.SysAccountId);
            this.Property(m => m.Ticket);
            HasRequired(t => t.SysAccount);
            HasRequired(t => t.Express);
            HasRequired(t => t.BookInfo);
            HasRequired(t => t.University);
        }
    }
}
