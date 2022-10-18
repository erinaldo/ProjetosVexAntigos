using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class PreFaturaMap : EntityTypeConfiguration<PreFatura>
    {
        public PreFaturaMap()
        {
            // Primary Key
            this.HasKey(t => t.IdPreFatura);

            // Properties
            this.Property(t => t.IdPreFatura)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Tipo)
                .HasMaxLength(10);

            this.Property(t => t.Cnpj)
                .HasMaxLength(20);

            this.Property(t => t.NumeroPreFatura)
                .IsFixedLength()
                .HasMaxLength(15);

            this.Property(t => t.Acao)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.Situacao)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("PreFatura");
            this.Property(t => t.IdPreFatura).HasColumnName("IdPreFatura");
            this.Property(t => t.Tipo).HasColumnName("Tipo");
            this.Property(t => t.Cnpj).HasColumnName("Cnpj");
            this.Property(t => t.NumeroPreFatura).HasColumnName("NumeroPreFatura");
            this.Property(t => t.Emissao).HasColumnName("Emissao");
            this.Property(t => t.Vencimento).HasColumnName("Vencimento");
            this.Property(t => t.QuantidadeDeDocumentos).HasColumnName("QuantidadeDeDocumentos");
            this.Property(t => t.ValorPreFatura).HasColumnName("ValorPreFatura");
            this.Property(t => t.Acao).HasColumnName("Acao");
            this.Property(t => t.Situacao).HasColumnName("Situacao");
            this.Property(t => t.Desconto).HasColumnName("Desconto");
            this.Property(t => t.Complemento).HasColumnName("Complemento");
            this.Property(t => t.IdTitulo).HasColumnName("IdTitulo");
        }
    }
}
