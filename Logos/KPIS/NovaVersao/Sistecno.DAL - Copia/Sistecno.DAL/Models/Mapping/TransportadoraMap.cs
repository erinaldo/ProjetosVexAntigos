using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class TransportadoraMap : EntityTypeConfiguration<Transportadora>
    {
        public TransportadoraMap()
        {
            // Primary Key
            this.HasKey(t => t.IDTransportadora);

            // Properties
            this.Property(t => t.IDTransportadora)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("Transportadora");
            this.Property(t => t.IDTransportadora).HasColumnName("IDTransportadora");
            this.Property(t => t.IDContaContabil).HasColumnName("IDContaContabil");

            // Relationships
            this.HasRequired(t => t.Cadastro)
                .WithOptional(t => t.Transportadora);
            this.HasRequired(t => t.ContaContabil)
                .WithMany(t => t.Transportadoras)
                .HasForeignKey(d => d.IDContaContabil);

        }
    }
}
