using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ZID_MotoristaHistoricoMap : EntityTypeConfiguration<ZID_MotoristaHistorico>
    {
        public ZID_MotoristaHistoricoMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("ZID_MotoristaHistorico");
            this.Property(t => t.ID).HasColumnName("ID");
        }
    }
}
