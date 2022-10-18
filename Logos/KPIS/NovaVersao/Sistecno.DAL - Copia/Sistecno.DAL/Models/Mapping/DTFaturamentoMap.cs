using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class DTFaturamentoMap : EntityTypeConfiguration<DTFaturamento>
    {
        public DTFaturamentoMap()
        {
            // Primary Key
            this.HasKey(t => t.IdDtFaturamento);

            // Properties
            this.Property(t => t.IdDtFaturamento)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("DTFaturamento");
            this.Property(t => t.IdDtFaturamento).HasColumnName("IdDtFaturamento");
            this.Property(t => t.IdDt).HasColumnName("IdDt");
            this.Property(t => t.DataFaturamento).HasColumnName("DataFaturamento");

            // Relationships
            this.HasRequired(t => t.DT)
                .WithMany(t => t.DTFaturamentoes)
                .HasForeignKey(d => d.IdDt);

        }
    }
}
