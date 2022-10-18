using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ZID_DOCUMENTOAGUARDANDOCTRCMap : EntityTypeConfiguration<ZID_DOCUMENTOAGUARDANDOCTRC>
    {
        public ZID_DOCUMENTOAGUARDANDOCTRCMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("ZID_DOCUMENTOAGUARDANDOCTRC");
            this.Property(t => t.ID).HasColumnName("ID");
        }
    }
}