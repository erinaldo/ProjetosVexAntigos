using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class UnidadeDeArmazenagemMovMap : EntityTypeConfiguration<UnidadeDeArmazenagemMov>
    {
        public UnidadeDeArmazenagemMovMap()
        {
            // Primary Key
            this.HasKey(t => new { t.IdUnidadeDeArmazenagemMov, t.IdUnidadeDeArmazenagem });

            // Properties
            this.Property(t => t.IdUnidadeDeArmazenagemMov)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.IdUnidadeDeArmazenagem)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("UnidadeDeArmazenagemMov");
            this.Property(t => t.IdUnidadeDeArmazenagemMov).HasColumnName("IdUnidadeDeArmazenagemMov");
            this.Property(t => t.IdUnidadeDeArmazenagem).HasColumnName("IdUnidadeDeArmazenagem");
            this.Property(t => t.IdEnderecoOrigem).HasColumnName("IdEnderecoOrigem");
            this.Property(t => t.IdEnderecoDestino).HasColumnName("IdEnderecoDestino");
            this.Property(t => t.IdUsuario).HasColumnName("IdUsuario");
            this.Property(t => t.DataHora).HasColumnName("DataHora");
        }
    }
}
