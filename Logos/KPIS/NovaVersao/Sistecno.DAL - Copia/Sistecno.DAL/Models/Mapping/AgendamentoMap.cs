using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class AgendamentoMap : EntityTypeConfiguration<Agendamento>
    {
        public AgendamentoMap()
        {
            // Primary Key
            this.HasKey(t => t.IdAgendamento);

            // Properties
            this.Property(t => t.IdAgendamento)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.TipoDeCarga)
                .HasMaxLength(15);

            this.Property(t => t.EDI)
                .IsFixedLength()
                .HasMaxLength(3);

            this.Property(t => t.NomeDoArquivo)
                .HasMaxLength(50);

            this.Property(t => t.Email)
                .HasMaxLength(300);

            this.Property(t => t.EmailCopia)
                .HasMaxLength(300);

            this.Property(t => t.Pessoa)
                .HasMaxLength(60);

            this.Property(t => t.Fone)
                .HasMaxLength(50);

            this.Property(t => t.Documento)
                .HasMaxLength(50);

            this.Property(t => t.Senha)
                .HasMaxLength(20);

            this.Property(t => t.Transportadora)
                .HasMaxLength(60);

            // Table & Column Mappings
            this.ToTable("Agendamento");
            this.Property(t => t.IdAgendamento).HasColumnName("IdAgendamento");
            this.Property(t => t.IdFilial).HasColumnName("IdFilial");
            this.Property(t => t.IdCadastro).HasColumnName("IdCadastro");
            this.Property(t => t.IdAgendaRecebimento).HasColumnName("IdAgendaRecebimento");
            this.Property(t => t.IdVeiculoTipo).HasColumnName("IdVeiculoTipo");
            this.Property(t => t.TipoDeCarga).HasColumnName("TipoDeCarga");
            this.Property(t => t.QuantidadeDeVeiculos).HasColumnName("QuantidadeDeVeiculos");
            this.Property(t => t.EDI).HasColumnName("EDI");
            this.Property(t => t.NotasFiscais).HasColumnName("NotasFiscais");
            this.Property(t => t.Peso).HasColumnName("Peso");
            this.Property(t => t.DataGeracaoDoArquivo).HasColumnName("DataGeracaoDoArquivo");
            this.Property(t => t.NomeDoArquivo).HasColumnName("NomeDoArquivo");
            this.Property(t => t.DataDeCadastro).HasColumnName("DataDeCadastro");
            this.Property(t => t.Email).HasColumnName("Email");
            this.Property(t => t.EmailCopia).HasColumnName("EmailCopia");
            this.Property(t => t.Pessoa).HasColumnName("Pessoa");
            this.Property(t => t.Fone).HasColumnName("Fone");
            this.Property(t => t.Documento).HasColumnName("Documento");
            this.Property(t => t.ValorDaNota).HasColumnName("ValorDaNota");
            this.Property(t => t.Senha).HasColumnName("Senha");
            this.Property(t => t.Transportadora).HasColumnName("Transportadora");

            // Relationships
            this.HasRequired(t => t.AgendaRecebimento)
                .WithMany(t => t.Agendamentoes)
                .HasForeignKey(d => d.IdAgendaRecebimento);
            this.HasRequired(t => t.Cadastro)
                .WithMany(t => t.Agendamentoes)
                .HasForeignKey(d => d.IdCadastro);
            this.HasRequired(t => t.Filial)
                .WithMany(t => t.Agendamentoes)
                .HasForeignKey(d => d.IdFilial);
            this.HasOptional(t => t.VeiculoTipo)
                .WithMany(t => t.Agendamentoes)
                .HasForeignKey(d => d.IdVeiculoTipo);

        }
    }
}
