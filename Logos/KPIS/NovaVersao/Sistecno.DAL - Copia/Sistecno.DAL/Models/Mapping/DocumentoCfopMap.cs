using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class DocumentoCfopMap : EntityTypeConfiguration<DocumentoCfop>
    {
        public DocumentoCfopMap()
        {
            // Primary Key
            this.HasKey(t => t.IDDocumentoCfop);

            // Properties
            this.Property(t => t.IDDocumentoCfop)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("DocumentoCfop");
            this.Property(t => t.IDDocumentoCfop).HasColumnName("IDDocumentoCfop");
            this.Property(t => t.IDDocumento).HasColumnName("IDDocumento");
            this.Property(t => t.IDCfop).HasColumnName("IDCfop");
            this.Property(t => t.Valor).HasColumnName("Valor");
            this.Property(t => t.ValorDaNota).HasColumnName("ValorDaNota");

            // Relationships
            this.HasRequired(t => t.Cfop)
                .WithMany(t => t.DocumentoCfops)
                .HasForeignKey(d => d.IDCfop);
            this.HasRequired(t => t.Documento)
                .WithMany(t => t.DocumentoCfops)
                .HasForeignKey(d => d.IDDocumento);

        }
    }
}
