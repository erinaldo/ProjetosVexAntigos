using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ZID_CadastroHistoricoMap : EntityTypeConfiguration<ZID_CadastroHistorico>
    {
        public ZID_CadastroHistoricoMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("ZID_CadastroHistorico");
            this.Property(t => t.ID).HasColumnName("ID");
        }
    }
}
