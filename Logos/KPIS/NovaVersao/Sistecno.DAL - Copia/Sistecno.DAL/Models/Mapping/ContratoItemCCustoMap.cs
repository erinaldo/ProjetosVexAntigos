using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ContratoItemCCustoMap : EntityTypeConfiguration<ContratoItemCCusto>
    {
        public ContratoItemCCustoMap()
        {
            // Primary Key
            this.HasKey(t => t.IdContratoItemCCusto);

            // Properties
            this.Property(t => t.IdContratoItemCCusto)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("ContratoItemCCusto");
            this.Property(t => t.IdContratoItemCCusto).HasColumnName("IdContratoItemCCusto");
            this.Property(t => t.IdContratoItem).HasColumnName("IdContratoItem");
            this.Property(t => t.IdCentroDeCustoFilial).HasColumnName("IdCentroDeCustoFilial");
            this.Property(t => t.PercentualRateioCentroDeCusto).HasColumnName("PercentualRateioCentroDeCusto");
            this.Property(t => t.ValorRateioCentroDeCusto).HasColumnName("ValorRateioCentroDeCusto");

            // Relationships
            this.HasRequired(t => t.ContratoItem)
                .WithMany(t => t.ContratoItemCCustoes)
                .HasForeignKey(d => d.IdContratoItem);

        }
    }
}
