using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class Face_REMap : EntityTypeConfiguration<Face_RE>
    {
        public Face_REMap()
        {
            // Primary Key
            this.HasKey(t => new { t.IDDT, t.NUMERO, t.TAXAADMINISTRATIVA, t.IMPOSTO, t.TAXADETRANFERENCIA, t.SEGURO });

            // Properties
            this.Property(t => t.IDDT)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.REGIAO)
                .HasMaxLength(60);

            this.Property(t => t.NUMERO)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.PLACA)
                .HasMaxLength(10);

            this.Property(t => t.PMNOME)
                .HasMaxLength(60);

            this.Property(t => t.TIPODEDT)
                .HasMaxLength(50);

            this.Property(t => t.FLNOME)
                .HasMaxLength(50);

            this.Property(t => t.FILIAL)
                .HasMaxLength(50);

            this.Property(t => t.TAXAADMINISTRATIVA)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.IMPOSTO)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.TAXADETRANFERENCIA)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.SEGURO)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.TRNOME)
                .HasMaxLength(60);

            // Table & Column Mappings
            this.ToTable("Face_RE");
            this.Property(t => t.IDDT).HasColumnName("IDDT");
            this.Property(t => t.IDROMANEIO).HasColumnName("IDROMANEIO");
            this.Property(t => t.REGIAO).HasColumnName("REGIAO");
            this.Property(t => t.NUMERO).HasColumnName("NUMERO");
            this.Property(t => t.EMISSAO).HasColumnName("EMISSAO");
            this.Property(t => t.PLACA).HasColumnName("PLACA");
            this.Property(t => t.PMNOME).HasColumnName("PMNOME");
            this.Property(t => t.TIPODEDT).HasColumnName("TIPODEDT");
            this.Property(t => t.FLNUMERO).HasColumnName("FLNUMERO");
            this.Property(t => t.FLNOME).HasColumnName("FLNOME");
            this.Property(t => t.FILIAL).HasColumnName("FILIAL");
            this.Property(t => t.TAXAADMINISTRATIVA).HasColumnName("TAXAADMINISTRATIVA");
            this.Property(t => t.IMPOSTO).HasColumnName("IMPOSTO");
            this.Property(t => t.TAXADETRANFERENCIA).HasColumnName("TAXADETRANFERENCIA");
            this.Property(t => t.SEGURO).HasColumnName("SEGURO");
            this.Property(t => t.TRNOME).HasColumnName("TRNOME");
            this.Property(t => t.CAPACIDADEDECARGAKG).HasColumnName("CAPACIDADEDECARGAKG");
            this.Property(t => t.VOLUMES).HasColumnName("VOLUMES");
            this.Property(t => t.VALORDANOTA).HasColumnName("VALORDANOTA");
            this.Property(t => t.PBRUTOTOTAL).HasColumnName("PBRUTOTOTAL");
            this.Property(t => t.PESOBRUTO).HasColumnName("PESOBRUTO");
            this.Property(t => t.NOTASFISCAIS).HasColumnName("NOTASFISCAIS");
            this.Property(t => t.ENTREGAS).HasColumnName("ENTREGAS");
            this.Property(t => t.FRETEMOTORISTARATEADO).HasColumnName("FRETEMOTORISTARATEADO");
            this.Property(t => t.FRETEMOTORISTA).HasColumnName("FRETEMOTORISTA");
            this.Property(t => t.FRETEEMPRESA).HasColumnName("FRETEEMPRESA");
            this.Property(t => t.REENTREGA).HasColumnName("REENTREGA");
            this.Property(t => t.QTD_REENTREGA).HasColumnName("QTD_REENTREGA");
        }
    }
}
