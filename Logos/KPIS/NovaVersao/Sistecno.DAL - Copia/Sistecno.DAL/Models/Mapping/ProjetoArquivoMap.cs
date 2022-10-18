using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ProjetoArquivoMap : EntityTypeConfiguration<ProjetoArquivo>
    {
        public ProjetoArquivoMap()
        {
            // Primary Key
            this.HasKey(t => new { t.IdProjetoArquivo, t.IdProjeto, t.Descricao, t.Data, t.Arquivo });

            // Properties
            this.Property(t => t.IdProjetoArquivo)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.IdProjeto)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Descricao)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.Arquivo)
                .IsRequired();

            this.Property(t => t.Nome)
                .HasMaxLength(50);

            this.Property(t => t.Tipo)
                .HasMaxLength(5);

            // Table & Column Mappings
            this.ToTable("ProjetoArquivo");
            this.Property(t => t.IdProjetoArquivo).HasColumnName("IdProjetoArquivo");
            this.Property(t => t.IdProjeto).HasColumnName("IdProjeto");
            this.Property(t => t.Descricao).HasColumnName("Descricao");
            this.Property(t => t.Data).HasColumnName("Data");
            this.Property(t => t.Arquivo).HasColumnName("Arquivo");
            this.Property(t => t.Nome).HasColumnName("Nome");
            this.Property(t => t.Tipo).HasColumnName("Tipo");

            // Relationships
            this.HasRequired(t => t.Projeto)
                .WithMany(t => t.ProjetoArquivoes)
                .HasForeignKey(d => d.IdProjeto);

        }
    }
}
