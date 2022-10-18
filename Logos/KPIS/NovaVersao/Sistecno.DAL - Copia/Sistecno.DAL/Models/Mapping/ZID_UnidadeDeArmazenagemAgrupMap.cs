using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ZID_UnidadeDeArmazenagemAgrupMap : EntityTypeConfiguration<ZID_UnidadeDeArmazenagemAgrup>
    {
        public ZID_UnidadeDeArmazenagemAgrupMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("ZID_UnidadeDeArmazenagemAgrup");
            this.Property(t => t.ID).HasColumnName("ID");
        }
    }
}
