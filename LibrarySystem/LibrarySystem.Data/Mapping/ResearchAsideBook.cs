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
    public class ResearchAsideBookMap : EntityTypeConfiguration<ResearchAsideBook>
    {
        public ResearchAsideBookMap()
        {
            this.ToTable("ResearchAsideBook");
            this.HasKey(m => m.ResearchAsideBookId);
            this.Property(m => m.ResearchAsideBookId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(m => m.Author);
            this.Property(m => m.Title);
            this.Property(m => m.Category);
            this.Property(m => m.PublicDate);
            this.Property(m => m.Remark);
            this.Property(m => m.UniversityId);
            this.Property(m => m.SysAccountId);
            //this.Property(m => m.CustomerInfo);
            this.Property(m => m.BaseImageId);
            this.Property(m => m.Isbn);
            this.Property(m => m.ShareSysAccountId);
            this.Property(m => m.TrafficType);
            //this.Property(m => m.ShareCustomerInfo);
            this.Property(m => m.PayType);
            this.Property(m => m.PayMoney);
            this.Property(m => m.ResearchStatus);
            this.Property(m => m.Status);
            this.Property(m => m.IsDelete);
            this.Property(m => m.CreateTime);
            this.Property(m => m.EditTime);
            HasRequired(t => t.University);
            HasRequired(t => t.SysAccount);
            HasRequired(t => t.BaseImage);
        }
    }
}
