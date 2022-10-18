using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class TipoDeTituloDuplicataMap : EntityTypeConfiguration<TipoDeTituloDuplicata>
    {
        public TipoDeTituloDuplicataMap()
        {
            // Primary Key
            this.HasKey(t => t.IdTipoDeTituloDuplicata);

            // Properties
            this.Property(t => t.IdTipoDeTituloDuplicata)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Nome)
                .HasMaxLength(50);

            this.Property(t => t.Ativo)
                .HasMaxLength(3);

            // Table & Column Mappings
            this.ToTable("TipoDeTituloDuplicata");
            this.Property(t => t.IdTipoDeTituloDuplicata).HasColumnName("IdTipoDeTituloDuplicata");
            this.Property(t => t.IdTipoDeTitulo).HasColumnName("IdTipoDeTitulo");
            this.Property(t => t.Nome).HasColumnName("Nome");
            this.Property(t => t.Ativo).HasColumnName("Ativo");

            // Relationships
            this.HasOptional(t => t.TipoDeTitulo)
                .WithMany(t => t.TipoDeTituloDuplicatas)
                .HasForeignKey(d => d.IdTipoDeTitulo);

        }
    }
}
