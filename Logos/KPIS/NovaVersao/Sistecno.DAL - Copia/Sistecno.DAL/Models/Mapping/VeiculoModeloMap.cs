using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class VeiculoModeloMap : EntityTypeConfiguration<VeiculoModelo>
    {
        public VeiculoModeloMap()
        {
            // Primary Key
            this.HasKey(t => t.IDVeiculoModelo);

            // Properties
            this.Property(t => t.IDVeiculoModelo)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Nome)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("VeiculoModelo");
            this.Property(t => t.IDVeiculoModelo).HasColumnName("IDVeiculoModelo");
            this.Property(t => t.IDVeiculoMarca).HasColumnName("IDVeiculoMarca");
            this.Property(t => t.Nome).HasColumnName("Nome");

            // Relationships
            this.HasRequired(t => t.VeiculoMarca)
                .WithMany(t => t.VeiculoModeloes)
                .HasForeignKey(d => d.IDVeiculoMarca);

        }
    }
}
