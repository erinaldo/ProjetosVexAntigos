using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class RepresentanteMap : EntityTypeConfiguration<Representante>
    {
        public RepresentanteMap()
        {
            // Primary Key
            this.HasKey(t => t.IdRepresentante);

            // Properties
            this.Property(t => t.IdRepresentante)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("Representante");
            this.Property(t => t.IdRepresentante).HasColumnName("IdRepresentante");
            this.Property(t => t.IdFilial).HasColumnName("IdFilial");

            // Relationships
            this.HasRequired(t => t.Cadastro)
                .WithOptional(t => t.Representante);
            this.HasRequired(t => t.Filial)
                .WithMany(t => t.Representantes)
                .HasForeignKey(d => d.IdFilial);

        }
    }
}
