using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class CotacaoFornecedorMap : EntityTypeConfiguration<CotacaoFornecedor>
    {
        public CotacaoFornecedorMap()
        {
            // Primary Key
            this.HasKey(t => t.IdCotacaoFornecedor);

            // Properties
            this.Property(t => t.IdCotacaoFornecedor)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Aprovada)
                .HasMaxLength(3);

            this.Property(t => t.ConcluidoSite)
                .HasMaxLength(3);

            this.Property(t => t.Responsavel)
                .HasMaxLength(60);

            // Table & Column Mappings
            this.ToTable("CotacaoFornecedor");
            this.Property(t => t.IdCotacaoFornecedor).HasColumnName("IdCotacaoFornecedor");
            this.Property(t => t.IdFornecedor).HasColumnName("IdFornecedor");
            this.Property(t => t.IdCotacaoDeCompra).HasColumnName("IdCotacaoDeCompra");
            this.Property(t => t.DataDeCotacao).HasColumnName("DataDeCotacao");
            this.Property(t => t.ValidadeDeCotacao).HasColumnName("ValidadeDeCotacao");
            this.Property(t => t.DataDeEntrega).HasColumnName("DataDeEntrega");
            this.Property(t => t.IdCondicaoDePagamento).HasColumnName("IdCondicaoDePagamento");
            this.Property(t => t.ValorTotalDeCompra).HasColumnName("ValorTotalDeCompra");
            this.Property(t => t.BaseDeIcms).HasColumnName("BaseDeIcms");
            this.Property(t => t.ValorDeIcms).HasColumnName("ValorDeIcms");
            this.Property(t => t.Desconto).HasColumnName("Desconto");
            this.Property(t => t.Acrescimo).HasColumnName("Acrescimo");
            this.Property(t => t.Aprovada).HasColumnName("Aprovada");
            this.Property(t => t.PrazoDeEntrega).HasColumnName("PrazoDeEntrega");
            this.Property(t => t.IdDocumento).HasColumnName("IdDocumento");
            this.Property(t => t.ConcluidoSite).HasColumnName("ConcluidoSite");
            this.Property(t => t.Responsavel).HasColumnName("Responsavel");
            this.Property(t => t.ValorDeIpi).HasColumnName("ValorDeIpi");
            this.Property(t => t.ValorDeFrete).HasColumnName("ValorDeFrete");
        }
    }
}
