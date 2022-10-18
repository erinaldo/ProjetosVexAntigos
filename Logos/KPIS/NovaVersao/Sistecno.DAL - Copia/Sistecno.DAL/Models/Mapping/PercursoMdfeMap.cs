using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class PercursoMdfeMap : EntityTypeConfiguration<PercursoMdfe>
    {
        public PercursoMdfeMap()
        {
            // Primary Key
            this.HasKey(t => new { t.Ordem, t.UfOrigem, t.UfDestino, t.UfPercurso });

            // Properties
            this.Property(t => t.Ordem)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.UfOrigem)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(2);

            this.Property(t => t.UfDestino)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(2);

            this.Property(t => t.UfPercurso)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(2);

            // Table & Column Mappings
            this.ToTable("PercursoMdfe");
            this.Property(t => t.Ordem).HasColumnName("Ordem");
            this.Property(t => t.UfOrigem).HasColumnName("UfOrigem");
            this.Property(t => t.UfDestino).HasColumnName("UfDestino");
            this.Property(t => t.UfPercurso).HasColumnName("UfPercurso");
        }
    }
}
