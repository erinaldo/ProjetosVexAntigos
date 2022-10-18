using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ConciliacaoMap : EntityTypeConfiguration<Conciliacao>
    {
        public ConciliacaoMap()
        {
            // Primary Key
            this.HasKey(t => t.IdConciliacao);

            // Properties
            this.Property(t => t.IdConciliacao)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.HEmpresaConvenio)
                .IsFixedLength()
                .HasMaxLength(20);

            this.Property(t => t.HEmpresaContaAgenciaDigito)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.HEmpresaContaContaDigito)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.HEmpresaContaDV)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.HEmpresaNome)
                .IsFixedLength()
                .HasMaxLength(30);

            this.Property(t => t.HNomeDoBanco)
                .IsFixedLength()
                .HasMaxLength(30);

            this.Property(t => t.HReservadoEmpresa)
                .IsFixedLength()
                .HasMaxLength(20);

            // Table & Column Mappings
            this.ToTable("Conciliacao");
            this.Property(t => t.IdConciliacao).HasColumnName("IdConciliacao");
            this.Property(t => t.HControleBanco).HasColumnName("HControleBanco");
            this.Property(t => t.HControleLote).HasColumnName("HControleLote");
            this.Property(t => t.HControleRegistro).HasColumnName("HControleRegistro");
            this.Property(t => t.HEmpresaInscricaoTipo).HasColumnName("HEmpresaInscricaoTipo");
            this.Property(t => t.HEmpresaInscricaoNumero).HasColumnName("HEmpresaInscricaoNumero");
            this.Property(t => t.HEmpresaConvenio).HasColumnName("HEmpresaConvenio");
            this.Property(t => t.HEmpresaContaAgenciaCodigo).HasColumnName("HEmpresaContaAgenciaCodigo");
            this.Property(t => t.HEmpresaContaAgenciaDigito).HasColumnName("HEmpresaContaAgenciaDigito");
            this.Property(t => t.HEmpresaContaContaNumero).HasColumnName("HEmpresaContaContaNumero");
            this.Property(t => t.HEmpresaContaContaDigito).HasColumnName("HEmpresaContaContaDigito");
            this.Property(t => t.HEmpresaContaDV).HasColumnName("HEmpresaContaDV");
            this.Property(t => t.HEmpresaNome).HasColumnName("HEmpresaNome");
            this.Property(t => t.HNomeDoBanco).HasColumnName("HNomeDoBanco");
            this.Property(t => t.HArquivoCodigo).HasColumnName("HArquivoCodigo");
            this.Property(t => t.HArquivoDataGeracao).HasColumnName("HArquivoDataGeracao");
            this.Property(t => t.HArquivoHoraGeracao).HasColumnName("HArquivoHoraGeracao");
            this.Property(t => t.HArquivoSequencia).HasColumnName("HArquivoSequencia");
            this.Property(t => t.HArquivoLayOut).HasColumnName("HArquivoLayOut");
            this.Property(t => t.HReservadoEmpresa).HasColumnName("HReservadoEmpresa");
            this.Property(t => t.TControleBanco).HasColumnName("TControleBanco");
            this.Property(t => t.TControleLote).HasColumnName("TControleLote");
            this.Property(t => t.TControleRegistro).HasColumnName("TControleRegistro");
            this.Property(t => t.TTotaisQuantidadeDeLotes).HasColumnName("TTotaisQuantidadeDeLotes");
            this.Property(t => t.TTotaisQuantidadeDeRegistros).HasColumnName("TTotaisQuantidadeDeRegistros");
            this.Property(t => t.TTotaisQuantidadeDeContasConciliadas).HasColumnName("TTotaisQuantidadeDeContasConciliadas");
        }
    }
}
