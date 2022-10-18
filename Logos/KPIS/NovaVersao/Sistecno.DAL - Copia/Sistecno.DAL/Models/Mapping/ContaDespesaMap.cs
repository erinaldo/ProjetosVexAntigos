using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ContaDespesaMap : EntityTypeConfiguration<ContaDespesa>
    {
        public ContaDespesaMap()
        {
            // Primary Key
            this.HasKey(t => t.IdContaDespesa);

            // Properties
            this.Property(t => t.IdContaDespesa)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.TipoDeDespesa)
                .IsFixedLength()
                .HasMaxLength(8);

            this.Property(t => t.Referencia)
                .HasMaxLength(50);

            this.Property(t => t.Nome)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("ContaDespesa");
            this.Property(t => t.IdContaDespesa).HasColumnName("IdContaDespesa");
            this.Property(t => t.TipoDeDespesa).HasColumnName("TipoDeDespesa");
            this.Property(t => t.DataDeCadastro).HasColumnName("DataDeCadastro");
            this.Property(t => t.Numero).HasColumnName("Numero");
            this.Property(t => t.Referencia).HasColumnName("Referencia");
            this.Property(t => t.Parcelas).HasColumnName("Parcelas");
            this.Property(t => t.ValorTotal).HasColumnName("ValorTotal");
            this.Property(t => t.PrimeiroVencimento).HasColumnName("PrimeiroVencimento");
            this.Property(t => t.IdFilial).HasColumnName("IdFilial");
            this.Property(t => t.Nome).HasColumnName("Nome");
            this.Property(t => t.IdFornecedor).HasColumnName("IdFornecedor");
        }
    }
}
