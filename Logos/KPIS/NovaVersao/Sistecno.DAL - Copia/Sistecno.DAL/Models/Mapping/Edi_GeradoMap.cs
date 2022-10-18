using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class Edi_GeradoMap : EntityTypeConfiguration<Edi_Gerado>
    {
        public Edi_GeradoMap()
        {
            // Primary Key
            this.HasKey(t => t.IdEdi_Gerado);

            // Properties
            this.Property(t => t.IdEdi_Gerado)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("Edi_Gerado");
            this.Property(t => t.IdEdi_Gerado).HasColumnName("IdEdi_Gerado");
            this.Property(t => t.IdDocumentoItem).HasColumnName("IdDocumentoItem");
            this.Property(t => t.IdDocumento).HasColumnName("IdDocumento");
            this.Property(t => t.Quantidade).HasColumnName("Quantidade");
        }
    }
}
