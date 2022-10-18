using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class tblCidadeMap : EntityTypeConfiguration<tblCidade>
    {
        public tblCidadeMap()
        {
            // Primary Key
            this.HasKey(t => new { t.Codigo, t.Descricao, t.UF, t.SITUACAO, t.TIPO_LOCALIDADE });

            // Properties
            this.Property(t => t.Codigo)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Descricao)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Descricao_B)
                .HasMaxLength(60);

            this.Property(t => t.CEP)
                .HasMaxLength(8);

            this.Property(t => t.UF)
                .IsRequired()
                .HasMaxLength(2);

            this.Property(t => t.SITUACAO)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.TIPO_LOCALIDADE)
                .IsRequired()
                .HasMaxLength(1);

            // Table & Column Mappings
            this.ToTable("tblCidades");
            this.Property(t => t.Codigo).HasColumnName("Codigo");
            this.Property(t => t.Descricao).HasColumnName("Descricao");
            this.Property(t => t.Descricao_B).HasColumnName("Descricao_B");
            this.Property(t => t.CEP).HasColumnName("CEP");
            this.Property(t => t.UF).HasColumnName("UF");
            this.Property(t => t.SITUACAO).HasColumnName("SITUACAO");
            this.Property(t => t.TIPO_LOCALIDADE).HasColumnName("TIPO_LOCALIDADE");
            this.Property(t => t.LOC_NU_SEQUENCIAL_SUB).HasColumnName("LOC_NU_SEQUENCIAL_SUB");
        }
    }
}
