using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ZID_EDI_ProdutoMap : EntityTypeConfiguration<ZID_EDI_Produto>
    {
        public ZID_EDI_ProdutoMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("ZID_EDI_Produto");
            this.Property(t => t.ID).HasColumnName("ID");
        }
    }
}
