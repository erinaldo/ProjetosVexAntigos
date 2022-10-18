using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class LoginPerfilPermissaoMap : EntityTypeConfiguration<LoginPerfilPermissao>
    {
        public LoginPerfilPermissaoMap()
        {
            // Primary Key
            this.HasKey(t => t.IDLoginPerfilPermissao);

            // Properties
            this.Property(t => t.IDLoginPerfilPermissao)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Programa)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("LoginPerfilPermissao");
            this.Property(t => t.IDLoginPerfilPermissao).HasColumnName("IDLoginPerfilPermissao");
            this.Property(t => t.IDLoginPerfil).HasColumnName("IDLoginPerfil");
            this.Property(t => t.IDModulo).HasColumnName("IDModulo");
            this.Property(t => t.Programa).HasColumnName("Programa");

            // Relationships
            this.HasRequired(t => t.LoginPerfil)
                .WithMany(t => t.LoginPerfilPermissaos)
                .HasForeignKey(d => d.IDLoginPerfil);

        }
    }
}
