using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class CidadeDistanciaMap : EntityTypeConfiguration<CidadeDistancia>
    {
        public CidadeDistanciaMap()
        {
            // Primary Key
            this.HasKey(t => t.IdCidadeDistancia);

            // Properties
            this.Property(t => t.IdCidadeDistancia)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("CidadeDistancia");
            this.Property(t => t.IdCidadeDistancia).HasColumnName("IdCidadeDistancia");
            this.Property(t => t.IdFilialOrigem).HasColumnName("IdFilialOrigem");
            this.Property(t => t.IdCidadeDestino).HasColumnName("IdCidadeDestino");
            this.Property(t => t.Distancia).HasColumnName("Distancia");

            // Relationships
            this.HasRequired(t => t.Cidade)
                .WithMany(t => t.CidadeDistancias)
                .HasForeignKey(d => d.IdCidadeDestino);
            this.HasRequired(t => t.Filial)
                .WithMany(t => t.CidadeDistancias)
                .HasForeignKey(d => d.IdFilialOrigem);

        }
    }
}
