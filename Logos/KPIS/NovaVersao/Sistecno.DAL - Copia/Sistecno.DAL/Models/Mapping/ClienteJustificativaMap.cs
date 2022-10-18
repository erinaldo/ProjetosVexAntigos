using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ClienteJustificativaMap : EntityTypeConfiguration<ClienteJustificativa>
    {
        public ClienteJustificativaMap()
        {
            // Primary Key
            this.HasKey(t => t.IdClienteJustificativa);

            // Properties
            this.Property(t => t.IdClienteJustificativa)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Nome)
                .IsRequired()
                .HasMaxLength(60);

            // Table & Column Mappings
            this.ToTable("ClienteJustificativa");
            this.Property(t => t.IdClienteJustificativa).HasColumnName("IdClienteJustificativa");
            this.Property(t => t.IdCliente).HasColumnName("IdCliente");
            this.Property(t => t.Nome).HasColumnName("Nome");
        }
    }
}
