using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class EstadoFaixaDeCepMap : EntityTypeConfiguration<EstadoFaixaDeCep>
    {
        public EstadoFaixaDeCepMap()
        {
            // Primary Key
            this.HasKey(t => t.IDEstadoFaixaDeCep);

            // Properties
            this.Property(t => t.IDEstadoFaixaDeCep)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.CepInicial)
                .IsFixedLength()
                .HasMaxLength(8);

            this.Property(t => t.CepFinal)
                .IsFixedLength()
                .HasMaxLength(8);

            // Table & Column Mappings
            this.ToTable("EstadoFaixaDeCep");
            this.Property(t => t.IDEstadoFaixaDeCep).HasColumnName("IDEstadoFaixaDeCep");
            this.Property(t => t.IDEstado).HasColumnName("IDEstado");
            this.Property(t => t.CepInicial).HasColumnName("CepInicial");
            this.Property(t => t.CepFinal).HasColumnName("CepFinal");

            // Relationships
            this.HasOptional(t => t.Estado)
                .WithMany(t => t.EstadoFaixaDeCeps)
                .HasForeignKey(d => d.IDEstado);

        }
    }
}
