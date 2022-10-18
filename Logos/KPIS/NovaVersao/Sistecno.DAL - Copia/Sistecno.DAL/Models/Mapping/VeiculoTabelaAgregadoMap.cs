using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class VeiculoTabelaAgregadoMap : EntityTypeConfiguration<VeiculoTabelaAgregado>
    {
        public VeiculoTabelaAgregadoMap()
        {
            // Primary Key
            this.HasKey(t => t.IdVeiculoTabelaAgregado);

            // Properties
            this.Property(t => t.IdVeiculoTabelaAgregado)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("VeiculoTabelaAgregado");
            this.Property(t => t.IdVeiculoTabelaAgregado).HasColumnName("IdVeiculoTabelaAgregado");
            this.Property(t => t.IdVeiculoTabela).HasColumnName("IdVeiculoTabela");
            this.Property(t => t.IdVeiculo).HasColumnName("IdVeiculo");

            // Relationships
            this.HasRequired(t => t.Veiculo)
                .WithMany(t => t.VeiculoTabelaAgregadoes)
                .HasForeignKey(d => d.IdVeiculo);
            this.HasRequired(t => t.VeiculoTabela)
                .WithMany(t => t.VeiculoTabelaAgregadoes)
                .HasForeignKey(d => d.IdVeiculoTabela);

        }
    }
}
