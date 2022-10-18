using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ZID_DOCUMENTOITEMMap : EntityTypeConfiguration<ZID_DOCUMENTOITEM>
    {
        public ZID_DOCUMENTOITEMMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("ZID_DOCUMENTOITEM");
            this.Property(t => t.ID).HasColumnName("ID");
        }
    }
}