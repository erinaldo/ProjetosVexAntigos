using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class VeiculoFotoMap : EntityTypeConfiguration<VeiculoFoto>
    {
        public VeiculoFotoMap()
        {
            // Primary Key
            this.HasKey(t => t.IDVeiculoFoto);

            // Properties
            this.Property(t => t.IDVeiculoFoto)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Descricao)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("VeiculoFoto");
            this.Property(t => t.IDVeiculoFoto).HasColumnName("IDVeiculoFoto");
            this.Property(t => t.IDVeiculo).HasColumnName("IDVeiculo");
            this.Property(t => t.Foto).HasColumnName("Foto");
            this.Property(t => t.Descricao).HasColumnName("Descricao");

            // Relationships
            this.HasRequired(t => t.Veiculo)
                .WithMany(t => t.VeiculoFotoes)
                .HasForeignKey(d => d.IDVeiculo);

        }
    }
}
