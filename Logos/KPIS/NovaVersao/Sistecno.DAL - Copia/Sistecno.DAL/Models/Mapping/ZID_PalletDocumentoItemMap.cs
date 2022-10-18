using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ZID_PalletDocumentoItemMap : EntityTypeConfiguration<ZID_PalletDocumentoItem>
    {
        public ZID_PalletDocumentoItemMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("ZID_PalletDocumentoItem");
            this.Property(t => t.ID).HasColumnName("ID");
        }
    }
}
