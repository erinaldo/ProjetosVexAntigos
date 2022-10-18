using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ZID_DocumentoFreteMap : EntityTypeConfiguration<ZID_DocumentoFrete>
    {
        public ZID_DocumentoFreteMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("ZID_DocumentoFrete");
            this.Property(t => t.ID).HasColumnName("ID");
        }
    }
}
