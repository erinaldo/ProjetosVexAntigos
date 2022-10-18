using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ZID_DtFaturamentoClienteMap : EntityTypeConfiguration<ZID_DtFaturamentoCliente>
    {
        public ZID_DtFaturamentoClienteMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("ZID_DtFaturamentoCliente");
            this.Property(t => t.ID).HasColumnName("ID");
        }
    }
}
