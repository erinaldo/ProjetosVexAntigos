using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class AgrupamentoRegiaoMap : EntityTypeConfiguration<AgrupamentoRegiao>
    {
        public AgrupamentoRegiaoMap()
        {
            // Primary Key
            this.HasKey(t => t.IdAgrupamentoRegiao);

            // Properties
            this.Property(t => t.IdAgrupamentoRegiao)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("AgrupamentoRegiao");
            this.Property(t => t.IdAgrupamentoRegiao).HasColumnName("IdAgrupamentoRegiao");
            this.Property(t => t.IdAgrupamento).HasColumnName("IdAgrupamento");
            this.Property(t => t.IdRegiao).HasColumnName("IdRegiao");

            // Relationships
            this.HasRequired(t => t.Agrupamento)
                .WithMany(t => t.AgrupamentoRegiaos)
                .HasForeignKey(d => d.IdAgrupamento);
            this.HasRequired(t => t.Regiao)
                .WithMany(t => t.AgrupamentoRegiaos)
                .HasForeignKey(d => d.IdRegiao);

        }
    }
}
