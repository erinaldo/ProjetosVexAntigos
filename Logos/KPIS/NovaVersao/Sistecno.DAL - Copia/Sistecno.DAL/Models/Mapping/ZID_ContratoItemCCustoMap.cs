using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ZID_ContratoItemCCustoMap : EntityTypeConfiguration<ZID_ContratoItemCCusto>
    {
        public ZID_ContratoItemCCustoMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("ZID_ContratoItemCCusto");
            this.Property(t => t.ID).HasColumnName("ID");
        }
    }
}
