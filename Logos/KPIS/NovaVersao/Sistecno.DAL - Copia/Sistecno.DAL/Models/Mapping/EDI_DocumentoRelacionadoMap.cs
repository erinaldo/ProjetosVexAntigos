using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class EDI_DocumentoRelacionadoMap : EntityTypeConfiguration<EDI_DocumentoRelacionado>
    {
        public EDI_DocumentoRelacionadoMap()
        {
            // Primary Key
            this.HasKey(t => t.EDI_Chave);

            // Properties
            this.Property(t => t.EDI_Chave)
                .IsRequired()
                .HasMaxLength(40);

            this.Property(t => t.EDI_Motivo)
                .HasMaxLength(500);

            // Table & Column Mappings
            this.ToTable("EDI_DocumentoRelacionado");
            this.Property(t => t.EDI_Chave).HasColumnName("EDI_Chave");
            this.Property(t => t.EDI_Motivo).HasColumnName("EDI_Motivo");
            this.Property(t => t.EDI_Data).HasColumnName("EDI_Data");
            this.Property(t => t.IdDocumentoRelacionado).HasColumnName("IdDocumentoRelacionado");
            this.Property(t => t.IdDocumentoPai).HasColumnName("IdDocumentoPai");
            this.Property(t => t.IdDocumentoFilho).HasColumnName("IdDocumentoFilho");
            this.Property(t => t.IdAgrupamento).HasColumnName("IdAgrupamento");
            this.Property(t => t.IdEDI_DocumentoRelacionado).HasColumnName("IdEDI_DocumentoRelacionado");
        }
    }
}
