using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ZID_EdiTrocaDeArquivoMap : EntityTypeConfiguration<ZID_EdiTrocaDeArquivo>
    {
        public ZID_EdiTrocaDeArquivoMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("ZID_EdiTrocaDeArquivo");
            this.Property(t => t.ID).HasColumnName("ID");
        }
    }
}
