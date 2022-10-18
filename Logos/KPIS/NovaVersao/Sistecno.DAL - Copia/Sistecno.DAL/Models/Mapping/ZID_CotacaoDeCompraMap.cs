using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ZID_CotacaoDeCompraMap : EntityTypeConfiguration<ZID_CotacaoDeCompra>
    {
        public ZID_CotacaoDeCompraMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("ZID_CotacaoDeCompra");
            this.Property(t => t.ID).HasColumnName("ID");
        }
    }
}
