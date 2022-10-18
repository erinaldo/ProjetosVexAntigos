using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ZID_CadastroImagemMap : EntityTypeConfiguration<ZID_CadastroImagem>
    {
        public ZID_CadastroImagemMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("ZID_CadastroImagem");
            this.Property(t => t.ID).HasColumnName("ID");
        }
    }
}
