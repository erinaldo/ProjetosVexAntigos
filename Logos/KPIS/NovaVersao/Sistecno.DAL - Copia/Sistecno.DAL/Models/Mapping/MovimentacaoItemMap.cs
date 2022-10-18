using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class MovimentacaoItemMap : EntityTypeConfiguration<MovimentacaoItem>
    {
        public MovimentacaoItemMap()
        {
            // Primary Key
            this.HasKey(t => t.IDMovimentacaoItem);

            // Properties
            this.Property(t => t.IDMovimentacaoItem)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.PedidoNotaFiscal)
                .HasMaxLength(30);

            this.Property(t => t.TipoMovto)
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.obs)
                .HasMaxLength(80);

            // Table & Column Mappings
            this.ToTable("MovimentacaoItem");
            this.Property(t => t.IDMovimentacaoItem).HasColumnName("IDMovimentacaoItem");
            this.Property(t => t.IDMovimentacao).HasColumnName("IDMovimentacao");
            this.Property(t => t.IDRomaneio).HasColumnName("IDRomaneio");
            this.Property(t => t.IDUnidadeDeArmazenagem).HasColumnName("IDUnidadeDeArmazenagem");
            this.Property(t => t.IDUnidadeDeArmazenagemLote).HasColumnName("IDUnidadeDeArmazenagemLote");
            this.Property(t => t.IDUnidadeDeArmazenagemDestino).HasColumnName("IDUnidadeDeArmazenagemDestino");
            this.Property(t => t.IDDepositoPlantaLocalizacaoOrigem).HasColumnName("IDDepositoPlantaLocalizacaoOrigem");
            this.Property(t => t.IDDepositoPlantaLocalizacaoDestino).HasColumnName("IDDepositoPlantaLocalizacaoDestino");
            this.Property(t => t.IDProdutoEmbalagem).HasColumnName("IDProdutoEmbalagem");
            this.Property(t => t.IDUsuario).HasColumnName("IDUsuario");
            this.Property(t => t.IDDocumento).HasColumnName("IDDocumento");
            this.Property(t => t.IDDocumentoItem).HasColumnName("IDDocumentoItem");
            this.Property(t => t.IDMapa).HasColumnName("IDMapa");
            this.Property(t => t.Quantidade).HasColumnName("Quantidade");
            this.Property(t => t.QuantidadeBaixada).HasColumnName("QuantidadeBaixada");
            this.Property(t => t.DataDeExecucao).HasColumnName("DataDeExecucao");
            this.Property(t => t.QuantidadeUnidadeEstoque).HasColumnName("QuantidadeUnidadeEstoque");
            this.Property(t => t.PedidoNotaFiscal).HasColumnName("PedidoNotaFiscal");
            this.Property(t => t.DataHoraPedidoNotaFiscal).HasColumnName("DataHoraPedidoNotaFiscal");
            this.Property(t => t.TipoMovto).HasColumnName("TipoMovto");
            this.Property(t => t.obs).HasColumnName("obs");
            this.Property(t => t.IdProdutoCliente).HasColumnName("IdProdutoCliente");
        }
    }
}
