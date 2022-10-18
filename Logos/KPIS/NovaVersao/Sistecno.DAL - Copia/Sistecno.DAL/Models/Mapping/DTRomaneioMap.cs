using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class DTRomaneioMap : EntityTypeConfiguration<DTRomaneio>
    {
        public DTRomaneioMap()
        {
            // Primary Key
            this.HasKey(t => t.IDDTRomaneio);

            // Properties
            this.Property(t => t.IDDTRomaneio)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("DTRomaneio");
            this.Property(t => t.IDDTRomaneio).HasColumnName("IDDTRomaneio");
            this.Property(t => t.IDDT).HasColumnName("IDDT");
            this.Property(t => t.IDRomaneio).HasColumnName("IDRomaneio");

            // Relationships
            this.HasRequired(t => t.DT)
                .WithMany(t => t.DTRomaneios)
                .HasForeignKey(d => d.IDDT);
            this.HasRequired(t => t.Romaneio)
                .WithMany(t => t.DTRomaneios)
                .HasForeignKey(d => d.IDRomaneio);

        }
    }
}
