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
    public class AsideBookInfoMap:EntityTypeConfiguration<AsideBookInfo>
    {
        public AsideBookInfoMap() {
            this.ToTable("AsideBookInfo");
            this.HasKey(m => m.AsideBookInfoId);
            this.Property(m => m.AsideBookInfoId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(m => m.SysAccountId);
            this.Property(m => m.BaseImageId);
            this.Property(m => m.Author);
            this.Property(m => m.Category);
            this.Property(m => m.PublicDate);
            this.Property(m => m.Title);
            this.Property(m => m.UniversityId);
            this.Property(m => m.Isbn);
            this.Property(m => m.Status);
            this.Property(m => m.IsDelete);
            this.Property(m => m.CreateTime);
            this.Property(m => m.EditTime);
            this.Property(m => m.Available);
            this.Property(m => m.Count);
            HasRequired(t => t.University);
            HasRequired(t => t.SysAccount);
            HasRequired(t => t.BaseImage);
            HasMany(m => m.AsideBookOrderList).WithRequired(n => n.AsideBookInfo);

        }
    }
}
