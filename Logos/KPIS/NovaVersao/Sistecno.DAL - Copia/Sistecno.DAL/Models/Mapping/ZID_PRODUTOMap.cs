using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ZID_PRODUTOMap : EntityTypeConfiguration<ZID_PRODUTO>
    {
        public ZID_PRODUTOMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("ZID_PRODUTO");
            this.Property(t => t.ID).HasColumnName("ID");
        }
    }
}
