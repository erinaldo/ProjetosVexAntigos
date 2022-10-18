using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ZID_ClienteTipoDeMaterialMap : EntityTypeConfiguration<ZID_ClienteTipoDeMaterial>
    {
        public ZID_ClienteTipoDeMaterialMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("ZID_ClienteTipoDeMaterial");
            this.Property(t => t.ID).HasColumnName("ID");
        }
    }
}
