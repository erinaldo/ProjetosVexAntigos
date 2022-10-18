using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class EdiTrocaDeArquivoMap : EntityTypeConfiguration<EdiTrocaDeArquivo>
    {
        public EdiTrocaDeArquivoMap()
        {
            // Primary Key
            this.HasKey(t => t.IdEdiTrocaDeArquivo);

            // Properties
            this.Property(t => t.IdEdiTrocaDeArquivo)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.TipoDeArquivo)
                .HasMaxLength(50);

            this.Property(t => t.NomeDoArquivo)
                .HasMaxLength(60);

            // Table & Column Mappings
            this.ToTable("EdiTrocaDeArquivo");
            this.Property(t => t.IdEdiTrocaDeArquivo).HasColumnName("IdEdiTrocaDeArquivo");
            this.Property(t => t.IdOrigem).HasColumnName("IdOrigem");
            this.Property(t => t.IdDestino).HasColumnName("IdDestino");
            this.Property(t => t.TipoDeArquivo).HasColumnName("TipoDeArquivo");
            this.Property(t => t.EntradaData).HasColumnName("EntradaData");
            this.Property(t => t.EntradaIdUsuario).HasColumnName("EntradaIdUsuario");
            this.Property(t => t.SaidaData).HasColumnName("SaidaData");
            this.Property(t => t.SaidaIdUsuario).HasColumnName("SaidaIdUsuario");
            this.Property(t => t.Arquivo).HasColumnName("Arquivo");
            this.Property(t => t.NomeDoArquivo).HasColumnName("NomeDoArquivo");

            // Relationships
            this.HasOptional(t => t.Cadastro)
                .WithMany(t => t.EdiTrocaDeArquivoes)
                .HasForeignKey(d => d.IdDestino);
            this.HasOptional(t => t.Cadastro1)
                .WithMany(t => t.EdiTrocaDeArquivoes1)
                .HasForeignKey(d => d.IdOrigem);

        }
    }
}
