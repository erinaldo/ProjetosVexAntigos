using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ClienteTipoDeMaterialMap : EntityTypeConfiguration<ClienteTipoDeMaterial>
    {
        public ClienteTipoDeMaterialMap()
        {
            // Primary Key
            this.HasKey(t => t.IDClienteTipoDeMaterial);

            // Properties
            this.Property(t => t.IDClienteTipoDeMaterial)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Nome)
                .HasMaxLength(60);

            this.Property(t => t.Solicitar)
                .HasMaxLength(30);

            // Table & Column Mappings
            this.ToTable("ClienteTipoDeMaterial");
            this.Property(t => t.IDClienteTipoDeMaterial).HasColumnName("IDClienteTipoDeMaterial");
            this.Property(t => t.IDCliente).HasColumnName("IDCliente");
            this.Property(t => t.Nome).HasColumnName("Nome");
            this.Property(t => t.Solicitar).HasColumnName("Solicitar");

            // Relationships
            this.HasRequired(t => t.Cliente)
                .WithMany(t => t.ClienteTipoDeMaterials)
                .HasForeignKey(d => d.IDCliente);

        }
    }
}
