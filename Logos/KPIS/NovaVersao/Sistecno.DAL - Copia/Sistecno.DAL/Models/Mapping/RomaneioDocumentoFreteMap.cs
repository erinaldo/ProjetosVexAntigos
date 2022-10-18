using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class RomaneioDocumentoFreteMap : EntityTypeConfiguration<RomaneioDocumentoFrete>
    {
        public RomaneioDocumentoFreteMap()
        {
            // Primary Key
            this.HasKey(t => t.IDRomaneioDocumentoFrete);

            // Properties
            this.Property(t => t.IDRomaneioDocumentoFrete)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("RomaneioDocumentoFrete");
            this.Property(t => t.IDRomaneioDocumentoFrete).HasColumnName("IDRomaneioDocumentoFrete");
            this.Property(t => t.IDDocumento).HasColumnName("IDDocumento");
            this.Property(t => t.IDFilial).HasColumnName("IDFilial");
            this.Property(t => t.IDCadastro).HasColumnName("IDCadastro");
            this.Property(t => t.IDServico).HasColumnName("IDServico");
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

            // Relationships
            this.HasRequired(t => t.Cadastro)
                .WithMany(t => t.RomaneioDocumentoFretes)
                .HasForeignKey(d => d.IDCadastro);
            this.HasRequired(t => t.Documento)
                .WithMany(t => t.RomaneioDocumentoFretes)
                .HasForeignKey(d => d.IDDocumento);
            this.HasRequired(t => t.Filial)
                .WithMany(t => t.RomaneioDocumentoFretes)
                .HasForeignKey(d => d.IDFilial);
            this.HasRequired(t => t.Servico)
                .WithMany(t => t.RomaneioDocumentoFretes)
                .HasForeignKey(d => d.IDServico);

        }
    }
}
