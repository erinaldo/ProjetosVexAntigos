using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class EDILayOutRegCpoMap : EntityTypeConfiguration<EDILayOutRegCpo>
    {
        public EDILayOutRegCpoMap()
        {
            // Primary Key
            this.HasKey(t => t.IDEDILayOutRegCpo);

            // Properties
            this.Property(t => t.IDEDILayOutRegCpo)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Descricao)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Tipo)
                .IsRequired()
                .HasMaxLength(12);

            this.Property(t => t.Formatacao)
                .HasMaxLength(50);

            this.Property(t => t.Alinhamento)
                .IsRequired()
                .HasMaxLength(12);

            this.Property(t => t.Preenchimento)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.Tabela)
                .HasMaxLength(50);

            this.Property(t => t.Campo)
                .HasMaxLength(50);

            this.Property(t => t.ValorFixo)
                .HasMaxLength(100);

            this.Property(t => t.Alias)
                .HasMaxLength(50);

            this.Property(t => t.Condicao)
                .HasMaxLength(50);

            this.Property(t => t.obs)
                .HasMaxLength(50);

            this.Property(t => t.Obrigatorio)
                .IsFixedLength()
                .HasMaxLength(3);

            this.Property(t => t.Identificador)
                .IsFixedLength()
                .HasMaxLength(3);

            // Table & Column Mappings
            this.ToTable("EDILayOutRegCpo");
            this.Property(t => t.IDEDILayOutRegCpo).HasColumnName("IDEDILayOutRegCpo");
            this.Property(t => t.IDEDILayOutReg).HasColumnName("IDEDILayOutReg");
            this.Property(t => t.Descricao).HasColumnName("Descricao");
            this.Property(t => t.Tipo).HasColumnName("Tipo");
            this.Property(t => t.Formatacao).HasColumnName("Formatacao");
            this.Property(t => t.Alinhamento).HasColumnName("Alinhamento");
            this.Property(t => t.Preenchimento).HasColumnName("Preenchimento");
            this.Property(t => t.Posicao).HasColumnName("Posicao");
            this.Property(t => t.Tamanho).HasColumnName("Tamanho");
            this.Property(t => t.Multiplicador).HasColumnName("Multiplicador");
            this.Property(t => t.Tabela).HasColumnName("Tabela");
            this.Property(t => t.Campo).HasColumnName("Campo");
            this.Property(t => t.ValorFixo).HasColumnName("ValorFixo");
            this.Property(t => t.Alias).HasColumnName("Alias");
            this.Property(t => t.Condicao).HasColumnName("Condicao");
            this.Property(t => t.obs).HasColumnName("obs");
            this.Property(t => t.Obrigatorio).HasColumnName("Obrigatorio");
            this.Property(t => t.Identificador).HasColumnName("Identificador");

            // Relationships
            this.HasRequired(t => t.EDILayOutReg)
                .WithMany(t => t.EDILayOutRegCpoes)
                .HasForeignKey(d => d.IDEDILayOutReg);

        }
    }
}
