using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class VeiculoTabelaMap : EntityTypeConfiguration<VeiculoTabela>
    {
        public VeiculoTabelaMap()
        {
            // Primary Key
            this.HasKey(t => t.IdVeiculoTabela);

            // Properties
            this.Property(t => t.IdVeiculoTabela)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("VeiculoTabela");
            this.Property(t => t.IdVeiculoTabela).HasColumnName("IdVeiculoTabela");
            this.Property(t => t.IdVeiculoTipo).HasColumnName("IdVeiculoTipo");
            this.Property(t => t.IdVeiculo).HasColumnName("IdVeiculo");
            this.Property(t => t.IdFilial).HasColumnName("IdFilial");
            this.Property(t => t.Diaria).HasColumnName("Diaria");
            this.Property(t => t.Ajudante).HasColumnName("Ajudante");
            this.Property(t => t.ValorPorEntrega).HasColumnName("ValorPorEntrega");
            this.Property(t => t.ValorKM).HasColumnName("ValorKM");
            this.Property(t => t.ValorKG).HasColumnName("ValorKG");
            this.Property(t => t.PercentualProximaSaida).HasColumnName("PercentualProximaSaida");
            this.Property(t => t.Cadastro).HasColumnName("Cadastro");
            this.Property(t => t.UltimaAlteracao).HasColumnName("UltimaAlteracao");
            this.Property(t => t.IdUsuario).HasColumnName("IdUsuario");

            // Relationships
            this.HasOptional(t => t.Filial)
                .WithMany(t => t.VeiculoTabelas)
                .HasForeignKey(d => d.IdFilial);
            this.HasOptional(t => t.Veiculo)
                .WithMany(t => t.VeiculoTabelas)
                .HasForeignKey(d => d.IdVeiculo);
            this.HasRequired(t => t.VeiculoTipo)
                .WithMany(t => t.VeiculoTabelas)
                .HasForeignKey(d => d.IdVeiculoTipo);

        }
    }
}
