using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ClienteMap : EntityTypeConfiguration<Cliente>
    {
        public ClienteMap()
        {
            // Primary Key
            this.HasKey(t => t.IDCliente);

            // Properties
            this.Property(t => t.IDCliente)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.SeguroProprio)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(3);

            this.Property(t => t.Bloqueado)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(3);

            this.Property(t => t.CodigoDeBarras)
                .HasMaxLength(32);

            this.Property(t => t.FretePadrao)
                .IsFixedLength()
                .HasMaxLength(3);

            this.Property(t => t.AgrupaDocumentos)
                .IsFixedLength()
                .HasMaxLength(3);

            this.Property(t => t.MetodoDeArmazenagem)
                .HasMaxLength(4);

            this.Property(t => t.SiglaDoCliente)
                .HasMaxLength(10);

            this.Property(t => t.Ativo)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(3);

            this.Property(t => t.ComposicaoDoCodigo)
                .HasMaxLength(10);

            this.Property(t => t.HorarioDeCorte)
                .IsFixedLength()
                .HasMaxLength(5);

            this.Property(t => t.UniRecebimento)
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.NFCodigoCliente)
                .HasMaxLength(3);

            this.Property(t => t.SistranWeb)
                .HasMaxLength(3);

            this.Property(t => t.VerificaSaldoPedido)
                .IsFixedLength()
                .HasMaxLength(3);

            this.Property(t => t.Expedicao_Unidade)
                .HasMaxLength(10);

            this.Property(t => t.contrato)
                .HasMaxLength(3);

            this.Property(t => t.Recebto_Unidade)
                .HasMaxLength(10);

            this.Property(t => t.FaturamentoPorCte)
                .HasMaxLength(20);

            this.Property(t => t.SerieNFE)
                .HasMaxLength(5);

            this.Property(t => t.EmiteNotaFiscalServicoDeTransporte)
                .HasMaxLength(3);

            this.Property(t => t.Averbacao)
                .HasMaxLength(3);

            // Table & Column Mappings
            this.ToTable("Cliente");
            this.Property(t => t.IDCliente).HasColumnName("IDCliente");
            this.Property(t => t.IDContaContabilCredito).HasColumnName("IDContaContabilCredito");
            this.Property(t => t.IDContaContabilDebito).HasColumnName("IDContaContabilDebito");
            this.Property(t => t.IDCentroDeCustoCredito).HasColumnName("IDCentroDeCustoCredito");
            this.Property(t => t.IDCentroDeCustoDebito).HasColumnName("IDCentroDeCustoDebito");
            this.Property(t => t.IDCfop).HasColumnName("IDCfop");
            this.Property(t => t.CodigoDoCliente).HasColumnName("CodigoDoCliente");
            this.Property(t => t.CodigoDoClienteFilial).HasColumnName("CodigoDoClienteFilial");
            this.Property(t => t.IDRamoDeAtividade).HasColumnName("IDRamoDeAtividade");
            this.Property(t => t.SeguroProprio).HasColumnName("SeguroProprio");
            this.Property(t => t.Bloqueado).HasColumnName("Bloqueado");
            this.Property(t => t.DataDeCadastro).HasColumnName("DataDeCadastro");
            this.Property(t => t.CodigoDeBarras).HasColumnName("CodigoDeBarras");
            this.Property(t => t.FretePadrao).HasColumnName("FretePadrao");
            this.Property(t => t.AgrupaDocumentos).HasColumnName("AgrupaDocumentos");
            this.Property(t => t.LimiteDeCredito).HasColumnName("LimiteDeCredito");
            this.Property(t => t.MetodoDeArmazenagem).HasColumnName("MetodoDeArmazenagem");
            this.Property(t => t.SiglaDoCliente).HasColumnName("SiglaDoCliente");
            this.Property(t => t.Ativo).HasColumnName("Ativo");
            this.Property(t => t.IDFilialPadraoInternet).HasColumnName("IDFilialPadraoInternet");
            this.Property(t => t.IDContaContabil).HasColumnName("IDContaContabil");
            this.Property(t => t.IDCentroDeCusto).HasColumnName("IDCentroDeCusto");
            this.Property(t => t.ComposicaoDoCodigo).HasColumnName("ComposicaoDoCodigo");
            this.Property(t => t.PrazoDePagamento).HasColumnName("PrazoDePagamento");
            this.Property(t => t.HorarioDeCorte).HasColumnName("HorarioDeCorte");
            this.Property(t => t.UniRecebimento).HasColumnName("UniRecebimento");
            this.Property(t => t.PrazoDeEntregaNoCD).HasColumnName("PrazoDeEntregaNoCD");
            this.Property(t => t.IdTes).HasColumnName("IdTes");
            this.Property(t => t.PrazoCorte).HasColumnName("PrazoCorte");
            this.Property(t => t.DiaExpiracaoLimite).HasColumnName("DiaExpiracaoLimite");
            this.Property(t => t.NFCodigoCliente).HasColumnName("NFCodigoCliente");
            this.Property(t => t.SistranWeb).HasColumnName("SistranWeb");
            this.Property(t => t.IcmsFixo).HasColumnName("IcmsFixo");
            this.Property(t => t.ValidadeDeCota).HasColumnName("ValidadeDeCota");
            this.Property(t => t.FatorDeCubagem).HasColumnName("FatorDeCubagem");
            this.Property(t => t.IdTesCfop).HasColumnName("IdTesCfop");
            this.Property(t => t.VerificaSaldoPedido).HasColumnName("VerificaSaldoPedido");
            this.Property(t => t.PrazoCorteD).HasColumnName("PrazoCorteD");
            this.Property(t => t.Recebimento).HasColumnName("Recebimento");
            this.Property(t => t.Expedicao).HasColumnName("Expedicao");
            this.Property(t => t.Expedicao_Unidade).HasColumnName("Expedicao_Unidade");
            this.Property(t => t.contrato).HasColumnName("contrato");
            this.Property(t => t.Recebto_Unidade).HasColumnName("Recebto_Unidade");
            this.Property(t => t.IdOcorrenciaSerie).HasColumnName("IdOcorrenciaSerie");
            this.Property(t => t.FaturamentoPorCte).HasColumnName("FaturamentoPorCte");
            this.Property(t => t.SerieNFE).HasColumnName("SerieNFE");
            this.Property(t => t.EmiteNotaFiscalServicoDeTransporte).HasColumnName("EmiteNotaFiscalServicoDeTransporte");
            this.Property(t => t.Averbacao).HasColumnName("Averbacao");
            this.Property(t => t.Lastro).HasColumnName("Lastro");
            this.Property(t => t.Altura).HasColumnName("Altura");

            // Relationships
            this.HasRequired(t => t.Cadastro)
                .WithOptional(t => t.Cliente);
            this.HasOptional(t => t.CentroDeCusto)
                .WithMany(t => t.Clientes)
                .HasForeignKey(d => d.IDCentroDeCusto);
            this.HasOptional(t => t.Cfop)
                .WithMany(t => t.Clientes)
                .HasForeignKey(d => d.IDCfop);
            this.HasOptional(t => t.ContaContabil)
                .WithMany(t => t.Clientes)
                .HasForeignKey(d => d.IDContaContabil);
            this.HasOptional(t => t.RamoDeAtividade)
                .WithMany(t => t.Clientes)
                .HasForeignKey(d => d.IDRamoDeAtividade);

        }
    }
}
