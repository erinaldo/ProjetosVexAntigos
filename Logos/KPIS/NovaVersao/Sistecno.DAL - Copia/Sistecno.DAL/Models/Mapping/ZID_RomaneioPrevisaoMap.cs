using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ZID_RomaneioPrevisaoMap : EntityTypeConfiguration<ZID_RomaneioPrevisao>
    {
        public ZID_RomaneioPrevisaoMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("ZID_RomaneioPrevisao");
            this.Property(t => t.ID).HasColumnName("ID");
        }
    }
}
