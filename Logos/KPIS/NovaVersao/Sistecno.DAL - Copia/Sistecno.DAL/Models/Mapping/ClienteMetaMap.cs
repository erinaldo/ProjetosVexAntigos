using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ClienteMetaMap : EntityTypeConfiguration<ClienteMeta>
    {
        public ClienteMetaMap()
        {
            // Primary Key
            this.HasKey(t => t.IdClienteMeta);

            // Properties
            this.Property(t => t.IdClienteMeta)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Cor)
                .IsRequired()
                .HasMaxLength(20);

            // Table & Column Mappings
            this.ToTable("ClienteMeta");
            this.Property(t => t.IdClienteMeta).HasColumnName("IdClienteMeta");
            this.Property(t => t.IdCliente).HasColumnName("IdCliente");
            this.Property(t => t.Prazo).HasColumnName("Prazo");
            this.Property(t => t.Cor).HasColumnName("Cor");
            this.Property(t => t.Percentual).HasColumnName("Percentual");

            // Relationships
            this.HasOptional(t => t.Cliente)
                .WithMany(t => t.ClienteMetas)
                .HasForeignKey(d => d.IdCliente);

        }
    }
}
