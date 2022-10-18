using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class TESCFOPMap : EntityTypeConfiguration<TESCFOP>
    {
        public TESCFOPMap()
        {
            // Primary Key
            this.HasKey(t => t.IdTesCFOP);

            // Properties
            this.Property(t => t.IdTesCFOP)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.GeraDuplicata)
                .IsFixedLength()
                .HasMaxLength(3);

            this.Property(t => t.GeraRomaneio)
                .IsFixedLength()
                .HasMaxLength(3);

            this.Property(t => t.GeraDocumento)
                .IsFixedLength()
                .HasMaxLength(3);

            this.Property(t => t.MovimentaEstoque)
                .IsFixedLength()
                .HasMaxLength(3);

            this.Property(t => t.ImprimeDocumento)
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

            this.Property(t => t.Observacao)
                .HasMaxLength(500);

            this.Property(t => t.Ativo)
                .IsFixedLength()
                .HasMaxLength(3);

            this.Property(t => t.CSTCTe)
                .HasMaxLength(5);

            this.Property(t => t.Servicos)
                .IsFixedLength()
                .HasMaxLength(3);

            this.Property(t => t.CalcularFrete)
                .IsFixedLength()
                .HasMaxLength(3);

            this.Property(t => t.TipoDeItem)
                .IsFixedLength()
                .HasMaxLength(2);

            this.Property(t => t.SituacaoTributariaIPI)
                .IsFixedLength()
                .HasMaxLength(2);

            this.Property(t => t.SituacaoTributariaCofins)
                .IsFixedLength()
                .HasMaxLength(2);

            this.Property(t => t.SituacaoTributariaIcms)
                .IsFixedLength()
                .HasMaxLength(3);

            this.Property(t => t.SituacaoTributariaPIS)
                .IsFixedLength()
                .HasMaxLength(2);

            this.Property(t => t.GerarCtrc)
                .IsFixedLength()
                .HasMaxLength(3);

            this.Property(t => t.EnquadramentoIPI)
                .IsFixedLength()
                .HasMaxLength(3);

            // Table & Column Mappings
            this.ToTable("TESCFOP");
            this.Property(t => t.IdTesCFOP).HasColumnName("IdTesCFOP");
            this.Property(t => t.IdTes).HasColumnName("IdTes");
            this.Property(t => t.IdCfop).HasColumnName("IdCfop");
            this.Property(t => t.IdCfopContraPartida).HasColumnName("IdCfopContraPartida");
            this.Property(t => t.GeraDuplicata).HasColumnName("GeraDuplicata");
            this.Property(t => t.GeraRomaneio).HasColumnName("GeraRomaneio");
            this.Property(t => t.GeraDocumento).HasColumnName("GeraDocumento");
            this.Property(t => t.MovimentaEstoque).HasColumnName("MovimentaEstoque");
            this.Property(t => t.ImprimeDocumento).HasColumnName("ImprimeDocumento");
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
            this.Property(t => t.DataDeCadastro).HasColumnName("DataDeCadastro");
            this.Property(t => t.Observacao).HasColumnName("Observacao");
            this.Property(t => t.Ativo).HasColumnName("Ativo");
            this.Property(t => t.CSTCTe).HasColumnName("CSTCTe");
            this.Property(t => t.Servicos).HasColumnName("Servicos");
            this.Property(t => t.CalcularFrete).HasColumnName("CalcularFrete");
            this.Property(t => t.TipoDeItem).HasColumnName("TipoDeItem");
            this.Property(t => t.SituacaoTributariaIPI).HasColumnName("SituacaoTributariaIPI");
            this.Property(t => t.SituacaoTributariaCofins).HasColumnName("SituacaoTributariaCofins");
            this.Property(t => t.SituacaoTributariaIcms).HasColumnName("SituacaoTributariaIcms");
            this.Property(t => t.SituacaoTributariaPIS).HasColumnName("SituacaoTributariaPIS");
            this.Property(t => t.Aplicacao).HasColumnName("Aplicacao");
            this.Property(t => t.GerarCtrc).HasColumnName("GerarCtrc");
            this.Property(t => t.EnquadramentoIPI).HasColumnName("EnquadramentoIPI");

            // Relationships
            this.HasRequired(t => t.Cfop)
                .WithMany(t => t.TESCFOPs)
                .HasForeignKey(d => d.IdCfop);
            this.HasRequired(t => t.TE)
                .WithMany(t => t.TESCFOPs)
                .HasForeignKey(d => d.IdTes);

        }
    }
}
