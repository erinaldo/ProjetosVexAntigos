using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class EdiPlanilhaDetalheMap : EntityTypeConfiguration<EdiPlanilhaDetalhe>
    {
        public EdiPlanilhaDetalheMap()
        {
            // Primary Key
            this.HasKey(t => t.IdEdiPlanilhaDetalhe);

            // Properties
            this.Property(t => t.IdEdiPlanilhaDetalhe)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.CnpjCpf)
                .HasMaxLength(50);

            this.Property(t => t.Rejeicao)
                .HasMaxLength(500);

            // Table & Column Mappings
            this.ToTable("EdiPlanilhaDetalhe");
            this.Property(t => t.IdEdiPlanilhaDetalhe).HasColumnName("IdEdiPlanilhaDetalhe");
            this.Property(t => t.IdEdiPlanilha).HasColumnName("IdEdiPlanilha");
            this.Property(t => t.SequenciaPlanilha).HasColumnName("SequenciaPlanilha");
            this.Property(t => t.CnpjCpf).HasColumnName("CnpjCpf");
            this.Property(t => t.IdDocumento).HasColumnName("IdDocumento");
            this.Property(t => t.Rejeicao).HasColumnName("Rejeicao");
        }
    }
}
