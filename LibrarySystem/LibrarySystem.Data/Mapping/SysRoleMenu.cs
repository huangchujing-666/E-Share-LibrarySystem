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
    public class SysRoleMenuMap : EntityTypeConfiguration<SysRoleMenu>
    {
        public SysRoleMenuMap()
        {
            this.ToTable("SysRoleMenu");
            this.HasKey(m => m.SysRoleMenuId);
            this.Property(m => m.SysRoleMenuId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(m => m.CreateTime);
            this.Property(m => m.EditTime); 
            this.Property(m => m.SysMenuId);
            this.Property(m => m.SysRoleId);
             
        }
    }
}
