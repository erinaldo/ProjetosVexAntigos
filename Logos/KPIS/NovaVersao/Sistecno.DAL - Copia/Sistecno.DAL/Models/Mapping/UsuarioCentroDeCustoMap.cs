using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class UsuarioCentroDeCustoMap : EntityTypeConfiguration<UsuarioCentroDeCusto>
    {
        public UsuarioCentroDeCustoMap()
        {
            // Primary Key
            this.HasKey(t => t.IdUsuarioCentroDeCusto);

            // Properties
            this.Property(t => t.IdUsuarioCentroDeCusto)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("UsuarioCentroDeCusto");
            this.Property(t => t.IdUsuarioCentroDeCusto).HasColumnName("IdUsuarioCentroDeCusto");
            this.Property(t => t.IdUsuario).HasColumnName("IdUsuario");
            this.Property(t => t.IdCentroDeCusto).HasColumnName("IdCentroDeCusto");

            // Relationships
            this.HasRequired(t => t.CentroDeCusto)
                .WithMany(t => t.UsuarioCentroDeCustoes)
                .HasForeignKey(d => d.IdCentroDeCusto);
            this.HasRequired(t => t.Usuario)
                .WithMany(t => t.UsuarioCentroDeCustoes)
                .HasForeignKey(d => d.IdUsuario);

        }
    }
}
