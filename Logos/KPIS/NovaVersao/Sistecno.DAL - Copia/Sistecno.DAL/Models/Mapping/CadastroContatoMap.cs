using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class CadastroContatoMap : EntityTypeConfiguration<CadastroContato>
    {
        public CadastroContatoMap()
        {
            // Primary Key
            this.HasKey(t => t.IDCadastroContato);

            // Properties
            this.Property(t => t.IDCadastroContato)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("CadastroContato");
            this.Property(t => t.IDCadastroContato).HasColumnName("IDCadastroContato");
            this.Property(t => t.IDCadastro).HasColumnName("IDCadastro");
            this.Property(t => t.IDContato).HasColumnName("IDContato");

            // Relationships
            this.HasRequired(t => t.Cadastro)
                .WithMany(t => t.CadastroContatoes)
                .HasForeignKey(d => d.IDCadastro);
            this.HasRequired(t => t.Cadastro1)
                .WithMany(t => t.CadastroContatoes1)
                .HasForeignKey(d => d.IDContato);

        }
    }
}
