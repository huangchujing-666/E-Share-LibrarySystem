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
    public class EmailCodeMap : EntityTypeConfiguration<EmailCode>
    {
        public EmailCodeMap()
        {
            this.ToTable("EmailCode");
            this.HasKey(m => m.EmailCodeId);
            this.Property(m => m.EmailCodeId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(m => m.Code);
            this.Property(m => m.Email);
            this.Property(m => m.CreateTime);
            this.Property(m => m.Type);
        }
    }
}
