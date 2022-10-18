using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class EDI_DocumentoNFEMap : EntityTypeConfiguration<EDI_DocumentoNFE>
    {
        public EDI_DocumentoNFEMap()
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
            this.ToTable("EDI_DocumentoNFE");
            this.Property(t => t.EDI_Chave).HasColumnName("EDI_Chave");
            this.Property(t => t.EDI_Motivo).HasColumnName("EDI_Motivo");
            this.Property(t => t.EDI_Data).HasColumnName("EDI_Data");
            this.Property(t => t.IdDocumentoNfe).HasColumnName("IdDocumentoNfe");
            this.Property(t => t.Data).HasColumnName("Data");
            this.Property(t => t.IdDocumento).HasColumnName("IdDocumento");
        }
    }
}
