using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class EDIMap : EntityTypeConfiguration<EDI>
    {
        public EDIMap()
        {
            // Primary Key
            this.HasKey(t => t.IDEDI);

            // Properties
            this.Property(t => t.IDEDI)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Descricao)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.Metodo)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.TabelasEnvolvidas)
                .IsRequired()
                .HasMaxLength(200);

            this.Property(t => t.EntradaSaida)
                .IsRequired()
                .HasMaxLength(7);

            this.Property(t => t.Sistema)
                .IsRequired()
                .HasMaxLength(3);

            this.Property(t => t.NomePadraoDoArquivo)
                .HasMaxLength(100);

            this.Property(t => t.TipoDeDocumento)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("EDI");
            this.Property(t => t.IDEDI).HasColumnName("IDEDI");
            this.Property(t => t.Descricao).HasColumnName("Descricao");
            this.Property(t => t.Metodo).HasColumnName("Metodo");
            this.Property(t => t.TabelasEnvolvidas).HasColumnName("TabelasEnvolvidas");
            this.Property(t => t.EntradaSaida).HasColumnName("EntradaSaida");
            this.Property(t => t.Sistema).HasColumnName("Sistema");
            this.Property(t => t.NomePadraoDoArquivo).HasColumnName("NomePadraoDoArquivo");
            this.Property(t => t.TipoDeDocumento).HasColumnName("TipoDeDocumento");
        }
    }
}
