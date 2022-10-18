using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ContaDespesaItemMap : EntityTypeConfiguration<ContaDespesaItem>
    {
        public ContaDespesaItemMap()
        {
            // Primary Key
            this.HasKey(t => t.IdContaDespesaItem);

            // Properties
            this.Property(t => t.IdContaDespesaItem)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("ContaDespesaItem");
            this.Property(t => t.IdContaDespesaItem).HasColumnName("IdContaDespesaItem");
            this.Property(t => t.IdContaDespesa).HasColumnName("IdContaDespesa");
            this.Property(t => t.PercentualRateioCentroDeCusto).HasColumnName("PercentualRateioCentroDeCusto");
            this.Property(t => t.PercentualRateioContaContabil).HasColumnName("PercentualRateioContaContabil");
            this.Property(t => t.IdContaContabilFilial).HasColumnName("IdContaContabilFilial");
            this.Property(t => t.IdCentroDeCustoFilial).HasColumnName("IdCentroDeCustoFilial");
            this.Property(t => t.ValorRateioCentroDeCusto).HasColumnName("ValorRateioCentroDeCusto");
            this.Property(t => t.ValorRateioContaContabil).HasColumnName("ValorRateioContaContabil");

            // Relationships
            this.HasRequired(t => t.ContaDespesa)
                .WithMany(t => t.ContaDespesaItems)
                .HasForeignKey(d => d.IdContaDespesa);

        }
    }
}
