using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class CadastroMap : EntityTypeConfiguration<Cadastro>
    {
        public CadastroMap()
        {
            // Primary Key
            this.HasKey(t => t.IDCadastro);

            // Properties
            this.Property(t => t.IDCadastro)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.CnpjCpf)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.InscricaoRG)
                .HasMaxLength(20);

            this.Property(t => t.RazaoSocialNome)
                .HasMaxLength(60);

            this.Property(t => t.FantasiaApelido)
                .HasMaxLength(30);

            this.Property(t => t.Endereco)
                .HasMaxLength(60);

            this.Property(t => t.Numero)
                .HasMaxLength(10);

            this.Property(t => t.Complemento)
                .HasMaxLength(60);

            this.Property(t => t.Cep)
                .HasMaxLength(8);

            this.Property(t => t.CnpjCpfErrado)
                .IsFixedLength()
                .HasMaxLength(3);

            this.Property(t => t.InscricaoErrada)
                .IsFixedLength()
                .HasMaxLength(3);

            this.Property(t => t.CEPValido)
                .IsFixedLength()
                .HasMaxLength(3);

            this.Property(t => t.Aniversario)
                .HasMaxLength(5);

            this.Property(t => t.SUFRAMA)
                .HasMaxLength(20);

            this.Property(t => t.OrgaoEmissor)
                .HasMaxLength(10);

            this.Property(t => t.TipoDeCadastro)
                .HasMaxLength(20);

            this.Property(t => t.SituacaoFiscal)
                .HasMaxLength(50);

            this.Property(t => t.GrupoDePreco)
                .HasMaxLength(50);

            this.Property(t => t.Regional)
                .HasMaxLength(50);

            this.Property(t => t.InscricaoMunicipal)
                .HasMaxLength(50);

            this.Property(t => t.ClassificacaoFiscal)
                .IsFixedLength()
                .HasMaxLength(3);

            this.Property(t => t.RecebeFinalSemana)
                .HasMaxLength(3);

            this.Property(t => t.PermiteAlteracaoNoCadastro)
                .IsFixedLength()
                .HasMaxLength(3);

            // Table & Column Mappings
            this.ToTable("Cadastro");
            this.Property(t => t.IDCadastro).HasColumnName("IDCadastro");
            this.Property(t => t.CnpjCpf).HasColumnName("CnpjCpf");
            this.Property(t => t.InscricaoRG).HasColumnName("InscricaoRG");
            this.Property(t => t.RazaoSocialNome).HasColumnName("RazaoSocialNome");
            this.Property(t => t.FantasiaApelido).HasColumnName("FantasiaApelido");
            this.Property(t => t.Endereco).HasColumnName("Endereco");
            this.Property(t => t.Numero).HasColumnName("Numero");
            this.Property(t => t.Complemento).HasColumnName("Complemento");
            this.Property(t => t.IDCidade).HasColumnName("IDCidade");
            this.Property(t => t.IDBairro).HasColumnName("IDBairro");
            this.Property(t => t.Cep).HasColumnName("Cep");
            this.Property(t => t.CnpjCpfErrado).HasColumnName("CnpjCpfErrado");
            this.Property(t => t.InscricaoErrada).HasColumnName("InscricaoErrada");
            this.Property(t => t.DataDeCadastro).HasColumnName("DataDeCadastro");
            this.Property(t => t.CEPValido).HasColumnName("CEPValido");
            this.Property(t => t.Aniversario).HasColumnName("Aniversario");
            this.Property(t => t.SUFRAMA).HasColumnName("SUFRAMA");
            this.Property(t => t.SUFRAMAVALIDADE).HasColumnName("SUFRAMAVALIDADE");
            this.Property(t => t.OrgaoEmissor).HasColumnName("OrgaoEmissor");
            this.Property(t => t.TipoDeCadastro).HasColumnName("TipoDeCadastro");
            this.Property(t => t.SituacaoFiscal).HasColumnName("SituacaoFiscal");
            this.Property(t => t.GrupoDePreco).HasColumnName("GrupoDePreco");
            this.Property(t => t.Regional).HasColumnName("Regional");
            this.Property(t => t.InscricaoMunicipal).HasColumnName("InscricaoMunicipal");
            this.Property(t => t.ClassificacaoFiscal).HasColumnName("ClassificacaoFiscal");
            this.Property(t => t.DataDaUltimaAtualizacaoEDI).HasColumnName("DataDaUltimaAtualizacaoEDI");
            this.Property(t => t.RecebeFinalSemana).HasColumnName("RecebeFinalSemana");
            this.Property(t => t.PermiteAlteracaoNoCadastro).HasColumnName("PermiteAlteracaoNoCadastro");
            this.Property(t => t.IdTipoDeOperacao).HasColumnName("IdTipoDeOperacao");

            // Relationships
            this.HasOptional(t => t.Bairro)
                .WithMany(t => t.Cadastroes)
                .HasForeignKey(d => d.IDBairro);
            this.HasOptional(t => t.Cidade)
                .WithMany(t => t.Cadastroes)
                .HasForeignKey(d => d.IDCidade);

        }
    }
}
