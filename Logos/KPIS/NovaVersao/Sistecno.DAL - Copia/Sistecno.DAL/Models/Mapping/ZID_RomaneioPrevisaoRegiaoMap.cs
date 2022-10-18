using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ZID_RomaneioPrevisaoRegiaoMap : EntityTypeConfiguration<ZID_RomaneioPrevisaoRegiao>
    {
        public ZID_RomaneioPrevisaoRegiaoMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("ZID_RomaneioPrevisaoRegiao");
            this.Property(t => t.ID).HasColumnName("ID");
        }
    }
}
