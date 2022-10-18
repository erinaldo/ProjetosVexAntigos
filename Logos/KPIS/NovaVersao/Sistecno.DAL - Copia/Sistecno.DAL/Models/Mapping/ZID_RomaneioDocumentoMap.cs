using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ZID_RomaneioDocumentoMap : EntityTypeConfiguration<ZID_RomaneioDocumento>
    {
        public ZID_RomaneioDocumentoMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("ZID_RomaneioDocumento");
            this.Property(t => t.ID).HasColumnName("ID");
        }
    }
}