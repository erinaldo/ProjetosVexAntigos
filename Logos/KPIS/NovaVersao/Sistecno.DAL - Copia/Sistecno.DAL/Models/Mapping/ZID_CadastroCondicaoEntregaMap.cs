using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ZID_CadastroCondicaoEntregaMap : EntityTypeConfiguration<ZID_CadastroCondicaoEntrega>
    {
        public ZID_CadastroCondicaoEntregaMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("ZID_CadastroCondicaoEntrega");
            this.Property(t => t.ID).HasColumnName("ID");
        }
    }
}
