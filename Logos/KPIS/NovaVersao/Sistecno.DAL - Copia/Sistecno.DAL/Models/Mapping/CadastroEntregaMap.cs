using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class CadastroEntregaMap : EntityTypeConfiguration<CadastroEntrega>
    {
        public CadastroEntregaMap()
        {
            // Primary Key
            this.HasKey(t => t.IdCadastroEntrega);

            // Properties
            this.Property(t => t.IdCadastroEntrega)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("CadastroEntrega");
            this.Property(t => t.IdCadastroEntrega).HasColumnName("IdCadastroEntrega");
            this.Property(t => t.IdCadastro).HasColumnName("IdCadastro");
            this.Property(t => t.DataDeCadastro).HasColumnName("DataDeCadastro");
            this.Property(t => t.IdUsuario).HasColumnName("IdUsuario");
            this.Property(t => t.IdUsuarioAprovou).HasColumnName("IdUsuarioAprovou");
            this.Property(t => t.Observacoes).HasColumnName("Observacoes");

            // Relationships
            this.HasRequired(t => t.Cadastro)
                .WithMany(t => t.CadastroEntregas)
                .HasForeignKey(d => d.IdCadastro);

        }
    }
}
