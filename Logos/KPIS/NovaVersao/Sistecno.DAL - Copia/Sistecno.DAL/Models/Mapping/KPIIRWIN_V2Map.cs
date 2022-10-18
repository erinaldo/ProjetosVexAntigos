using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class KPIIRWIN_V2Map : EntityTypeConfiguration<KPIIRWIN_V2>
    {
        public KPIIRWIN_V2Map()
        {
            // Primary Key
            this.HasKey(t => t.IdKPIIRWIN);

            // Properties
            this.Property(t => t.Chave)
                .HasMaxLength(100);

            this.Property(t => t.Descricao)
                .HasMaxLength(100);

            this.Property(t => t.DescricaoUnidadeDeMedida)
                .HasMaxLength(100);

            this.Property(t => t.DescricaoTarguet)
                .HasMaxLength(100);

            this.Property(t => t.UnidadeTarget)
                .HasMaxLength(20);

            this.Property(t => t.Calculado)
                .IsFixedLength()
                .HasMaxLength(3);

            // Table & Column Mappings
            this.ToTable("KPIIRWIN_V2");
            this.Property(t => t.IdKPIIRWIN).HasColumnName("IdKPIIRWIN");
            this.Property(t => t.Chave).HasColumnName("Chave");
            this.Property(t => t.Descricao).HasColumnName("Descricao");
            this.Property(t => t.DescricaoUnidadeDeMedida).HasColumnName("DescricaoUnidadeDeMedida");
            this.Property(t => t.DescricaoTarguet).HasColumnName("DescricaoTarguet");
            this.Property(t => t.Target).HasColumnName("Target");
            this.Property(t => t.UnidadeTarget).HasColumnName("UnidadeTarget");
            this.Property(t => t.Calculado).HasColumnName("Calculado");
            this.Property(t => t.Ordem).HasColumnName("Ordem");
        }
    }
}
