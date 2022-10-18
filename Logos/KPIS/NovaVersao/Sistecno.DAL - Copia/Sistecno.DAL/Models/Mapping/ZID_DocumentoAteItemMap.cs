using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ZID_DocumentoAteItemMap : EntityTypeConfiguration<ZID_DocumentoAteItem>
    {
        public ZID_DocumentoAteItemMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("ZID_DocumentoAteItem");
            this.Property(t => t.ID).HasColumnName("ID");
        }
    }
}
