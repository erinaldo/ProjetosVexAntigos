using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class EDI_DocumentoFreteMap : EntityTypeConfiguration<EDI_DocumentoFrete>
    {
        public EDI_DocumentoFreteMap()
        {
            // Primary Key
            this.HasKey(t => t.EDI_Chave);

            // Properties
            this.Property(t => t.EDI_Chave)
                .IsRequired()
                .HasMaxLength(40);

            this.Property(t => t.EDI_Motivo)
                .HasMaxLength(500);

            // Table & Column Mappings
            this.ToTable("EDI_DocumentoFrete");
            this.Property(t => t.EDI_Chave).HasColumnName("EDI_Chave");
            this.Property(t => t.EDI_Motivo).HasColumnName("EDI_Motivo");
            this.Property(t => t.EDI_Data).HasColumnName("EDI_Data");
            this.Property(t => t.IDDocumentoFrete).HasColumnName("IDDocumentoFrete");
            this.Property(t => t.IDDocumento).HasColumnName("IDDocumento");
            this.Property(t => t.IDFilial).HasColumnName("IDFilial");
            this.Property(t => t.IDPagadorDoFrete).HasColumnName("IDPagadorDoFrete");
            this.Property(t => t.IDServico).HasColumnName("IDServico");
            this.Property(t => t.IDCidadeOrigemCalculo).HasColumnName("IDCidadeOrigemCalculo");
            this.Property(t => t.IDCidadeDestinoCalculo).HasColumnName("IDCidadeDestinoCalculo");
            this.Property(t => t.FretePeso).HasColumnName("FretePeso");
            this.Property(t => t.FreteExcedente).HasColumnName("FreteExcedente");
            this.Property(t => t.FretePorNotaFiscal).HasColumnName("FretePorNotaFiscal");
            this.Property(t => t.FretePercentual).HasColumnName("FretePercentual");
            this.Property(t => t.FreteValor).HasColumnName("FreteValor");
            this.Property(t => t.Gris).HasColumnName("Gris");
            this.Property(t => t.Cat).HasColumnName("Cat");
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
        }
    }
}
