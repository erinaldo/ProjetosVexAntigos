using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ZID_DOCUMENTOMap : EntityTypeConfiguration<ZID_DOCUMENTO>
    {
        public ZID_DOCUMENTOMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("ZID_DOCUMENTO");
            this.Property(t => t.ID).HasColumnName("ID");
        }
    }
}
