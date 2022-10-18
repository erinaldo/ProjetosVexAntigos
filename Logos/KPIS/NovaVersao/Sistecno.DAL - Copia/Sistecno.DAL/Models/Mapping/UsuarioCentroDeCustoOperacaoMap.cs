using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class UsuarioCentroDeCustoOperacaoMap : EntityTypeConfiguration<UsuarioCentroDeCustoOperacao>
    {
        public UsuarioCentroDeCustoOperacaoMap()
        {
            // Primary Key
            this.HasKey(t => t.IdUsuarioCentroDeCustoOperacao);

            // Properties
            this.Property(t => t.IdUsuarioCentroDeCustoOperacao)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("UsuarioCentroDeCustoOperacao");
            this.Property(t => t.IdUsuarioCentroDeCustoOperacao).HasColumnName("IdUsuarioCentroDeCustoOperacao");
            this.Property(t => t.IdUsuarioCentroDeCusto).HasColumnName("IdUsuarioCentroDeCusto");
            this.Property(t => t.IdOperacao).HasColumnName("IdOperacao");
            this.Property(t => t.Valor).HasColumnName("Valor");
        }
    }
}
