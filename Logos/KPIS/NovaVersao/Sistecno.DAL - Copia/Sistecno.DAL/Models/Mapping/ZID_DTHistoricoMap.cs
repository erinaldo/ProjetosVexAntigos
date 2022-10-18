using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ZID_DTHistoricoMap : EntityTypeConfiguration<ZID_DTHistorico>
    {
        public ZID_DTHistoricoMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("ZID_DTHistorico");
            this.Property(t => t.ID).HasColumnName("ID");
        }
    }
}
