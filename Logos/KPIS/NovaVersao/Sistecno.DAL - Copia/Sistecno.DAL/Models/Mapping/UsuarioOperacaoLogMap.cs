using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class UsuarioOperacaoLogMap : EntityTypeConfiguration<UsuarioOperacaoLog>
    {
        public UsuarioOperacaoLogMap()
        {
            // Primary Key
            this.HasKey(t => t.IdUsuarioOperacaoLog);

            // Properties
            this.Property(t => t.IdUsuarioOperacaoLog)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Historico)
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("UsuarioOperacaoLog");
            this.Property(t => t.IdUsuarioOperacaoLog).HasColumnName("IdUsuarioOperacaoLog");
            this.Property(t => t.IdUsuarioOperacao).HasColumnName("IdUsuarioOperacao");
            this.Property(t => t.DataHora).HasColumnName("DataHora");
            this.Property(t => t.Historico).HasColumnName("Historico");
        }
    }
}
