using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class EDILayOutPerguntaMap : EntityTypeConfiguration<EDILayOutPergunta>
    {
        public EDILayOutPerguntaMap()
        {
            // Primary Key
            this.HasKey(t => t.IDEDILayoutPergunta);

            // Properties
            this.Property(t => t.IDEDILayoutPergunta)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Pergunta)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Tipo)
                .IsRequired()
                .HasMaxLength(10);

            this.Property(t => t.ItensDaLista)
                .HasMaxLength(100);

            this.Property(t => t.PesquisaPadrao)
                .HasMaxLength(50);

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
            this.ToTable("EDILayOutPergunta");
            this.Property(t => t.IDEDILayoutPergunta).HasColumnName("IDEDILayoutPergunta");
            this.Property(t => t.IDEDILayout).HasColumnName("IDEDILayout");
            this.Property(t => t.Pergunta).HasColumnName("Pergunta");
            this.Property(t => t.Tipo).HasColumnName("Tipo");
            this.Property(t => t.ItensDaLista).HasColumnName("ItensDaLista");
            this.Property(t => t.PesquisaPadrao).HasColumnName("PesquisaPadrao");
            this.Property(t => t.Tabela).HasColumnName("Tabela");
            this.Property(t => t.Campo).HasColumnName("Campo");
            this.Property(t => t.UsaIntervalo).HasColumnName("UsaIntervalo");

            // Relationships
            this.HasRequired(t => t.EDILayOut)
                .WithMany(t => t.EDILayOutPerguntas)
                .HasForeignKey(d => d.IDEDILayout);

        }
    }
}
