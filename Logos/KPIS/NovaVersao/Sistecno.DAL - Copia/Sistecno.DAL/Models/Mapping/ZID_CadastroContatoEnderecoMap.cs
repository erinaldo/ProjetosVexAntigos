using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ZID_CadastroContatoEnderecoMap : EntityTypeConfiguration<ZID_CadastroContatoEndereco>
    {
        public ZID_CadastroContatoEnderecoMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("ZID_CadastroContatoEndereco");
            this.Property(t => t.ID).HasColumnName("ID");
        }
    }
}
