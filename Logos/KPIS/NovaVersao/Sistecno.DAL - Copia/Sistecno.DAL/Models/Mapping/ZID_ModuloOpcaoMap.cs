using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ZID_ModuloOpcaoMap : EntityTypeConfiguration<ZID_ModuloOpcao>
    {
        public ZID_ModuloOpcaoMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("ZID_ModuloOpcao");
            this.Property(t => t.ID).HasColumnName("ID");
        }
    }
}
