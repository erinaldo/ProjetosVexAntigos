using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class RespostaMap : EntityTypeConfiguration<Resposta>
    {
        public RespostaMap()
        {
            // Primary Key
            this.HasKey(t => new { t.IDUsuario, t.Formulario, t.Componente, t.Valor });

            // Properties
            this.Property(t => t.IDUsuario)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Formulario)
                .IsRequired()
                .HasMaxLength(80);

            this.Property(t => t.Componente)
                .IsRequired()
                .HasMaxLength(80);

            this.Property(t => t.Propriedade)
                .HasMaxLength(50);

            this.Property(t => t.Valor)
                .IsRequired()
                .HasMaxLength(150);

            // Table & Column Mappings
            this.ToTable("Respostas");
            this.Property(t => t.IDUsuario).HasColumnName("IDUsuario");
            this.Property(t => t.Formulario).HasColumnName("Formulario");
            this.Property(t => t.Componente).HasColumnName("Componente");
            this.Property(t => t.Propriedade).HasColumnName("Propriedade");
            this.Property(t => t.Linha).HasColumnName("Linha");
            this.Property(t => t.Valor).HasColumnName("Valor");

            // Relationships
            this.HasRequired(t => t.Usuario)
                .WithMany(t => t.Respostas)
                .HasForeignKey(d => d.IDUsuario);

        }
    }
}
