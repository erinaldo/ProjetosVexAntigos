using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class UsuarioMap : EntityTypeConfiguration<Usuario>
    {
        public UsuarioMap()
        {
            // Primary Key
            this.HasKey(t => t.IDUsuario);

            // Properties
            this.Property(t => t.IDUsuario)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Nome)
                .HasMaxLength(50);

            this.Property(t => t.Login)
                .HasMaxLength(100);

            this.Property(t => t.Senha)
                .HasMaxLength(20);

            this.Property(t => t.CriaUsuario)
                .IsFixedLength()
                .HasMaxLength(3);

            this.Property(t => t.Administrador)
                .IsFixedLength()
                .HasMaxLength(3);

            this.Property(t => t.AutoOcultarMenu)
                .IsFixedLength()
                .HasMaxLength(3);

            this.Property(t => t.Tipo)
                .IsRequired()
                .HasMaxLength(8);

            this.Property(t => t.Ativo)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(3);

            this.Property(t => t.AtivaProtecaoTela)
                .IsFixedLength()
                .HasMaxLength(3);

            this.Property(t => t.TipoDeSistema)
                .IsFixedLength()
                .HasMaxLength(3);

            this.Property(t => t.AlterarSenhaNoLogin)
                .IsFixedLength()
                .HasMaxLength(3);

            this.Property(t => t.Site)
                .IsFixedLength()
                .HasMaxLength(3);

            this.Property(t => t.ValidarUsuarioNoBD)
                .IsFixedLength()
                .HasMaxLength(3);

            this.Property(t => t.DadosMaquinaLocal)
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("Usuario");
            this.Property(t => t.IDUsuario).HasColumnName("IDUsuario");
            this.Property(t => t.IDCadastro).HasColumnName("IDCadastro");
            this.Property(t => t.IDGrupo).HasColumnName("IDGrupo");
            this.Property(t => t.IDPerfil).HasColumnName("IDPerfil");
            this.Property(t => t.UltimaEmpresa).HasColumnName("UltimaEmpresa");
            this.Property(t => t.UltimaFilial).HasColumnName("UltimaFilial");
            this.Property(t => t.Nome).HasColumnName("Nome");
            this.Property(t => t.Login).HasColumnName("Login");
            this.Property(t => t.Senha).HasColumnName("Senha");
            this.Property(t => t.CriaUsuario).HasColumnName("CriaUsuario");
            this.Property(t => t.Administrador).HasColumnName("Administrador");
            this.Property(t => t.AutoOcultarMenu).HasColumnName("AutoOcultarMenu");
            this.Property(t => t.LarguraMenu).HasColumnName("LarguraMenu");
            this.Property(t => t.AlturaFavoritos).HasColumnName("AlturaFavoritos");
            this.Property(t => t.DataDeCadastro).HasColumnName("DataDeCadastro");
            this.Property(t => t.Tipo).HasColumnName("Tipo");
            this.Property(t => t.Ativo).HasColumnName("Ativo");
            this.Property(t => t.AtivaProtecaoTela).HasColumnName("AtivaProtecaoTela");
            this.Property(t => t.SenhaValidaAte).HasColumnName("SenhaValidaAte");
            this.Property(t => t.TipoDeSistema).HasColumnName("TipoDeSistema");
            this.Property(t => t.ValorLimite).HasColumnName("ValorLimite");
            this.Property(t => t.ExpirarSenha).HasColumnName("ExpirarSenha");
            this.Property(t => t.AlterarSenhaNoLogin).HasColumnName("AlterarSenhaNoLogin");
            this.Property(t => t.Site).HasColumnName("Site");
            this.Property(t => t.ValidarUsuarioNoBD).HasColumnName("ValidarUsuarioNoBD");
            this.Property(t => t.UltimoAcesso).HasColumnName("UltimoAcesso");
            this.Property(t => t.DadosMaquinaLocal).HasColumnName("DadosMaquinaLocal");

            // Relationships
            this.HasOptional(t => t.Cadastro)
                .WithMany(t => t.Usuarios)
                .HasForeignKey(d => d.IDCadastro);
            this.HasOptional(t => t.Grupo)
                .WithMany(t => t.Usuarios)
                .HasForeignKey(d => d.IDGrupo);

        }
    }
}
