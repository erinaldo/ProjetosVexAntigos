using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class DocumentoCDAMap : EntityTypeConfiguration<DocumentoCDA>
    {
        public DocumentoCDAMap()
        {
            // Primary Key
            this.HasKey(t => t.IdDocumentoCDA);

            // Properties
            this.Property(t => t.IdDocumentoCDA)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Situacao)
                .HasMaxLength(60);

            // Table & Column Mappings
            this.ToTable("DocumentoCDA");
            this.Property(t => t.IdDocumentoCDA).HasColumnName("IdDocumentoCDA");
            this.Property(t => t.IdDocumento).HasColumnName("IdDocumento");
            this.Property(t => t.Situacao).HasColumnName("Situacao");

            // Relationships
            this.HasRequired(t => t.Documento)
                .WithMany(t => t.DocumentoCDAs)
                .HasForeignKey(d => d.IdDocumento);

        }
    }
}
