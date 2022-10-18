using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ClienteFilialMap : EntityTypeConfiguration<ClienteFilial>
    {
        public ClienteFilialMap()
        {
            // Primary Key
            this.HasKey(t => t.IDClienteFilial);

            // Properties
            this.Property(t => t.IDClienteFilial)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.ClienteLogistica)
                .IsFixedLength()
                .HasMaxLength(3);

            this.Property(t => t.ColetaAutomatica)
                .IsFixedLength()
                .HasMaxLength(3);

            this.Property(t => t.FilialRobo)
                .IsFixedLength()
                .HasMaxLength(3);

            this.Property(t => t.RealizaConferencia)
                .IsFixedLength()
                .HasMaxLength(3);

            // Table & Column Mappings
            this.ToTable("ClienteFilial");
            this.Property(t => t.IDClienteFilial).HasColumnName("IDClienteFilial");
            this.Property(t => t.IDCliente).HasColumnName("IDCliente");
            this.Property(t => t.IDFilial).HasColumnName("IDFilial");
            this.Property(t => t.ClienteLogistica).HasColumnName("ClienteLogistica");
            this.Property(t => t.ColetaAutomatica).HasColumnName("ColetaAutomatica");
            this.Property(t => t.FilialRobo).HasColumnName("FilialRobo");
            this.Property(t => t.RealizaConferencia).HasColumnName("RealizaConferencia");

            // Relationships
            this.HasRequired(t => t.Cliente)
                .WithMany(t => t.ClienteFilials)
                .HasForeignKey(d => d.IDCliente);
            this.HasRequired(t => t.Filial)
                .WithMany(t => t.ClienteFilials)
                .HasForeignKey(d => d.IDFilial);

        }
    }
}
