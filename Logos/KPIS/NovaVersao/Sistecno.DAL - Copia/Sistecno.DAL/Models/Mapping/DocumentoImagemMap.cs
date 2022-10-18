using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class DocumentoImagemMap : EntityTypeConfiguration<DocumentoImagem>
    {
        public DocumentoImagemMap()
        {
            // Primary Key
            this.HasKey(t => t.IDDocumentoImagem);

            // Properties
            this.Property(t => t.IDDocumentoImagem)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Titulo)
                .IsRequired()
                .HasMaxLength(60);

            // Table & Column Mappings
            this.ToTable("DocumentoImagem");
            this.Property(t => t.IDDocumentoImagem).HasColumnName("IDDocumentoImagem");
            this.Property(t => t.IDDocumento).HasColumnName("IDDocumento");
            this.Property(t => t.Titulo).HasColumnName("Titulo");
            this.Property(t => t.Imagem).HasColumnName("Imagem");

            // Relationships
            this.HasRequired(t => t.Documento)
                .WithMany(t => t.DocumentoImagems)
                .HasForeignKey(d => d.IDDocumento);

        }
    }
}
