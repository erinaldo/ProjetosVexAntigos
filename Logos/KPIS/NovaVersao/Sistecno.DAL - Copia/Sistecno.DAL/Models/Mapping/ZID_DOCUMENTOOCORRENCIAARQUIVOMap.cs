using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ZID_DOCUMENTOOCORRENCIAARQUIVOMap : EntityTypeConfiguration<ZID_DOCUMENTOOCORRENCIAARQUIVO>
    {
        public ZID_DOCUMENTOOCORRENCIAARQUIVOMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("ZID_DOCUMENTOOCORRENCIAARQUIVO");
            this.Property(t => t.ID).HasColumnName("ID");
        }
    }
}
