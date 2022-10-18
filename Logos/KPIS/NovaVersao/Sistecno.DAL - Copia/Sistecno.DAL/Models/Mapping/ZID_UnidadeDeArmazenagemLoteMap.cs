using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ZID_UnidadeDeArmazenagemLoteMap : EntityTypeConfiguration<ZID_UnidadeDeArmazenagemLote>
    {
        public ZID_UnidadeDeArmazenagemLoteMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("ZID_UnidadeDeArmazenagemLote");
            this.Property(t => t.ID).HasColumnName("ID");
        }
    }
}
