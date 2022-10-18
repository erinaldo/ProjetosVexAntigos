using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class CotacaoDeCompraItemMap : EntityTypeConfiguration<CotacaoDeCompraItem>
    {
        public CotacaoDeCompraItemMap()
        {
            // Primary Key
            this.HasKey(t => t.IdCotacaoDeCompraItem);

            // Properties
            this.Property(t => t.IdCotacaoDeCompraItem)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Observacao)
                .HasMaxLength(300);

            // Table & Column Mappings
            this.ToTable("CotacaoDeCompraItem");
            this.Property(t => t.IdCotacaoDeCompraItem).HasColumnName("IdCotacaoDeCompraItem");
            this.Property(t => t.IdCotacaoFornecedor).HasColumnName("IdCotacaoFornecedor");
            this.Property(t => t.IdRequisicaoDeMaterialItem).HasColumnName("IdRequisicaoDeMaterialItem");
            this.Property(t => t.IdProdutoCliente).HasColumnName("IdProdutoCliente");
            this.Property(t => t.Quantidade).HasColumnName("Quantidade");
            this.Property(t => t.Saldo).HasColumnName("Saldo");
            this.Property(t => t.ValorUnitario).HasColumnName("ValorUnitario");
            this.Property(t => t.ValorTotalDoItem).HasColumnName("ValorTotalDoItem");
            this.Property(t => t.IdUnidadeDeMedida).HasColumnName("IdUnidadeDeMedida");
            this.Property(t => t.AliquotaDeIcms).HasColumnName("AliquotaDeIcms");
            this.Property(t => t.ValorDeIcms).HasColumnName("ValorDeIcms");
            this.Property(t => t.Desconto).HasColumnName("Desconto");
            this.Property(t => t.Acrescimo).HasColumnName("Acrescimo");
            this.Property(t => t.IdDocumentoItem).HasColumnName("IdDocumentoItem");
            this.Property(t => t.AliquotaDeIpi).HasColumnName("AliquotaDeIpi");
            this.Property(t => t.ValorDeIpi).HasColumnName("ValorDeIpi");
            this.Property(t => t.Observacao).HasColumnName("Observacao");
            this.Property(t => t.IdCentroDeCusto).HasColumnName("IdCentroDeCusto");

            // Relationships
            this.HasOptional(t => t.RequisicaoDeMaterialItem)
                .WithMany(t => t.CotacaoDeCompraItems)
                .HasForeignKey(d => d.IdRequisicaoDeMaterialItem);

        }
    }
}
