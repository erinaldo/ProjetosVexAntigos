using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ClienteSetorFilialMap : EntityTypeConfiguration<ClienteSetorFilial>
    {
        public ClienteSetorFilialMap()
        {
            // Primary Key
            this.HasKey(t => t.IdClienteSetorFilial);

            // Properties
            this.Property(t => t.IdClienteSetorFilial)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Roteiro)
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("ClienteSetorFilial");
            this.Property(t => t.IdClienteSetorFilial).HasColumnName("IdClienteSetorFilial");
            this.Property(t => t.IdCliente).HasColumnName("IdCliente");
            this.Property(t => t.IdSetor).HasColumnName("IdSetor");
            this.Property(t => t.IdFilial).HasColumnName("IdFilial");
            this.Property(t => t.Roteiro).HasColumnName("Roteiro");
            this.Property(t => t.NumeroDoRoteiro).HasColumnName("NumeroDoRoteiro");
            this.Property(t => t.CodigoDoCliente).HasColumnName("CodigoDoCliente");
            this.Property(t => t.CodigoDoClienteFilial).HasColumnName("CodigoDoClienteFilial");

            // Relationships
            this.HasRequired(t => t.Cliente)
                .WithMany(t => t.ClienteSetorFilials)
                .HasForeignKey(d => d.IdCliente);
            this.HasRequired(t => t.Filial)
                .WithMany(t => t.ClienteSetorFilials)
                .HasForeignKey(d => d.IdFilial);
            this.HasOptional(t => t.Setor)
                .WithMany(t => t.ClienteSetorFilials)
                .HasForeignKey(d => d.IdSetor);

        }
    }
}
