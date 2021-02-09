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
    public class ExpressMap: EntityTypeConfiguration<Express>
    {
        public ExpressMap()
        {
            this.ToTable("Express");
            this.HasKey(m => m.ExpressId);
            this.Property(m => m.ExpressId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(m => m.ExpressName);
            this.Property(m => m.ExpressNo);
            this.Property(m => m.TrafficFee);
        }
    }
}
