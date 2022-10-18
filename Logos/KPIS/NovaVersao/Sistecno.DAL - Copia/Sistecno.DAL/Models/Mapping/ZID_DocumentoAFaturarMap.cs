using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ZID_DocumentoAFaturarMap : EntityTypeConfiguration<ZID_DocumentoAFaturar>
    {
        public ZID_DocumentoAFaturarMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("ZID_DocumentoAFaturar");
            this.Property(t => t.ID).HasColumnName("ID");
        }
    }
}
