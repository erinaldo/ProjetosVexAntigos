using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class UsuarioCentroDeCustoOperacaoLogMap : EntityTypeConfiguration<UsuarioCentroDeCustoOperacaoLog>
    {
        public UsuarioCentroDeCustoOperacaoLogMap()
        {
            // Primary Key
            this.HasKey(t => t.idUsuarioCentroDeCustoOperacaolog);

            // Properties
            this.Property(t => t.idUsuarioCentroDeCustoOperacaolog)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("UsuarioCentroDeCustoOperacaoLog");
            this.Property(t => t.idUsuarioCentroDeCustoOperacaolog).HasColumnName("idUsuarioCentroDeCustoOperacaolog");
            this.Property(t => t.idUsuario).HasColumnName("idUsuario");
            this.Property(t => t.obs).HasColumnName("obs");
            this.Property(t => t.DataAprovacao).HasColumnName("DataAprovacao");
            this.Property(t => t.idUsuarioCentroDeCustoOperacao).HasColumnName("idUsuarioCentroDeCustoOperacao");
        }
    }
}
