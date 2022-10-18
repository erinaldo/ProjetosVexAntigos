using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ZID_CadastroEntregaMap : EntityTypeConfiguration<ZID_CadastroEntrega>
    {
        public ZID_CadastroEntregaMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("ZID_CadastroEntrega");
            this.Property(t => t.ID).HasColumnName("ID");
        }
    }
}
