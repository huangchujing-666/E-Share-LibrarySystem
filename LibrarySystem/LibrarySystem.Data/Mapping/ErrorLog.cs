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
    public class ErrorLogMap : EntityTypeConfiguration<ErrorLog>
    {
        public ErrorLogMap()
        {
            this.ToTable("ErrorLog");
            this.HasKey(m => m.ErrorLogId);
            this.Property(m => m.ErrorLogId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(m => m.ErrorData);
            this.Property(m => m.ErrorMsg);
            this.Property(m => m.ErrorSrc);
            this.Property(m => m.ErrorTime);
            this.Property(m => m.StartTime);
            this.Property(m => m.EndTime);
            this.Property(m => m.Status);
            this.Property(m => m.SysAccountId);
            this.Property(m => m.Type);
            this.Property(m => m.UniversityId);
            this.Property(m => m.UpdateCount);

            HasRequired(t => t.University);
            HasRequired(t => t.SysAccount);
        } 
     }
}
