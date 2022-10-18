using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class DocumentoOrcamentoMap : EntityTypeConfiguration<DocumentoOrcamento>
    {
        public DocumentoOrcamentoMap()
        {
            // Primary Key
            this.HasKey(t => t.IDDocumentoOrcamento);

            // Properties
            this.Property(t => t.IDDocumentoOrcamento)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Descricao)
                .HasMaxLength(500);

            this.Property(t => t.Nome)
                .HasMaxLength(100);

            this.Property(t => t.Extencao)
                .HasMaxLength(100);

            this.Property(t => t.Status)
                .HasMaxLength(30);

            this.Property(t => t.Motivo)
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("DocumentoOrcamento");
            this.Property(t => t.IDDocumentoOrcamento).HasColumnName("IDDocumentoOrcamento");
            this.Property(t => t.IDDocumento).HasColumnName("IDDocumento");
            this.Property(t => t.IDUsuario).HasColumnName("IDUsuario");
            this.Property(t => t.DataDoMovimento).HasColumnName("DataDoMovimento");
            this.Property(t => t.Descricao).HasColumnName("Descricao");
            this.Property(t => t.Nome).HasColumnName("Nome");
            this.Property(t => t.Extencao).HasColumnName("Extencao");
            this.Property(t => t.Arquivo).HasColumnName("Arquivo");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.Motivo).HasColumnName("Motivo");

            // Relationships
            this.HasRequired(t => t.Documento)
                .WithMany(t => t.DocumentoOrcamentoes)
                .HasForeignKey(d => d.IDDocumento);
            this.HasOptional(t => t.Usuario)
                .WithMany(t => t.DocumentoOrcamentoes)
                .HasForeignKey(d => d.IDUsuario);

        }
    }
}
