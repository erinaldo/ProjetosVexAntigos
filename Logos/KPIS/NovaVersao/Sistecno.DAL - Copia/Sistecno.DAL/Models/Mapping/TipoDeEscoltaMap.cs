using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class TipoDeEscoltaMap : EntityTypeConfiguration<TipoDeEscolta>
    {
        public TipoDeEscoltaMap()
        {
            // Primary Key
            this.HasKey(t => t.IdTipoDeEscolta);

            // Properties
            this.Property(t => t.IdTipoDeEscolta)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Nome)
                .IsRequired()
                .HasMaxLength(60);

            // Table & Column Mappings
            this.ToTable("TipoDeEscolta");
            this.Property(t => t.IdTipoDeEscolta).HasColumnName("IdTipoDeEscolta");
            this.Property(t => t.Nome).HasColumnName("Nome");
        }
    }
}
