using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class CotacaoFornecedorCondPgtoMap : EntityTypeConfiguration<CotacaoFornecedorCondPgto>
    {
        public CotacaoFornecedorCondPgtoMap()
        {
            // Primary Key
            this.HasKey(t => t.IdCotacaoFornecedorCondPgto);

            // Properties
            this.Property(t => t.IdCotacaoFornecedorCondPgto)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("CotacaoFornecedorCondPgto");
            this.Property(t => t.IdCotacaoFornecedorCondPgto).HasColumnName("IdCotacaoFornecedorCondPgto");
            this.Property(t => t.IdCotacaoFornecedor).HasColumnName("IdCotacaoFornecedor");
            this.Property(t => t.Vencimento).HasColumnName("Vencimento");
            this.Property(t => t.Valor).HasColumnName("Valor");

            // Relationships
            this.HasRequired(t => t.CotacaoFornecedor)
                .WithMany(t => t.CotacaoFornecedorCondPgtoes)
                .HasForeignKey(d => d.IdCotacaoFornecedor);

        }
    }
}
