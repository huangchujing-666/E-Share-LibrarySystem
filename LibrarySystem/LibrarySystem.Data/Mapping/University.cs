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
    public class UniversityMap:EntityTypeConfiguration<University>
    {
        public UniversityMap()
        {
            this.ToTable("University");
            this.HasKey(m => m.UniversityId);
            this.Property(m => m.UniversityId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(m => m.Name);
            this.Property(m => m.Service);
            this.Property(m => m.DataBase);
            this.Property(m => m.UserId);
            this.Property(m => m.UserPwd);
            this.Property(m => m.TimeStart);
            this.Property(m => m.Status);
            this.Property(m => m.IsDelete);
            this.Property(m => m.CreateTime);
            this.Property(m => m.EditTime);
            this.Property(m => m.IsUpdate);
        }
    }
}
