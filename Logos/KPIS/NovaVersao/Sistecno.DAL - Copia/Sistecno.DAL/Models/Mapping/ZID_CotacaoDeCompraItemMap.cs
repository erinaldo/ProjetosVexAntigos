using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ZID_CotacaoDeCompraItemMap : EntityTypeConfiguration<ZID_CotacaoDeCompraItem>
    {
        public ZID_CotacaoDeCompraItemMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("ZID_CotacaoDeCompraItem");
            this.Property(t => t.ID).HasColumnName("ID");
        }
    }
}
