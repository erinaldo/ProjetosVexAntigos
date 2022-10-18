using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ZID_PRODUTOCLIENTEMap : EntityTypeConfiguration<ZID_PRODUTOCLIENTE>
    {
        public ZID_PRODUTOCLIENTEMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("ZID_PRODUTOCLIENTE");
            this.Property(t => t.ID).HasColumnName("ID");
        }
    }
}
