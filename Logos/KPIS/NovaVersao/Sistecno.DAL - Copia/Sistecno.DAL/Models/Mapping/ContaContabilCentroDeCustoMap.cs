using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ContaContabilCentroDeCustoMap : EntityTypeConfiguration<ContaContabilCentroDeCusto>
    {
        public ContaContabilCentroDeCustoMap()
        {
            // Primary Key
            this.HasKey(t => t.IdContaContabilCentroDeCusto);

            // Properties
            this.Property(t => t.IdContaContabilCentroDeCusto)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Operacao)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("ContaContabilCentroDeCusto");
            this.Property(t => t.IdContaContabilCentroDeCusto).HasColumnName("IdContaContabilCentroDeCusto");
            this.Property(t => t.IdContaContabil).HasColumnName("IdContaContabil");
            this.Property(t => t.IdCentroDeCusto).HasColumnName("IdCentroDeCusto");
            this.Property(t => t.Operacao).HasColumnName("Operacao");

            // Relationships
            this.HasRequired(t => t.CentroDeCusto)
                .WithMany(t => t.ContaContabilCentroDeCustoes)
                .HasForeignKey(d => d.IdCentroDeCusto);
            this.HasRequired(t => t.ContaContabil)
                .WithMany(t => t.ContaContabilCentroDeCustoes)
                .HasForeignKey(d => d.IdContaContabil);

        }
    }
}
