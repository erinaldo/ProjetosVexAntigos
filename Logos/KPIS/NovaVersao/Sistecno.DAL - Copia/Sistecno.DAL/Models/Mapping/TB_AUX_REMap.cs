using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class TB_AUX_REMap : EntityTypeConfiguration<TB_AUX_RE>
    {
        public TB_AUX_REMap()
        {
            // Primary Key
            this.HasKey(t => t.NUMERO);

            // Properties
            this.Property(t => t.FILIAL)
                .HasMaxLength(50);

            this.Property(t => t.NUMERO)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.MOTORISTA)
                .HasMaxLength(60);

            // Table & Column Mappings
            this.ToTable("TB_AUX_RE");
            this.Property(t => t.FILIAL).HasColumnName("FILIAL");
            this.Property(t => t.IMPOSTO).HasColumnName("IMPOSTO");
            this.Property(t => t.TAXAADMINISTRATIVA).HasColumnName("TAXAADMINISTRATIVA");
            this.Property(t => t.SEGURO).HasColumnName("SEGURO");
            this.Property(t => t.TAXADETRANFERENCIA).HasColumnName("TAXADETRANFERENCIA");
            this.Property(t => t.NUMERO).HasColumnName("NUMERO");
            this.Property(t => t.EMISSAO).HasColumnName("EMISSAO");
            this.Property(t => t.MOTORISTA).HasColumnName("MOTORISTA");
            this.Property(t => t.ENTREGAS).HasColumnName("ENTREGAS");
            this.Property(t => t.PESO).HasColumnName("PESO");
            this.Property(t => t.VALOR_DA_NOTA).HasColumnName("VALOR_DA_NOTA");
            this.Property(t => t.VALOR_DO_FRETE).HasColumnName("VALOR_DO_FRETE");
            this.Property(t => t.PERC_FRETE).HasColumnName("PERC_FRETE");
            this.Property(t => t.FRETE_MOTORISTA).HasColumnName("FRETE_MOTORISTA");
            this.Property(t => t.VLIMPOSTOS).HasColumnName("VLIMPOSTOS");
            this.Property(t => t.VLSEGURO).HasColumnName("VLSEGURO");
            this.Property(t => t.ADM).HasColumnName("ADM");
            this.Property(t => t.TRANSF).HasColumnName("TRANSF");
            this.Property(t => t.LUCRO).HasColumnName("LUCRO");
            this.Property(t => t.PER_LUCRO).HasColumnName("PER_LUCRO");
        }
    }
}
