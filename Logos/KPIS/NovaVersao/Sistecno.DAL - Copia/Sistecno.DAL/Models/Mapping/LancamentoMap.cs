using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class LancamentoMap : EntityTypeConfiguration<Lancamento>
    {
        public LancamentoMap()
        {
            // Primary Key
            this.HasKey(t => t.IdLancamento);

            // Properties
            this.Property(t => t.IdLancamento)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Tabela)
                .HasMaxLength(50);

            this.Property(t => t.Ativo)
                .HasMaxLength(3);

            // Table & Column Mappings
            this.ToTable("Lancamento");
            this.Property(t => t.IdLancamento).HasColumnName("IdLancamento");
            this.Property(t => t.IdDocumentoOrigem).HasColumnName("IdDocumentoOrigem");
            this.Property(t => t.Tabela).HasColumnName("Tabela");
            this.Property(t => t.IdUsuario).HasColumnName("IdUsuario");
            this.Property(t => t.Ativo).HasColumnName("Ativo");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.DataLancamento).HasColumnName("DataLancamento");
        }
    }
}
