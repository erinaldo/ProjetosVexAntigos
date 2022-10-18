using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ZID_DocumentoFilialMap : EntityTypeConfiguration<ZID_DocumentoFilial>
    {
        public ZID_DocumentoFilialMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("ZID_DocumentoFilial");
            this.Property(t => t.ID).HasColumnName("ID");
        }
    }
}
