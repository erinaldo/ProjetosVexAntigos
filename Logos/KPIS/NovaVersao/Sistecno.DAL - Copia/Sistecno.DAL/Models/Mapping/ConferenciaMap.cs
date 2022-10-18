using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ConferenciaMap : EntityTypeConfiguration<Conferencia>
    {
        public ConferenciaMap()
        {
            // Primary Key
            this.HasKey(t => t.IdConferencia);

            // Properties
            this.Property(t => t.IdConferencia)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Situacao)
                .HasMaxLength(50);

            this.Property(t => t.Chave)
                .HasMaxLength(40);

            // Table & Column Mappings
            this.ToTable("Conferencia");
            this.Property(t => t.IdConferencia).HasColumnName("IdConferencia");
            this.Property(t => t.IdRomaneio).HasColumnName("IdRomaneio");
            this.Property(t => t.IdUsuario).HasColumnName("IdUsuario");
            this.Property(t => t.Inicio).HasColumnName("Inicio");
            this.Property(t => t.Final).HasColumnName("Final");
            this.Property(t => t.Situacao).HasColumnName("Situacao");
            this.Property(t => t.Chave).HasColumnName("Chave");

            // Relationships
            this.HasRequired(t => t.Romaneio)
                .WithMany(t => t.Conferencias)
                .HasForeignKey(d => d.IdRomaneio);

        }
    }
}
