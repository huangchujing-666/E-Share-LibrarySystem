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
    public class AsideBookOrderMap : EntityTypeConfiguration<AsideBookOrder>
    {
        public AsideBookOrderMap()
        {
            this.ToTable("AsideBookOrder");
            this.HasKey(m => m.AsideBookOrderId);
            this.Property(m => m.AsideBookOrderId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(m => m.AsideBookInfoId);
            this.Property(m => m.SysAccountId);
            this.Property(m => m.ExpressId);
            this.Property(m => m.TrafficAccountId);
            this.Property(m => m.Count);
            this.Property(m => m.TrafficType);
            this.Property(m => m.Remark);
            this.Property(m => m.OrderStatus);
            //this.Property(m => m.CustomerInfo);
            this.Property(m => m.TrafficFee);
            this.Property(m => m.SenderInfo);
            this.Property(m => m.Status);
            this.Property(m => m.IsDelete);
            this.Property(m => m.CreateTime);
            this.Property(m => m.EditTime);
            HasRequired(t => t.AsideBookInfo);
            HasRequired(t => t.SysAccount);
            HasRequired(t => t.Express);
        }
    }
}
