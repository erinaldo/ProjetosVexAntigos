using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class AvisoDocumentoMap : EntityTypeConfiguration<AvisoDocumento>
    {
        public AvisoDocumentoMap()
        {
            // Primary Key
            this.HasKey(t => t.IdAvisoDocumento);

            // Properties
            this.Property(t => t.IdAvisoDocumento)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("AvisoDocumento");
            this.Property(t => t.IdAvisoDocumento).HasColumnName("IdAvisoDocumento");
            this.Property(t => t.IdDocumento).HasColumnName("IdDocumento");
            this.Property(t => t.IdUsuario).HasColumnName("IdUsuario");
        }
    }
}
