using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ZID_EDI_ProdutoClienteMap : EntityTypeConfiguration<ZID_EDI_ProdutoCliente>
    {
        public ZID_EDI_ProdutoClienteMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("ZID_EDI_ProdutoCliente");
            this.Property(t => t.ID).HasColumnName("ID");
        }
    }
}
