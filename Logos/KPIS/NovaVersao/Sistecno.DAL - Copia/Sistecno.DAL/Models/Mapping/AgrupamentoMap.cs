using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class AgrupamentoMap : EntityTypeConfiguration<Agrupamento>
    {
        public AgrupamentoMap()
        {
            // Primary Key
            this.HasKey(t => t.IdAgrupamento);

            // Properties
            this.Property(t => t.IdAgrupamento)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Nome)
                .HasMaxLength(60);

            this.Property(t => t.Ordem)
                .HasMaxLength(15);

            // Table & Column Mappings
            this.ToTable("Agrupamento");
            this.Property(t => t.IdAgrupamento).HasColumnName("IdAgrupamento");
            this.Property(t => t.Nome).HasColumnName("Nome");
            this.Property(t => t.Ordem).HasColumnName("Ordem");
        }
    }
}
