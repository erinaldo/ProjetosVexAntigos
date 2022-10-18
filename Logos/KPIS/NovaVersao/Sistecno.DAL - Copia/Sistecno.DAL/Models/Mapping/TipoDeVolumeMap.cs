using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class TipoDeVolumeMap : EntityTypeConfiguration<TipoDeVolume>
    {
        public TipoDeVolumeMap()
        {
            // Primary Key
            this.HasKey(t => t.IdTipoDeVolume);

            // Properties
            this.Property(t => t.IdTipoDeVolume)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Descricao)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("TipoDeVolume");
            this.Property(t => t.IdTipoDeVolume).HasColumnName("IdTipoDeVolume");
            this.Property(t => t.Descricao).HasColumnName("Descricao");
        }
    }
}
