using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class DocumentoRecebimentoMap : EntityTypeConfiguration<DocumentoRecebimento>
    {
        public DocumentoRecebimentoMap()
        {
            // Primary Key
            this.HasKey(t => t.IdDocumentoRecebimento);

            // Properties
            this.Property(t => t.IdDocumentoRecebimento)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Transferido)
                .IsFixedLength()
                .HasMaxLength(3);

            this.Property(t => t.Status)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("DocumentoRecebimento");
            this.Property(t => t.IdDocumentoRecebimento).HasColumnName("IdDocumentoRecebimento");
            this.Property(t => t.IdDocumento).HasColumnName("IdDocumento");
            this.Property(t => t.IdTitulo).HasColumnName("IdTitulo");
            this.Property(t => t.Transferido).HasColumnName("Transferido");
            this.Property(t => t.idUsuario).HasColumnName("idUsuario");
            this.Property(t => t.DataDoRecebimento).HasColumnName("DataDoRecebimento");
            this.Property(t => t.Status).HasColumnName("Status");

            // Relationships
            this.HasOptional(t => t.Documento)
                .WithMany(t => t.DocumentoRecebimentoes)
                .HasForeignKey(d => d.IdDocumento);

        }
    }
}
