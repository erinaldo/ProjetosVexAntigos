using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ZID_TituloImagemMap : EntityTypeConfiguration<ZID_TituloImagem>
    {
        public ZID_TituloImagemMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("ZID_TituloImagem");
            this.Property(t => t.ID).HasColumnName("ID");
        }
    }
}
