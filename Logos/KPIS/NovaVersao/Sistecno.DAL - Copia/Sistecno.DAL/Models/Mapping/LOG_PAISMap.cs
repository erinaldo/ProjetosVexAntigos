using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class LOG_PAISMap : EntityTypeConfiguration<LOG_PAIS>
    {
        public LOG_PAISMap()
        {
            // Primary Key
            this.HasKey(t => new { t.PAI_SG, t.PAI_NO_PORTUGUES });

            // Properties
            this.Property(t => t.PAI_SG)
                .IsRequired()
                .HasMaxLength(2);

            this.Property(t => t.PAI_SG_ALTERNATIVA)
                .HasMaxLength(3);

            this.Property(t => t.PAI_NO_PORTUGUES)
                .IsRequired()
                .HasMaxLength(72);

            this.Property(t => t.PAI_NO_INGLES)
                .HasMaxLength(72);

            this.Property(t => t.PAI_NO_FRANCES)
                .HasMaxLength(72);

            this.Property(t => t.PAI_ABREVIATURA)
                .HasMaxLength(36);

            // Table & Column Mappings
            this.ToTable("LOG_PAIS");
            this.Property(t => t.PAI_SG).HasColumnName("PAI_SG");
            this.Property(t => t.PAI_SG_ALTERNATIVA).HasColumnName("PAI_SG_ALTERNATIVA");
            this.Property(t => t.PAI_NO_PORTUGUES).HasColumnName("PAI_NO_PORTUGUES");
            this.Property(t => t.PAI_NO_INGLES).HasColumnName("PAI_NO_INGLES");
            this.Property(t => t.PAI_NO_FRANCES).HasColumnName("PAI_NO_FRANCES");
            this.Property(t => t.PAI_ABREVIATURA).HasColumnName("PAI_ABREVIATURA");
        }
    }
}
