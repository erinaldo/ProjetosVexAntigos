using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class IdTabelaMap : EntityTypeConfiguration<IdTabela>
    {
        public IdTabelaMap()
        {
            // Primary Key
            this.HasKey(t => t.Tabela);

            // Properties
            this.Property(t => t.Tabela)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("IdTabela");
            this.Property(t => t.Tabela).HasColumnName("Tabela");
            this.Property(t => t.Id).HasColumnName("Id");
        }
    }
}
