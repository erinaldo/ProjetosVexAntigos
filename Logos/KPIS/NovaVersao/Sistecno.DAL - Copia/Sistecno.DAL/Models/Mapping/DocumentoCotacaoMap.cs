using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class DocumentoCotacaoMap : EntityTypeConfiguration<DocumentoCotacao>
    {
        public DocumentoCotacaoMap()
        {
            // Primary Key
            this.HasKey(t => t.IddocumentoCotacao);

            // Properties
            this.Property(t => t.IddocumentoCotacao)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("DocumentoCotacao");
            this.Property(t => t.IddocumentoCotacao).HasColumnName("IddocumentoCotacao");
            this.Property(t => t.IdDocumento).HasColumnName("IdDocumento");
            this.Property(t => t.IdCotacaoFornecedor).HasColumnName("IdCotacaoFornecedor");
            this.Property(t => t.IdCotacaoDeCompra).HasColumnName("IdCotacaoDeCompra");
        }
    }
}
