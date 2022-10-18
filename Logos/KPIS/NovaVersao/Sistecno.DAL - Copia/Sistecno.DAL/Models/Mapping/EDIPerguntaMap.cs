using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class EDIPerguntaMap : EntityTypeConfiguration<EDIPergunta>
    {
        public EDIPerguntaMap()
        {
            // Primary Key
            this.HasKey(t => t.IDEDIPergunta);

            // Properties
            this.Property(t => t.IDEDIPergunta)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Pergunta)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.Tipo)
                .IsRequired()
                .HasMaxLength(10);

            this.Property(t => t.ItensDaLista)
                .HasMaxLength(200);

            this.Property(t => t.PesquisaPadrao)
                .HasMaxLength(100);

            this.Property(t => t.Tabela)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Campo)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.UsaIntervalo)
                .IsFixedLength()
                .HasMaxLength(3);

            // Table & Column Mappings
            this.ToTable("EDIPergunta");
            this.Property(t => t.IDEDIPergunta).HasColumnName("IDEDIPergunta");
            this.Property(t => t.IDEDI).HasColumnName("IDEDI");
            this.Property(t => t.Pergunta).HasColumnName("Pergunta");
            this.Property(t => t.Tipo).HasColumnName("Tipo");
            this.Property(t => t.ItensDaLista).HasColumnName("ItensDaLista");
            this.Property(t => t.PesquisaPadrao).HasColumnName("PesquisaPadrao");
            this.Property(t => t.Tabela).HasColumnName("Tabela");
            this.Property(t => t.Campo).HasColumnName("Campo");
            this.Property(t => t.UsaIntervalo).HasColumnName("UsaIntervalo");
        }
    }
}
