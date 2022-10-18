using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ZID_EDI_DocumentoItemMap : EntityTypeConfiguration<ZID_EDI_DocumentoItem>
    {
        public ZID_EDI_DocumentoItemMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("ZID_EDI_DocumentoItem");
            this.Property(t => t.ID).HasColumnName("ID");
        }
    }
}
