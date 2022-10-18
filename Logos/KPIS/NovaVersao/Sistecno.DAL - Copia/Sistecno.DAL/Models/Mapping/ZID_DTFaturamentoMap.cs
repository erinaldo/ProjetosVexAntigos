using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ZID_DTFaturamentoMap : EntityTypeConfiguration<ZID_DTFaturamento>
    {
        public ZID_DTFaturamentoMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("ZID_DTFaturamento");
            this.Property(t => t.ID).HasColumnName("ID");
        }
    }
}
