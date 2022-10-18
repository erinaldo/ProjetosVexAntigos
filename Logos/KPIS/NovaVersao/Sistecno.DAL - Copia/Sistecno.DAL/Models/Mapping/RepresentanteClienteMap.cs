using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class RepresentanteClienteMap : EntityTypeConfiguration<RepresentanteCliente>
    {
        public RepresentanteClienteMap()
        {
            // Primary Key
            this.HasKey(t => t.IdRepresentanteCliente);

            // Properties
            this.Property(t => t.IdRepresentanteCliente)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Tipo)
                .HasMaxLength(20);

            // Table & Column Mappings
            this.ToTable("RepresentanteCliente");
            this.Property(t => t.IdRepresentanteCliente).HasColumnName("IdRepresentanteCliente");
            this.Property(t => t.IdRepresentante).HasColumnName("IdRepresentante");
            this.Property(t => t.IdCliente).HasColumnName("IdCliente");
            this.Property(t => t.Comissao).HasColumnName("Comissao");
            this.Property(t => t.Tipo).HasColumnName("Tipo");

            // Relationships
            this.HasRequired(t => t.Cliente)
                .WithMany(t => t.RepresentanteClientes)
                .HasForeignKey(d => d.IdCliente);
            this.HasRequired(t => t.Representante)
                .WithMany(t => t.RepresentanteClientes)
                .HasForeignKey(d => d.IdRepresentante);

        }
    }
}
