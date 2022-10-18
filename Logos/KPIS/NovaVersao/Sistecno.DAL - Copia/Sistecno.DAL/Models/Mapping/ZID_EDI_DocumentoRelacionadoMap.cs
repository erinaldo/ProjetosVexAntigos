using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ZID_EDI_DocumentoRelacionadoMap : EntityTypeConfiguration<ZID_EDI_DocumentoRelacionado>
    {
        public ZID_EDI_DocumentoRelacionadoMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("ZID_EDI_DocumentoRelacionado");
            this.Property(t => t.ID).HasColumnName("ID");
        }
    }
}