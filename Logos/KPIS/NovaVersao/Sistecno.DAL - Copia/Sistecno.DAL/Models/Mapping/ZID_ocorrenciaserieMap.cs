using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ZID_ocorrenciaserieMap : EntityTypeConfiguration<ZID_ocorrenciaserie>
    {
        public ZID_ocorrenciaserieMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("ZID_ocorrenciaserie");
            this.Property(t => t.ID).HasColumnName("ID");
        }
    }
}
