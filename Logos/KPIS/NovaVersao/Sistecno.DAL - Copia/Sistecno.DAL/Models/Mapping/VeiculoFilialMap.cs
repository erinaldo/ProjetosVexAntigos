using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class VeiculoFilialMap : EntityTypeConfiguration<VeiculoFilial>
    {
        public VeiculoFilialMap()
        {
            // Primary Key
            this.HasKey(t => t.IDVeiculoFilial);

            // Properties
            this.Property(t => t.IDVeiculoFilial)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("VeiculoFilial");
            this.Property(t => t.IDVeiculoFilial).HasColumnName("IDVeiculoFilial");
            this.Property(t => t.IDVeiculo).HasColumnName("IDVeiculo");
            this.Property(t => t.IDFilial).HasColumnName("IDFilial");

            // Relationships
            this.HasRequired(t => t.Filial)
                .WithMany(t => t.VeiculoFilials)
                .HasForeignKey(d => d.IDFilial);
            this.HasRequired(t => t.Veiculo)
                .WithMany(t => t.VeiculoFilials)
                .HasForeignKey(d => d.IDVeiculo);

        }
    }
}
