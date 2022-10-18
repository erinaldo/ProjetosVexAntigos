using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ConferenciaPalletMap : EntityTypeConfiguration<ConferenciaPallet>
    {
        public ConferenciaPalletMap()
        {
            // Primary Key
            this.HasKey(t => t.IdConferenciaPallet);

            // Properties
            this.Property(t => t.IdConferenciaPallet)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("ConferenciaPallet");
            this.Property(t => t.IdConferenciaPallet).HasColumnName("IdConferenciaPallet");
            this.Property(t => t.IdConferencia).HasColumnName("IdConferencia");
            this.Property(t => t.IdPallet).HasColumnName("IdPallet");
            this.Property(t => t.IdDepositoPlantaLocalizacao).HasColumnName("IdDepositoPlantaLocalizacao");

            // Relationships
            this.HasRequired(t => t.Conferencia)
                .WithMany(t => t.ConferenciaPallets)
                .HasForeignKey(d => d.IdConferencia);
            this.HasRequired(t => t.DepositoPlantaLocalizacao)
                .WithMany(t => t.ConferenciaPallets)
                .HasForeignKey(d => d.IdDepositoPlantaLocalizacao);
            this.HasRequired(t => t.UnidadeDeArmazenagem)
                .WithMany(t => t.ConferenciaPallets)
                .HasForeignKey(d => d.IdPallet);

        }
    }
}
