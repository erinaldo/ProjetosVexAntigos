using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ZID_TabelaDeFreteProdutoMap : EntityTypeConfiguration<ZID_TabelaDeFreteProduto>
    {
        public ZID_TabelaDeFreteProdutoMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("ZID_TabelaDeFreteProduto");
            this.Property(t => t.ID).HasColumnName("ID");
        }
    }
}
