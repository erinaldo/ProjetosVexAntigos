using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class RPCIImagemMap : EntityTypeConfiguration<RPCIImagem>
    {
        public RPCIImagemMap()
        {
            // Primary Key
            this.HasKey(t => t.IdRpciImagem);

            // Properties
            this.Property(t => t.IdRpciImagem)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Titulo)
                .IsRequired()
                .HasMaxLength(60);

            // Table & Column Mappings
            this.ToTable("RPCIImagem");
            this.Property(t => t.IdRpciImagem).HasColumnName("IdRpciImagem");
            this.Property(t => t.IdRpci).HasColumnName("IdRpci");
            this.Property(t => t.Titulo).HasColumnName("Titulo");
            this.Property(t => t.Imagem).HasColumnName("Imagem");

            // Relationships
            this.HasRequired(t => t.RPCI)
                .WithMany(t => t.RPCIImagems)
                .HasForeignKey(d => d.IdRpci);

        }
    }
}
