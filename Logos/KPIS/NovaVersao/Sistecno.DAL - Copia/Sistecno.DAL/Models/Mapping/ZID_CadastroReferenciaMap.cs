using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ZID_CadastroReferenciaMap : EntityTypeConfiguration<ZID_CadastroReferencia>
    {
        public ZID_CadastroReferenciaMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("ZID_CadastroReferencia");
            this.Property(t => t.ID).HasColumnName("ID");
        }
    }
}
