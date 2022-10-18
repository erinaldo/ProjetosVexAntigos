using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class UsuarioOpcaoAcessoMap : EntityTypeConfiguration<UsuarioOpcaoAcesso>
    {
        public UsuarioOpcaoAcessoMap()
        {
            // Primary Key
            this.HasKey(t => t.IDUsuarioOpcaoAcesso);

            // Properties
            this.Property(t => t.IDUsuarioOpcaoAcesso)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Objeto)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Acesso)
                .IsRequired()
                .HasMaxLength(20);

            // Table & Column Mappings
            this.ToTable("UsuarioOpcaoAcesso");
            this.Property(t => t.IDUsuarioOpcaoAcesso).HasColumnName("IDUsuarioOpcaoAcesso");
            this.Property(t => t.IDUsuarioOpcao).HasColumnName("IDUsuarioOpcao");
            this.Property(t => t.Objeto).HasColumnName("Objeto");
            this.Property(t => t.Acesso).HasColumnName("Acesso");

            // Relationships
            this.HasRequired(t => t.UsuarioOpcao)
                .WithMany(t => t.UsuarioOpcaoAcessoes)
                .HasForeignKey(d => d.IDUsuarioOpcao);

        }
    }
}
