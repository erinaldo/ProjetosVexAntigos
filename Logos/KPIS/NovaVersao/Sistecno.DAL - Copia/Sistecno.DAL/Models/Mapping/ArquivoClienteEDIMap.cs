using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ArquivoClienteEDIMap : EntityTypeConfiguration<ArquivoClienteEDI>
    {
        public ArquivoClienteEDIMap()
        {
            // Primary Key
            this.HasKey(t => new { t.ChaveRegistro, t.NomeDoArquivo, t.Cliente });

            // Properties
            this.Property(t => t.ChaveRegistro)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.NomeDoArquivo)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.Cliente)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.TipoDeArquivo)
                .HasMaxLength(30);

            this.Property(t => t.usuario)
                .HasMaxLength(30);

            this.Property(t => t.Situacao)
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.Arquivo)
                .HasMaxLength(50);

            this.Property(t => t.FilialLancamento)
                .IsFixedLength()
                .HasMaxLength(2);

            // Table & Column Mappings
            this.ToTable("ArquivoClienteEDI");
            this.Property(t => t.ChaveRegistro).HasColumnName("ChaveRegistro");
            this.Property(t => t.NomeDoArquivo).HasColumnName("NomeDoArquivo");
            this.Property(t => t.Cliente).HasColumnName("Cliente");
            this.Property(t => t.DataHoraImportacao).HasColumnName("DataHoraImportacao");
            this.Property(t => t.TipoDeArquivo).HasColumnName("TipoDeArquivo");
            this.Property(t => t.usuario).HasColumnName("usuario");
            this.Property(t => t.TotalLinhas).HasColumnName("TotalLinhas");
            this.Property(t => t.TotalPedidosNFS).HasColumnName("TotalPedidosNFS");
            this.Property(t => t.TotalNovos).HasColumnName("TotalNovos");
            this.Property(t => t.Situacao).HasColumnName("Situacao");
            this.Property(t => t.DataHoraFimImportacao).HasColumnName("DataHoraFimImportacao");
            this.Property(t => t.Arquivo).HasColumnName("Arquivo");
            this.Property(t => t.Sequencia).HasColumnName("Sequencia");
            this.Property(t => t.Fornecedor).HasColumnName("Fornecedor");
            this.Property(t => t.FornecedorFilial).HasColumnName("FornecedorFilial");
            this.Property(t => t.FilialLancamento).HasColumnName("FilialLancamento");
            this.Property(t => t.DataInicioReImportacao).HasColumnName("DataInicioReImportacao");
            this.Property(t => t.DataFimReImportacao).HasColumnName("DataFimReImportacao");
            this.Property(t => t.totalnovosImportacao).HasColumnName("totalnovosImportacao");
            this.Property(t => t.TotalnovosreeImportacao).HasColumnName("TotalnovosreeImportacao");
        }
    }
}
