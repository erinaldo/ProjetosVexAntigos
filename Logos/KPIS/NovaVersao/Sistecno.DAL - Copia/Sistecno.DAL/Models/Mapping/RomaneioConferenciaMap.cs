using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class RomaneioConferenciaMap : EntityTypeConfiguration<RomaneioConferencia>
    {
        public RomaneioConferenciaMap()
        {
            // Primary Key
            this.HasKey(t => t.IdRomaneioConferencia);

            // Properties
            this.Property(t => t.IdRomaneioConferencia)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Status)
                .HasMaxLength(50);

            this.Property(t => t.Descricao)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("RomaneioConferencia");
            this.Property(t => t.IdRomaneioConferencia).HasColumnName("IdRomaneioConferencia");
            this.Property(t => t.IdRomaneio).HasColumnName("IdRomaneio");
            this.Property(t => t.IdUsuario).HasColumnName("IdUsuario");
            this.Property(t => t.Abertura).HasColumnName("Abertura");
            this.Property(t => t.Inicio).HasColumnName("Inicio");
            this.Property(t => t.Final).HasColumnName("Final");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.Descricao).HasColumnName("Descricao");

            // Relationships
            this.HasRequired(t => t.Romaneio)
                .WithMany(t => t.RomaneioConferencias)
                .HasForeignKey(d => d.IdRomaneio);

        }
    }
}
