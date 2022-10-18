using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ZID_DocumentoRelacionadoMap : EntityTypeConfiguration<ZID_DocumentoRelacionado>
    {
        public ZID_DocumentoRelacionadoMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("ZID_DocumentoRelacionado");
            this.Property(t => t.ID).HasColumnName("ID");
        }
    }
}
