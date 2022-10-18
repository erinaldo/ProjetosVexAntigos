using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class BairroFaixaDeCepMap : EntityTypeConfiguration<BairroFaixaDeCep>
    {
        public BairroFaixaDeCepMap()
        {
            // Primary Key
            this.HasKey(t => t.IDBairroFaixaDeCep);

            // Properties
            this.Property(t => t.IDBairroFaixaDeCep)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Inicial)
                .IsFixedLength()
                .HasMaxLength(8);

            this.Property(t => t.Final)
                .IsFixedLength()
                .HasMaxLength(8);

            // Table & Column Mappings
            this.ToTable("BairroFaixaDeCep");
            this.Property(t => t.IDBairroFaixaDeCep).HasColumnName("IDBairroFaixaDeCep");
            this.Property(t => t.IDBairro).HasColumnName("IDBairro");
            this.Property(t => t.Inicial).HasColumnName("Inicial");
            this.Property(t => t.Final).HasColumnName("Final");

            // Relationships
            this.HasOptional(t => t.Bairro)
                .WithMany(t => t.BairroFaixaDeCeps)
                .HasForeignKey(d => d.IDBairro);

        }
    }
}
