using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class EDI_ProdutoClienteMap : EntityTypeConfiguration<EDI_ProdutoCliente>
    {
        public EDI_ProdutoClienteMap()
        {
            // Primary Key
            this.HasKey(t => t.EDI_Chave);

            // Properties
            this.Property(t => t.EDI_Chave)
                .IsRequired()
                .HasMaxLength(40);

            this.Property(t => t.EDI_Motivo)
                .HasMaxLength(500);

            this.Property(t => t.Codigo)
                .HasMaxLength(20);

            this.Property(t => t.CodigoDoCliente)
                .HasMaxLength(20);

            this.Property(t => t.Descricao)
                .HasMaxLength(60);

            this.Property(t => t.DesmembraNaNF)
                .IsFixedLength()
                .HasMaxLength(3);

            this.Property(t => t.MetodoDeMovimentacao)
                .IsFixedLength()
                .HasMaxLength(4);

            this.Property(t => t.SolicitarDataDeValidade)
                .IsFixedLength()
                .HasMaxLength(3);

            this.Property(t => t.IsentoDeICMS)
                .IsFixedLength()
                .HasMaxLength(3);

            this.Property(t => t.DecretoDoICMS)
                .HasMaxLength(100);

            this.Property(t => t.Ativo)
                .IsFixedLength()
                .HasMaxLength(3);

            this.Property(t => t.GrupoDeProdutoCliente)
                .HasMaxLength(20);

            // Table & Column Mappings
            this.ToTable("EDI_ProdutoCliente");
            this.Property(t => t.EDI_Chave).HasColumnName("EDI_Chave");
            this.Property(t => t.EDI_Motivo).HasColumnName("EDI_Motivo");
            this.Property(t => t.EDI_Data).HasColumnName("EDI_Data");
            this.Property(t => t.IDProdutoCliente).HasColumnName("IDProdutoCliente");
            this.Property(t => t.IDCliente).HasColumnName("IDCliente");
            this.Property(t => t.IDGrupoDeProduto).HasColumnName("IDGrupoDeProduto");
            this.Property(t => t.IDDepositoPlantaLocalizacao).HasColumnName("IDDepositoPlantaLocalizacao");
            this.Property(t => t.IDClienteTipoDeMaterial).HasColumnName("IDClienteTipoDeMaterial");
            this.Property(t => t.IDUnidadeDeMedida).HasColumnName("IDUnidadeDeMedida");
            this.Property(t => t.IDContaContabilCredito).HasColumnName("IDContaContabilCredito");
            this.Property(t => t.IDContaContabilDebito).HasColumnName("IDContaContabilDebito");
            this.Property(t => t.IDCentroDeCustoCredito).HasColumnName("IDCentroDeCustoCredito");
            this.Property(t => t.IDCentroDeCustoDebito).HasColumnName("IDCentroDeCustoDebito");
            this.Property(t => t.IDCfop).HasColumnName("IDCfop");
            this.Property(t => t.Codigo).HasColumnName("Codigo");
            this.Property(t => t.CodigoDoCliente).HasColumnName("CodigoDoCliente");
            this.Property(t => t.Descricao).HasColumnName("Descricao");
            this.Property(t => t.DesmembraNaNF).HasColumnName("DesmembraNaNF");
            this.Property(t => t.MetodoDeMovimentacao).HasColumnName("MetodoDeMovimentacao");
            this.Property(t => t.SolicitarDataDeValidade).HasColumnName("SolicitarDataDeValidade");
            this.Property(t => t.SaldoMinimo).HasColumnName("SaldoMinimo");
            this.Property(t => t.ConsumoMensal).HasColumnName("ConsumoMensal");
            this.Property(t => t.Ressuprimento).HasColumnName("Ressuprimento");
            this.Property(t => t.UnidadeDoFornecedor).HasColumnName("UnidadeDoFornecedor");
            this.Property(t => t.DataLimiteDeUso).HasColumnName("DataLimiteDeUso");
            this.Property(t => t.IsentoDeICMS).HasColumnName("IsentoDeICMS");
            this.Property(t => t.ReducaoDeICMS).HasColumnName("ReducaoDeICMS");
            this.Property(t => t.DecretoDoICMS).HasColumnName("DecretoDoICMS");
            this.Property(t => t.DataDeCadastro).HasColumnName("DataDeCadastro");
            this.Property(t => t.Lastro).HasColumnName("Lastro");
            this.Property(t => t.Altura).HasColumnName("Altura");
            this.Property(t => t.Ativo).HasColumnName("Ativo");
            this.Property(t => t.GrupoDeProdutoCliente).HasColumnName("GrupoDeProdutoCliente");
        }
    }
}
