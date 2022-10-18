using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ContaContabilLancamentoMap : EntityTypeConfiguration<ContaContabilLancamento>
    {
        public ContaContabilLancamentoMap()
        {
            // Primary Key
            this.HasKey(t => t.IDContaContabilLancamento);

            // Properties
            this.Property(t => t.IDContaContabilLancamento)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Historico)
                .IsRequired()
                .HasMaxLength(300);

            this.Property(t => t.DebitoCredito)
                .IsRequired()
                .HasMaxLength(15);

            this.Property(t => t.Tabela)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("ContaContabilLancamento");
            this.Property(t => t.IDContaContabilLancamento).HasColumnName("IDContaContabilLancamento");
            this.Property(t => t.IDContaContabilCredito).HasColumnName("IDContaContabilCredito");
            this.Property(t => t.IDContaContabilDebito).HasColumnName("IDContaContabilDebito");
            this.Property(t => t.IDFilial).HasColumnName("IDFilial");
            this.Property(t => t.IDDocumentoOrigem).HasColumnName("IDDocumentoOrigem");
            this.Property(t => t.DataDeLancamento).HasColumnName("DataDeLancamento");
            this.Property(t => t.DataDeCompetencia).HasColumnName("DataDeCompetencia");
            this.Property(t => t.Historico).HasColumnName("Historico");
            this.Property(t => t.Valor).HasColumnName("Valor");
            this.Property(t => t.DebitoCredito).HasColumnName("DebitoCredito");
            this.Property(t => t.Tabela).HasColumnName("Tabela");

            // Relationships
            this.HasOptional(t => t.ContaContabil)
                .WithMany(t => t.ContaContabilLancamentoes)
                .HasForeignKey(d => d.IDContaContabilCredito);
            this.HasOptional(t => t.ContaContabil1)
                .WithMany(t => t.ContaContabilLancamentoes1)
                .HasForeignKey(d => d.IDContaContabilDebito);
            this.HasRequired(t => t.Filial)
                .WithMany(t => t.ContaContabilLancamentoes)
                .HasForeignKey(d => d.IDFilial);

        }
    }
}
