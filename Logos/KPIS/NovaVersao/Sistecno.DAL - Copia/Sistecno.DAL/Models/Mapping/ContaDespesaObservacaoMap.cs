using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ContaDespesaObservacaoMap : EntityTypeConfiguration<ContaDespesaObservacao>
    {
        public ContaDespesaObservacaoMap()
        {
            // Primary Key
            this.HasKey(t => t.IdContaDespesaObservacao);

            // Properties
            this.Property(t => t.IdContaDespesaObservacao)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Observacao)
                .HasMaxLength(500);

            // Table & Column Mappings
            this.ToTable("ContaDespesaObservacao");
            this.Property(t => t.IdContaDespesaObservacao).HasColumnName("IdContaDespesaObservacao");
            this.Property(t => t.IdContaDespesa).HasColumnName("IdContaDespesa");
            this.Property(t => t.Observacao).HasColumnName("Observacao");

            // Relationships
            this.HasRequired(t => t.ContaDespesa)
                .WithMany(t => t.ContaDespesaObservacaos)
                .HasForeignKey(d => d.IdContaDespesa);

        }
    }
}
