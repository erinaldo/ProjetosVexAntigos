using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class DocumentoEmbalagemMap : EntityTypeConfiguration<DocumentoEmbalagem>
    {
        public DocumentoEmbalagemMap()
        {
            // Primary Key
            this.HasKey(t => t.IdDocumentoEmbalagem);

            // Properties
            this.Property(t => t.IdDocumentoEmbalagem)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("DocumentoEmbalagem");
            this.Property(t => t.IdDocumentoEmbalagem).HasColumnName("IdDocumentoEmbalagem");
            this.Property(t => t.IdDocumento).HasColumnName("IdDocumento");
            this.Property(t => t.Ordem).HasColumnName("Ordem");

            // Relationships
            this.HasRequired(t => t.Documento)
                .WithMany(t => t.DocumentoEmbalagems)
                .HasForeignKey(d => d.IdDocumento);

        }
    }
}
