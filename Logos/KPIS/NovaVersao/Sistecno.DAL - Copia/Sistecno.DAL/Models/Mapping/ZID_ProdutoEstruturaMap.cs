using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ZID_ProdutoEstruturaMap : EntityTypeConfiguration<ZID_ProdutoEstrutura>
    {
        public ZID_ProdutoEstruturaMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("ZID_ProdutoEstrutura");
            this.Property(t => t.ID).HasColumnName("ID");
        }
    }
}
