using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class EDI_EdiMap : EntityTypeConfiguration<EDI_Edi>
    {
        public EDI_EdiMap()
        {
            // Primary Key
            this.HasKey(t => t.EDI_Chave);

            // Properties
            this.Property(t => t.EDI_Chave)
                .IsRequired()
                .HasMaxLength(40);

            this.Property(t => t.Descricao)
                .HasMaxLength(100);

            this.Property(t => t.Metodo)
                .HasMaxLength(50);

            this.Property(t => t.TabelasEnvolvidas)
                .HasMaxLength(200);

            this.Property(t => t.EntradaSaida)
                .HasMaxLength(7);

            this.Property(t => t.Sistema)
                .HasMaxLength(3);

            // Table & Column Mappings
            this.ToTable("EDI_Edi");
            this.Property(t => t.EDI_Chave).HasColumnName("EDI_Chave");
            this.Property(t => t.EDI_Motivo).HasColumnName("EDI_Motivo");
            this.Property(t => t.EDI_Data).HasColumnName("EDI_Data");
            this.Property(t => t.IDEDI).HasColumnName("IDEDI");
            this.Property(t => t.Descricao).HasColumnName("Descricao");
            this.Property(t => t.Metodo).HasColumnName("Metodo");
            this.Property(t => t.TabelasEnvolvidas).HasColumnName("TabelasEnvolvidas");
            this.Property(t => t.EntradaSaida).HasColumnName("EntradaSaida");
            this.Property(t => t.Sistema).HasColumnName("Sistema");
        }
    }
}
