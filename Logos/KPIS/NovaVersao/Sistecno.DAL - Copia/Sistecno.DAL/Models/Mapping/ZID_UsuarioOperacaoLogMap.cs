using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ZID_UsuarioOperacaoLogMap : EntityTypeConfiguration<ZID_UsuarioOperacaoLog>
    {
        public ZID_UsuarioOperacaoLogMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("ZID_UsuarioOperacaoLog");
            this.Property(t => t.ID).HasColumnName("ID");
        }
    }
}
