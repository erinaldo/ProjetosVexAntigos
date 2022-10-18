using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ZID_DepositoPlantaLocalizacaoMap : EntityTypeConfiguration<ZID_DepositoPlantaLocalizacao>
    {
        public ZID_DepositoPlantaLocalizacaoMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("ZID_DepositoPlantaLocalizacao");
            this.Property(t => t.ID).HasColumnName("ID");
        }
    }
}
