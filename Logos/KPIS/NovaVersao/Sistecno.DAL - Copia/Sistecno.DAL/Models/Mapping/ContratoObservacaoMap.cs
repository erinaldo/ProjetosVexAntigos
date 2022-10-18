using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ContratoObservacaoMap : EntityTypeConfiguration<ContratoObservacao>
    {
        public ContratoObservacaoMap()
        {
            // Primary Key
            this.HasKey(t => t.IdContratoObservacao);

            // Properties
            this.Property(t => t.IdContratoObservacao)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Observacao)
                .HasMaxLength(500);

            // Table & Column Mappings
            this.ToTable("ContratoObservacao");
            this.Property(t => t.IdContratoObservacao).HasColumnName("IdContratoObservacao");
            this.Property(t => t.IdContrato).HasColumnName("IdContrato");
            this.Property(t => t.Observacao).HasColumnName("Observacao");
        }
    }
}
