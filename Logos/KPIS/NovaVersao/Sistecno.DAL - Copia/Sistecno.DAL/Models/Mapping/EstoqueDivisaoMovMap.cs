using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class EstoqueDivisaoMovMap : EntityTypeConfiguration<EstoqueDivisaoMov>
    {
        public EstoqueDivisaoMovMap()
        {
            // Primary Key
            this.HasKey(t => t.IDEstoqueDivisaoMov);

            // Properties
            this.Property(t => t.IDEstoqueDivisaoMov)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Historico)
                .HasMaxLength(300);

            // Table & Column Mappings
            this.ToTable("EstoqueDivisaoMov");
            this.Property(t => t.IDEstoqueDivisaoMov).HasColumnName("IDEstoqueDivisaoMov");
            this.Property(t => t.IDEstoqueDivisao).HasColumnName("IDEstoqueDivisao");
            this.Property(t => t.IDEstoqueOperacao).HasColumnName("IDEstoqueOperacao");
            this.Property(t => t.IDEstoqueDivisaoOrigem).HasColumnName("IDEstoqueDivisaoOrigem");
            this.Property(t => t.IDEstoqueDivisaoDestino).HasColumnName("IDEstoqueDivisaoDestino");
            this.Property(t => t.IDUsuario).HasColumnName("IDUsuario");
            this.Property(t => t.IdMovimentacaoItem).HasColumnName("IdMovimentacaoItem");
            this.Property(t => t.Historico).HasColumnName("Historico");
            this.Property(t => t.DataHora).HasColumnName("DataHora");
            this.Property(t => t.Quantidade).HasColumnName("Quantidade");
            this.Property(t => t.Saldo).HasColumnName("Saldo");
            this.Property(t => t.SaldoBaseExterna).HasColumnName("SaldoBaseExterna");
            this.Property(t => t.IdUsuarioSolicitante).HasColumnName("IdUsuarioSolicitante");
            this.Property(t => t.IdDocumento).HasColumnName("IdDocumento");

            // Relationships
            this.HasRequired(t => t.EstoqueDivisao)
                .WithMany(t => t.EstoqueDivisaoMovs)
                .HasForeignKey(d => d.IDEstoqueDivisao);
            this.HasOptional(t => t.EstoqueDivisao1)
                .WithMany(t => t.EstoqueDivisaoMovs1)
                .HasForeignKey(d => d.IDEstoqueDivisaoDestino);
            this.HasOptional(t => t.EstoqueDivisao2)
                .WithMany(t => t.EstoqueDivisaoMovs2)
                .HasForeignKey(d => d.IDEstoqueDivisaoOrigem);
            this.HasRequired(t => t.EstoqueOperacao)
                .WithMany(t => t.EstoqueDivisaoMovs)
                .HasForeignKey(d => d.IDEstoqueOperacao);
            this.HasOptional(t => t.MovimentacaoItem)
                .WithMany(t => t.EstoqueDivisaoMovs)
                .HasForeignKey(d => d.IdMovimentacaoItem);
            this.HasRequired(t => t.Usuario)
                .WithMany(t => t.EstoqueDivisaoMovs)
                .HasForeignKey(d => d.IDUsuario);

        }
    }
}
