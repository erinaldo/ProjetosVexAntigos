using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ZID_movimentacaobancariaMap : EntityTypeConfiguration<ZID_movimentacaobancaria>
    {
        public ZID_movimentacaobancariaMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("ZID_movimentacaobancaria");
            this.Property(t => t.ID).HasColumnName("ID");
        }
    }
}
