using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class DocumentoItemLabelPickingMap : EntityTypeConfiguration<DocumentoItemLabelPicking>
    {
        public DocumentoItemLabelPickingMap()
        {
            // Primary Key
            this.HasKey(t => t.IdDocumentoItemLabelPicking);

            // Properties
            this.Property(t => t.IdDocumentoItemLabelPicking)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Ordem)
                .IsRequired()
                .HasMaxLength(15);

            // Table & Column Mappings
            this.ToTable("DocumentoItemLabelPicking");
            this.Property(t => t.IdDocumentoItemLabelPicking).HasColumnName("IdDocumentoItemLabelPicking");
            this.Property(t => t.IdDocumentoItem).HasColumnName("IdDocumentoItem");
            this.Property(t => t.Ordem).HasColumnName("Ordem");
            this.Property(t => t.Impresso).HasColumnName("Impresso");

            // Relationships
            this.HasRequired(t => t.DocumentoItem)
                .WithMany(t => t.DocumentoItemLabelPickings)
                .HasForeignKey(d => d.IdDocumentoItem);

        }
    }
}
