using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ZID_DOCUMENTOCUBAGEMMap : EntityTypeConfiguration<ZID_DOCUMENTOCUBAGEM>
    {
        public ZID_DOCUMENTOCUBAGEMMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("ZID_DOCUMENTOCUBAGEM");
            this.Property(t => t.ID).HasColumnName("ID");
        }
    }
}
