using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ZID_EDI_EdiMap : EntityTypeConfiguration<ZID_EDI_Edi>
    {
        public ZID_EDI_EdiMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("ZID_EDI_Edi");
            this.Property(t => t.ID).HasColumnName("ID");
        }
    }
}
