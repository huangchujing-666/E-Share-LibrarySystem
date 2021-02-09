using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibrarySystem.Domain;
using System.Data.Entity.ModelConfiguration;
using LibrarySystem.Domain.Model;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibrarySystem.Data.Mapping
{
    public class SysAccountMap : EntityTypeConfiguration<SysAccount>
    {
        public SysAccountMap()
        {
            this.ToTable("SysAccount");
            this.HasKey(m => m.SysAccountId);
            this.Property(m => m.SysAccountId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(m => m.Account);
            this.Property(m => m.BaseImageId);
            this.Property(m => m.CreateTime);
            this.Property(m => m.EditTime);
            this.Property(m => m.IsDelete);
            this.Property(m => m.MobilePhone);
            this.Property(m => m.NickName);
            this.Property(m => m.PassWord);
            this.Property(m => m.Status);
            this.Property(m => m.Email);
            this.Property(m => m.Address);
            this.Property(m => m.Status);
            this.Property(m => m.SysRoleId);
            this.Property(m => m.Remarks);
            this.Property(m => m.Token);
            this.Property(m => m.UniversityId);

            HasRequired(t => t.BaseImage);
            HasRequired(t => t.SysRole);
            HasRequired(t => t.University);

        }
    }
}
