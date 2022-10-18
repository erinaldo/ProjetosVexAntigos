using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class TabelaDeFreteVigenciaMap : EntityTypeConfiguration<TabelaDeFreteVigencia>
    {
        public TabelaDeFreteVigenciaMap()
        {
            // Primary Key
            this.HasKey(t => t.IDTabelaDeFreteVigencia);

            // Properties
            this.Property(t => t.IDTabelaDeFreteVigencia)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("TabelaDeFreteVigencia");
            this.Property(t => t.IDTabelaDeFreteVigencia).HasColumnName("IDTabelaDeFreteVigencia");
            this.Property(t => t.IDTabelaDeFrete).HasColumnName("IDTabelaDeFrete");
            this.Property(t => t.Data).HasColumnName("Data");

            // Relationships
            this.HasRequired(t => t.TabelaDeFrete)
                .WithMany(t => t.TabelaDeFreteVigencias)
                .HasForeignKey(d => d.IDTabelaDeFrete);

        }
    }
}
