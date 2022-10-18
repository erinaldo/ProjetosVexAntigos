using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class AgendumMap : EntityTypeConfiguration<Agendum>
    {
        public AgendumMap()
        {
            // Primary Key
            this.HasKey(t => t.IdAgenda);

            // Properties
            this.Property(t => t.IdAgenda)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("Agenda");
            this.Property(t => t.IdAgenda).HasColumnName("IdAgenda");
            this.Property(t => t.IdCadastro).HasColumnName("IdCadastro");
            this.Property(t => t.IdCadastroContato).HasColumnName("IdCadastroContato");
            this.Property(t => t.Data).HasColumnName("Data");

            // Relationships
            this.HasRequired(t => t.Cadastro)
                .WithMany(t => t.Agenda)
                .HasForeignKey(d => d.IdCadastro);

        }
    }
}
