using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class UsuarioGradeCampoMap : EntityTypeConfiguration<UsuarioGradeCampo>
    {
        public UsuarioGradeCampoMap()
        {
            // Primary Key
            this.HasKey(t => t.IDUsuarioGradeCampo);

            // Properties
            this.Property(t => t.IDUsuarioGradeCampo)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Campo)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Visivel)
                .IsFixedLength()
                .HasMaxLength(3);

            // Table & Column Mappings
            this.ToTable("UsuarioGradeCampo");
            this.Property(t => t.IDUsuarioGradeCampo).HasColumnName("IDUsuarioGradeCampo");
            this.Property(t => t.IDUsuarioGrade).HasColumnName("IDUsuarioGrade");
            this.Property(t => t.Campo).HasColumnName("Campo");
            this.Property(t => t.Posicao).HasColumnName("Posicao");
            this.Property(t => t.Visivel).HasColumnName("Visivel");
            this.Property(t => t.Largura).HasColumnName("Largura");

            // Relationships
            this.HasRequired(t => t.UsuarioGrade)
                .WithMany(t => t.UsuarioGradeCampoes)
                .HasForeignKey(d => d.IDUsuarioGrade);

        }
    }
}
