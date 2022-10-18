using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class EDI_DocumentoEdiMap : EntityTypeConfiguration<EDI_DocumentoEdi>
    {
        public EDI_DocumentoEdiMap()
        {
            // Primary Key
            this.HasKey(t => new { t.EDI_Chave, t.IdDocumentoEdi });

            // Properties
            this.Property(t => t.EDI_Chave)
                .IsRequired()
                .HasMaxLength(40);

            this.Property(t => t.EDI_Motivo)
                .HasMaxLength(500);

            this.Property(t => t.IdDocumentoEdi)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Tipo)
                .HasMaxLength(10);

            this.Property(t => t.NomeDoArquivo)
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("EDI_DocumentoEdi");
            this.Property(t => t.EDI_Chave).HasColumnName("EDI_Chave");
            this.Property(t => t.EDI_Motivo).HasColumnName("EDI_Motivo");
            this.Property(t => t.IdDocumentoEdi).HasColumnName("IdDocumentoEdi");
            this.Property(t => t.IdEdi).HasColumnName("IdEdi");
            this.Property(t => t.Tipo).HasColumnName("Tipo");
            this.Property(t => t.IdDocumento).HasColumnName("IdDocumento");
            this.Property(t => t.IdTitulo).HasColumnName("IdTitulo");
            this.Property(t => t.Data).HasColumnName("Data");
            this.Property(t => t.IdUsuario).HasColumnName("IdUsuario");
            this.Property(t => t.NomeDoArquivo).HasColumnName("NomeDoArquivo");
        }
    }
}
