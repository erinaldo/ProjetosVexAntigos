using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ConciliacaoLoteMap : EntityTypeConfiguration<ConciliacaoLote>
    {
        public ConciliacaoLoteMap()
        {
            // Primary Key
            this.HasKey(t => t.IdConciliacaoLote);

            // Properties
            this.Property(t => t.IdConciliacaoLote)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.HServicoOperacao)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.HEmpresaConvenio)
                .IsFixedLength()
                .HasMaxLength(20);

            this.Property(t => t.HEmpresaContaCorrenteAgenciaDigito)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.HEmpresaContaCorrenteContaDigito)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.HEmpresaContaCorrenteDV)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.HEmpresaNome)
                .HasMaxLength(30);

            this.Property(t => t.HEmpresaSaldoInicialSituacao)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.HEmpresaSaldoInicialStatus)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.HEmpresaSaldoInicialTipoDeMoeda)
                .IsFixedLength()
                .HasMaxLength(3);

            this.Property(t => t.TEmpresaConvenio)
                .IsFixedLength()
                .HasMaxLength(20);

            this.Property(t => t.TEmpresaContaCorrenteAgenciaDigito)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.TEmpresaContaCorrenteContaDigito)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.TEmpresaContaCorrenteDV)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.TSaldoFinalSituacao)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.TSaldoFinalStatus)
                .IsFixedLength()
                .HasMaxLength(1);

            // Table & Column Mappings
            this.ToTable("ConciliacaoLote");
            this.Property(t => t.IdConciliacaoLote).HasColumnName("IdConciliacaoLote");
            this.Property(t => t.IdConciliacao).HasColumnName("IdConciliacao");
            this.Property(t => t.HControleBanco).HasColumnName("HControleBanco");
            this.Property(t => t.HControleLote).HasColumnName("HControleLote");
            this.Property(t => t.HControleRegistro).HasColumnName("HControleRegistro");
            this.Property(t => t.HServicoOperacao).HasColumnName("HServicoOperacao");
            this.Property(t => t.HServicoServico).HasColumnName("HServicoServico");
            this.Property(t => t.HServicoFormaLancamento).HasColumnName("HServicoFormaLancamento");
            this.Property(t => t.HServicoLayOutDoLote).HasColumnName("HServicoLayOutDoLote");
            this.Property(t => t.HEmpresaIncricaoTipo).HasColumnName("HEmpresaIncricaoTipo");
            this.Property(t => t.HEmpresaIncricaoNumero).HasColumnName("HEmpresaIncricaoNumero");
            this.Property(t => t.HEmpresaConvenio).HasColumnName("HEmpresaConvenio");
            this.Property(t => t.HEmpresaContaCorrenteAgenciaCodigo).HasColumnName("HEmpresaContaCorrenteAgenciaCodigo");
            this.Property(t => t.HEmpresaContaCorrenteAgenciaDigito).HasColumnName("HEmpresaContaCorrenteAgenciaDigito");
            this.Property(t => t.HEmpresaContaCorrenteContaNumero).HasColumnName("HEmpresaContaCorrenteContaNumero");
            this.Property(t => t.HEmpresaContaCorrenteContaDigito).HasColumnName("HEmpresaContaCorrenteContaDigito");
            this.Property(t => t.HEmpresaContaCorrenteDV).HasColumnName("HEmpresaContaCorrenteDV");
            this.Property(t => t.HEmpresaNome).HasColumnName("HEmpresaNome");
            this.Property(t => t.HEmpresaSaldoInicialData).HasColumnName("HEmpresaSaldoInicialData");
            this.Property(t => t.HEmpresaSaldoInicialValor).HasColumnName("HEmpresaSaldoInicialValor");
            this.Property(t => t.HEmpresaSaldoInicialSituacao).HasColumnName("HEmpresaSaldoInicialSituacao");
            this.Property(t => t.HEmpresaSaldoInicialStatus).HasColumnName("HEmpresaSaldoInicialStatus");
            this.Property(t => t.HEmpresaSaldoInicialTipoDeMoeda).HasColumnName("HEmpresaSaldoInicialTipoDeMoeda");
            this.Property(t => t.HEmpresaSaldoInicialSequncia).HasColumnName("HEmpresaSaldoInicialSequncia");
            this.Property(t => t.TControleBanco).HasColumnName("TControleBanco");
            this.Property(t => t.TControleLote).HasColumnName("TControleLote");
            this.Property(t => t.TControleRegistro).HasColumnName("TControleRegistro");
            this.Property(t => t.TEmpresaInscricaoTipo).HasColumnName("TEmpresaInscricaoTipo");
            this.Property(t => t.TEmpresaInscricaoNumero).HasColumnName("TEmpresaInscricaoNumero");
            this.Property(t => t.TEmpresaConvenio).HasColumnName("TEmpresaConvenio");
            this.Property(t => t.TEmpresaContaCorrenteAgenciaCodigo).HasColumnName("TEmpresaContaCorrenteAgenciaCodigo");
            this.Property(t => t.TEmpresaContaCorrenteAgenciaDigito).HasColumnName("TEmpresaContaCorrenteAgenciaDigito");
            this.Property(t => t.TEmpresaContaCorrenteContaNumero).HasColumnName("TEmpresaContaCorrenteContaNumero");
            this.Property(t => t.TEmpresaContaCorrenteContaDigito).HasColumnName("TEmpresaContaCorrenteContaDigito");
            this.Property(t => t.TEmpresaContaCorrenteDV).HasColumnName("TEmpresaContaCorrenteDV");
            this.Property(t => t.TValoresBloqueadoDiaAnterior).HasColumnName("TValoresBloqueadoDiaAnterior");
            this.Property(t => t.TValoresLimite).HasColumnName("TValoresLimite");
            this.Property(t => t.TValoresLoqueadoDia).HasColumnName("TValoresLoqueadoDia");
            this.Property(t => t.TSaldoFinalData).HasColumnName("TSaldoFinalData");
            this.Property(t => t.TSaldoFinalValor).HasColumnName("TSaldoFinalValor");
            this.Property(t => t.TSaldoFinalSituacao).HasColumnName("TSaldoFinalSituacao");
            this.Property(t => t.TSaldoFinalStatus).HasColumnName("TSaldoFinalStatus");
            this.Property(t => t.TTotaisQuantidadeDeRegistros).HasColumnName("TTotaisQuantidadeDeRegistros");
            this.Property(t => t.TTotaisValorDebitos).HasColumnName("TTotaisValorDebitos");
            this.Property(t => t.TTotaisValorCreditos).HasColumnName("TTotaisValorCreditos");

            // Relationships
            this.HasRequired(t => t.Conciliacao)
                .WithMany(t => t.ConciliacaoLotes)
                .HasForeignKey(d => d.IdConciliacao);

        }
    }
}
