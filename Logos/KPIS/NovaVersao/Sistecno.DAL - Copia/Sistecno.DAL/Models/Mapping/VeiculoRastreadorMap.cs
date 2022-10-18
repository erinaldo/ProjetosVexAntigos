using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class VeiculoRastreadorMap : EntityTypeConfiguration<VeiculoRastreador>
    {
        public VeiculoRastreadorMap()
        {
            // Primary Key
            this.HasKey(t => t.IDVeiculoRastreador);

            // Properties
            this.Property(t => t.IDVeiculoRastreador)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Nome)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("VeiculoRastreador");
            this.Property(t => t.IDVeiculoRastreador).HasColumnName("IDVeiculoRastreador");
            this.Property(t => t.Nome).HasColumnName("Nome");
        }
    }
}
