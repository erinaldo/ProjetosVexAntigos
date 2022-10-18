using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class DOCUMENTOPEDIDOITEMMap : EntityTypeConfiguration<DOCUMENTOPEDIDOITEM>
    {
        public DOCUMENTOPEDIDOITEMMap()
        {
            // Primary Key
            this.HasKey(t => new { t.IDDocumentoItem, t.IDProdutoEmbalagem, t.IDUsuario, t.Quantidade });

            // Properties
            this.Property(t => t.IDDocumentoItem)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.IDProdutoEmbalagem)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.IDUsuario)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Referencia)
                .HasMaxLength(30);

            this.Property(t => t.Quantidade)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.StatusSeparacao)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.EstoqueProcessado)
                .IsFixedLength()
                .HasMaxLength(3);

            this.Property(t => t.StatusDoItem)
                .HasMaxLength(50);

            this.Property(t => t.CfopOrigem)
                .HasMaxLength(4);

            this.Property(t => t.TipoDeItem)
                .IsFixedLength()
                .HasMaxLength(2);

            this.Property(t => t.SituacaoTributariaIPI)
                .HasMaxLength(2);

            this.Property(t => t.SituacaoTributariaPIS)
                .HasMaxLength(2);

            this.Property(t => t.SituacaoTributariaCofins)
                .HasMaxLength(2);

            this.Property(t => t.SituacaoTributariaICMS)
                .HasMaxLength(3);

            // Table & Column Mappings
            this.ToTable("DOCUMENTOPEDIDOITEM");
            this.Property(t => t.IDDocumentoItem).HasColumnName("IDDocumentoItem");
            this.Property(t => t.IDDocumento).HasColumnName("IDDocumento");
            this.Property(t => t.IDProdutoEmbalagem).HasColumnName("IDProdutoEmbalagem");
            this.Property(t => t.IDUsuario).HasColumnName("IDUsuario");
            this.Property(t => t.IDClienteDivisao).HasColumnName("IDClienteDivisao");
            this.Property(t => t.IDDocumentoOrigem).HasColumnName("IDDocumentoOrigem");
            this.Property(t => t.IDDocumentoItemOrigem).HasColumnName("IDDocumentoItemOrigem");
            this.Property(t => t.IDCFOP).HasColumnName("IDCFOP");
            this.Property(t => t.IDTES).HasColumnName("IDTES");
            this.Property(t => t.IDLote).HasColumnName("IDLote");
            this.Property(t => t.IDOcorrencia).HasColumnName("IDOcorrencia");
            this.Property(t => t.Referencia).HasColumnName("Referencia");
            this.Property(t => t.Quantidade).HasColumnName("Quantidade");
            this.Property(t => t.QuantidadeUnidadeEstoque).HasColumnName("QuantidadeUnidadeEstoque");
            this.Property(t => t.Saldo).HasColumnName("Saldo");
            this.Property(t => t.UnidadeLogistica).HasColumnName("UnidadeLogistica");
            this.Property(t => t.StatusSeparacao).HasColumnName("StatusSeparacao");
            this.Property(t => t.ValorUnitario).HasColumnName("ValorUnitario");
            this.Property(t => t.ValorTotalDoItem).HasColumnName("ValorTotalDoItem");
            this.Property(t => t.ValorDoDesconto).HasColumnName("ValorDoDesconto");
            this.Property(t => t.PercentualDeDesconto).HasColumnName("PercentualDeDesconto");
            this.Property(t => t.AliquotaDeIcms).HasColumnName("AliquotaDeIcms");
            this.Property(t => t.ValorIcms).HasColumnName("ValorIcms");
            this.Property(t => t.AliquotaDeIPI).HasColumnName("AliquotaDeIPI");
            this.Property(t => t.ValorDeIPI).HasColumnName("ValorDeIPI");
            this.Property(t => t.AliquotaDeICMSSubst).HasColumnName("AliquotaDeICMSSubst");
            this.Property(t => t.ValorDeICMSSubst).HasColumnName("ValorDeICMSSubst");
            this.Property(t => t.PesoLiquido).HasColumnName("PesoLiquido");
            this.Property(t => t.PesoBruto).HasColumnName("PesoBruto");
            this.Property(t => t.MetragemCubica).HasColumnName("MetragemCubica");
            this.Property(t => t.EstoqueProcessado).HasColumnName("EstoqueProcessado");
            this.Property(t => t.StatusDoItem).HasColumnName("StatusDoItem");
            this.Property(t => t.UnidadeDoCLiente).HasColumnName("UnidadeDoCLiente");
            this.Property(t => t.RedBaseIcms).HasColumnName("RedBaseIcms");
            this.Property(t => t.QuantidadeEDI).HasColumnName("QuantidadeEDI");
            this.Property(t => t.ValorUnitarioEDI).HasColumnName("ValorUnitarioEDI");
            this.Property(t => t.QuantidadeRecebida).HasColumnName("QuantidadeRecebida");
            this.Property(t => t.IdCda).HasColumnName("IdCda");
            this.Property(t => t.Item).HasColumnName("Item");
            this.Property(t => t.CfopOrigem).HasColumnName("CfopOrigem");
            this.Property(t => t.TipoDeItem).HasColumnName("TipoDeItem");
            this.Property(t => t.SituacaoTributariaIPI).HasColumnName("SituacaoTributariaIPI");
            this.Property(t => t.SituacaoTributariaPIS).HasColumnName("SituacaoTributariaPIS");
            this.Property(t => t.BaseDoIPI).HasColumnName("BaseDoIPI");
            this.Property(t => t.BaseCalculoPis).HasColumnName("BaseCalculoPis");
            this.Property(t => t.AliquotaPis).HasColumnName("AliquotaPis");
            this.Property(t => t.ValorPis).HasColumnName("ValorPis");
            this.Property(t => t.AliquotaCofins).HasColumnName("AliquotaCofins");
            this.Property(t => t.ValorCofins).HasColumnName("ValorCofins");
            this.Property(t => t.BaseDoIcms).HasColumnName("BaseDoIcms");
            this.Property(t => t.SituacaoTributariaCofins).HasColumnName("SituacaoTributariaCofins");
            this.Property(t => t.BaseCalculoCofins).HasColumnName("BaseCalculoCofins");
            this.Property(t => t.AliquotaDeISS).HasColumnName("AliquotaDeISS");
            this.Property(t => t.SituacaoTributariaICMS).HasColumnName("SituacaoTributariaICMS");
            this.Property(t => t.IdTesCfop).HasColumnName("IdTesCfop");
            this.Property(t => t.IdUnidadeDeArmazenagemLote).HasColumnName("IdUnidadeDeArmazenagemLote");
            this.Property(t => t.IdProdutoCliente).HasColumnName("IdProdutoCliente");
        }
    }
}
