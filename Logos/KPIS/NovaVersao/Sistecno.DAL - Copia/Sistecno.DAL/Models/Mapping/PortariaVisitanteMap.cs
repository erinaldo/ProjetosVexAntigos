using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class PortariaVisitanteMap : EntityTypeConfiguration<PortariaVisitante>
    {
        public PortariaVisitanteMap()
        {
            // Primary Key
            this.HasKey(t => t.IdPortariaVisitante);

            // Properties
            this.Property(t => t.IdPortariaVisitante)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Tipo)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("PortariaVisitante");
            this.Property(t => t.IdPortariaVisitante).HasColumnName("IdPortariaVisitante");
            this.Property(t => t.IdPortaria).HasColumnName("IdPortaria");
            this.Property(t => t.IdCadastro).HasColumnName("IdCadastro");
            this.Property(t => t.Tipo).HasColumnName("Tipo");

            // Relationships
            this.HasRequired(t => t.Cadastro)
                .WithMany(t => t.PortariaVisitantes)
                .HasForeignKey(d => d.IdCadastro);
            this.HasRequired(t => t.Portaria)
                .WithMany(t => t.PortariaVisitantes)
                .HasForeignKey(d => d.IdPortaria);

        }
    }
}
