using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ZID_RomaneioOcorrenciaMap : EntityTypeConfiguration<ZID_RomaneioOcorrencia>
    {
        public ZID_RomaneioOcorrenciaMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("ZID_RomaneioOcorrencia");
            this.Property(t => t.ID).HasColumnName("ID");
        }
    }
}
