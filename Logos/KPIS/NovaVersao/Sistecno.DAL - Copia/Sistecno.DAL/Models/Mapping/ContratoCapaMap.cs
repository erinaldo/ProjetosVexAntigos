using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ContratoCapaMap : EntityTypeConfiguration<ContratoCapa>
    {
        public ContratoCapaMap()
        {
            // Primary Key
            this.HasKey(t => t.IdContratoCapa);

            // Properties
            this.Property(t => t.IdContratoCapa)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("ContratoCapa");
            this.Property(t => t.IdContratoCapa).HasColumnName("IdContratoCapa");
            this.Property(t => t.IdContrato).HasColumnName("IdContrato");
            this.Property(t => t.IdContratoEvento).HasColumnName("IdContratoEvento");
            this.Property(t => t.Data).HasColumnName("Data");
            this.Property(t => t.Valor).HasColumnName("Valor");
            this.Property(t => t.Percentual).HasColumnName("Percentual");
            this.Property(t => t.Descritivo).HasColumnName("Descritivo");

            // Relationships
            this.HasRequired(t => t.Contrato)
                .WithMany(t => t.ContratoCapas)
                .HasForeignKey(d => d.IdContrato);

        }
    }
}
