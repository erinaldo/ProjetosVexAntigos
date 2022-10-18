using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class CentroDeCustoFilialMap : EntityTypeConfiguration<CentroDeCustoFilial>
    {
        public CentroDeCustoFilialMap()
        {
            // Primary Key
            this.HasKey(t => t.IDCentroDeCustoFilial);

            // Properties
            this.Property(t => t.IDCentroDeCustoFilial)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("CentroDeCustoFilial");
            this.Property(t => t.IDCentroDeCustoFilial).HasColumnName("IDCentroDeCustoFilial");
            this.Property(t => t.IDCentroDeCusto).HasColumnName("IDCentroDeCusto");
            this.Property(t => t.IDFilial).HasColumnName("IDFilial");
            this.Property(t => t.SaldoInicial).HasColumnName("SaldoInicial");
            this.Property(t => t.Saldo).HasColumnName("Saldo");

            // Relationships
            this.HasRequired(t => t.CentroDeCusto)
                .WithMany(t => t.CentroDeCustoFilials)
                .HasForeignKey(d => d.IDCentroDeCusto);
            this.HasRequired(t => t.Filial)
                .WithMany(t => t.CentroDeCustoFilials)
                .HasForeignKey(d => d.IDFilial);

        }
    }
}
