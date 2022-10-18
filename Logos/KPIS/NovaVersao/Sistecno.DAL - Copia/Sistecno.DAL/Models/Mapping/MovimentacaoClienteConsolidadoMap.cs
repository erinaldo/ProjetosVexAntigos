using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class MovimentacaoClienteConsolidadoMap : EntityTypeConfiguration<MovimentacaoClienteConsolidado>
    {
        public MovimentacaoClienteConsolidadoMap()
        {
            // Primary Key
            this.HasKey(t => new { t.IdMovimentacaoClienteConsolidado, t.IdCliente, t.Data });

            // Properties
            this.Property(t => t.IdMovimentacaoClienteConsolidado)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.IdCliente)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("MovimentacaoClienteConsolidado");
            this.Property(t => t.IdMovimentacaoClienteConsolidado).HasColumnName("IdMovimentacaoClienteConsolidado");
            this.Property(t => t.IdCliente).HasColumnName("IdCliente");
            this.Property(t => t.Data).HasColumnName("Data");
            this.Property(t => t.PalletsArmazenagem).HasColumnName("PalletsArmazenagem");
            this.Property(t => t.PalletsEntrada).HasColumnName("PalletsEntrada");
            this.Property(t => t.PalletsSaida).HasColumnName("PalletsSaida");
            this.Property(t => t.M3Armazenagem).HasColumnName("M3Armazenagem");
            this.Property(t => t.M3Entrada).HasColumnName("M3Entrada");
            this.Property(t => t.M3Saida).HasColumnName("M3Saida");
        }
    }
}
