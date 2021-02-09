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
    class ViewConfigMap : EntityTypeConfiguration<ViewConfig>
    {

        public ViewConfigMap()
        {
            this.ToTable("ViewConfig");
            this.HasKey(m => m.ViewConfigId);
            this.Property(m => m.ViewConfigId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(m => m.Author);
            this.Property(m => m.Available);
            this.Property(m => m.Category);
            this.Property(m => m.Count);
            this.Property(m => m.Isbn);
            this.Property(m => m.PublicDate);
            this.Property(m => m.Title);
            this.Property(m => m.ViewName);
        }
    }
}
