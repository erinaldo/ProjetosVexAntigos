using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class UsuarioGradeMap : EntityTypeConfiguration<UsuarioGrade>
    {
        public UsuarioGradeMap()
        {
            // Primary Key
            this.HasKey(t => t.IDUsuarioGrade);

            // Properties
            this.Property(t => t.IDUsuarioGrade)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Formulario)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.Grade)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.Descricao)
                .IsRequired()
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("UsuarioGrade");
            this.Property(t => t.IDUsuarioGrade).HasColumnName("IDUsuarioGrade");
            this.Property(t => t.IDUsuario).HasColumnName("IDUsuario");
            this.Property(t => t.Formulario).HasColumnName("Formulario");
            this.Property(t => t.Grade).HasColumnName("Grade");
            this.Property(t => t.Descricao).HasColumnName("Descricao");

            // Relationships
            this.HasRequired(t => t.Usuario)
                .WithMany(t => t.UsuarioGrades)
                .HasForeignKey(d => d.IDUsuario);

        }
    }
}
