using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ConferenciaPalletDocMap : EntityTypeConfiguration<ConferenciaPalletDoc>
    {
        public ConferenciaPalletDocMap()
        {
            // Primary Key
            this.HasKey(t => t.IdConferenciaPalletDoc);

            // Properties
            this.Property(t => t.IdConferenciaPalletDoc)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Situacao)
                .HasMaxLength(14);

            this.Property(t => t.pedidonotafiscal)
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("ConferenciaPalletDoc");
            this.Property(t => t.IdConferenciaPalletDoc).HasColumnName("IdConferenciaPalletDoc");
            this.Property(t => t.IdConferenciaPallet).HasColumnName("IdConferenciaPallet");
            this.Property(t => t.IdDocumento).HasColumnName("IdDocumento");
            this.Property(t => t.Situacao).HasColumnName("Situacao");
            this.Property(t => t.Data).HasColumnName("Data");
            this.Property(t => t.pedidonotafiscal).HasColumnName("pedidonotafiscal");

            // Relationships
            this.HasRequired(t => t.ConferenciaPallet)
                .WithMany(t => t.ConferenciaPalletDocs)
                .HasForeignKey(d => d.IdConferenciaPallet);
            this.HasOptional(t => t.Documento)
                .WithMany(t => t.ConferenciaPalletDocs)
                .HasForeignKey(d => d.IdDocumento);

        }
    }
}
