using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class DDAImagemMap : EntityTypeConfiguration<DDAImagem>
    {
        public DDAImagemMap()
        {
            // Primary Key
            this.HasKey(t => t.IdDDAImagem);

            // Properties
            this.Property(t => t.IdDDAImagem)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("DDAImagem");
            this.Property(t => t.IdDDAImagem).HasColumnName("IdDDAImagem");
            this.Property(t => t.IdDDA).HasColumnName("IdDDA");
            this.Property(t => t.Imagem).HasColumnName("Imagem");

            // Relationships
            this.HasRequired(t => t.DDA)
                .WithMany(t => t.DDAImagems)
                .HasForeignKey(d => d.IdDDA);

        }
    }
}
