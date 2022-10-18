using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class DocumentoFreteMap : EntityTypeConfiguration<DocumentoFrete>
    {
        public DocumentoFreteMap()
        {
            // Primary Key
            this.HasKey(t => t.IDDocumentoFrete);

            // Properties
            this.Property(t => t.IDDocumentoFrete)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Proprietario)
                .HasMaxLength(30);

            this.Property(t => t.Observacao)
                .HasMaxLength(50);

            this.Property(t => t.EntregaEfetuada)
                .HasMaxLength(3);

            // Table & Column Mappings
            this.ToTable("DocumentoFrete");
            this.Property(t => t.IDDocumentoFrete).HasColumnName("IDDocumentoFrete");
            this.Property(t => t.IDDocumento).HasColumnName("IDDocumento");
            this.Property(t => t.IDFilial).HasColumnName("IDFilial");
            this.Property(t => t.IDPagadorDoFrete).HasColumnName("IDPagadorDoFrete");
            this.Property(t => t.IDServico).HasColumnName("IDServico");
            this.Property(t => t.IDCidadeOrigemCalculo).HasColumnName("IDCidadeOrigemCalculo");
            this.Property(t => t.IDCidadeDestinoCalculo).HasColumnName("IDCidadeDestinoCalculo");
            this.Property(t => t.IDCidadeColeta).HasColumnName("IDCidadeColeta");
            this.Property(t => t.IDCidadeEntrega).HasColumnName("IDCidadeEntrega");
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
            this.Property(t => t.Observacao).HasColumnName("Observacao");
            this.Property(t => t.PercentualDoFrete).HasColumnName("PercentualDoFrete");
            this.Property(t => t.TaxaDeSevico).HasColumnName("TaxaDeSevico");
            this.Property(t => t.ValorAgregado).HasColumnName("ValorAgregado");
            this.Property(t => t.ValorDoServico).HasColumnName("ValorDoServico");
            this.Property(t => t.IdDt).HasColumnName("IdDt");
            this.Property(t => t.IdLinhaPlanilhaRoge).HasColumnName("IdLinhaPlanilhaRoge");
            this.Property(t => t.EntregaEfetuada).HasColumnName("EntregaEfetuada");

            // Relationships
            this.HasRequired(t => t.Documento)
                .WithMany(t => t.DocumentoFretes)
                .HasForeignKey(d => d.IDDocumento);

        }
    }
}
