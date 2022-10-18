using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ZID_RequisicaoDeMaterialItemMap : EntityTypeConfiguration<ZID_RequisicaoDeMaterialItem>
    {
        public ZID_RequisicaoDeMaterialItemMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("ZID_RequisicaoDeMaterialItem");
            this.Property(t => t.ID).HasColumnName("ID");
        }
    }
}
