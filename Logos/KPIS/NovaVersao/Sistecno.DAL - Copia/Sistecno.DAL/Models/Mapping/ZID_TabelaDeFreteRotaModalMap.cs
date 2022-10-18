using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ZID_TabelaDeFreteRotaModalMap : EntityTypeConfiguration<ZID_TabelaDeFreteRotaModal>
    {
        public ZID_TabelaDeFreteRotaModalMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("ZID_TabelaDeFreteRotaModal");
            this.Property(t => t.ID).HasColumnName("ID");
        }
    }
}
