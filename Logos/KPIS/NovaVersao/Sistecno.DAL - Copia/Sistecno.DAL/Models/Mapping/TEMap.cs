using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class TEMap : EntityTypeConfiguration<TE>
    {
        public TEMap()
        {
            // Primary Key
            this.HasKey(t => t.IDTES);

            // Properties
            this.Property(t => t.IDTES)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Descricao)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.TipoDeDocumento)
                .HasMaxLength(20);

            this.Property(t => t.EntradaSaida)
                .HasMaxLength(10);

            this.Property(t => t.TipoDeServico)
                .HasMaxLength(20);

            this.Property(t => t.GeraDuplicata)
                .IsFixedLength()
                .HasMaxLength(3);

            this.Property(t => t.GeraRomaneio)
                .IsFixedLength()
                .HasMaxLength(3);

            this.Property(t => t.GeraDocumento)
                .IsFixedLength()
                .HasMaxLength(3);

            this.Property(t => t.RequerDocumentoDeOrigem)
                .IsFixedLength()
                .HasMaxLength(3);

            this.Property(t => t.CalculaICMS)
                .IsFixedLength()
                .HasMaxLength(3);

            this.Property(t => t.CalculaIPI)
                .IsFixedLength()
                .HasMaxLength(3);

            this.Property(t => t.DestacaIPI)
                .IsFixedLength()
                .HasMaxLength(3);

            this.Property(t => t.CalculaISS)
                .IsFixedLength()
                .HasMaxLength(3);

            this.Property(t => t.CalculaPIS)
                .IsFixedLength()
                .HasMaxLength(3);

            this.Property(t => t.CalculaCOFINS)
                .IsFixedLength()
                .HasMaxLength(3);

            this.Property(t => t.CalculaCSLL)
                .IsFixedLength()
                .HasMaxLength(3);

            this.Property(t => t.AtualizaEstoque)
                .IsFixedLength()
                .HasMaxLength(3);

            this.Property(t => t.CreditaICMS)
                .IsFixedLength()
                .HasMaxLength(3);

            this.Property(t => t.CreditaIPI)
                .IsFixedLength()
                .HasMaxLength(3);

            this.Property(t => t.IPINaBaseICMS)
                .IsFixedLength()
                .HasMaxLength(3);

            this.Property(t => t.IPISobreFrete)
                .IsFixedLength()
                .HasMaxLength(3);

            this.Property(t => t.PoderDeTerceiros)
                .IsFixedLength()
                .HasMaxLength(3);

            this.Property(t => t.DiferencaICMS)
                .IsFixedLength()
                .HasMaxLength(3);

            this.Property(t => t.LivroICMS)
                .HasMaxLength(10);

            this.Property(t => t.LivroIPI)
                .HasMaxLength(10);

            this.Property(t => t.ConsideraOM)
                .HasMaxLength(3);

            this.Property(t => t.Ativo)
                .IsFixedLength()
                .HasMaxLength(3);

            this.Property(t => t.Sistema)
                .HasMaxLength(10);

            this.Property(t => t.OrigemMovimentacao)
                .IsFixedLength()
                .HasMaxLength(3);

            this.Property(t => t.CalculaFrete)
                .IsFixedLength()
                .HasMaxLength(3);

            this.Property(t => t.MovimentaEstoque)
                .IsFixedLength()
                .HasMaxLength(3);

            this.Property(t => t.LoteEntradaSaida)
                .IsFixedLength()
                .HasMaxLength(3);

            // Table & Column Mappings
            this.ToTable("TES");
            this.Property(t => t.IDTES).HasColumnName("IDTES");
            this.Property(t => t.Descricao).HasColumnName("Descricao");
            this.Property(t => t.Aplicacao).HasColumnName("Aplicacao");
            this.Property(t => t.TipoDeDocumento).HasColumnName("TipoDeDocumento");
            this.Property(t => t.EntradaSaida).HasColumnName("EntradaSaida");
            this.Property(t => t.TipoDeServico).HasColumnName("TipoDeServico");
            this.Property(t => t.GeraDuplicata).HasColumnName("GeraDuplicata");
            this.Property(t => t.GeraRomaneio).HasColumnName("GeraRomaneio");
            this.Property(t => t.GeraDocumento).HasColumnName("GeraDocumento");
            this.Property(t => t.RequerDocumentoDeOrigem).HasColumnName("RequerDocumentoDeOrigem");
            this.Property(t => t.CalculaICMS).HasColumnName("CalculaICMS");
            this.Property(t => t.CalculaIPI).HasColumnName("CalculaIPI");
            this.Property(t => t.DestacaIPI).HasColumnName("DestacaIPI");
            this.Property(t => t.CalculaISS).HasColumnName("CalculaISS");
            this.Property(t => t.CalculaPIS).HasColumnName("CalculaPIS");
            this.Property(t => t.CalculaCOFINS).HasColumnName("CalculaCOFINS");
            this.Property(t => t.CalculaCSLL).HasColumnName("CalculaCSLL");
            this.Property(t => t.AtualizaEstoque).HasColumnName("AtualizaEstoque");
            this.Property(t => t.CreditaICMS).HasColumnName("CreditaICMS");
            this.Property(t => t.CreditaIPI).HasColumnName("CreditaIPI");
            this.Property(t => t.IPINaBaseICMS).HasColumnName("IPINaBaseICMS");
            this.Property(t => t.ReducaoICMS).HasColumnName("ReducaoICMS");
            this.Property(t => t.ReducaoIPI).HasColumnName("ReducaoIPI");
            this.Property(t => t.ReducaoISS).HasColumnName("ReducaoISS");
            this.Property(t => t.IPISobreFrete).HasColumnName("IPISobreFrete");
            this.Property(t => t.PoderDeTerceiros).HasColumnName("PoderDeTerceiros");
            this.Property(t => t.DiferencaICMS).HasColumnName("DiferencaICMS");
            this.Property(t => t.LivroICMS).HasColumnName("LivroICMS");
            this.Property(t => t.LivroIPI).HasColumnName("LivroIPI");
            this.Property(t => t.DataDeCadastro).HasColumnName("DataDeCadastro");
            this.Property(t => t.IDOcorrencia).HasColumnName("IDOcorrencia");
            this.Property(t => t.ConsideraOM).HasColumnName("ConsideraOM");
            this.Property(t => t.IDLocal).HasColumnName("IDLocal");
            this.Property(t => t.Ativo).HasColumnName("Ativo");
            this.Property(t => t.Sistema).HasColumnName("Sistema");
            this.Property(t => t.OrigemMovimentacao).HasColumnName("OrigemMovimentacao");
            this.Property(t => t.CalculaFrete).HasColumnName("CalculaFrete");
            this.Property(t => t.MovimentaEstoque).HasColumnName("MovimentaEstoque");
            this.Property(t => t.LoteEntradaSaida).HasColumnName("LoteEntradaSaida");

            // Relationships
            this.HasOptional(t => t.Ocorrencia)
                .WithMany(t => t.TES)
                .HasForeignKey(d => d.IDOcorrencia);

        }
    }
}
