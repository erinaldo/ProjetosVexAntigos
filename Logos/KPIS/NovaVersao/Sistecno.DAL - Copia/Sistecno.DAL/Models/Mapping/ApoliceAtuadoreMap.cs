using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ApoliceAtuadoreMap : EntityTypeConfiguration<ApoliceAtuadore>
    {
        public ApoliceAtuadoreMap()
        {
            // Primary Key
            this.HasKey(t => t.IdApoliceAtuadores);

            // Properties
            this.Property(t => t.IdApoliceAtuadores)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Nome)
                .IsRequired()
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("ApoliceAtuadores");
            this.Property(t => t.IdApoliceAtuadores).HasColumnName("IdApoliceAtuadores");
            this.Property(t => t.Nome).HasColumnName("Nome");
        }
    }
}
