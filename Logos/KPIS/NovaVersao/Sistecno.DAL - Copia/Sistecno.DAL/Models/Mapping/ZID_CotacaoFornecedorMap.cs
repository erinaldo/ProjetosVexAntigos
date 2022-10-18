using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ZID_CotacaoFornecedorMap : EntityTypeConfiguration<ZID_CotacaoFornecedor>
    {
        public ZID_CotacaoFornecedorMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("ZID_CotacaoFornecedor");
            this.Property(t => t.ID).HasColumnName("ID");
        }
    }
}
