using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ZID_TituloDocumentoMap : EntityTypeConfiguration<ZID_TituloDocumento>
    {
        public ZID_TituloDocumentoMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("ZID_TituloDocumento");
            this.Property(t => t.ID).HasColumnName("ID");
        }
    }
}
