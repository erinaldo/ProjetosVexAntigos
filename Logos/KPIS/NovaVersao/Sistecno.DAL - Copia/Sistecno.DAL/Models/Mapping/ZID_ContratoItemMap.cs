using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ZID_ContratoItemMap : EntityTypeConfiguration<ZID_ContratoItem>
    {
        public ZID_ContratoItemMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("ZID_ContratoItem");
            this.Property(t => t.ID).HasColumnName("ID");
        }
    }
}
