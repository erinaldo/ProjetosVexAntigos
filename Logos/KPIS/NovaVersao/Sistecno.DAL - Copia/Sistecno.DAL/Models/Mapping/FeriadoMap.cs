using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class FeriadoMap : EntityTypeConfiguration<Feriado>
    {
        public FeriadoMap()
        {
            // Primary Key
            this.HasKey(t => t.IdFeriado);

            // Properties
            this.Property(t => t.IdFeriado)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Descricao)
                .HasMaxLength(60);

            // Table & Column Mappings
            this.ToTable("Feriado");
            this.Property(t => t.IdFeriado).HasColumnName("IdFeriado");
            this.Property(t => t.IdCidade).HasColumnName("IdCidade");
            this.Property(t => t.IdEstado).HasColumnName("IdEstado");
            this.Property(t => t.Data).HasColumnName("Data");
            this.Property(t => t.Descricao).HasColumnName("Descricao");

            // Relationships
            this.HasOptional(t => t.Cidade)
                .WithMany(t => t.Feriadoes)
                .HasForeignKey(d => d.IdCidade);
            this.HasOptional(t => t.Estado)
                .WithMany(t => t.Feriadoes)
                .HasForeignKey(d => d.IdEstado);

        }
    }
}
