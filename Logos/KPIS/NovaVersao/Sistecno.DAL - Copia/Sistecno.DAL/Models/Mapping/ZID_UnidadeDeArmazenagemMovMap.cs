using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ZID_UnidadeDeArmazenagemMovMap : EntityTypeConfiguration<ZID_UnidadeDeArmazenagemMov>
    {
        public ZID_UnidadeDeArmazenagemMovMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("ZID_UnidadeDeArmazenagemMov");
            this.Property(t => t.ID).HasColumnName("ID");
        }
    }
}
