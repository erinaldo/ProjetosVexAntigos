using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class LoginPerfilMap : EntityTypeConfiguration<LoginPerfil>
    {
        public LoginPerfilMap()
        {
            // Primary Key
            this.HasKey(t => t.IDLoginPerfil);

            // Properties
            this.Property(t => t.IDLoginPerfil)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Descricao)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.CriaUsuario)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(3);

            this.Property(t => t.Administrador)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(3);

            // Table & Column Mappings
            this.ToTable("LoginPerfil");
            this.Property(t => t.IDLoginPerfil).HasColumnName("IDLoginPerfil");
            this.Property(t => t.IDGrupo).HasColumnName("IDGrupo");
            this.Property(t => t.Descricao).HasColumnName("Descricao");
            this.Property(t => t.CriaUsuario).HasColumnName("CriaUsuario");
            this.Property(t => t.Administrador).HasColumnName("Administrador");

            // Relationships
            this.HasRequired(t => t.Grupo)
                .WithMany(t => t.LoginPerfils)
                .HasForeignKey(d => d.IDGrupo);

        }
    }
}
