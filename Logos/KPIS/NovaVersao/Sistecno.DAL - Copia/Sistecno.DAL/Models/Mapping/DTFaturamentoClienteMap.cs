using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class DTFaturamentoClienteMap : EntityTypeConfiguration<DTFaturamentoCliente>
    {
        public DTFaturamentoClienteMap()
        {
            // Primary Key
            this.HasKey(t => t.IdDtFaturamentoCliente);

            // Properties
            this.Property(t => t.IdDtFaturamentoCliente)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Proprietario)
                .HasMaxLength(30);

            this.Property(t => t.ClasseCFOP)
                .HasMaxLength(1);

            // Table & Column Mappings
            this.ToTable("DTFaturamentoCliente");
            this.Property(t => t.IdDtFaturamentoCliente).HasColumnName("IdDtFaturamentoCliente");
            this.Property(t => t.IdDtFaturamento).HasColumnName("IdDtFaturamento");
            this.Property(t => t.IdCadastro).HasColumnName("IdCadastro");
            this.Property(t => t.IdCte).HasColumnName("IdCte");
            this.Property(t => t.FretePeso).HasColumnName("FretePeso");
            this.Property(t => t.FreteExcedente).HasColumnName("FreteExcedente");
            this.Property(t => t.FretePorNotaFiscal).HasColumnName("FretePorNotaFiscal");
            this.Property(t => t.FretePercentual).HasColumnName("FretePercentual");
            this.Property(t => t.FreteValor).HasColumnName("FreteValor");
            this.Property(t => t.Gris).HasColumnName("Gris");
            this.Property(t => t.Cat).HasColumnName("Cat");
            this.Property(t => t.Despacho).HasColumnName("Despacho");
            this.Property(t => t.Tas).HasColumnName("Tas");
            this.Property(t => t.Ted).HasColumnName("Ted");
            this.Property(t => t.Pedagio).HasColumnName("Pedagio");
            this.Property(t => t.Seguro).HasColumnName("Seguro");
            this.Property(t => t.Suframa).HasColumnName("Suframa");
            this.Property(t => t.TaxaDeColeta).HasColumnName("TaxaDeColeta");
            this.Property(t => t.TaxaDeEntrega).HasColumnName("TaxaDeEntrega");
            this.Property(t => t.Descarga).HasColumnName("Descarga");
            this.Property(t => t.Paletizacao).HasColumnName("Paletizacao");
            this.Property(t => t.Ajudante).HasColumnName("Ajudante");
            this.Property(t => t.Diaria).HasColumnName("Diaria");
            this.Property(t => t.Aliquota).HasColumnName("Aliquota");
            this.Property(t => t.IcmsIss).HasColumnName("IcmsIss");
            this.Property(t => t.Frete).HasColumnName("Frete");
            this.Property(t => t.DescargaNaoPaletizada).HasColumnName("DescargaNaoPaletizada");
            this.Property(t => t.DescargaPaletizada).HasColumnName("DescargaPaletizada");
            this.Property(t => t.DevCan).HasColumnName("DevCan");
            this.Property(t => t.ImpostoARecolher).HasColumnName("ImpostoARecolher");
            this.Property(t => t.Outros).HasColumnName("Outros");
            this.Property(t => t.Proprietario).HasColumnName("Proprietario");
            this.Property(t => t.DiferencaDeFrete).HasColumnName("DiferencaDeFrete");
            this.Property(t => t.BaseDeCalculo).HasColumnName("BaseDeCalculo");
            this.Property(t => t.FreteSimulado).HasColumnName("FreteSimulado");
            this.Property(t => t.IdTabelaDeFreteRotaModal).HasColumnName("IdTabelaDeFreteRotaModal");
            this.Property(t => t.PesoLiquido).HasColumnName("PesoLiquido");
            this.Property(t => t.PesoBruto).HasColumnName("PesoBruto");
            this.Property(t => t.PesoCubado).HasColumnName("PesoCubado");
            this.Property(t => t.PesoCalculado).HasColumnName("PesoCalculado");
            this.Property(t => t.MetragemCubica).HasColumnName("MetragemCubica");
            this.Property(t => t.Volumes).HasColumnName("Volumes");
            this.Property(t => t.ValorDaNota).HasColumnName("ValorDaNota");
            this.Property(t => t.IdRegiaoItemFilial).HasColumnName("IdRegiaoItemFilial");
            this.Property(t => t.ClasseCFOP).HasColumnName("ClasseCFOP");

            // Relationships
            this.HasRequired(t => t.Cliente)
                .WithMany(t => t.DTFaturamentoClientes)
                .HasForeignKey(d => d.IdCadastro);
            this.HasOptional(t => t.Documento)
                .WithMany(t => t.DTFaturamentoClientes)
                .HasForeignKey(d => d.IdCte);
            this.HasRequired(t => t.DTFaturamento)
                .WithMany(t => t.DTFaturamentoClientes)
                .HasForeignKey(d => d.IdDtFaturamento);

        }
    }
}
