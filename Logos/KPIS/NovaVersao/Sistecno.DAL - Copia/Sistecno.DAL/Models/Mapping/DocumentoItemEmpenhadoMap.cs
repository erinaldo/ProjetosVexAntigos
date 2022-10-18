using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class DocumentoItemEmpenhadoMap : EntityTypeConfiguration<DocumentoItemEmpenhado>
    {
        public DocumentoItemEmpenhadoMap()
        {
            // Primary Key
            this.HasKey(t => t.IdDocumentoItemEmpenhado);

            // Properties
            this.Property(t => t.IdDocumentoItemEmpenhado)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("DocumentoItemEmpenhado");
            this.Property(t => t.IdDocumentoItemEmpenhado).HasColumnName("IdDocumentoItemEmpenhado");
            this.Property(t => t.IdDocumentoItem).HasColumnName("IdDocumentoItem");
            this.Property(t => t.Quantidade).HasColumnName("Quantidade");

            // Relationships
            this.HasRequired(t => t.DocumentoItem)
                .WithMany(t => t.DocumentoItemEmpenhadoes)
                .HasForeignKey(d => d.IdDocumentoItem);

        }
    }
}
