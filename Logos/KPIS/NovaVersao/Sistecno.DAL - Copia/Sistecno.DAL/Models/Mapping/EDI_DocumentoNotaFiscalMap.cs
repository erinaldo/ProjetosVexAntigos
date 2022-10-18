using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class EDI_DocumentoNotaFiscalMap : EntityTypeConfiguration<EDI_DocumentoNotaFiscal>
    {
        public EDI_DocumentoNotaFiscalMap()
        {
            // Primary Key
            this.HasKey(t => t.EDI_Chave);

            // Properties
            this.Property(t => t.EDI_Chave)
                .IsRequired()
                .HasMaxLength(40);

            // Table & Column Mappings
            this.ToTable("EDI_DocumentoNotaFiscal");
            this.Property(t => t.EDI_Chave).HasColumnName("EDI_Chave");
            this.Property(t => t.EDI_Motivo).HasColumnName("EDI_Motivo");
            this.Property(t => t.EDI_Data).HasColumnName("EDI_Data");
            this.Property(t => t.IDDocumentoNotaFiscal).HasColumnName("IDDocumentoNotaFiscal");
            this.Property(t => t.IDDocumentoOrigem).HasColumnName("IDDocumentoOrigem");
            this.Property(t => t.IDNotaFiscal).HasColumnName("IDNotaFiscal");
        }
    }
}
