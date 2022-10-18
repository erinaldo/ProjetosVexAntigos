using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ZID_ocorrenciaMap : EntityTypeConfiguration<ZID_ocorrencia>
    {
        public ZID_ocorrenciaMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("ZID_ocorrencia");
            this.Property(t => t.ID).HasColumnName("ID");
        }
    }
}
