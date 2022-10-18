using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ZID_veiculomarcaMap : EntityTypeConfiguration<ZID_veiculomarca>
    {
        public ZID_veiculomarcaMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("ZID_veiculomarca");
            this.Property(t => t.ID).HasColumnName("ID");
        }
    }
}
