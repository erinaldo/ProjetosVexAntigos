using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ConferenciaPalletDocVolMap : EntityTypeConfiguration<ConferenciaPalletDocVol>
    {
        public ConferenciaPalletDocVolMap()
        {
            // Primary Key
            this.HasKey(t => t.IdConferenciaPalletDocVol);

            // Properties
            this.Property(t => t.IdConferenciaPalletDocVol)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("ConferenciaPalletDocVol");
            this.Property(t => t.IdConferenciaPalletDocVol).HasColumnName("IdConferenciaPalletDocVol");
            this.Property(t => t.IdConferenciaPalletDoc).HasColumnName("IdConferenciaPalletDoc");
            this.Property(t => t.IdVolume).HasColumnName("IdVolume");

            // Relationships
            this.HasRequired(t => t.ConferenciaPalletDoc)
                .WithMany(t => t.ConferenciaPalletDocVols)
                .HasForeignKey(d => d.IdConferenciaPalletDoc);

        }
    }
}
