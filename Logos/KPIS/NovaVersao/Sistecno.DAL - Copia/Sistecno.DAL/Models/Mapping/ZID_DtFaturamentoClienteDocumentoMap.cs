using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ZID_DtFaturamentoClienteDocumentoMap : EntityTypeConfiguration<ZID_DtFaturamentoClienteDocumento>
    {
        public ZID_DtFaturamentoClienteDocumentoMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("ZID_DtFaturamentoClienteDocumento");
            this.Property(t => t.ID).HasColumnName("ID");
        }
    }
}
