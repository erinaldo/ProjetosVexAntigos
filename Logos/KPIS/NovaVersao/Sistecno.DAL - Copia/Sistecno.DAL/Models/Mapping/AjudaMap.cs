using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class AjudaMap : EntityTypeConfiguration<Ajuda>
    {
        public AjudaMap()
        {
            // Primary Key
            this.HasKey(t => new { t.IDModuloOpcao, t.Campo });

            // Properties
            this.Property(t => t.IDModuloOpcao)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Campo)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.AjudaUsuario)
                .HasMaxLength(500);

            // Table & Column Mappings
            this.ToTable("Ajuda");
            this.Property(t => t.IDModuloOpcao).HasColumnName("IDModuloOpcao");
            this.Property(t => t.Campo).HasColumnName("Campo");
            this.Property(t => t.Ajuda1).HasColumnName("Ajuda");
            this.Property(t => t.AjudaUsuario).HasColumnName("AjudaUsuario");

            // Relationships
            this.HasRequired(t => t.ModuloOpcao)
                .WithMany(t => t.Ajudas)
                .HasForeignKey(d => d.IDModuloOpcao);

        }
    }
}
