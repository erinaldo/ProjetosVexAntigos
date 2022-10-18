using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class tblLogradouroMap : EntityTypeConfiguration<tblLogradouro>
    {
        public tblLogradouroMap()
        {
            // Primary Key
            this.HasKey(t => new { t.Codigo, t.UF, t.CodigoCidade, t.DescricaoNaoAbreviada, t.CEP, t.LOG_TIPO_LOGRADOURO, t.LOG_STATUS_TIPO_LOG, t.DescricaoSemAcento });

            // Properties
            this.Property(t => t.Codigo)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.UF)
                .IsRequired()
                .HasMaxLength(2);

            this.Property(t => t.CodigoCidade)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.DescricaoNaoAbreviada)
                .IsRequired()
                .HasMaxLength(70);

            this.Property(t => t.Descricao)
                .HasMaxLength(125);

            this.Property(t => t.CEP)
                .IsRequired()
                .HasMaxLength(8);

            this.Property(t => t.LOG_COMPLEMENTO)
                .HasMaxLength(100);

            this.Property(t => t.LOG_TIPO_LOGRADOURO)
                .IsRequired()
                .HasMaxLength(72);

            this.Property(t => t.LOG_STATUS_TIPO_LOG)
                .IsRequired()
                .HasMaxLength(1);

            this.Property(t => t.DescricaoSemAcento)
                .IsRequired()
                .HasMaxLength(70);

            // Table & Column Mappings
            this.ToTable("tblLogradouros");
            this.Property(t => t.Codigo).HasColumnName("Codigo");
            this.Property(t => t.UF).HasColumnName("UF");
            this.Property(t => t.CodigoCidade).HasColumnName("CodigoCidade");
            this.Property(t => t.DescricaoNaoAbreviada).HasColumnName("DescricaoNaoAbreviada");
            this.Property(t => t.Descricao).HasColumnName("Descricao");
            this.Property(t => t.CodigoBairro).HasColumnName("CodigoBairro");
            this.Property(t => t.CEP).HasColumnName("CEP");
            this.Property(t => t.BAI_NU_SEQUENCIAL_FIM).HasColumnName("BAI_NU_SEQUENCIAL_FIM");
            this.Property(t => t.LOG_COMPLEMENTO).HasColumnName("LOG_COMPLEMENTO");
            this.Property(t => t.LOG_TIPO_LOGRADOURO).HasColumnName("LOG_TIPO_LOGRADOURO");
            this.Property(t => t.LOG_STATUS_TIPO_LOG).HasColumnName("LOG_STATUS_TIPO_LOG");
            this.Property(t => t.DescricaoSemAcento).HasColumnName("DescricaoSemAcento");
        }
    }
}
