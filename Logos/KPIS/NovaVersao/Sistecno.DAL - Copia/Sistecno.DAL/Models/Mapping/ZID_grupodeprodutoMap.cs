using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ZID_grupodeprodutoMap : EntityTypeConfiguration<ZID_grupodeproduto>
    {
        public ZID_grupodeprodutoMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("ZID_grupodeproduto");
            this.Property(t => t.ID).HasColumnName("ID");
        }
    }
}
