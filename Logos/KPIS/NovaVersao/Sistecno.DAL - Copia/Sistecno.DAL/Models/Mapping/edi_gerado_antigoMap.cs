using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class edi_gerado_antigoMap : EntityTypeConfiguration<edi_gerado_antigo>
    {
        public edi_gerado_antigoMap()
        {
            // Primary Key
            this.HasKey(t => t.IdEdi_Gerado);

            // Properties
            this.Property(t => t.IdEdi_Gerado)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("edi_gerado_antigo");
            this.Property(t => t.IdEdi_Gerado).HasColumnName("IdEdi_Gerado");
            this.Property(t => t.IdDocumentoItem).HasColumnName("IdDocumentoItem");
            this.Property(t => t.IdDocumento).HasColumnName("IdDocumento");
            this.Property(t => t.Quantidade).HasColumnName("Quantidade");
        }
    }
}
