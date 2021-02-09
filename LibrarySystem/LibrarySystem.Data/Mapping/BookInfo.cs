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
    public class BookInfoMap : EntityTypeConfiguration<BookInfo>
    {
        public BookInfoMap()
        {
            this.ToTable("BookInfo");
            this.HasKey(m => m.BookInfoId);
            this.Property(m => m.BookInfoId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(m => m.Author);
            this.Property(m => m.Category);
            this.Property(m => m.Count);
            this.Property(m => m.PublicDate);
            this.Property(m => m.Title);
            this.Property(m => m.UniversityId);
            this.Property(m => m.Isbn);
            this.Property(m => m.Status);
            this.Property(m => m.IsDelete);
            this.Property(m => m.CreateTime);
            this.Property(m => m.EditTime);
            this.Property(m => m.Available);
            HasRequired(t => t.University);

            HasMany(m => m.BookOrderList).WithRequired(n => n.BookInfo);
        }
    }
}
