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
    public class ShareAsideBookMap: EntityTypeConfiguration<ShareAsideBook>
    {
        public ShareAsideBookMap()
        {
            this.ToTable("ShareAsideBook");
            this.HasKey(m => m.ShareAsideBookId);
            this.Property(m => m.ShareAsideBookId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(m => m.Author);
            this.Property(m => m.Title);
            this.Property(m => m.Category);
            this.Property(m => m.PublicDate);
            this.Property(m => m.SysAccountId);
            this.Property(m => m.BaseImageId);
            this.Property(m => m.ResearchAsideBookId);
            this.Property(m => m.Count);
            this.Property(m => m.Isbn);
            this.Property(m => m.OperaAccountId);
            this.Property(m => m.TrafficType);
            //this.Property(m => m.ShareCustomerInfo);
            this.Property(m => m.PayType);
            this.Property(m => m.PayMoney);
            this.Property(m => m.ShareStatus);
            this.Property(m => m.Status);
            this.Property(m => m.IsDelete);
            this.Property(m => m.CreateTime);
            this.Property(m => m.EditTime);
            HasRequired(t => t.SysAccount);
            HasRequired(t => t.BaseImage);
            HasRequired(t => t.ResearchAsideBook);
        }
    }
}
