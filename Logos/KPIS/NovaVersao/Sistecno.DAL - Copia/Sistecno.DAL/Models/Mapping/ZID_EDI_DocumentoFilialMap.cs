using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ZID_EDI_DocumentoFilialMap : EntityTypeConfiguration<ZID_EDI_DocumentoFilial>
    {
        public ZID_EDI_DocumentoFilialMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("ZID_EDI_DocumentoFilial");
            this.Property(t => t.ID).HasColumnName("ID");
        }
    }
}
