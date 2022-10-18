using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ApoliceItemMap : EntityTypeConfiguration<ApoliceItem>
    {
        public ApoliceItemMap()
        {
            // Primary Key
            this.HasKey(t => t.IdApoliceItem);

            // Properties
            this.Property(t => t.IdApoliceItem)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.ExigeLiberacao)
                .HasMaxLength(3);

            // Table & Column Mappings
            this.ToTable("ApoliceItem");
            this.Property(t => t.IdApoliceItem).HasColumnName("IdApoliceItem");
            this.Property(t => t.IdApolice).HasColumnName("IdApolice");
            this.Property(t => t.De).HasColumnName("De");
            this.Property(t => t.Ate).HasColumnName("Ate");
            this.Property(t => t.Produto).HasColumnName("Produto");
            this.Property(t => t.Exigencias).HasColumnName("Exigencias");
            this.Property(t => t.ExigeLiberacao).HasColumnName("ExigeLiberacao");
        }
    }
}
