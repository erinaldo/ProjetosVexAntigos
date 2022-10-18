using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class EstoqueMovMap : EntityTypeConfiguration<EstoqueMov>
    {
        public EstoqueMovMap()
        {
            // Primary Key
            this.HasKey(t => t.IDEstoqueMov);

            // Properties
            this.Property(t => t.IDEstoqueMov)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Historico)
                .IsRequired()
                .HasMaxLength(300);

            // Table & Column Mappings
            this.ToTable("EstoqueMov");
            this.Property(t => t.IDEstoqueMov).HasColumnName("IDEstoqueMov");
            this.Property(t => t.IDEstoque).HasColumnName("IDEstoque");
            this.Property(t => t.IDUnidadeDeArmazenagemLote).HasColumnName("IDUnidadeDeArmazenagemLote");
            this.Property(t => t.IDProdutoCliente).HasColumnName("IDProdutoCliente");
            this.Property(t => t.IDDocumento).HasColumnName("IDDocumento");
            this.Property(t => t.IDDepositoPlantaLocalizacaoOrigem).HasColumnName("IDDepositoPlantaLocalizacaoOrigem");
            this.Property(t => t.IDDepositoPlantaLocalizacaoDestino).HasColumnName("IDDepositoPlantaLocalizacaoDestino");
            this.Property(t => t.IDEstoqueOperacao).HasColumnName("IDEstoqueOperacao");
            this.Property(t => t.IDUsuario).HasColumnName("IDUsuario");
            this.Property(t => t.IdMovimentacaoItem).HasColumnName("IdMovimentacaoItem");
            this.Property(t => t.Historico).HasColumnName("Historico");
            this.Property(t => t.DataHora).HasColumnName("DataHora");
            this.Property(t => t.QuantidadeSolicitada).HasColumnName("QuantidadeSolicitada");
            this.Property(t => t.UnidadeDoCliente).HasColumnName("UnidadeDoCliente");
            this.Property(t => t.Quantidade).HasColumnName("Quantidade");
            this.Property(t => t.Saldo).HasColumnName("Saldo");
            this.Property(t => t.ValorEmEstoque).HasColumnName("ValorEmEstoque");
            this.Property(t => t.Aliquota).HasColumnName("Aliquota");
            this.Property(t => t.ValorIcms).HasColumnName("ValorIcms");
            this.Property(t => t.ValorIcmsAcumulado).HasColumnName("ValorIcmsAcumulado");
            this.Property(t => t.ValorEmEstoqueAcumulado).HasColumnName("ValorEmEstoqueAcumulado");
        }
    }
}
