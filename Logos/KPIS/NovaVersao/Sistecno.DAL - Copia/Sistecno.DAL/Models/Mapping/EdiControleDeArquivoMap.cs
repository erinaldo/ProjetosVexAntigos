using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class EdiControleDeArquivoMap : EntityTypeConfiguration<EdiControleDeArquivo>
    {
        public EdiControleDeArquivoMap()
        {
            // Primary Key
            this.HasKey(t => t.IdEDIControleDeArquivo);

            // Properties
            this.Property(t => t.IdEDIControleDeArquivo)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.ChaveDoArquivo)
                .HasMaxLength(100);

            this.Property(t => t.NomeDoMetodo)
                .HasMaxLength(100);

            this.Property(t => t.NomeDoArquivo)
                .HasMaxLength(100);

            this.Property(t => t.EnderecoDoArquivo)
                .HasMaxLength(100);

            this.Property(t => t.TipoDeArquivo)
                .HasMaxLength(15);

            // Table & Column Mappings
            this.ToTable("EdiControleDeArquivo");
            this.Property(t => t.IdEDIControleDeArquivo).HasColumnName("IdEDIControleDeArquivo");
            this.Property(t => t.IdUsuario).HasColumnName("IdUsuario");
            this.Property(t => t.ChaveDoArquivo).HasColumnName("ChaveDoArquivo");
            this.Property(t => t.NomeDoMetodo).HasColumnName("NomeDoMetodo");
            this.Property(t => t.NomeDoArquivo).HasColumnName("NomeDoArquivo");
            this.Property(t => t.EnderecoDoArquivo).HasColumnName("EnderecoDoArquivo");
            this.Property(t => t.DataHoraInicio).HasColumnName("DataHoraInicio");
            this.Property(t => t.DataHoraFinal).HasColumnName("DataHoraFinal");
            this.Property(t => t.TipoDeArquivo).HasColumnName("TipoDeArquivo");

            // Relationships
            this.HasRequired(t => t.Usuario)
                .WithMany(t => t.EdiControleDeArquivoes)
                .HasForeignKey(d => d.IdUsuario);

        }
    }
}
