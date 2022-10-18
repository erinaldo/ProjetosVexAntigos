using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ZID_DocumentoItemComplementoMap : EntityTypeConfiguration<ZID_DocumentoItemComplemento>
    {
        public ZID_DocumentoItemComplementoMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("ZID_DocumentoItemComplemento");
            this.Property(t => t.ID).HasColumnName("ID");
        }
    }
}
