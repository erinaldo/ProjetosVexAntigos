using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class FrotaItemControleMap : EntityTypeConfiguration<FrotaItemControle>
    {
        public FrotaItemControleMap()
        {
            // Primary Key
            this.HasKey(t => t.IDFrotaItemControle);

            // Properties
            this.Property(t => t.IDFrotaItemControle)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Nome)
                .IsRequired()
                .HasMaxLength(60);

            this.Property(t => t.TipoDeControle)
                .IsRequired()
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("FrotaItemControle");
            this.Property(t => t.IDFrotaItemControle).HasColumnName("IDFrotaItemControle");
            this.Property(t => t.Nome).HasColumnName("Nome");
            this.Property(t => t.TipoDeControle).HasColumnName("TipoDeControle");
        }
    }
}
