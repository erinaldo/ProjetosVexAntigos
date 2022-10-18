using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ContaDespesaCapaMap : EntityTypeConfiguration<ContaDespesaCapa>
    {
        public ContaDespesaCapaMap()
        {
            // Primary Key
            this.HasKey(t => t.IdContaDespesaCapa);

            // Properties
            this.Property(t => t.IdContaDespesaCapa)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("ContaDespesaCapa");
            this.Property(t => t.IdContaDespesaCapa).HasColumnName("IdContaDespesaCapa");
            this.Property(t => t.IdContaDespesa).HasColumnName("IdContaDespesa");
            this.Property(t => t.IdContaDespesaEvento).HasColumnName("IdContaDespesaEvento");
            this.Property(t => t.Data).HasColumnName("Data");
            this.Property(t => t.Valor).HasColumnName("Valor");
            this.Property(t => t.Percentual).HasColumnName("Percentual");
            this.Property(t => t.Descritivo).HasColumnName("Descritivo");

            // Relationships
            this.HasRequired(t => t.ContaDespesa)
                .WithMany(t => t.ContaDespesaCapas)
                .HasForeignKey(d => d.IdContaDespesa);

        }
    }
}
