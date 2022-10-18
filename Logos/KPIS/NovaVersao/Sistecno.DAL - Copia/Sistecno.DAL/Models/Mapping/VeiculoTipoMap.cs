using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class VeiculoTipoMap : EntityTypeConfiguration<VeiculoTipo>
    {
        public VeiculoTipoMap()
        {
            // Primary Key
            this.HasKey(t => t.IDVeiculoTipo);

            // Properties
            this.Property(t => t.IDVeiculoTipo)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Nome)
                .HasMaxLength(50);

            this.Property(t => t.CategoriaPermitida)
                .IsFixedLength()
                .HasMaxLength(5);

            this.Property(t => t.Ativo)
                .HasMaxLength(3);

            this.Property(t => t.TracaoReboque)
                .HasMaxLength(10);

            this.Property(t => t.TipoDeRodado)
                .HasMaxLength(30);

            // Table & Column Mappings
            this.ToTable("VeiculoTipo");
            this.Property(t => t.IDVeiculoTipo).HasColumnName("IDVeiculoTipo");
            this.Property(t => t.Nome).HasColumnName("Nome");
            this.Property(t => t.CapacidadeDeCargaKG).HasColumnName("CapacidadeDeCargaKG");
            this.Property(t => t.CapacidadeDeCargaM3).HasColumnName("CapacidadeDeCargaM3");
            this.Property(t => t.CategoriaPermitida).HasColumnName("CategoriaPermitida");
            this.Property(t => t.Ativo).HasColumnName("Ativo");
            this.Property(t => t.TracaoReboque).HasColumnName("TracaoReboque");
            this.Property(t => t.TipoDeRodado).HasColumnName("TipoDeRodado");
        }
    }
}
