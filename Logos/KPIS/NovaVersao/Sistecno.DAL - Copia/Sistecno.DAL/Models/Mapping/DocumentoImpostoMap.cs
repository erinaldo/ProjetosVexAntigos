using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class DocumentoImpostoMap : EntityTypeConfiguration<DocumentoImposto>
    {
        public DocumentoImpostoMap()
        {
            // Primary Key
            this.HasKey(t => t.IdDocumentoImposto);

            // Properties
            this.Property(t => t.IdDocumentoImposto)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.IssRetido)
                .HasMaxLength(3);

            // Table & Column Mappings
            this.ToTable("DocumentoImposto");
            this.Property(t => t.IdDocumentoImposto).HasColumnName("IdDocumentoImposto");
            this.Property(t => t.IdDocumento).HasColumnName("IdDocumento");
            this.Property(t => t.Pis).HasColumnName("Pis");
            this.Property(t => t.Cofins).HasColumnName("Cofins");
            this.Property(t => t.Inss).HasColumnName("Inss");
            this.Property(t => t.IRRF).HasColumnName("IRRF");
            this.Property(t => t.CSLL).HasColumnName("CSLL");
            this.Property(t => t.IdCodigoDoServico).HasColumnName("IdCodigoDoServico");
            this.Property(t => t.AliquotaServicos).HasColumnName("AliquotaServicos");
            this.Property(t => t.IssRetido).HasColumnName("IssRetido");
        }
    }
}
