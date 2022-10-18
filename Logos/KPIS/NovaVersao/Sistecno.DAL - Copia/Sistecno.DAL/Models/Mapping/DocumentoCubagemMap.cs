using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class DocumentoCubagemMap : EntityTypeConfiguration<DocumentoCubagem>
    {
        public DocumentoCubagemMap()
        {
            // Primary Key
            this.HasKey(t => t.IDDocumentoCubagem);

            // Properties
            this.Property(t => t.IDDocumentoCubagem)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("DocumentoCubagem");
            this.Property(t => t.IDDocumentoCubagem).HasColumnName("IDDocumentoCubagem");
            this.Property(t => t.IDDocumento).HasColumnName("IDDocumento");
            this.Property(t => t.Largura).HasColumnName("Largura");
            this.Property(t => t.Profundidade).HasColumnName("Profundidade");
            this.Property(t => t.Altura).HasColumnName("Altura");
            this.Property(t => t.Quantidade).HasColumnName("Quantidade");
            this.Property(t => t.VolumeTotal).HasColumnName("VolumeTotal");

            // Relationships
            this.HasRequired(t => t.Documento)
                .WithMany(t => t.DocumentoCubagems)
                .HasForeignKey(d => d.IDDocumento);

        }
    }
}
