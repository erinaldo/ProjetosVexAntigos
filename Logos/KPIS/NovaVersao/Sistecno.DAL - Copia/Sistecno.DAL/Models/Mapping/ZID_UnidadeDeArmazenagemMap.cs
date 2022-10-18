using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ZID_UnidadeDeArmazenagemMap : EntityTypeConfiguration<ZID_UnidadeDeArmazenagem>
    {
        public ZID_UnidadeDeArmazenagemMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("ZID_UnidadeDeArmazenagem");
            this.Property(t => t.ID).HasColumnName("ID");
        }
    }
}
