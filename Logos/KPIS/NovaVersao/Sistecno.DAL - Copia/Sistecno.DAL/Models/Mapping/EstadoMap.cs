using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class EstadoMap : EntityTypeConfiguration<Estado>
    {
        public EstadoMap()
        {
            // Primary Key
            this.HasKey(t => t.IDEstado);

            // Properties
            this.Property(t => t.IDEstado)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Uf)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(2);

            this.Property(t => t.Nome)
                .IsRequired()
                .HasMaxLength(80);

            this.Property(t => t.CodigoDoIbge)
                .HasMaxLength(2);

            this.Property(t => t.CepInicial)
                .HasMaxLength(8);

            this.Property(t => t.CepFinal)
                .HasMaxLength(8);

            // Table & Column Mappings
            this.ToTable("Estado");
            this.Property(t => t.IDEstado).HasColumnName("IDEstado");
            this.Property(t => t.IDPais).HasColumnName("IDPais");
            this.Property(t => t.Uf).HasColumnName("Uf");
            this.Property(t => t.Nome).HasColumnName("Nome");
            this.Property(t => t.CodigoDoIbge).HasColumnName("CodigoDoIbge");
            this.Property(t => t.CepInicial).HasColumnName("CepInicial");
            this.Property(t => t.CepFinal).HasColumnName("CepFinal");

            // Relationships
            this.HasRequired(t => t.Pai)
                .WithMany(t => t.Estadoes)
                .HasForeignKey(d => d.IDPais);

        }
    }
}
