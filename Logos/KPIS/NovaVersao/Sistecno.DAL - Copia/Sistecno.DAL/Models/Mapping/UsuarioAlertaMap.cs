using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class UsuarioAlertaMap : EntityTypeConfiguration<UsuarioAlerta>
    {
        public UsuarioAlertaMap()
        {
            // Primary Key
            this.HasKey(t => t.IdUsuarioAlerta);

            // Properties
            this.Property(t => t.IdUsuarioAlerta)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Ativo)
                .IsFixedLength()
                .HasMaxLength(3);

            // Table & Column Mappings
            this.ToTable("UsuarioAlerta");
            this.Property(t => t.IdUsuarioAlerta).HasColumnName("IdUsuarioAlerta");
            this.Property(t => t.IdUsuario).HasColumnName("IdUsuario");
            this.Property(t => t.IdAlerta).HasColumnName("IdAlerta");
            this.Property(t => t.Cadastro).HasColumnName("Cadastro");
            this.Property(t => t.Ativo).HasColumnName("Ativo");

            // Relationships
            this.HasRequired(t => t.Alerta)
                .WithMany(t => t.UsuarioAlertas)
                .HasForeignKey(d => d.IdAlerta);
            this.HasRequired(t => t.Usuario)
                .WithMany(t => t.UsuarioAlertas)
                .HasForeignKey(d => d.IdUsuario);

        }
    }
}
