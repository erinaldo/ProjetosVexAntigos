using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class TipoDeOperacaoMap : EntityTypeConfiguration<TipoDeOperacao>
    {
        public TipoDeOperacaoMap()
        {
            // Primary Key
            this.HasKey(t => t.IdTipoDeOperacao);

            // Properties
            this.Property(t => t.IdTipoDeOperacao)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Nome)
                .IsRequired()
                .HasMaxLength(60);

            this.Property(t => t.Ativo)
                .IsFixedLength()
                .HasMaxLength(3);

            // Table & Column Mappings
            this.ToTable("TipoDeOperacao");
            this.Property(t => t.IdTipoDeOperacao).HasColumnName("IdTipoDeOperacao");
            this.Property(t => t.Nome).HasColumnName("Nome");
            this.Property(t => t.Ativo).HasColumnName("Ativo");
        }
    }
}
