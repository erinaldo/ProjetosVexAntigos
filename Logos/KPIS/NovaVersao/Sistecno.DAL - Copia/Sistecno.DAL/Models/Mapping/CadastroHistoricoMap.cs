using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class CadastroHistoricoMap : EntityTypeConfiguration<CadastroHistorico>
    {
        public CadastroHistoricoMap()
        {
            // Primary Key
            this.HasKey(t => t.IDCadastroHistorico);

            // Properties
            this.Property(t => t.IDCadastroHistorico)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Data)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.Assunto)
                .HasMaxLength(60);

            this.Property(t => t.PessoaDeContato)
                .HasMaxLength(60);

            this.Property(t => t.NomeArquivo)
                .HasMaxLength(254);

            // Table & Column Mappings
            this.ToTable("CadastroHistorico");
            this.Property(t => t.IDCadastroHistorico).HasColumnName("IDCadastroHistorico");
            this.Property(t => t.IDCadastro).HasColumnName("IDCadastro");
            this.Property(t => t.Data).HasColumnName("Data");
            this.Property(t => t.IDUsuario).HasColumnName("IDUsuario");
            this.Property(t => t.Assunto).HasColumnName("Assunto");
            this.Property(t => t.PessoaDeContato).HasColumnName("PessoaDeContato");
            this.Property(t => t.Texto).HasColumnName("Texto");
            this.Property(t => t.Arquivo).HasColumnName("Arquivo");
            this.Property(t => t.NomeArquivo).HasColumnName("NomeArquivo");

            // Relationships
            this.HasRequired(t => t.Cadastro)
                .WithMany(t => t.CadastroHistoricoes)
                .HasForeignKey(d => d.IDCadastro);

        }
    }
}
