using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ZID_TabelaDeFreteRotaModalValorMap : EntityTypeConfiguration<ZID_TabelaDeFreteRotaModalValor>
    {
        public ZID_TabelaDeFreteRotaModalValorMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("ZID_TabelaDeFreteRotaModalValor");
            this.Property(t => t.ID).HasColumnName("ID");
        }
    }
}
