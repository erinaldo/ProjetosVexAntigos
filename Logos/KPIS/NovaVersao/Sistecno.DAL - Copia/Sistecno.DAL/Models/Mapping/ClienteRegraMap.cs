using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ClienteRegraMap : EntityTypeConfiguration<ClienteRegra>
    {
        public ClienteRegraMap()
        {
            // Primary Key
            this.HasKey(t => t.IdClienteRegra);

            // Properties
            this.Property(t => t.IdClienteRegra)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("ClienteRegra");
            this.Property(t => t.IdClienteRegra).HasColumnName("IdClienteRegra");
            this.Property(t => t.PrazoDeEntrega).HasColumnName("PrazoDeEntrega");
            this.Property(t => t.PrazoDeEntregaTolerancia).HasColumnName("PrazoDeEntregaTolerancia");
            this.Property(t => t.NumeroDoRecebimento).HasColumnName("NumeroDoRecebimento");
            this.Property(t => t.ValorPorCaixa).HasColumnName("ValorPorCaixa");
            this.Property(t => t.DataDoRecebimento).HasColumnName("DataDoRecebimento");
            this.Property(t => t.KM).HasColumnName("KM");
            this.Property(t => t.Faixa).HasColumnName("Faixa");

            // Relationships
            this.HasRequired(t => t.Cliente)
                .WithOptional(t => t.ClienteRegra);

        }
    }
}
