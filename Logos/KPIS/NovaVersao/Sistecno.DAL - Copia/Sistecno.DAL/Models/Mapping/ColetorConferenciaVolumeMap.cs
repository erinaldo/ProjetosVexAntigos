using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ColetorConferenciaVolumeMap : EntityTypeConfiguration<ColetorConferenciaVolume>
    {
        public ColetorConferenciaVolumeMap()
        {
            // Primary Key
            this.HasKey(t => t.IdColetorConferenciaVolume);

            // Properties
            this.Property(t => t.IdColetorConferenciaVolume)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.CodigoDeBarras)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("ColetorConferenciaVolume");
            this.Property(t => t.IdColetorConferenciaVolume).HasColumnName("IdColetorConferenciaVolume");
            this.Property(t => t.IdColetorConferencia).HasColumnName("IdColetorConferencia");
            this.Property(t => t.CodigoDeBarras).HasColumnName("CodigoDeBarras");
            this.Property(t => t.DataHora).HasColumnName("DataHora");

            // Relationships
            this.HasRequired(t => t.ColetorConferencia)
                .WithMany(t => t.ColetorConferenciaVolumes)
                .HasForeignKey(d => d.IdColetorConferencia);

        }
    }
}
