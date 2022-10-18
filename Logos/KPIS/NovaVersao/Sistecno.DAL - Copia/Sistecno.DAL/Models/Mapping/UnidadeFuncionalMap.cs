using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class UnidadeFuncionalMap : EntityTypeConfiguration<UnidadeFuncional>
    {
        public UnidadeFuncionalMap()
        {
            // Primary Key
            this.HasKey(t => t.IdUnidadeFuncional);

            // Properties
            this.Property(t => t.IdUnidadeFuncional)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.UnidadeFuncional1)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("UnidadeFuncional");
            this.Property(t => t.IdUnidadeFuncional).HasColumnName("IdUnidadeFuncional");
            this.Property(t => t.UnidadeFuncional1).HasColumnName("UnidadeFuncional");
            this.Property(t => t.IdParente).HasColumnName("IdParente");
        }
    }
}
