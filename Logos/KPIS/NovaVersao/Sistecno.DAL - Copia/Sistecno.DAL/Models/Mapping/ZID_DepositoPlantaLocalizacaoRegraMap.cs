using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ZID_DepositoPlantaLocalizacaoRegraMap : EntityTypeConfiguration<ZID_DepositoPlantaLocalizacaoRegra>
    {
        public ZID_DepositoPlantaLocalizacaoRegraMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("ZID_DepositoPlantaLocalizacaoRegra");
            this.Property(t => t.ID).HasColumnName("ID");
        }
    }
}
