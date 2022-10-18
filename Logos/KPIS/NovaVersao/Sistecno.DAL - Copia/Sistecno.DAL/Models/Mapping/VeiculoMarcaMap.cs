using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class VeiculoMarcaMap : EntityTypeConfiguration<VeiculoMarca>
    {
        public VeiculoMarcaMap()
        {
            // Primary Key
            this.HasKey(t => t.IDVeiculoMarca);

            // Properties
            this.Property(t => t.IDVeiculoMarca)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Nome)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("VeiculoMarca");
            this.Property(t => t.IDVeiculoMarca).HasColumnName("IDVeiculoMarca");
            this.Property(t => t.Nome).HasColumnName("Nome");
        }
    }
}
