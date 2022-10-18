using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class UsuarioFilialMap : EntityTypeConfiguration<UsuarioFilial>
    {
        public UsuarioFilialMap()
        {
            // Primary Key
            this.HasKey(t => t.idUsuarioFilial);

            // Properties
            this.Property(t => t.idUsuarioFilial)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Permissao)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(3);

            // Table & Column Mappings
            this.ToTable("UsuarioFilial");
            this.Property(t => t.idUsuarioFilial).HasColumnName("idUsuarioFilial");
            this.Property(t => t.IdFilial).HasColumnName("IdFilial");
            this.Property(t => t.Permissao).HasColumnName("Permissao");
            this.Property(t => t.idUsuario).HasColumnName("idUsuario");

            // Relationships
            this.HasRequired(t => t.Filial)
                .WithMany(t => t.UsuarioFilials)
                .HasForeignKey(d => d.IdFilial);
            this.HasOptional(t => t.Usuario)
                .WithMany(t => t.UsuarioFilials)
                .HasForeignKey(d => d.idUsuario);
            this.HasRequired(t => t.UsuarioFilial2)
                .WithOptional(t => t.UsuarioFilial1);

        }
    }
}
