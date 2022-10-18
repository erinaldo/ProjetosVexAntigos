using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ZID_TituloDuplicataHistoricoMap : EntityTypeConfiguration<ZID_TituloDuplicataHistorico>
    {
        public ZID_TituloDuplicataHistoricoMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("ZID_TituloDuplicataHistorico");
            this.Property(t => t.ID).HasColumnName("ID");
        }
    }
}
