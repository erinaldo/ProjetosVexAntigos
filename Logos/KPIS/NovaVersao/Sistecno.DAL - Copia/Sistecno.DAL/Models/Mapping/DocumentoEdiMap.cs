using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class DocumentoEdiMap : EntityTypeConfiguration<DocumentoEdi>
    {
        public DocumentoEdiMap()
        {
            // Primary Key
            this.HasKey(t => t.IdDocumentoEdi);

            // Properties
            this.Property(t => t.IdDocumentoEdi)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Tipo)
                .HasMaxLength(10);

            this.Property(t => t.NomeDoArquivo)
                .HasMaxLength(1000);

            // Table & Column Mappings
            this.ToTable("DocumentoEdi");
            this.Property(t => t.IdDocumentoEdi).HasColumnName("IdDocumentoEdi");
            this.Property(t => t.IdEdi).HasColumnName("IdEdi");
            this.Property(t => t.Tipo).HasColumnName("Tipo");
            this.Property(t => t.IdDocumento).HasColumnName("IdDocumento");
            this.Property(t => t.IdTitulo).HasColumnName("IdTitulo");
            this.Property(t => t.Data).HasColumnName("Data");
            this.Property(t => t.IdUsuario).HasColumnName("IdUsuario");
            this.Property(t => t.NomeDoArquivo).HasColumnName("NomeDoArquivo");

            // Relationships
            this.HasOptional(t => t.Documento)
                .WithMany(t => t.DocumentoEdis)
                .HasForeignKey(d => d.IdDocumento);
            this.HasOptional(t => t.EDI)
                .WithMany(t => t.DocumentoEdis)
                .HasForeignKey(d => d.IdEdi);
            this.HasOptional(t => t.Titulo)
                .WithMany(t => t.DocumentoEdis)
                .HasForeignKey(d => d.IdTitulo);

        }
    }
}
