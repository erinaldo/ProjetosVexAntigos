using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ClientesComproveiMap : EntityTypeConfiguration<ClientesComprovei>
    {
        public ClientesComproveiMap()
        {
            // Primary Key
            this.HasKey(t => new { t.IdCadastro, t.CnpjCPF });

            // Properties
            this.Property(t => t.IdCadastro)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.CnpjCPF)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.RazaoSocialNome)
                .HasMaxLength(60);

            this.Property(t => t.FantasiaApelido)
                .HasMaxLength(30);

            this.Property(t => t.Ativo)
                .HasMaxLength(3);

            // Table & Column Mappings
            this.ToTable("ClientesComprovei");
            this.Property(t => t.IdCadastro).HasColumnName("IdCadastro");
            this.Property(t => t.CnpjCPF).HasColumnName("CnpjCPF");
            this.Property(t => t.RazaoSocialNome).HasColumnName("RazaoSocialNome");
            this.Property(t => t.FantasiaApelido).HasColumnName("FantasiaApelido");
            this.Property(t => t.Ativo).HasColumnName("Ativo");
        }
    }
}
