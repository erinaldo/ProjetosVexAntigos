using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ZID_RequisicaodeMaterialDocumentoMap : EntityTypeConfiguration<ZID_RequisicaodeMaterialDocumento>
    {
        public ZID_RequisicaodeMaterialDocumentoMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("ZID_RequisicaodeMaterialDocumento");
            this.Property(t => t.ID).HasColumnName("ID");
        }
    }
}
