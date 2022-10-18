using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class EDI_DocumentoAguardandoCtrcMap : EntityTypeConfiguration<EDI_DocumentoAguardandoCtrc>
    {
        public EDI_DocumentoAguardandoCtrcMap()
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
            this.ToTable("EDI_DocumentoAguardandoCtrc");
            this.Property(t => t.EDI_Chave).HasColumnName("EDI_Chave");
            this.Property(t => t.EDI_Motivo).HasColumnName("EDI_Motivo");
            this.Property(t => t.EDI_Data).HasColumnName("EDI_Data");
            this.Property(t => t.IdDocumentoAguardandoCTRC).HasColumnName("IdDocumentoAguardandoCTRC");
            this.Property(t => t.IdFilial).HasColumnName("IdFilial");
            this.Property(t => t.IdDocumento).HasColumnName("IdDocumento");
        }
    }
}
