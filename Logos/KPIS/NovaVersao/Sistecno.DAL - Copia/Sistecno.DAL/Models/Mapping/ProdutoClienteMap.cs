using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ProdutoClienteMap : EntityTypeConfiguration<ProdutoCliente>
    {
        public ProdutoClienteMap()
        {
            // Primary Key
            this.HasKey(t => t.IDProdutoCliente);

            // Properties
            this.Property(t => t.IDProdutoCliente)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Codigo)
                .IsRequired()
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

            this.Property(t => t.CodigoNBM)
                .HasMaxLength(12);

            this.Property(t => t.CodigoNCM)
                .HasMaxLength(10);

            this.Property(t => t.NomeDoArquivo)
                .HasMaxLength(50);

            this.Property(t => t.Marca)
                .HasMaxLength(40);

            this.Property(t => t.CodigoDoFornecedor)
                .HasMaxLength(20);

            this.Property(t => t.OrigemDaMercadoria)
                .HasMaxLength(1);

            this.Property(t => t.InseridoPor)
                .HasMaxLength(20);

            // Table & Column Mappings
            this.ToTable("ProdutoCliente");
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
            this.Property(t => t.UnidadeDoCliente).HasColumnName("UnidadeDoCliente");
            this.Property(t => t.DataLimiteDeUso).HasColumnName("DataLimiteDeUso");
            this.Property(t => t.IsentoDeICMS).HasColumnName("IsentoDeICMS");
            this.Property(t => t.ReducaoDeICMS).HasColumnName("ReducaoDeICMS");
            this.Property(t => t.DecretoDoICMS).HasColumnName("DecretoDoICMS");
            this.Property(t => t.DataDeCadastro).HasColumnName("DataDeCadastro");
            this.Property(t => t.Lastro).HasColumnName("Lastro");
            this.Property(t => t.Altura).HasColumnName("Altura");
            this.Property(t => t.Ativo).HasColumnName("Ativo");
            this.Property(t => t.GrupoDeProdutoCliente).HasColumnName("GrupoDeProdutoCliente");
            this.Property(t => t.CodigoNBM).HasColumnName("CodigoNBM");
            this.Property(t => t.CodigoNCM).HasColumnName("CodigoNCM");
            this.Property(t => t.CodigoGenero).HasColumnName("CodigoGenero");
            this.Property(t => t.UnidadeDoFornecedorAlterado).HasColumnName("UnidadeDoFornecedorAlterado");
            this.Property(t => t.UnidadeDoClienteAlterada).HasColumnName("UnidadeDoClienteAlterada");
            this.Property(t => t.DataAlteracaoDaUnidade).HasColumnName("DataAlteracaoDaUnidade");
            this.Property(t => t.NomeDoArquivo).HasColumnName("NomeDoArquivo");
            this.Property(t => t.IDFornecedor).HasColumnName("IDFornecedor");
            this.Property(t => t.Marca).HasColumnName("Marca");
            this.Property(t => t.FatorUsoPosicaoPallet).HasColumnName("FatorUsoPosicaoPallet");
            this.Property(t => t.ShelfLife).HasColumnName("ShelfLife");
            this.Property(t => t.IdContaContabil).HasColumnName("IdContaContabil");
            this.Property(t => t.IdCentroDeCusto).HasColumnName("IdCentroDeCusto");
            this.Property(t => t.CodigoDoFornecedor).HasColumnName("CodigoDoFornecedor");
            this.Property(t => t.OrigemDaMercadoria).HasColumnName("OrigemDaMercadoria");
            this.Property(t => t.SLDEntrada).HasColumnName("SLDEntrada");
            this.Property(t => t.SLDRetorno).HasColumnName("SLDRetorno");
            this.Property(t => t.SLDARetornar).HasColumnName("SLDARetornar");
            this.Property(t => t.SLDUA).HasColumnName("SLDUA");
            this.Property(t => t.InseridoPor).HasColumnName("InseridoPor");

            // Relationships
            this.HasOptional(t => t.CentroDeCusto)
                .WithMany(t => t.ProdutoClientes)
                .HasForeignKey(d => d.IDCentroDeCustoCredito);
            this.HasOptional(t => t.CentroDeCusto1)
                .WithMany(t => t.ProdutoClientes1)
                .HasForeignKey(d => d.IDCentroDeCustoDebito);
            this.HasOptional(t => t.Cfop)
                .WithMany(t => t.ProdutoClientes)
                .HasForeignKey(d => d.IDCfop);
            this.HasOptional(t => t.ClienteTipoDeMaterial)
                .WithMany(t => t.ProdutoClientes)
                .HasForeignKey(d => d.IDClienteTipoDeMaterial);
            this.HasOptional(t => t.ContaContabil)
                .WithMany(t => t.ProdutoClientes)
                .HasForeignKey(d => d.IDContaContabilCredito);
            this.HasOptional(t => t.ContaContabil1)
                .WithMany(t => t.ProdutoClientes1)
                .HasForeignKey(d => d.IDContaContabilDebito);
            this.HasOptional(t => t.DepositoPlantaLocalizacao)
                .WithMany(t => t.ProdutoClientes)
                .HasForeignKey(d => d.IDDepositoPlantaLocalizacao);
            this.HasOptional(t => t.GrupoDeProduto)
                .WithMany(t => t.ProdutoClientes)
                .HasForeignKey(d => d.IDGrupoDeProduto);
            this.HasOptional(t => t.UnidadeDeMedida)
                .WithMany(t => t.ProdutoClientes)
                .HasForeignKey(d => d.IDUnidadeDeMedida);

        }
    }
}
