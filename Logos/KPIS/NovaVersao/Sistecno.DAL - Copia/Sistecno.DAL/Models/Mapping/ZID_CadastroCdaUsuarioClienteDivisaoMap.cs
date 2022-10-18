using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ZID_CadastroCdaUsuarioClienteDivisaoMap : EntityTypeConfiguration<ZID_CadastroCdaUsuarioClienteDivisao>
    {
        public ZID_CadastroCdaUsuarioClienteDivisaoMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("ZID_CadastroCdaUsuarioClienteDivisao");
            this.Property(t => t.ID).HasColumnName("ID");
        }
    }
}
