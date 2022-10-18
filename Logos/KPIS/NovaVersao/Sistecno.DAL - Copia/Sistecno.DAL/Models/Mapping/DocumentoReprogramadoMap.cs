using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class DocumentoReprogramadoMap : EntityTypeConfiguration<DocumentoReprogramado>
    {
        public DocumentoReprogramadoMap()
        {
            // Primary Key
            this.HasKey(t => t.IdDocumentoReprogramado);

            // Properties
            this.Property(t => t.IdDocumentoReprogramado)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.NomeDoArquivo)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("DocumentoReprogramado");
            this.Property(t => t.IdDocumentoReprogramado).HasColumnName("IdDocumentoReprogramado");
            this.Property(t => t.IdDocumento).HasColumnName("IdDocumento");
            this.Property(t => t.DataParaEntrega).HasColumnName("DataParaEntrega");
            this.Property(t => t.DataSugerida).HasColumnName("DataSugerida");
            this.Property(t => t.DataGeracaoDoArquivo).HasColumnName("DataGeracaoDoArquivo");
            this.Property(t => t.NomeDoArquivo).HasColumnName("NomeDoArquivo");

            // Relationships
            this.HasRequired(t => t.Documento)
                .WithMany(t => t.DocumentoReprogramadoes)
                .HasForeignKey(d => d.IdDocumento);

        }
    }
}
