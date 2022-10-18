using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class RoteirizacaoMap : EntityTypeConfiguration<Roteirizacao>
    {
        public RoteirizacaoMap()
        {
            // Primary Key
            this.HasKey(t => t.IDRoteirizacao);

            // Properties
            this.Property(t => t.IDRoteirizacao)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("Roteirizacao");
            this.Property(t => t.IDRoteirizacao).HasColumnName("IDRoteirizacao");
            this.Property(t => t.IDFilial).HasColumnName("IDFilial");
            this.Property(t => t.IDRoteirizacaoTipo).HasColumnName("IDRoteirizacaoTipo");
            this.Property(t => t.IDParent).HasColumnName("IDParent");

            // Relationships
            this.HasRequired(t => t.Filial)
                .WithMany(t => t.Roteirizacaos)
                .HasForeignKey(d => d.IDFilial);
            this.HasRequired(t => t.Roteirizacao2)
                .WithMany(t => t.Roteirizacao1)
                .HasForeignKey(d => d.IDParent);
            this.HasRequired(t => t.Roteirizacao3)
                .WithMany(t => t.Roteirizacao11)
                .HasForeignKey(d => d.IDParent);

        }
    }
}
