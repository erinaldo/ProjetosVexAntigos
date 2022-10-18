using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class CadastroContatoAlertaMap : EntityTypeConfiguration<CadastroContatoAlerta>
    {
        public CadastroContatoAlertaMap()
        {
            // Primary Key
            this.HasKey(t => t.IDCadastroContatoAlerta);

            // Properties
            this.Property(t => t.IDCadastroContatoAlerta)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("CadastroContatoAlerta");
            this.Property(t => t.IDCadastroContatoAlerta).HasColumnName("IDCadastroContatoAlerta");
            this.Property(t => t.IDCadastroContato).HasColumnName("IDCadastroContato");
            this.Property(t => t.IDAlerta).HasColumnName("IDAlerta");

            // Relationships
            this.HasRequired(t => t.Alerta)
                .WithMany(t => t.CadastroContatoAlertas)
                .HasForeignKey(d => d.IDAlerta);
            this.HasRequired(t => t.CadastroContato)
                .WithMany(t => t.CadastroContatoAlertas)
                .HasForeignKey(d => d.IDCadastroContato);

        }
    }
}
