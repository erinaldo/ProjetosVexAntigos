using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ZID_DepositoPlantaMap : EntityTypeConfiguration<ZID_DepositoPlanta>
    {
        public ZID_DepositoPlantaMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("ZID_DepositoPlanta");
            this.Property(t => t.ID).HasColumnName("ID");
        }
    }
}
