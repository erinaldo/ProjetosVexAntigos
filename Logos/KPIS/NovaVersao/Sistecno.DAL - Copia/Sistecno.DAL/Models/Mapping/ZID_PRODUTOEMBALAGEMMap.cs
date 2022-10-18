using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ZID_PRODUTOEMBALAGEMMap : EntityTypeConfiguration<ZID_PRODUTOEMBALAGEM>
    {
        public ZID_PRODUTOEMBALAGEMMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("ZID_PRODUTOEMBALAGEM");
            this.Property(t => t.ID).HasColumnName("ID");
        }
    }
}
