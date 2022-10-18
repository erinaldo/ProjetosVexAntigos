using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ZID_ContratoImagemMap : EntityTypeConfiguration<ZID_ContratoImagem>
    {
        public ZID_ContratoImagemMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("ZID_ContratoImagem");
            this.Property(t => t.ID).HasColumnName("ID");
        }
    }
}
