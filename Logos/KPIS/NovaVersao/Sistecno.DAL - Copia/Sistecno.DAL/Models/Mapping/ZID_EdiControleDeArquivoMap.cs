using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ZID_EdiControleDeArquivoMap : EntityTypeConfiguration<ZID_EdiControleDeArquivo>
    {
        public ZID_EdiControleDeArquivoMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("ZID_EdiControleDeArquivo");
            this.Property(t => t.ID).HasColumnName("ID");
        }
    }
}
