using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class CalculosREMap : EntityTypeConfiguration<CalculosRE>
    {
        public CalculosREMap()
        {
            // Primary Key
            this.HasKey(t => new { t.IDDOCUMENTO, t.VOLUMES, t.IDDT });

            // Properties
            this.Property(t => t.IDDOCUMENTO)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.VOLUMES)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.IDDT)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("CalculosRE");
            this.Property(t => t.IDDOCUMENTO).HasColumnName("IDDOCUMENTO");
            this.Property(t => t.VOLUMES).HasColumnName("VOLUMES");
            this.Property(t => t.VALORDANOTA).HasColumnName("VALORDANOTA");
            this.Property(t => t.PESOBRUTO).HasColumnName("PESOBRUTO");
            this.Property(t => t.PESOLIQUIDO).HasColumnName("PESOLIQUIDO");
            this.Property(t => t.IDDT).HasColumnName("IDDT");
        }
    }
}
