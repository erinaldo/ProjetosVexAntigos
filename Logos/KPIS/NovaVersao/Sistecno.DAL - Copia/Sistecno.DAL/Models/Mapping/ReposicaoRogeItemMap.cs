using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ReposicaoRogeItemMap : EntityTypeConfiguration<ReposicaoRogeItem>
    {
        public ReposicaoRogeItemMap()
        {
            // Primary Key
            this.HasKey(t => t.IdReposicaoRogeItem);

            // Properties
            this.Property(t => t.IdReposicaoRogeItem)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.CodigoRoge)
                .HasMaxLength(50);

            this.Property(t => t.PerteceANota)
                .HasMaxLength(3);

            this.Property(t => t.CodigoBarrasLido)
                .HasMaxLength(100);

            this.Property(t => t.Descricao)
                .HasMaxLength(100);

            this.Property(t => t.Pago)
                .HasMaxLength(3);

            this.Property(t => t.ObservacaoPago)
                .HasMaxLength(2000);

            // Table & Column Mappings
            this.ToTable("ReposicaoRogeItem");
            this.Property(t => t.IdReposicaoRogeItem).HasColumnName("IdReposicaoRogeItem");
            this.Property(t => t.IdReposicaoRoge).HasColumnName("IdReposicaoRoge");
            this.Property(t => t.CodigoRoge).HasColumnName("CodigoRoge");
            this.Property(t => t.DataDaInclusao).HasColumnName("DataDaInclusao");
            this.Property(t => t.DataConferido).HasColumnName("DataConferido");
            this.Property(t => t.QuantidadeLido).HasColumnName("QuantidadeLido");
            this.Property(t => t.PerteceANota).HasColumnName("PerteceANota");
            this.Property(t => t.CodigoBarrasLido).HasColumnName("CodigoBarrasLido");
            this.Property(t => t.Descricao).HasColumnName("Descricao");
            this.Property(t => t.QuantidadeNota).HasColumnName("QuantidadeNota");
            this.Property(t => t.Valor).HasColumnName("Valor");
            this.Property(t => t.ValorTotal).HasColumnName("ValorTotal");
            this.Property(t => t.Pago).HasColumnName("Pago");
            this.Property(t => t.ObservacaoPago).HasColumnName("ObservacaoPago");

            // Relationships
            this.HasOptional(t => t.ReposicaoRoge)
                .WithMany(t => t.ReposicaoRogeItems)
                .HasForeignKey(d => d.IdReposicaoRoge);

        }
    }
}
