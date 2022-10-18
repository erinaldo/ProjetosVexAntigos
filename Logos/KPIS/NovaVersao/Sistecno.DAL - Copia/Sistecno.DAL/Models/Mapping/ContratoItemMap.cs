using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ContratoItemMap : EntityTypeConfiguration<ContratoItem>
    {
        public ContratoItemMap()
        {
            // Primary Key
            this.HasKey(t => t.IdContratoItem);

            // Properties
            this.Property(t => t.IdContratoItem)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("ContratoItem");
            this.Property(t => t.IdContratoItem).HasColumnName("IdContratoItem");
            this.Property(t => t.IdContrato).HasColumnName("IdContrato");
            this.Property(t => t.PercentualRateioCentroDeCusto).HasColumnName("PercentualRateioCentroDeCusto");
            this.Property(t => t.PercentualRateioContaContabil).HasColumnName("PercentualRateioContaContabil");
            this.Property(t => t.IdContaContabilFilial).HasColumnName("IdContaContabilFilial");
            this.Property(t => t.IdCentroDeCustoFilial).HasColumnName("IdCentroDeCustoFilial");
            this.Property(t => t.ValorRateioCentroDeCusto).HasColumnName("ValorRateioCentroDeCusto");
            this.Property(t => t.ValorRateioContaContabil).HasColumnName("ValorRateioContaContabil");

            // Relationships
            this.HasRequired(t => t.Contrato)
                .WithMany(t => t.ContratoItems)
                .HasForeignKey(d => d.IdContrato);

        }
    }
}
