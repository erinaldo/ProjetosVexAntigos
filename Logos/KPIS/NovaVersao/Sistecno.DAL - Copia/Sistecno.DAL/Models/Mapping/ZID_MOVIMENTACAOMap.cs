using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ZID_MOVIMENTACAOMap : EntityTypeConfiguration<ZID_MOVIMENTACAO>
    {
        public ZID_MOVIMENTACAOMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("ZID_MOVIMENTACAO");
            this.Property(t => t.ID).HasColumnName("ID");
        }
    }
}
