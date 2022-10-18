using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ConciliacaoLoteDetalheMap : EntityTypeConfiguration<ConciliacaoLoteDetalhe>
    {
        public ConciliacaoLoteDetalheMap()
        {
            // Primary Key
            this.HasKey(t => t.IdConciliacaoLoteDeTalhe);

            // Properties
            this.Property(t => t.IdConciliacaoLoteDeTalhe)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.DServicoSegmento)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.DEmpresaConvenio)
                .HasMaxLength(20);

            this.Property(t => t.DEmpresaContaCorrenteAgenciaDigito)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.DEmpresaContaCorrenteContaDigito)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.DEmpresaContaCorrenteDV)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.DEmpresaNome)
                .HasMaxLength(30);

            this.Property(t => t.DNatureza)
                .HasMaxLength(3);

            this.Property(t => t.DComplemento)
                .HasMaxLength(20);

            this.Property(t => t.DCpmf)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.DLancamentoTipo)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.DLancamentoHistoricoCodigo)
                .HasMaxLength(4);

            this.Property(t => t.DLancamentoHistorico)
                .HasMaxLength(25);

            this.Property(t => t.DLancamentoDocumento)
                .HasMaxLength(39);

            // Table & Column Mappings
            this.ToTable("ConciliacaoLoteDetalhe");
            this.Property(t => t.IdConciliacaoLoteDeTalhe).HasColumnName("IdConciliacaoLoteDeTalhe");
            this.Property(t => t.IdConciliacaoLote).HasColumnName("IdConciliacaoLote");
            this.Property(t => t.DControleBanco).HasColumnName("DControleBanco");
            this.Property(t => t.DControleLote).HasColumnName("DControleLote");
            this.Property(t => t.DControleRegistro).HasColumnName("DControleRegistro");
            this.Property(t => t.DServicoRegistro).HasColumnName("DServicoRegistro");
            this.Property(t => t.DServicoSegmento).HasColumnName("DServicoSegmento");
            this.Property(t => t.DEmpresaInscricaoTipo).HasColumnName("DEmpresaInscricaoTipo");
            this.Property(t => t.DEmpresaInscricaoNumero).HasColumnName("DEmpresaInscricaoNumero");
            this.Property(t => t.DEmpresaConvenio).HasColumnName("DEmpresaConvenio");
            this.Property(t => t.DEmpresaContaCorrenteAgenciaCodigo).HasColumnName("DEmpresaContaCorrenteAgenciaCodigo");
            this.Property(t => t.DEmpresaContaCorrenteAgenciaDigito).HasColumnName("DEmpresaContaCorrenteAgenciaDigito");
            this.Property(t => t.DEmpresaContaCorrenteContaNumero).HasColumnName("DEmpresaContaCorrenteContaNumero");
            this.Property(t => t.DEmpresaContaCorrenteContaDigito).HasColumnName("DEmpresaContaCorrenteContaDigito");
            this.Property(t => t.DEmpresaContaCorrenteDV).HasColumnName("DEmpresaContaCorrenteDV");
            this.Property(t => t.DEmpresaNome).HasColumnName("DEmpresaNome");
            this.Property(t => t.DNatureza).HasColumnName("DNatureza");
            this.Property(t => t.DTipoComplemento).HasColumnName("DTipoComplemento");
            this.Property(t => t.DComplemento).HasColumnName("DComplemento");
            this.Property(t => t.DCpmf).HasColumnName("DCpmf");
            this.Property(t => t.DData).HasColumnName("DData");
            this.Property(t => t.DLancamentoData).HasColumnName("DLancamentoData");
            this.Property(t => t.DLancamentoValor).HasColumnName("DLancamentoValor");
            this.Property(t => t.DLancamentoTipo).HasColumnName("DLancamentoTipo");
            this.Property(t => t.DLancamentoCategoria).HasColumnName("DLancamentoCategoria");
            this.Property(t => t.DLancamentoHistoricoCodigo).HasColumnName("DLancamentoHistoricoCodigo");
            this.Property(t => t.DLancamentoHistorico).HasColumnName("DLancamentoHistorico");
            this.Property(t => t.DLancamentoDocumento).HasColumnName("DLancamentoDocumento");

            // Relationships
            this.HasRequired(t => t.ConciliacaoLote)
                .WithMany(t => t.ConciliacaoLoteDetalhes)
                .HasForeignKey(d => d.IdConciliacaoLote);

        }
    }
}
