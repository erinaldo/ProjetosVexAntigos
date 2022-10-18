using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class PrevisaoDeMaterialMap : EntityTypeConfiguration<PrevisaoDeMaterial>
    {
        public PrevisaoDeMaterialMap()
        {
            // Primary Key
            this.HasKey(t => t.IDPrevisaoDeMaterial);

            // Properties
            this.Property(t => t.IDPrevisaoDeMaterial)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Pedido)
                .IsFixedLength()
                .HasMaxLength(50);

            this.Property(t => t.Fornecedor)
                .IsFixedLength()
                .HasMaxLength(150);

            this.Property(t => t.Descricao)
                .IsFixedLength()
                .HasMaxLength(250);

            this.Property(t => t.NotaFiscal)
                .IsFixedLength()
                .HasMaxLength(50);

            this.Property(t => t.TipoDeMaterial)
                .IsFixedLength()
                .HasMaxLength(250);

            this.Property(t => t.VolumesCaixas)
                .HasMaxLength(100);

            this.Property(t => t.QualidadeDoProduto)
                .IsFixedLength()
                .HasMaxLength(50);

            this.Property(t => t.TipoDeVeiculo)
                .IsFixedLength()
                .HasMaxLength(20);

            this.Property(t => t.OpcaoCarga)
                .IsFixedLength()
                .HasMaxLength(20);

            this.Property(t => t.Cidade)
                .IsFixedLength()
                .HasMaxLength(50);

            this.Property(t => t.Telefone)
                .HasMaxLength(50);

            this.Property(t => t.FornecedorCnpj)
                .IsFixedLength()
                .HasMaxLength(18);

            this.Property(t => t.Estado)
                .IsFixedLength()
                .HasMaxLength(2);

            this.Property(t => t.Transportadora)
                .HasMaxLength(150);

            this.Property(t => t.TransportadoraContato)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("PrevisaoDeMaterial");
            this.Property(t => t.IDPrevisaoDeMaterial).HasColumnName("IDPrevisaoDeMaterial");
            this.Property(t => t.IDClienteDivisao).HasColumnName("IDClienteDivisao");
            this.Property(t => t.Pedido).HasColumnName("Pedido");
            this.Property(t => t.Fornecedor).HasColumnName("Fornecedor");
            this.Property(t => t.Descricao).HasColumnName("Descricao");
            this.Property(t => t.Quantidade).HasColumnName("Quantidade");
            this.Property(t => t.PrevisaoDeEntrega).HasColumnName("PrevisaoDeEntrega");
            this.Property(t => t.DataDeRecebimentoDoMaterial).HasColumnName("DataDeRecebimentoDoMaterial");
            this.Property(t => t.QuantidadeRecebidaNoFisico).HasColumnName("QuantidadeRecebidaNoFisico");
            this.Property(t => t.NotaFiscal).HasColumnName("NotaFiscal");
            this.Property(t => t.DataDeDisponibilidadeDoMaterialNoSite).HasColumnName("DataDeDisponibilidadeDoMaterialNoSite");
            this.Property(t => t.IDCliente).HasColumnName("IDCliente");
            this.Property(t => t.TipoDeMaterial).HasColumnName("TipoDeMaterial");
            this.Property(t => t.QuantidadeRecebidaNoFiscal).HasColumnName("QuantidadeRecebidaNoFiscal");
            this.Property(t => t.VolumesCaixas).HasColumnName("VolumesCaixas");
            this.Property(t => t.QualidadeDoProduto).HasColumnName("QualidadeDoProduto");
            this.Property(t => t.IDUsuario).HasColumnName("IDUsuario");
            this.Property(t => t.TipoDeVeiculo).HasColumnName("TipoDeVeiculo");
            this.Property(t => t.QtdVeiculo).HasColumnName("QtdVeiculo");
            this.Property(t => t.OpcaoCarga).HasColumnName("OpcaoCarga");
            this.Property(t => t.QtdAjudante).HasColumnName("QtdAjudante");
            this.Property(t => t.NCM).HasColumnName("NCM");
            this.Property(t => t.Cidade).HasColumnName("Cidade");
            this.Property(t => t.Telefone).HasColumnName("Telefone");
            this.Property(t => t.FornecedorCnpj).HasColumnName("FornecedorCnpj");
            this.Property(t => t.Estado).HasColumnName("Estado");
            this.Property(t => t.Transportadora).HasColumnName("Transportadora");
            this.Property(t => t.TransportadoraContato).HasColumnName("TransportadoraContato");
            this.Property(t => t.QtdPallets).HasColumnName("QtdPallets");
        }
    }
}
