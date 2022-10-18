using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class DocumentoOcorrenciaItemMap : EntityTypeConfiguration<DocumentoOcorrenciaItem>
    {
        public DocumentoOcorrenciaItemMap()
        {
            // Primary Key
            this.HasKey(t => t.IdDocumentoOcorrenciaItem);

            // Properties
            this.Property(t => t.IdDocumentoOcorrenciaItem)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Codigo)
                .HasMaxLength(20);

            this.Property(t => t.Descricao)
                .HasMaxLength(60);

            // Table & Column Mappings
            this.ToTable("DocumentoOcorrenciaItem");
            this.Property(t => t.IdDocumentoOcorrenciaItem).HasColumnName("IdDocumentoOcorrenciaItem");
            this.Property(t => t.IdDocumentoOcorrencia).HasColumnName("IdDocumentoOcorrencia");
            this.Property(t => t.IdDocumentoItem).HasColumnName("IdDocumentoItem");
            this.Property(t => t.Codigo).HasColumnName("Codigo");
            this.Property(t => t.Descricao).HasColumnName("Descricao");
            this.Property(t => t.Quantidade).HasColumnName("Quantidade");
            this.Property(t => t.ValorUnitario).HasColumnName("ValorUnitario");
            this.Property(t => t.TotalDoItem).HasColumnName("TotalDoItem");

            // Relationships
            this.HasOptional(t => t.DocumentoItem)
                .WithMany(t => t.DocumentoOcorrenciaItems)
                .HasForeignKey(d => d.IdDocumentoItem);
            this.HasRequired(t => t.DocumentoOcorrencia)
                .WithMany(t => t.DocumentoOcorrenciaItems)
                .HasForeignKey(d => d.IdDocumentoOcorrencia);

        }
    }
}
