using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class LancamentoPadraoConfiguracaoMap : EntityTypeConfiguration<LancamentoPadraoConfiguracao>
    {
        public LancamentoPadraoConfiguracaoMap()
        {
            // Primary Key
            this.HasKey(t => t.IDLancamentoPadraoConfiguracao);

            // Properties
            this.Property(t => t.IDLancamentoPadraoConfiguracao)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.TipoDeLancamento)
                .IsRequired()
                .HasMaxLength(15);

            this.Property(t => t.ContaContabilCredito)
                .HasMaxLength(50);

            this.Property(t => t.ContaContabilDebito)
                .HasMaxLength(50);

            this.Property(t => t.Valor)
                .HasMaxLength(50);

            this.Property(t => t.Historico)
                .HasMaxLength(300);

            this.Property(t => t.CentroDeCustoDebito)
                .HasMaxLength(50);

            this.Property(t => t.CentroDeCustoCredito)
                .HasMaxLength(50);

            this.Property(t => t.OrigemDoLancamento)
                .HasMaxLength(100);

            this.Property(t => t.OutrasInformacoesCredito)
                .HasMaxLength(200);

            this.Property(t => t.OutrasInformacoesDebito)
                .HasMaxLength(200);

            this.Property(t => t.TipoDoSaldo)
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("LancamentoPadraoConfiguracao");
            this.Property(t => t.IDLancamentoPadraoConfiguracao).HasColumnName("IDLancamentoPadraoConfiguracao");
            this.Property(t => t.IDLancamentoPadrao).HasColumnName("IDLancamentoPadrao");
            this.Property(t => t.Sequencia).HasColumnName("Sequencia");
            this.Property(t => t.TipoDeLancamento).HasColumnName("TipoDeLancamento");
            this.Property(t => t.ContaContabilCredito).HasColumnName("ContaContabilCredito");
            this.Property(t => t.ContaContabilDebito).HasColumnName("ContaContabilDebito");
            this.Property(t => t.Valor).HasColumnName("Valor");
            this.Property(t => t.Historico).HasColumnName("Historico");
            this.Property(t => t.CentroDeCustoDebito).HasColumnName("CentroDeCustoDebito");
            this.Property(t => t.CentroDeCustoCredito).HasColumnName("CentroDeCustoCredito");
            this.Property(t => t.OrigemDoLancamento).HasColumnName("OrigemDoLancamento");
            this.Property(t => t.OutrasInformacoesCredito).HasColumnName("OutrasInformacoesCredito");
            this.Property(t => t.OutrasInformacoesDebito).HasColumnName("OutrasInformacoesDebito");
            this.Property(t => t.TipoDoSaldo).HasColumnName("TipoDoSaldo");
            this.Property(t => t.IdContaContabilDebito).HasColumnName("IdContaContabilDebito");
            this.Property(t => t.IdContaContabilCredito).HasColumnName("IdContaContabilCredito");

            // Relationships
            this.HasRequired(t => t.LancamentoPadrao)
                .WithMany(t => t.LancamentoPadraoConfiguracaos)
                .HasForeignKey(d => d.IDLancamentoPadrao);

        }
    }
}
