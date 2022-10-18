using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class syspropertyMap : EntityTypeConfiguration<sysproperty>
    {
        public syspropertyMap()
        {
            // Primary Key
            this.HasKey(t => new { t.id, t.smallid, t.type, t.name });

            // Properties
            this.Property(t => t.id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.smallid)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.name)
                .IsRequired()
                .HasMaxLength(128);

            // Table & Column Mappings
            this.ToTable("sysproperties");
            this.Property(t => t.id).HasColumnName("id");
            this.Property(t => t.smallid).HasColumnName("smallid");
            this.Property(t => t.type).HasColumnName("type");
            this.Property(t => t.name).HasColumnName("name");
        }
    }
}
