using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ZID_UsuarioClienteDivisaoMap : EntityTypeConfiguration<ZID_UsuarioClienteDivisao>
    {
        public ZID_UsuarioClienteDivisaoMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("ZID_UsuarioClienteDivisao");
            this.Property(t => t.ID).HasColumnName("ID");
        }
    }
}
