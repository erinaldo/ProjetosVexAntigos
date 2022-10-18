using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ModalidadeDePagamentoMap : EntityTypeConfiguration<ModalidadeDePagamento>
    {
        public ModalidadeDePagamentoMap()
        {
            // Primary Key
            this.HasKey(t => t.IdModalidadeDePagamento);

            // Properties
            this.Property(t => t.IdModalidadeDePagamento)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Nome)
                .HasMaxLength(100);

            this.Property(t => t.Ativo)
                .HasMaxLength(3);

            // Table & Column Mappings
            this.ToTable("ModalidadeDePagamento");
            this.Property(t => t.IdModalidadeDePagamento).HasColumnName("IdModalidadeDePagamento");
            this.Property(t => t.Nome).HasColumnName("Nome");
            this.Property(t => t.Ativo).HasColumnName("Ativo");

            // Relationships
            this.HasRequired(t => t.FinalidadeDocTed)
                .WithOptional(t => t.ModalidadeDePagamento);

        }
    }
}
